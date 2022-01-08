using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;

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
            File.Delete(this.testFile);
            Directory.Delete(this.testDirectory);
        }

        [TestMethod]
        public void FileEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = this.sampleText;

            Directory.CreateDirectory(this.testDirectory);

            encryptor.EncryptToFile(this.testFile, this.sampleText, this.encryptionKey);
            string actual = encryptor.DecryptFromFile(this.testFile, this.encryptionKey);

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void StringEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = this.sampleText;

            string encryptedString = encryptor.EncryptToString(this.sampleText, this.encryptionKey);
            string actual = encryptor.DecryptFromString(encryptedString, this.encryptionKey);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ByteArrayEncryptionTest()
        {
            EncryptorManager encryptor = new EncryptorManager();

            string expected = this.sampleText;

            byte[] encryptedBytes = encryptor.EncryptToByteArray(this.sampleText, this.encryptionKey);
            string actual = encryptor.DecryptFromByteArray(encryptedBytes, this.encryptionKey);

            Assert.AreEqual(expected, actual);
        }
    }
}
