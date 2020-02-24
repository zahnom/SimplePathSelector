using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePathSelectorNamespace;

namespace SimplePathSelectorNamespaceTests
{
    [TestClass]
    public class ClassEqualityComparerNamespaceTests
    {
        [TestMethod]
        public void CompareClasses()
        {
            var comparerUnderTest = new ClassEqualityComparer();

            var c1d1 = new DummyClass1();
            var c1d2 = new DummyClass1();
            var c2d1 = new DummyClass2();
            var c2d2 = new DummyClass2();
            var c3d1 = new DummyClass3();

            Assert.IsTrue( comparerUnderTest.Equals(c1d1, c1d2) );
            Assert.IsTrue( comparerUnderTest.Equals(c2d1, c2d2) );
            Assert.IsFalse( comparerUnderTest.Equals(c1d1, c2d2) );
            Assert.IsFalse( comparerUnderTest.Equals(c1d1, c2d1) );
            Assert.IsFalse( comparerUnderTest.Equals(c1d1, c3d1) );
            Assert.IsFalse( comparerUnderTest.Equals(c2d1, c3d1) );
            Assert.IsFalse( comparerUnderTest.Equals(c2d2, c3d1) );
        }

        [TestMethod]
        public void CompareHashes()
        {
            var comparerUnderTest = new ClassEqualityComparer();

            var c1d1 = new DummyClass1();
            var c1d2 = new DummyClass1();
            var c2d1 = new DummyClass2();
            var c2d2 = new DummyClass2();
            var c3d1 = new DummyClass3();

            var c1d1Hash = comparerUnderTest.GetHashCode(c1d1);
            var c1d2Hash = comparerUnderTest.GetHashCode(c1d2);
            var c2d1Hash = comparerUnderTest.GetHashCode(c2d1);
            var c2d2Hash = comparerUnderTest.GetHashCode(c2d2);
            var c3d1Hash = comparerUnderTest.GetHashCode(c3d1);

            Assert.AreEqual(c1d1Hash, c1d2Hash);
            Assert.AreEqual(c2d1Hash, c2d2Hash);
            Assert.AreNotEqual(c1d1Hash, c2d1Hash);
            Assert.AreNotEqual(c1d2Hash, c2d1Hash);
            Assert.AreNotEqual(c1d1Hash, c3d1Hash);
            Assert.AreNotEqual(c2d1Hash, c3d1Hash);
        }

        private class DummyClass1
        {
            public override string ToString()
            {
                return "DummyClass1";
            }
        }
        private class DummyClass2
        {
            public override string ToString()
            {
                return "DummyClass2";
            }
        }
        private class DummyClass3
        {
            public override string ToString()
            {
                return "DummyClass3";
            }
        }
    }
}
