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
    public partial class MainWindow : Form
    {
        string selectedPath = "";
        bool isPathSelected = false;

        public MainWindow()
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

        private void handleFileExtension(ref string extensionWithoutDot, ref string extension)
        {
            if (extension != "")
            {
                extensionWithoutDot = extension.Substring(1, extension.Length - 1);
            }
            else
            {
                extensionWithoutDot = "File";
            }
        }

        static object organizeLock = new object();
        private void organizeFilesStartButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected == true) 
            {
                progressTextBox.Clear();

                if (Directory.Exists(selectedPath)) 
                {
                    string[] files = Directory.GetFiles(selectedPath, "*.*", SearchOption.TopDirectoryOnly);
                    fileProgressBar.Maximum = files.Length;
                    string saveFilePath;
                    bool fileUsedState = true;
                    

                    Parallel.For(0, files.Length, i =>
                    {
                        string extension = Path.GetExtension(files[i]);
                        string extenstionWithoutDot = "";

                        handleFileExtension(ref extenstionWithoutDot, ref extension);

                        string newPath = selectedPath;
                        string fileName = Path.GetFileName(files[i]);

                        newPath += "\\" + extenstionWithoutDot;
                        saveFilePath = newPath;
                        DirectoryInfo dirInfo = Directory.CreateDirectory(newPath);
                        newPath += "\\" + fileName;

                        ifDuplicateAddDate(ref newPath, saveFilePath);

                        try
                        {
                            System.IO.File.Move(files[i], newPath);
                        }
                        catch (System.IO.IOException)
                        {
                            fileUsedState = false;
                        }

                        lock(organizeLock)
                        {
                            fileProgressBar.PerformStep();

                            if (fileUsedState == true)
                            {
                                printActionsToTextBox(fileName, dirInfo.FullName, eAction.Move);
                            }
                        }
                    });
                    
                    if (fileUsedState == false && files.Length == 1)
                    {
                        MessageBox.Show("Cannot move file, Currently in use", "Operation Failed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (MessageBox.Show("Do you want to enter the directory?", "Opeartiong finished", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) 
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

        private void ifDuplicateAddDate(ref string path, string pathWithFile)
        {
            if(System.IO.File.Exists(path) == true)
            {
                DateTime thisDay = DateTime.Now;
                string thisDayString = String.Format("{0:dd-MM-HH_HH-mm-ss}", thisDay);
                string extension = Path.GetExtension(path);
                string onlyFileName = Path.GetFileNameWithoutExtension(path);
                onlyFileName += "_" + thisDayString;
                onlyFileName += extension;
                pathWithFile += "\\" + onlyFileName;
                path = pathWithFile;
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
