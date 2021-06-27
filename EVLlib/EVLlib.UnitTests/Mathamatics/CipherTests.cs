using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVLlib.Mathamatics;

using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Mathamatics.Tests
{
    [TestClass()]
    public class CipherTests
    {
        static object[] Data
        {
            get => new[]
            {
                new object[] {"Hello World!", "7H1y23oiws", "g.OfYdG/YdG3QJGDA9KnU1eP" }
            };
        }

        [TestMethod()]
        [DynamicData("Data")]
        public void EncodeTest(string decodedData, string key, string encodedData)
        {
            Cipher cipher = new Cipher();

            string expected = encodedData;
            string actual = cipher.Encode(decodedData, key);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [DynamicData("Data")]
        public void DecodeTest(string decodedData, string key, string encodedData)
        {
            Cipher cipher = new Cipher();

            string expected = decodedData;
            string actual = cipher.Decode(encodedData, key);

            Assert.AreEqual(expected, actual);
        }
    }
}