using NLog;
using S3ApiSampleCsharp.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S3ApiSampleCsharp.Forms
{
    public partial class FileUploadSampleForm : Form
    {

        #region フィールド・プロパティ
        /// <summary>
        ///  ロガー
        /// </summary>
        Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        public FileUploadSampleForm()
        {
            InitializeComponent();
        }

        private void _buttonUploadDirectoryRef_Click(object sender, EventArgs e)
        {
            var selectedPath = _textBoxUploadDirectory.Text;

            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "アップロードするフォルダを選択してください。";
                if (File.Exists(selectedPath))
                {
                    dialog.SelectedPath = selectedPath;
                }
                if (DialogResult.OK == dialog.ShowDialog())
                {
                    selectedPath = dialog.SelectedPath;
                }
            }

            if (selectedPath != _textBoxUploadDirectory.Text)
            {
                _textBoxUploadDirectory.Text = selectedPath;
            }
        }

        private async void _buttonUpload_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("アップロードします。\n\nよろしいですか？", "アップロード確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            // 選択したフォルダをアップロード
            if (string.IsNullOrEmpty(_textBoxUploadDirectory.Text))
            {
                MessageBox.Show("アップロードするバケットを指定してください。");
                return;
            }

            _buttonUpload.Enabled = false;
            _uploadProgressBar.Visible = true;
            var taskE = await Task.Run(async () =>
            {
                try
                {
                    //var api = new BoxApi
                    //{
                    //    ConfigJsonFile = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString(), "box_config.json")
                    //};
                    //// 接続
                    //await api.Connect();

                    //// yyyyMMdd_HHmmss フォルダを作ります
                    //var createFolderReq = new List<BoxFolderItem>()
                    //{
                    //    new BoxFolderItem
                    //    {
                    //        Name = DateTime.Now.ToString("yyyyMMdd_HHmmss")
                    //    }
                    //};
                    //var createFolderRes = await api.CreateFolders(_textBoxRootDirectoryId.Text, createFolderReq);
                    //var targetId = createFolderRes[0].Id;

                    //// フォルダをアップロード
                    //await api.UploadFolder(targetId, _textBoxUploadDirectory.Text);
                }
                catch (Exception ex)
                {
                    return ex;
                }

                return null;
            });
            _uploadProgressBar.Visible = false;
            _buttonUpload.Enabled = true;

            if (taskE != null)
            {
                MessageBox.Show(taskE.ToString());
            }
            else
            {
                MessageBox.Show("アップロードが完了しました。");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var api = new S3Api
                {
                    Region = Properties.Settings.Default.s3_region,
                    AccessKeyId = Properties.Settings.Default.s3_access_key_id,
                    SecretAccessKey = Properties.Settings.Default.s3_secret_access_key
                })
                {
                    api.Connect();

                    Console.WriteLine("接続できました。");

                    if (api.IsExistBucket(_textBoxBucketName.Text))
                    {
                        Console.WriteLine("あったみたい");
                    }
                    else
                    {
                        MessageBox.Show("バケットがないみたいだよ");
                        return;
                    }

                    // 適当なファイルをアップロード
                    var now = DateTime.Now;
                    var destKey = string.Format("{0}/{1}/100mb.xxx",
                        now.ToString("yyyyMMdd"),
                        now.ToString("HHmmss")
                        );
                    await api.Upload(@"C:\Users\nukui\Jobs\camera_control_remote\s3_api_sample_csharp.git\S3ApiSampleCsharp\100_plus_mb.xxx", _textBoxBucketName.Text,
                        destKey);

                    MessageBox.Show("アップロードできたよ");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
