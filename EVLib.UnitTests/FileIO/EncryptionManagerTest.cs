﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

namespace EVLib.FileIO.Tests
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
            File.Delete(this.testFile);
            Directory.Delete(this.testDirectory);
        }

        [TestMethod]
        public void FileEncryptionTest()
        {
            EncryptionManager encryptionManager = new EncryptionManager();

            string expected = this.sampleText;

            Directory.CreateDirectory(this.testDirectory);

            encryptionManager.EncryptStringToFile(this.testFile, this.sampleText, this.encryptionKey);
            string actual = encryptionManager.DecryptStringFromFile(this.testFile, this.encryptionKey);

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }
    }
}
