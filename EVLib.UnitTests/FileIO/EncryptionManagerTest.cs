using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVLlib.FileIO.Tests
{
    [TestClass]
    public class EncryptionManagerTests
    {
        private string encryptionKey = "7pGG9!ech449*10+FLe52ac&";
        private string sampleText = "Hi, I am sample text to be used in this test. Can you see me? o_O";

        private const string folderName = "PleaseDelete";
        private const string fileName = "PleaseDelete.txt";
        private static string[] fullDirectoryPath = new string[] { Directory.GetCurrentDirectory(), folderName };
        private static string[] fullFilePath = new string[] { Directory.GetCurrentDirectory(), folderName, fileName };
        private string testDirectory = Path.Combine(fullDirectoryPath);
        private string testFile = Path.Combine(fullFilePath);

        private void CleanupDirectories()
        {
            File.Delete(testFile);
            Directory.Delete(testDirectory);
        }

        [TestMethod]
        public void FileEncryptionTest()
        {
            EncryptionManager encryptionManager = new EncryptionManager();

            string expected = sampleText;

            Directory.CreateDirectory(testDirectory);

            encryptionManager.EncryptStringToFile(testFile, sampleText, encryptionKey);
            string actual = encryptionManager.DecryptStringFromFile(testFile, encryptionKey);

            Assert.AreEqual(expected, actual);

            CleanupDirectories();
        }
    }
}
