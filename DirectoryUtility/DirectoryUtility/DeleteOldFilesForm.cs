using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryUtility
{
    public partial class DeleteOldFilesForm : Form
    {
        string path = "";

        public DeleteOldFilesForm(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            showUnusedFiles();
        }

        private void showUnusedFiles()
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly); // TODO SearchOption.TopDirectoryOnly ?

            for (int i = 0; i < files.Length; i++)
            {
                DateTime lastAccess = File.GetLastAccessTime(files[i]);
                DateTime todayDate = DateTime.Now;

                if (DateTime.Compare(lastAccess, todayDate) < 0)
                {
                    filesListBox.Items.Add(files[i]);
                }
                
            }
        }
    }
}
