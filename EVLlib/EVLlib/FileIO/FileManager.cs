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
    }
}
