
namespace TrayApp
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label allowedExtensionsLabel;
        private System.Windows.Forms.TextBox allowedExtensionsTextBox;
        private System.Windows.Forms.CheckBox includeSubfoldersCheckBox;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.watchPathLabel = new System.Windows.Forms.Label();
            this.savePathLabel = new System.Windows.Forms.Label();
            this.logPathLabel = new System.Windows.Forms.Label();
            this.watchPathTextBox = new System.Windows.Forms.TextBox();
            this.savePathTextBox = new System.Windows.Forms.TextBox();
            this.logPathTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.allowedExtensionsLabel = new System.Windows.Forms.Label();
            this.allowedExtensionsTextBox = new System.Windows.Forms.TextBox();
            this.includeSubfoldersCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // watchPathLabel
            // 
            this.watchPathLabel.AutoSize = true;
            this.watchPathLabel.Location = new System.Drawing.Point(15, 12);
            this.watchPathLabel.Name = "watchPathLabel";
            this.watchPathLabel.Size = new System.Drawing.Size(72, 12);
            this.watchPathLabel.TabIndex = 0;
            this.watchPathLabel.Text = "Watch Path:";
            // 
            // savePathLabel
            // 
            this.savePathLabel.AutoSize = true;
            this.savePathLabel.Location = new System.Drawing.Point(15, 49);
            this.savePathLabel.Name = "savePathLabel";
            this.savePathLabel.Size = new System.Drawing.Size(80, 12);
            this.savePathLabel.TabIndex = 1;
            this.savePathLabel.Text = "Backup Path:";
            // 
            // logPathLabel
            // 
            this.logPathLabel.AutoSize = true;
            this.logPathLabel.Location = new System.Drawing.Point(15, 86);
            this.logPathLabel.Name = "logPathLabel";
            this.logPathLabel.Size = new System.Drawing.Size(59, 12);
            this.logPathLabel.TabIndex = 2;
            this.logPathLabel.Text = "Log Path:";
            // 
            // watchPathTextBox
            // 
            this.watchPathTextBox.Location = new System.Drawing.Point(98, 9);
            this.watchPathTextBox.Name = "watchPathTextBox";
            this.watchPathTextBox.Size = new System.Drawing.Size(258, 21);
            this.watchPathTextBox.TabIndex = 3;
            this.watchPathTextBox.TextChanged += new System.EventHandler(this.watchPathTextBox_TextChanged);
            // 
            // savePathTextBox
            // 
            this.savePathTextBox.Location = new System.Drawing.Point(98, 46);
            this.savePathTextBox.Name = "savePathTextBox";
            this.savePathTextBox.Size = new System.Drawing.Size(258, 21);
            this.savePathTextBox.TabIndex = 4;
            // 
            // logPathTextBox
            // 
            this.logPathTextBox.Location = new System.Drawing.Point(98, 83);
            this.logPathTextBox.Name = "logPathTextBox";
            this.logPathTextBox.Size = new System.Drawing.Size(258, 21);
            this.logPathTextBox.TabIndex = 5;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(17, 156);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(87, 21);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(274, 156);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(87, 21);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // allowedExtensionsLabel
            // 
            this.allowedExtensionsLabel.AutoSize = true;
            this.allowedExtensionsLabel.Location = new System.Drawing.Point(15, 123);
            this.allowedExtensionsLabel.Name = "allowedExtensionsLabel";
            this.allowedExtensionsLabel.Size = new System.Drawing.Size(83, 12);
            this.allowedExtensionsLabel.TabIndex = 8;
            this.allowedExtensionsLabel.Text = "Allowed Exts:";
            // 
            // allowedExtensionsTextBox
            // 
            this.allowedExtensionsTextBox.Location = new System.Drawing.Point(98, 120);
            this.allowedExtensionsTextBox.Name = "allowedExtensionsTextBox";
            this.allowedExtensionsTextBox.Size = new System.Drawing.Size(258, 21);
            this.allowedExtensionsTextBox.TabIndex = 9;
            // 
            // includeSubfoldersCheckBox
            // 
            this.includeSubfoldersCheckBox.AutoSize = true;
            this.includeSubfoldersCheckBox.Checked = true;
            this.includeSubfoldersCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.includeSubfoldersCheckBox.Location = new System.Drawing.Point(17, 194);
            this.includeSubfoldersCheckBox.Name = "includeSubfoldersCheckBox";
            this.includeSubfoldersCheckBox.Size = new System.Drawing.Size(129, 16);
            this.includeSubfoldersCheckBox.TabIndex = 10;
            this.includeSubfoldersCheckBox.Text = "Include Subfolders";
            this.includeSubfoldersCheckBox.UseVisualStyleBackColor = true;
            this.includeSubfoldersCheckBox.CheckedChanged += new System.EventHandler(this.includeSubfoldersCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 222);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.logPathTextBox);
            this.Controls.Add(this.savePathTextBox);
            this.Controls.Add(this.watchPathTextBox);
            this.Controls.Add(this.logPathLabel);
            this.Controls.Add(this.savePathLabel);
            this.Controls.Add(this.watchPathLabel);
            this.Controls.Add(this.allowedExtensionsLabel);
            this.Controls.Add(this.allowedExtensionsTextBox);
            this.Controls.Add(this.includeSubfoldersCheckBox);
            this.Name = "MainForm";
            this.Text = "File Watcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
    }
 }

