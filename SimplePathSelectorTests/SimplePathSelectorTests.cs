using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePathSelectorNamespace;

namespace SimplePathSelectorTestsNamespace
{
    [TestClass]
    public class SimplePathSelectorNamespaceTests
    {
        SimplePathSelector PathSelectorUnderTest = new SimplePathSelector(
                typeof(DummyClass1),
                typeof(DummyClass2),
                typeof(DummyClass3));

        [TestMethod]
        public void SelectPathTest1()
        {
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass1());

            var selectedPath = PathSelectorUnderTest.SelectPathFor("test");

            Assert.AreEqual("DummyClass1", selectedPath);
        }

        [TestMethod]
        public void SelectPathTest2()
        {
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass2());

            var selectedPath = PathSelectorUnderTest.SelectPathFor("test");

            Assert.AreEqual("DummyClass2", selectedPath);
        }

        [TestMethod]
        public void SelectPathTest3()
        {
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass3());

            var selectedPath = PathSelectorUnderTest.SelectPathFor("test");

            Assert.AreEqual("DummyClass3", selectedPath);
        }

        [TestMethod]
        public void SelectPathTestCorrectOrder()
        {
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass3());
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass2());
            PathSelectorUnderTest.AddPathProviderFor("test", new DummyClass1());

            var selectedPath = PathSelectorUnderTest.SelectPathFor("test");

            Assert.AreEqual("DummyClass1", selectedPath);
        }

        [TestMethod]
        public void SelectPathDifferentEntries()
        {
            PathSelectorUnderTest.AddPathProviderFor("test1", new DummyClass1());
            PathSelectorUnderTest.AddPathProviderFor("test2", new DummyClass2());
            PathSelectorUnderTest.AddPathProviderFor("test3", new DummyClass2());
            PathSelectorUnderTest.AddPathProviderFor("test3", new DummyClass3());

            var selectedPathForTest1 = PathSelectorUnderTest.SelectPathFor("test1");
            var selectedPathForTest2 = PathSelectorUnderTest.SelectPathFor("test2");
            var selectedPathForTest3 = PathSelectorUnderTest.SelectPathFor("test3");

            Assert.AreEqual("DummyClass1", selectedPathForTest1);
            Assert.AreEqual("DummyClass2", selectedPathForTest2);
            Assert.AreEqual("DummyClass2", selectedPathForTest3);
        }

        [TestMethod]
        [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsForThisEntry))]
        public void NoPathForEntry()
        {
            var selectedPath = PathSelectorUnderTest.SelectPathFor("test");
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
