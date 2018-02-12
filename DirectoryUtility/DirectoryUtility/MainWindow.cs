using System;
using System.Collections.Generic;
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
        private HashSet<string> knownFileExtensions = new HashSet<string>();

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

        private void loadSave()
        {
            string savePath = selectedPath + "\\Save";

            if (File.Exists(savePath))
            {
                knownFileExtensions = ReadFromBinaryFile<HashSet<string>>(selectedPath + "\\Save");
            }
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
                    loadSave();
                    fileProgressBar.Maximum = files.Length;

                    foreach (string t in files)
                    {
                        string extension = Path.GetExtension(t);
                        string extensionWithoutDot = "";

                        handleFileExtension(ref extensionWithoutDot, ref extension);
                        knownFileExtensions.Add(extensionWithoutDot);

                        string newPath = selectedPath;
                        string fileName = Path.GetFileName(t);

                        newPath += "\\" + extensionWithoutDot;
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

                    unifyFolders();
                    displayMessages(fileUsedState, files.Length);
                }
            }
            else
            {
                invalidPath();
            }
        }

        private void displayMessages(bool fileUsedState, int filesLength)
        {
            if (fileUsedState == false && filesLength == 1)
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

        private void unifyFolders()
        {
            try
            {
                string[] folders = Directory.GetDirectories(selectedPath);
                string otherFolderPath = selectedPath + "\\Other";
                Directory.CreateDirectory(otherFolderPath);

                foreach (string file in folders)
                {
                    if (!knownFileExtensions.Contains(Path.GetFileName(file)) && file != otherFolderPath)
                    {
                        string newPath = otherFolderPath + "\\" + Path.GetFileName(file);
                        Directory.Move(file, newPath);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The following error occurred: ${e}", e.Message);
            }
        }


        private void ifDuplicateAddDate(ref string path, string pathWithFile)
        {
            if (File.Exists(path))
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


        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            string saveFilePath = selectedPath + "\\Save";
            WriteToBinaryFile(saveFilePath, knownFileExtensions, true);
        }

        private static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        private static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

    }
}