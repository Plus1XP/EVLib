using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVLlib.FileIO
{
    public class FileManager
    {
        /// <summary>
        /// Checks if folder exists.
        /// </summary>
        /// <param name="filePath">The file to write to.</param>
        /// <returns>Boolean true if folder exists, false if folder does not exist.</returns>
        public bool IsFolderCreated(string filePath)
        {
            return Directory.Exists(filePath);
        }

        /// <summary>
        /// Checks if file exists.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Boolean true if file exists, false if file does not exist.</returns>
        public bool IsFileCreated(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Creates folder in the specified path.
        /// </summary>
        /// <param name="folderPath">Path to folder.</param>
        public void CreateFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// Creates file in the specified path.
        /// </summary>
        /// <param name="filePath">Path to a file.</param>
        public void CreateFile(string filePath)
        {
            File.Create(filePath).Close();
        }

        /// <summary>
        /// Overwrites file with an empty string.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        public void ClearFile(string filePath)
        {
            File.WriteAllText(filePath, string.Empty);
        }

        /// <summary>
        /// Deletes folder from the specified path.
        /// </summary>
        /// <param name="folderPath">Path to folder.</param>
        public void DeleteFolder(string folderPath)
        {
            Directory.Delete(folderPath);
        }
    }
}
