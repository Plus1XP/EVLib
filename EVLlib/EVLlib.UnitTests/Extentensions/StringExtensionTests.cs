using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVLlib.Extentensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Extentensions.Tests
{
    [TestClass()]
    public class StringExtensionTests
    {
        string normalTestString = "Hello! i am a simple string for testing purposes. " +
                                    "Please be gentle!";

        string multipleSpacedTestString = "   Hello! i     am a simple string     with far too many  " +
                "spaces...  Please    Help!   ";

        [TestMethod()]
        public void BetweenTest()
        {
            string expected = "for testing purposes. ";

            string results = normalTestString.Between("string ", "Please");

            Assert.AreEqual(expected, results);
        }
    }
}