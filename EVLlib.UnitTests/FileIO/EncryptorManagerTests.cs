using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EVLlib.FileIO.Tests
{
    [TestClass]
    public class EncryptorManagerTests
    {
        private string encryptionKey = "7pGG9!ech449*10";
        private string sampleText = "Hi, I am sample text to be used in this test. Can you see me? o_O";

        private const string folderName = "PleaseDelete";
        private const string fileName = "PleaseDelete.txt";
        private string testDirectory = $"{Directory.GetCurrentDirectory()}\\{folderName}";
        private string testFile = $"{Directory.GetCurrentDirectory()}\\{folderName}\\{fileName}";
    }
}
