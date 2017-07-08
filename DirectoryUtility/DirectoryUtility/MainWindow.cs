using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace DirectoryUtility
{
    public partial class MainWindow : Form
    {
        private string selectedPath = "";
        private bool isPathSelected;

        public MainWindow()
        {
            InitializeComponent();
            fileProgressBar.Minimum = 1;
            fileProgressBar.Step = 1;
            isPathSelected = false;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            browseFilesDialog();
        }

        private void browseFilesDialog()
        {
            progressTextBox.Clear();
            fileProgressBar.Minimum = 1;
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
            if (extensionWithoutDot == null) throw new ArgumentNullException(nameof(extensionWithoutDot));
            extensionWithoutDot = extension != "" ? extension.Substring(1, extension.Length - 1) : "File";
        }

        private void organizeFilesStartButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected)
            {
                progressTextBox.Clear();

                if (Directory.Exists(selectedPath))
                {
                    string[] files = Directory.GetFiles(selectedPath, "*.*", SearchOption.TopDirectoryOnly);
                    bool fileUsedState = true;

                    fileProgressBar.Maximum = files.Length;

                    foreach (string t in files)
                    {
                        string extension = Path.GetExtension(t);
                        string extenstionWithoutDot = "";

                        handleFileExtension(ref extenstionWithoutDot, ref extension);

                        string newPath = selectedPath;
                        string fileName = Path.GetFileName(t);

                        newPath += "\\" + extenstionWithoutDot;
                        var saveFilePath = newPath;
                        DirectoryInfo dirInfo = Directory.CreateDirectory(newPath);
                        newPath += "\\" + fileName;

                        ifDuplicateAddDate(ref newPath, saveFilePath);

                        try
                        {
                            File.Move(t, newPath);
                        }
                        catch (IOException)
                        {
                            fileUsedState = false;
                        }

                        fileProgressBar.PerformStep();

                        if (fileUsedState)
                        {
                            printActionsToTextBox(fileName, dirInfo.FullName, eAction.Move);
                        }
                    }


                    if (fileUsedState == false && files.Length == 1)
                    {
                        MessageBox.Show("Cannot move file, Currently in use", "Operation Failed", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else if (MessageBox.Show("Do you want to enter the directory?", "Opeartiong finished",
                                 MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
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
            if (System.IO.File.Exists(path) == true)
            {
                DateTime thisDay = DateTime.Now;
                string thisDayString = $"{thisDay:dd-MM-HH_HH-mm-ss}";
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
                sb.AppendLine($"Moving {fileName} to {newDirectory}");
                progressTextBox.Text += sb.ToString();
                progressTextBox.Refresh();
                progressTextBox.SelectionStart = progressTextBox.Text.Length;
                progressTextBox.ScrollToCaret();
            }
        }

        private void invalidPath()
        {
            MessageBox.Show("Please select a folder", "No folder selected", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
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