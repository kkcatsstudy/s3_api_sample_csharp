namespace S3ApiSampleCsharp.Forms
{
    partial class FileUploadSampleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonUploadDirectoryRef = new System.Windows.Forms.Button();
            this._textBoxUploadDirectory = new System.Windows.Forms.TextBox();
            this._labelUploadDirectorySummary = new System.Windows.Forms.Label();
            this._labelRootDirectoryIdSummary = new System.Windows.Forms.Label();
            this._textBoxBucketName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._uploadProgressBar = new System.Windows.Forms.ProgressBar();
            this._buttonUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonUploadDirectoryRef
            // 
            this._buttonUploadDirectoryRef.Location = new System.Drawing.Point(272, 163);
            this._buttonUploadDirectoryRef.Margin = new System.Windows.Forms.Padding(2);
            this._buttonUploadDirectoryRef.Name = "_buttonUploadDirectoryRef";
            this._buttonUploadDirectoryRef.Size = new System.Drawing.Size(56, 24);
            this._buttonUploadDirectoryRef.TabIndex = 13;
            this._buttonUploadDirectoryRef.Text = "...";
            this._buttonUploadDirectoryRef.UseVisualStyleBackColor = true;
            this._buttonUploadDirectoryRef.Click += new System.EventHandler(this._buttonUploadDirectoryRef_Click);
            // 
            // _textBoxUploadDirectory
            // 
            this._textBoxUploadDirectory.Location = new System.Drawing.Point(47, 167);
            this._textBoxUploadDirectory.Margin = new System.Windows.Forms.Padding(2);
            this._textBoxUploadDirectory.Name = "_textBoxUploadDirectory";
            this._textBoxUploadDirectory.Size = new System.Drawing.Size(222, 19);
            this._textBoxUploadDirectory.TabIndex = 12;
            // 
            // _labelUploadDirectorySummary
            // 
            this._labelUploadDirectorySummary.AutoSize = true;
            this._labelUploadDirectorySummary.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._labelUploadDirectorySummary.Location = new System.Drawing.Point(11, 133);
            this._labelUploadDirectorySummary.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._labelUploadDirectorySummary.Name = "_labelUploadDirectorySummary";
            this._labelUploadDirectorySummary.Size = new System.Drawing.Size(388, 22);
            this._labelUploadDirectorySummary.TabIndex = 11;
            this._labelUploadDirectorySummary.Text = "2. アップロードするフォルダを選んでください";
            // 
            // _labelRootDirectoryIdSummary
            // 
            this._labelRootDirectoryIdSummary.AutoSize = true;
            this._labelRootDirectoryIdSummary.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this._labelRootDirectoryIdSummary.Location = new System.Drawing.Point(11, 21);
            this._labelRootDirectoryIdSummary.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._labelRootDirectoryIdSummary.Name = "_labelRootDirectoryIdSummary";
            this._labelRootDirectoryIdSummary.Size = new System.Drawing.Size(291, 22);
            this._labelRootDirectoryIdSummary.TabIndex = 10;
            this._labelRootDirectoryIdSummary.Text = "1. バケット名を入力してください";
            // 
            // _textBoxBucketName
            // 
            this._textBoxBucketName.Location = new System.Drawing.Point(47, 67);
            this._textBoxBucketName.Margin = new System.Windows.Forms.Padding(2);
            this._textBoxBucketName.Name = "_textBoxBucketName";
            this._textBoxBucketName.Size = new System.Drawing.Size(255, 19);
            this._textBoxBucketName.TabIndex = 9;
            this._textBoxBucketName.Text = "camera-control-remote-cats-dev";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(427, 25);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 18);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _uploadProgressBar
            // 
            this._uploadProgressBar.Location = new System.Drawing.Point(338, 265);
            this._uploadProgressBar.Margin = new System.Windows.Forms.Padding(2);
            this._uploadProgressBar.Name = "_uploadProgressBar";
            this._uploadProgressBar.Size = new System.Drawing.Size(182, 32);
            this._uploadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this._uploadProgressBar.TabIndex = 16;
            this._uploadProgressBar.Visible = false;
            // 
            // _buttonUpload
            // 
            this._buttonUpload.Location = new System.Drawing.Point(50, 265);
            this._buttonUpload.Margin = new System.Windows.Forms.Padding(2);
            this._buttonUpload.Name = "_buttonUpload";
            this._buttonUpload.Size = new System.Drawing.Size(232, 32);
            this._buttonUpload.TabIndex = 15;
            this._buttonUpload.Text = "アップロード！！";
            this._buttonUpload.UseVisualStyleBackColor = true;
            this._buttonUpload.Click += new System.EventHandler(this._buttonUpload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(11, 234);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 22);
            this.label1.TabIndex = 14;
            this.label1.Text = "3. アップロードします";
            // 
            // FileUploadSampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 325);
            this.Controls.Add(this._uploadProgressBar);
            this.Controls.Add(this._buttonUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._buttonUploadDirectoryRef);
            this.Controls.Add(this._textBoxUploadDirectory);
            this.Controls.Add(this._labelUploadDirectorySummary);
            this.Controls.Add(this._labelRootDirectoryIdSummary);
            this.Controls.Add(this._textBoxBucketName);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FileUploadSampleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S3にファイルをアップロードします";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonUploadDirectoryRef;
        private System.Windows.Forms.TextBox _textBoxUploadDirectory;
        private System.Windows.Forms.Label _labelUploadDirectorySummary;
        private System.Windows.Forms.Label _labelRootDirectoryIdSummary;
        private System.Windows.Forms.TextBox _textBoxBucketName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar _uploadProgressBar;
        private System.Windows.Forms.Button _buttonUpload;
        private System.Windows.Forms.Label label1;
    }
}