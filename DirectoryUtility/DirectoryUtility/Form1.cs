using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace DirectoryUtility
{
    public partial class Form1 : Form
    {
        string selectedPath = "";
        bool isPathSelected = false;

        public Form1()
        {
            InitializeComponent();
            fileProgressBar.Minimum = 1;
            fileProgressBar.Step = 1;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            browseFilesDialog();
        }

        private void browseFilesDialog()
        {
            progressTextBox.Clear();
            fileProgressBar.Value = 1;

            if (directoryBrowserDialog.ShowDialog() == DialogResult.OK)
            { 
                selectedPath = directoryBrowserDialog.SelectedPath;
                directoryTextBox.Text = selectedPath;
                isPathSelected = true;
            }

        }

        //public void Wait(double seconds, Action action) // http://stackoverflow.com/questions/15597711/delay-in-c-sharp-not-thread-sleep
        //{
        //    Timer timer = new Timer();
        //    timer.Interval = (int)(seconds * 1000.0);
        //    timer.Tick += (s, o) => {
        //        timer.Enabled = false;
        //        timer.Dispose();
        //        action();
        //    };
        //    timer.Enabled = true;
        //}

        private void organizeFilesStartButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected == true) 
            {
                if (Directory.Exists(selectedPath)) 
                {
                    string[] files = Directory.GetFiles(selectedPath, "*.*", SearchOption.TopDirectoryOnly);
                    fileProgressBar.Maximum = files.Length;

                    for (int i = 0; i < files.Length; i++)
                    {
                        string extenstion = Path.GetExtension(files[i]);
                        string extenstionWithoutDot = "";

                        if (extenstion != "")
                        {
                            extenstionWithoutDot = extenstion.Substring(1, extenstion.Length - 1);
                        }
                        else
                        {
                            extenstionWithoutDot = "File";
                        }

                        string newPath = selectedPath;
                        string fileName = Path.GetFileName(files[i]);

                        newPath += "\\" + extenstionWithoutDot;
                        DirectoryInfo dirInfo = Directory.CreateDirectory(newPath);
                        newPath += "\\" + fileName;
                        System.IO.File.Move(files[i], newPath);

                        printActionsToTextBox(fileName, dirInfo.FullName, eAction.Move);
                        fileProgressBar.PerformStep();
                    
                    }

                    if (MessageBox.Show("Do you want to enter the directory?", "Opeartiong finished", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) 
                        == DialogResult.OK)
                    {
                        Process.Start("explorer.exe", selectedPath);
                    }
                }
            }
            else
            {
                invalidPath();
            }
        }
        void printActionsToTextBox(string fileName, string newDirectory, eAction actionType)
        {
            if (actionType == eAction.Move)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(String.Format("Moving {0} to {1}", fileName, newDirectory));
                progressTextBox.Text += sb.ToString();
                progressTextBox.Refresh();
                progressTextBox.SelectionStart = progressTextBox.Text.Length;
                progressTextBox.ScrollToCaret();
            }
        }

        private void invalidPath()
        {
            MessageBox.Show("Please select a folder", "No folder selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            browseFilesDialog();
        }

        private enum eAction
        {
            Move
        }

        private void startRemoveFilesButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected)
            {
                DeleteOldFilesForm childForm = new DeleteOldFilesForm(selectedPath);
                childForm.ShowDialog();
            }
            else
            {
                invalidPath();
            }
        }
    }
}
