using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVLib.FileIO.Tests
{
    [TestClass]
    public class EncryptorManagerTests
    {
        private string encryptionKey = "7pGG9!ech449*10";
        private string sampleText = "Hi, I am sample text to be used in this test. Can you see me? o_O";

        private const string folderName = "PleaseDelete";
        private const string fileName = "PleaseDelete.txt";
        static string[] fullDirectoryPath = new string[] { Directory.GetCurrentDirectory(), folderName };
        static string[] fullFilePath = new string[] { Directory.GetCurrentDirectory(), folderName, fileName };
        string testDirectory = Path.Combine(fullDirectoryPath);
        string testFile = Path.Combine(fullFilePath);

        private void CleanupDirectories()
        {
            File.Delete(testFile);
            Directory.Delete(testDirectory);
        }

        [TestMethod]
        public void FileEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = sampleText;

            Directory.CreateDirectory(testDirectory);

            encryptor.EncryptToFile(testFile, sampleText, encryptionKey);
            string actual = encryptor.DecryptFromFile(testFile, encryptionKey);

            Assert.AreEqual(expected, actual);

            CleanupDirectories();
        }

        [TestMethod]
        public void StringEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = sampleText;

            string encryptedString = encryptor.EncryptToString(sampleText, encryptionKey);
            string actual = encryptor.DecryptFromString(encryptedString, encryptionKey);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ByteArrayEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = sampleText;

            byte[] encryptedBytes = encryptor.EncryptToByteArray(sampleText, encryptionKey);
            string actual = encryptor.DecryptFromByteArray(encryptedBytes, encryptionKey);

            Assert.AreEqual(expected, actual);
        }
    }
}
