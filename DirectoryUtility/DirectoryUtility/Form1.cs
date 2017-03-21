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


namespace DirectoryUtility
{
    public partial class Form1 : Form
    {
        string selectedPath = "";
        bool isPathSelected = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            browseFilesDialog();
        }

        private void browseFilesDialog()
        {
            progressTextBox.Clear();

            if (directoryBrowserDialog.ShowDialog() == DialogResult.OK)
            { 
                selectedPath = directoryBrowserDialog.SelectedPath;
                directoryTextBox.Text = selectedPath;
                isPathSelected = true;
            }

        }

        private void organizeFilesStartButton_Click(object sender, EventArgs e)
        {
            if (isPathSelected == true) 
            {
                if (Directory.Exists(selectedPath)) // might be useles??
                {
                    string[] files = Directory.GetFiles(selectedPath, "*.*", SearchOption.TopDirectoryOnly);

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
                MessageBox.Show("Please select a folder", "No folder selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                browseFilesDialog();
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
    }
}
