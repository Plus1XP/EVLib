using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace EVLib.FileIO
{
    public class FileManager
    {
        /// <summary>
        /// Checks if folder exists.
        /// </summary>
        /// <param name="folderPath">Path to folder.</param>
        /// <returns>Boolean true if folder exists, false if folder does not exist.</returns>
        public bool IsFolderCreated(string folderPath)
        {
            return Directory.Exists(folderPath);
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
        /// <remarks>
        /// This operation is recursive and will delete all subdirectories and files.
        /// </remarks>
        /// <param name="folderPath">Path to folder.</param>
        public void DeleteFolder(string folderPath)
        {
            Directory.Delete(folderPath, true);
        }

        /// <summary>
        /// Deletes file from the specified path and in case of error will wait and try again.
        /// </summary>
        /// <remarks>
        /// Retry pattern tries 3 times (NumberOfRetries) with a 1 second delay (DelayOnretry).
        /// </remarks>
        /// <param name="filePath">Path to file.</param>
        public void DeleteFile(string filePath)
        {
            const int NumberOfRetries = 3;
            const int DelayOnRetry = 1000;

            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    File.Delete(filePath);
                    // When done we can break loop
                    break;
                }
                catch (IOException e) when (i <= NumberOfRetries)
                {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    Thread.Sleep(DelayOnRetry);
                }
            }
        }

        /// <summary>
        /// Writes a String to a file on disk.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="Value">String value to save.</param>
        public void SaveToFile(string filePath, string Value)
        {
            File.WriteAllText(filePath, Value);
            //StreamWriter writeFile = new StreamWriter(filePath);
            //writeFile.Write(Value);
            //writeFile.Close();
        }

        /// <summary>
        /// Writes an array of bytes to a file on disk.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="Value">Byte array to save.</param>
        public void SaveToFile(string filePath, byte[] Value)
        {
            File.WriteAllBytes(filePath, Value);
        }

        /// <summary>
        /// Reads text from file on disk.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>String containing all text from file.</returns>
        public string ReadStringFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Reads bytes from file on disk.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>Byte Array containing all text from file.</returns>
        public byte[] ReadBytesFromFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Reads a specific line from a file on disk.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <param name="lineNumber">Line number to read.</param>
        /// <returns>String containing text from specific line number.</returns>
        public string ReadLineFromFile(string filePath, int lineNumber)
        {
            return this.ReadSpecificLine(filePath, lineNumber);
        }

        /// <summary>
        /// Reads a specific line from a file on disk.
        /// </summary>
        //// <param name="filePath">Path to file.</param>
        /// <param name="lineNumber">Line number to read.</param>
        /// <returns>String containing text from specific line number.</returns>
        private string ReadSpecificLine(string filePath, int lineNumber)
        {
            using (StreamReader file = new StreamReader(filePath))
            {
                string content = null;
                for (int i = 1; i < lineNumber; i++)
                {
                    file.ReadLine();

                    if (file.EndOfStream)
                    {
                        break;
                    }
                }
                content = file.ReadLine();
                return content;
            }
        }
    }
}
