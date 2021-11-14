using Microsoft.VisualStudio.TestTools.UnitTesting;
using EVLlib.Extentensions;

using System;
using System.Collections.Generic;
using System.Text;

namespace EVLlib.Extentensions.Tests
{
    public class TestObject
    {
        public string TestRef;
        public int TestVal;

        public TestObject(string testRef, int testVal)
        {
            TestRef = testRef;
            TestVal = testVal;
        }

        public TestObject()
        {

        }
    }

    [TestClass()]
    public class ObjectExtensionTests
    {
        [DataTestMethod]
        [DataRow("Testing", 123)]
        public void ShallowCopyTest_AreSame(string reference, int value)
        {
            TestObject PopulatedObject = new TestObject(reference, value);
            TestObject BlankObject = new TestObject();

            BlankObject.ShallowCopy(PopulatedObject);

            Assert.AreSame(BlankObject.TestRef, PopulatedObject.TestRef);
            Assert.AreEqual(BlankObject.TestVal, PopulatedObject.TestVal);
        }
    }
}