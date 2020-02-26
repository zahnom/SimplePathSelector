using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePathSelectorNamespace;

namespace SimplePathSelectorBuilderTests
{
    [TestClass]
    public class SimplePathSelectorBuilderTests
    {
        [TestMethod]
        public void SimpleBuild()
        {
            var selector = new SimplePathSelectorBuilder()
                .FirstChoice(typeof(DummyClass1))
                .Otherwise(typeof(DummyClass2))
                .Create();

            selector.AddProvider(new DummyClass1());

            Assert.AreEqual("DummyClass1", selector.SelectPath());
        }

        [TestMethod]
        public void OtherwiseOptionShortNotation()
        {
            var selector = new SimplePathSelectorBuilder()
                .FirstChoice(typeof(DummyClass1))
                .Otherwise(@"C:\some\default\path\")
                .Create();

            Assert.AreEqual(@"C:\some\default\path\", selector.SelectPath());
        }

        [TestMethod]
        [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsAvailable))]
        public void NoProviders()
        {
            var selector = new SimplePathSelectorBuilder()
                .FirstChoice(typeof(DummyClass1))
                .Otherwise(typeof(DummyClass2))
                .Create();

            selector.SelectPath();
        }

        [TestMethod]
        public void SetProviderOrderWithValue1()
        {
            var selector = new SimplePathSelectorBuilder()
                .FirstChoice(typeof(DummyClass1))
                .Otherwise(new DummyClass2())
                .Create();

            Assert.AreEqual("DummyClass2", selector.SelectPath());
        }

        [TestMethod]
        public void SetProviderOrderWithValue2()
        {
            var selector = new SimplePathSelectorBuilder()
                .FirstChoice(typeof(DummyClass1))
                .Otherwise(new DummyClass2())
                .Create();

            selector.AddProvider(new DummyClass1());

            Assert.AreEqual("DummyClass1", selector.SelectPath());
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
        private class NoPathAvailableDummy
        {
            public override string ToString()
            {
                throw new PathProviderExceptions.NoPathAvailable();
            }
        }
        private class InvalidPathDummy
        {
            public override string ToString()
            {
                throw new PathProviderExceptions.InvalidPath();
            }
        }
    }
}
