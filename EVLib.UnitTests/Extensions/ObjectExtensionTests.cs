using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EVLib.Extensions.Tests
{
    public class TestObject
    {
        public string TestRef;
        public int TestVal;

        public TestObject(string testRef, int testVal)
        {
            this.TestRef = testRef;
            this.TestVal = testVal;
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