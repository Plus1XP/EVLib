using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVLlib.FileIO.Tests
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


        string testDirectory = $"{Directory.GetCurrentDirectory()}\\{folderName}";
        string testFile = $"{Directory.GetCurrentDirectory()}\\{folderName}\\{fileName}";


        [TestMethod]
        public void FolderCreatedTests()
        {
            FileManager fileManager = new FileManager();

            bool expected = Directory.Exists(testDirectory);
            bool actual = fileManager.IsFolderCreated(testDirectory);

            Assert.AreEqual(expected, actual);

            if (!expected)
            {
                Directory.CreateDirectory(testDirectory);

                bool expected2 = Directory.Exists(testDirectory);
                bool actual2 = fileManager.IsFolderCreated(testDirectory);

                Assert.AreEqual(expected2, actual2);
            }
            
            Directory.Delete(testDirectory);
        }

        [TestMethod]
        public void FilecreatedTest()
        {
            FileManager fileManager = new FileManager();

            bool expected = File.Exists(testFile);
            bool actual = fileManager.IsFileCreated(testFile);

            Assert.AreEqual(expected, actual);

            if (!expected)
            {
                Directory.CreateDirectory(testDirectory);
                File.Create(testFile).Close();

                bool expected2 = File.Exists(testFile);
                bool actual2 = fileManager.IsFileCreated(testFile);

                Assert.AreEqual(expected2, actual2);
            }

            File.Delete(testFile);
            Directory.Delete(testDirectory);
        }

        [TestMethod]
        public void CreateFolderTest()
        {
            FileManager fileManager = new FileManager();
            fileManager.CreateFolder(testDirectory);

            string expected = testDirectory;

            Assert.IsTrue(Directory.Exists(expected));

            Directory.Delete(testDirectory);
        }
    }
}
