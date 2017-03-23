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
        List<Tuple<string, int>> selectedList;

        public DeleteOldFilesForm(string path)
        {
            InitializeComponent();
            this.path = path;
            selectedList = new List<Tuple<string, int>>();

            filesListBox.Items.Clear();
            showUnusedFiles();
        }


        private void showUnusedFiles()
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories); // SearchOption.TopDirectoryOnly ?
            DateTime todayDate = DateTime.Now;
            int j = 0;
            for (int i = 0; i < files.Length; i++)
            {
                DateTime lastAccess = File.GetLastAccessTime(files[i]);

                if (DateTime.Compare(lastAccess, todayDate) < 0)
                {
                    filesListBox.Items.Add(files[i]);
                } 
            }
        }

        private void addSelectedItemsToList()
        {
            selectedList.Clear();
            foreach (var item in filesListBox.SelectedItems)
            {
                Tuple<string, int> itemToAdd = new Tuple<string, int>(item.ToString(), filesListBox.SelectedIndex);
                selectedList.Add(itemToAdd);
            }

        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            addSelectedItemsToList();
            if (selectedList.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to delete the files?", "Confirmation required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                      == DialogResult.OK)
                {
                    foreach (var item in selectedList)
                    {
                        File.Delete(item.Item1);
                        filesListBox.Items.RemoveAt(item.Item2);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select items", "No files selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < filesListBox.Items.Count; i++)
            {
                filesListBox.SetSelected(i, true);
            }
        }
    }
}
