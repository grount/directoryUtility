namespace DirectoryUtility
{
    partial class DeleteOldFilesForm
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
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.startButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filesListBox
            // 
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.HorizontalScrollbar = true;
            this.filesListBox.Location = new System.Drawing.Point(13, 13);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.filesListBox.Size = new System.Drawing.Size(606, 212);
            this.filesListBox.Sorted = true;
            this.filesListBox.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(226, 231);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(307, 231);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 2;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // DeleteOldFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 263);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.filesListBox);
            this.Name = "DeleteOldFilesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Old Files";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button removeButton;
    }
}