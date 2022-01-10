using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.IO;
using System.Text;

namespace EVLib.FileIO.Tests
{
    [TestClass]
    public class FileManagerTests
    {
        private string buildFolder = Directory.GetCurrentDirectory();
        private const string folderName = "PleaseDelete";
        private const string fileName = "PleaseDelete.txt";
        private const string sampleString = "Hello, I have nothing important inside (:";
        private byte[] sampleBytes = { 72, 101, 108, 108, 111, 44, 32, 73, 32, 104, 97, 118, 101,
            32, 110, 111, 116, 104, 105, 110, 103, 32, 105, 109, 112, 111, 114, 116, 97, 110,
            116, 32, 105, 110, 115, 105, 100, 101, 32, 40, 58 };

        static string[] fullDirectoryPath = new string[] { Directory.GetCurrentDirectory(), folderName };
        static string[] fullFilePath = new string[] { Directory.GetCurrentDirectory(), folderName, fileName };
        string testDirectory = Path.Combine(fullDirectoryPath);
        string testFile = Path.Combine(fullFilePath);

        public void CleanupDirectories()
        {
            File.Delete(this.testFile);
            Directory.Delete(this.testDirectory);
        }

        [TestMethod]
        public void FolderCreatedTests()
        {
            FileManager fileManager = new FileManager();

            bool expected = Directory.Exists(this.testDirectory);
            bool actual = fileManager.IsFolderCreated(this.testDirectory);

            Assert.AreEqual(expected, actual);

            if (!expected)
            {
                Directory.CreateDirectory(this.testDirectory);

                bool expected2 = Directory.Exists(this.testDirectory);
                bool actual2 = fileManager.IsFolderCreated(this.testDirectory);

                Assert.AreEqual(expected2, actual2);
            }

            this.CleanupDirectories();
        }

        [TestMethod]
        public void FilecreatedTest()
        {
            FileManager fileManager = new FileManager();

            bool expected = File.Exists(this.testFile);
            bool actual = fileManager.IsFileCreated(this.testFile);

            Assert.AreEqual(expected, actual);

            if (!expected)
            {
                Directory.CreateDirectory(this.testDirectory);
                File.Create(this.testFile).Close();

                bool expected2 = File.Exists(this.testFile);
                bool actual2 = fileManager.IsFileCreated(this.testFile);

                Assert.AreEqual(expected2, actual2);
            }

            this.CleanupDirectories();
        }

        [TestMethod]
        public void CreateFolderTest()
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateFolder(this.testDirectory);

            string expected = this.testDirectory;

            Assert.IsTrue(Directory.Exists(expected));

            this.CleanupDirectories();
        }

        [TestMethod]
        public void CreateFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);

            FileManager fileManager = new FileManager();
            fileManager.CreateFile(this.testFile);

            string expected = this.testFile;

            Assert.IsTrue(File.Exists(expected));

            this.CleanupDirectories();
        }

        [TestMethod]
        public void ClearFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);
            File.WriteAllText(this.testFile, sampleString);

            Assert.AreEqual(File.ReadAllText(this.testFile), sampleString);

            FileManager fileManager = new FileManager();
            fileManager.ClearFile(this.testFile);

            string actual = File.ReadAllText(this.testFile);
            string expected = string.Empty;

            Assert.AreEqual(actual, expected);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void DeleteFolderTest()
        {
            FileManager fileManager = new FileManager();

            bool isDirectoryCreated = Directory.Exists(this.testDirectory);

            if (!isDirectoryCreated)
            {
                Directory.CreateDirectory(this.testDirectory);
            }

            fileManager.DeleteFolder(this.testDirectory);

            isDirectoryCreated = Directory.Exists(this.testDirectory);

            Assert.IsFalse(isDirectoryCreated);

            if (isDirectoryCreated)
            {
                this.CleanupDirectories();
            }
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);

            using (var stream = File.Create(this.testFile))
            {
                stream.Close();
            }

            FileManager fileManager = new FileManager();
            fileManager.DeleteFile(this.testFile);

            string expected = this.testFile;

            Assert.IsFalse(File.Exists(expected));

            this.CleanupDirectories();
        }

        [TestMethod]
        public void SaveStringToFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);

            FileManager fileManager = new FileManager();
            fileManager.SaveToFile(this.testFile, sampleString);

            string expected = sampleString;
            string actual = File.ReadAllText(this.testFile);

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void SaveBytesToFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);

            FileManager fileManager = new FileManager();
            fileManager.SaveToFile(this.testFile, this.sampleBytes);

            byte[] actual = File.ReadAllBytes(this.testFile);
            byte[] expected = this.sampleBytes;

            CollectionAssert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void ReadStringFromFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);
            File.WriteAllText(this.testFile, sampleString);

            FileManager fileManager = new FileManager();

            string actual = fileManager.ReadStringFromFile(this.testFile);
            string expected = sampleString;

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void ReadBytesFromFileTest()
        {
            Directory.CreateDirectory(this.testDirectory);
            File.WriteAllBytes(this.testFile, this.sampleBytes);

            FileManager fileManager = new FileManager();

            byte[] byteArray = fileManager.ReadBytesFromFile(this.testFile);

            string actual = Encoding.UTF8.GetString(byteArray);
            string expected = sampleString;

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }

        [TestMethod]
        public void ReadLineFromFileTest()
        {
            StringBuilder multilineString = new StringBuilder();
            Random random = new Random();

            int linesToWrite = random.Next(1, 100);

            for (int i = 1; i <= linesToWrite; i++)
            {
                multilineString.AppendLine(i.ToString());
            }

            Directory.CreateDirectory(this.testDirectory);
            File.WriteAllText(this.testFile, multilineString.ToString());

            int lineToRead = random.Next(1, linesToWrite);

            FileManager fileManager = new FileManager();

            string actual = fileManager.ReadLineFromFile(this.testFile, lineToRead);
            string expected = lineToRead.ToString();

            Assert.AreEqual(expected, actual);

            this.CleanupDirectories();
        }
    }
}
