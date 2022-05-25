using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3ApiSampleCsharp.Api
{
    /// <summary>
    /// S3 操作用のクラス
    /// </summary>
    public class S3Api
    {
        protected IAmazonS3 _client = null;
        public string Region { get; set; }
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public S3Api()
        {
        }

        /// <summary>
        /// 接続
        /// </summary>
        /// <remarks>生成後に呼び出す必要があります</remarks>
        public void Connect()
        {
            _client = new AmazonS3Client(AccessKeyId, SecretAccessKey, RegionEndpoint.GetBySystemName(Region));
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
    }
}
