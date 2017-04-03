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
        bool isPathSelected;
        bool isFileUsed;

        public MainWindow()
        {
            InitializeComponent();
            fileProgressBar.Minimum = 1;
            fileProgressBar.Step = 1;
            isPathSelected = false;
            isFileUsed = false;
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

        static object organizeLock = new object();

        private void organizeFilesStartButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected == true) 
            {
                progressTextBox.Clear();

                if (Directory.Exists(selectedPath)) 
                {
                    DirectoryOrganize dirOrganize = new DirectoryOrganize(selectedPath);

                    fileProgressBar.Maximum = dirOrganize.GetFilesLength;

                    isFileUsed = false;
                    Parallel.For(0, dirOrganize.GetFilesLength, i =>
                    {
                        dirOrganize.PreparePathForMoving(i);

                        try
                        {
                            System.IO.File.Move(dirOrganize.GetFileInDirectoryAtIndex(i), dirOrganize.GetDestinationPath);
                        }
                        catch (System.IO.IOException)
                        {
                            isFileUsed = false;
                        }

                        lock (organizeLock)
                        {
                            fileProgressBar.PerformStep();

                            if (isFileUsed == true)
                            {
                                printActionsToTextBox(dirOrganize.GetFileName, dirOrganize.GetFullPath, eAction.Move);
                            }
                        }
                    });

                    if (isFileUsed == false && dirOrganize.GetFilesLength == 1)
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

        private enum eAction
        {
            Move
        }

        private void invalidPath()
        {
            MessageBox.Show("Please select a folder", "No folder selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            browseFilesDialog();
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
