namespace DirectoryUtility
{
    partial class MainWindow
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
            this.directoryBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.directoryTextBox = new System.Windows.Forms.TextBox();
            this.organizeFilesStartButton = new System.Windows.Forms.Button();
            this.organizeFilesLabel = new System.Windows.Forms.Label();
            this.progressTextBox = new System.Windows.Forms.TextBox();
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.deleteLabel = new System.Windows.Forms.Label();
            this.startRemoveFilesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(13, 11);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(98, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse directory";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // directoryTextBox
            // 
            this.directoryTextBox.BackColor = System.Drawing.Color.White;
            this.directoryTextBox.Location = new System.Drawing.Point(118, 12);
            this.directoryTextBox.Name = "directoryTextBox";
            this.directoryTextBox.ReadOnly = true;
            this.directoryTextBox.Size = new System.Drawing.Size(672, 20);
            this.directoryTextBox.TabIndex = 1;
            // 
            // organizeFilesStartButton
            // 
            this.organizeFilesStartButton.Location = new System.Drawing.Point(13, 64);
            this.organizeFilesStartButton.Name = "organizeFilesStartButton";
            this.organizeFilesStartButton.Size = new System.Drawing.Size(98, 23);
            this.organizeFilesStartButton.TabIndex = 2;
            this.organizeFilesStartButton.Text = "Start";
            this.organizeFilesStartButton.UseVisualStyleBackColor = true;
            this.organizeFilesStartButton.Click += new System.EventHandler(this.organizeFilesStartButton_Click);
            // 
            // organizeFilesLabel
            // 
            this.organizeFilesLabel.AutoSize = true;
            this.organizeFilesLabel.Location = new System.Drawing.Point(26, 48);
            this.organizeFilesLabel.Name = "organizeFilesLabel";
            this.organizeFilesLabel.Size = new System.Drawing.Size(73, 13);
            this.organizeFilesLabel.TabIndex = 3;
            this.organizeFilesLabel.Text = "Organize Files";
            // 
            // progressTextBox
            // 
            this.progressTextBox.BackColor = System.Drawing.SystemColors.HighlightText;
            this.progressTextBox.Location = new System.Drawing.Point(13, 93);
            this.progressTextBox.Multiline = true;
            this.progressTextBox.Name = "progressTextBox";
            this.progressTextBox.ReadOnly = true;
            this.progressTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.progressTextBox.Size = new System.Drawing.Size(777, 223);
            this.progressTextBox.TabIndex = 4;
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Location = new System.Drawing.Point(12, 322);
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(777, 23);
            this.fileProgressBar.TabIndex = 5;
            // 
            // deleteLabel
            // 
            this.deleteLabel.AutoSize = true;
            this.deleteLabel.Location = new System.Drawing.Point(127, 48);
            this.deleteLabel.Name = "deleteLabel";
            this.deleteLabel.Size = new System.Drawing.Size(76, 13);
            this.deleteLabel.TabIndex = 6;
            this.deleteLabel.Text = "Delete old files";
            // 
            // startRemoveFilesButton
            // 
            this.startRemoveFilesButton.Location = new System.Drawing.Point(116, 64);
            this.startRemoveFilesButton.Name = "startRemoveFilesButton";
            this.startRemoveFilesButton.Size = new System.Drawing.Size(98, 23);
            this.startRemoveFilesButton.TabIndex = 7;
            this.startRemoveFilesButton.Text = "Open";
            this.startRemoveFilesButton.UseVisualStyleBackColor = true;
            this.startRemoveFilesButton.Click += new System.EventHandler(this.startRemoveFilesButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 357);
            this.Controls.Add(this.startRemoveFilesButton);
            this.Controls.Add(this.deleteLabel);
            this.Controls.Add(this.fileProgressBar);
            this.Controls.Add(this.progressTextBox);
            this.Controls.Add(this.organizeFilesLabel);
            this.Controls.Add(this.organizeFilesStartButton);
            this.Controls.Add(this.directoryTextBox);
            this.Controls.Add(this.browseButton);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory Utility";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog directoryBrowserDialog;
        private System.Windows.Forms.Button browseButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox directoryTextBox;
        private System.Windows.Forms.Button organizeFilesStartButton;
        private System.Windows.Forms.Label organizeFilesLabel;
        private System.Windows.Forms.Label deleteLabel;
        private System.Windows.Forms.Button startRemoveFilesButton;
        public System.Windows.Forms.ProgressBar fileProgressBar;
        public System.Windows.Forms.TextBox progressTextBox;
    }
}

