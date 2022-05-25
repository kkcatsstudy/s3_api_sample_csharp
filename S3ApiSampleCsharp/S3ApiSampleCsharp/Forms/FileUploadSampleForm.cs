using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
