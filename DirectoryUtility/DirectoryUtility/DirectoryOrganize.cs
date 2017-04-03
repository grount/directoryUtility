using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryUtility
{
    class DirectoryOrganize
    {
        string[] filesInDirectory;
        string extension;
        string extensionWithoutDot;
        string destinationPath;
        string fileName;
        string selectedPath;
        DirectoryInfo dirInfo;

        public DirectoryOrganize(string selectedPath)
        {
            this.selectedPath = selectedPath;
            filesInDirectory = Directory.GetFiles(selectedPath, "*.*", SearchOption.TopDirectoryOnly);
            destinationPath = selectedPath;
        }

        public void PreparePathForMoving(int i)
        {
            extension = Path.GetExtension(filesInDirectory[i]);
            HandleFileExtension();
            fileName = Path.GetFileName(filesInDirectory[i]);
            destinationPath += "\\" + extensionWithoutDot;
            string pathForAddingTime = destinationPath;
            dirInfo = Directory.CreateDirectory(destinationPath);
            destinationPath += "\\" + fileName;

            IfDuplicateAddDate(pathForAddingTime);
        }

        public string GetFileName
        {
            get { return fileName; }
        }

        public int GetFilesLength
        {
            get { return filesInDirectory.Length; }
        }

        public string GetFileInDirectoryAtIndex(int i)
        {
            return filesInDirectory[i];
        }

        public string GetDestinationPath
        {
            get { return destinationPath; }
        }

        public string GetFullPath
        {
            get { return dirInfo.FullName; }
        }

        private void HandleFileExtension()
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

        private void IfDuplicateAddDate(string pathForAddingTime)
        {
            if (System.IO.File.Exists(selectedPath) == true)
            {
                DateTime thisDay = DateTime.Now;
                string thisDayString = String.Format("{0:dd-MM-HH_HH-mm-ss}", thisDay);
                string onlyFileName = Path.GetFileNameWithoutExtension(selectedPath);
                onlyFileName += "_" + thisDayString;
                onlyFileName += extension;

                pathForAddingTime += "\\" + onlyFileName;
                selectedPath = pathForAddingTime;
            }
        }


    }
}
