using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace S3ApiSampleCsharp.Api
{
    /// <summary>
    /// S3 操作用のクラス
    /// </summary>
    public class S3Api : IDisposable
    {
        #region フィールド・プロパティ
        /// <summary>
        /// ロガー
        /// </summary>
        Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// クライアント
        /// </summary>
        protected IAmazonS3 _client = null;

        /// <summary>
        /// 転送用ラッパー
        /// </summary>
        protected ITransferUtility _transferUtility = null;

        /// <summary>
        /// リージョン
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// アクセスキーID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// シークレットアクセスキー
        /// </summary>
        public string SecretAccessKey { get; set; }

        /// <summary>
        /// シングルスレッド配信での上限サイズ（バイト）
        /// （このサイズを超えるとマルチパート配信となります）
        /// </summary>
        public long SingleUploadLimit { get; set; }

        /// <summary>
        /// マルチパート配信するときの1パートのサイズ（バイト）
        /// </summary>
        public long MultiPartSize { get; set; }

        /// <summary>
        /// 再試行回数
        /// </summary>
        public int RetryCount { get; set; }
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public S3Api()
        {
            // シングル配信の上限のデフォルト値は 100 MB
            SingleUploadLimit = 104857600;

            // マルチパート配信の1パートのデフォルト値は 100 MB
            MultiPartSize = 104857600;

            // 再試行回数は 3
            RetryCount = 3;
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <remarks>生成後に呼び出す必要があります</remarks>
        public void Connect()
        {
            logger.Info("S3 に接続します。");
            var config = new AmazonS3Config
            {
                RetryMode = RequestRetryMode.Standard,
                MaxErrorRetry = RetryCount,
                // タイムアウト（ReadWriteTimeout, Timeout もここでいじれるようだが、下手にいじらないほうが良いか・・・）
                RegionEndpoint = RegionEndpoint.GetBySystemName(Region),
            };

            _client = new AmazonS3Client(AccessKeyId, SecretAccessKey, config);
            _transferUtility = new TransferUtility(_client);
            logger.Info("  -> S3 に接続しました。");
        }

        /// <summary>
        /// バケットが存在しているか確認します
        /// </summary>
        /// <param name="bucketName">バケット名</param>
        /// <returns>バケットが存在していれば true。存在しなかったら false</returns>
        public bool IsExistBucket(string bucketName)
        {
            var ret = false;

            try
            {
                ret = AmazonS3Util.DoesS3BucketExistV2(_client, bucketName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        /// <summary>
        /// アップロードを行います（ファイルサイズによる自動判定）
        /// </summary>
        /// <param name="localFile">アップロード対象のファイル</param>
        /// <param name="bucket">バケット名</param>
        /// <param name="key">アップロード先のパス</param>
        /// <returns></returns>
        public async Task Upload(string localFile, string bucket, string key)
        {
            var fi = new FileInfo(localFile);
            if (SingleUploadLimit < fi.Length)
            {
                logger.Info("シングルアップロード上限[{0}]を超えているため、マルチパート配信を行います。");
                await UploadMultipart(localFile, bucket, key);
            }
            else
            {
                await UploadSingle(localFile, bucket, key);
            }
        }

        /// <summary>
        /// アップロードを行います（シングルスレッド配信）
        /// </summary>
        /// <param name="localFile">アップロード対象のファイル</param>
        /// <param name="bucket">バケット名</param>
        /// <param name="key">アップロード先のパス</param>
        /// <returns></returns>
        public async Task UploadSingle(string localFile, string bucket, string key)
        {
            var fi = new FileInfo(localFile);
            logger.Info("アップロードを開始します。[File: {0}, FileSize: {1}, DestBucket: {2}, DestKey: {3}"
                , localFile, fi.Length, bucket, key);


            var req = new PutObjectRequest
            {
                FilePath = localFile,
                BucketName = bucket,
                Key = key,
            };
            var res = await _client.PutObjectAsync(req);

            logger.Info("  -> アップロードが完了しました。");
        }

        /// <summary>
        /// アップロードを行います（マルチパート配信）
        /// </summary>
        /// <param name="localFile">アップロード対象のファイル</param>
        /// <param name="bucket">バケット名</param>
        /// <param name="key">アップロード先のパス</param>
        /// <returns></returns>
        public async Task UploadMultipart(string localFile, string bucket, string key)
        {
            var fi = new FileInfo(localFile);
            logger.Info("マルチパートアップロードを開始します。[File: {0}, FileSize: {1}, DestBucket: {2}, DestKey: {3}"
                , localFile, fi.Length, bucket, key);
            var req = new TransferUtilityUploadRequest
            {
                FilePath = localFile,
                BucketName = bucket,
                Key = key,
                PartSize = MultiPartSize,
            };
            req.UploadProgressEvent +=
                new EventHandler<UploadProgressArgs>
                    (multipartUploadRequest_UploadPartProgressEvent);
            await _transferUtility.UploadAsync(req);
            req.UploadProgressEvent -=
                new EventHandler<UploadProgressArgs>
                    (multipartUploadRequest_UploadPartProgressEvent);


            logger.Info("  -> マルチパートアップロードが完了しました。");
        }

        /// <summary>
        /// マルチパートアップロードの進捗を受け取ります
        /// </summary>
        /// <param name="sender">対象オブジェクト</param>
        /// <param name="e">イベントデータソース</param>
        private void multipartUploadRequest_UploadPartProgressEvent(object sender, UploadProgressArgs e)
        {
            // ここはイベントとして公開しても良いかも。
            logger.Info("    * uploading ... {0} {1, 3}% [{2, 12}/{3, 12}]",
                e.FilePath,
                e.PercentDone,
                e.TransferredBytes,
                e.TotalBytes
            );
        }

        /// <summary>
        /// 破棄処理
        /// </summary>
        public void Dispose()
        {
            if (_transferUtility != null)
            {
                _transferUtility.Dispose();
                _transferUtility = null;
            }
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
        }
    }
}
