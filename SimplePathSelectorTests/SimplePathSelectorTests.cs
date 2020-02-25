﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePathSelectorNamespace;

namespace SimplePathSelectorTestsNamespace
{
    [TestClass]
    public class SimplePathSelectorNamespaceTests
    {
        SimplePathSelector PathSelectorUnderTest;

        [TestInitialize]
        public void Setup()
        {
            PathSelectorUnderTest = new SimplePathSelector(
                typeof(DummyClass1),
                typeof(DummyClass2),
                typeof(DummyClass3),
                typeof(NoPathAvailableDummy),
                typeof(InvalidPathDummy));
        }

        [TestMethod]
        public void SelectPathTest1()
        {
            PathSelectorUnderTest.AddProvider(new DummyClass1());

            var selectedPath = PathSelectorUnderTest.SelectPath();

            Assert.AreEqual("DummyClass1", selectedPath);
        }

        [TestMethod]
        public void SelectPathTest2()
        {
            PathSelectorUnderTest.AddProvider(new DummyClass2());

            var selectedPath = PathSelectorUnderTest.SelectPath();

            Assert.AreEqual("DummyClass2", selectedPath);
        }

        [TestMethod]
        public void SelectPathTest3()
        {
            PathSelectorUnderTest.AddProvider(new DummyClass3());

            var selectedPath = PathSelectorUnderTest.SelectPath();

            Assert.AreEqual("DummyClass3", selectedPath);
        }

        [TestMethod]
        public void SelectPathTestCorrectOrder()
        {
            PathSelectorUnderTest.AddProvider(new DummyClass3());
            PathSelectorUnderTest.AddProvider(new DummyClass2());
            PathSelectorUnderTest.AddProvider(new DummyClass1());

            var selectedPath = PathSelectorUnderTest.SelectPath();

            Assert.AreEqual("DummyClass1", selectedPath);
        }

        [TestMethod]
        [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsForThisEntry))]
        public void NoPathForEntry()
        {
            PathSelectorUnderTest.SelectPath();
        }

        [TestMethod]
        [ExpectedException(typeof(PathProviderExceptions.NoPathAvailable))]
        public void NoPathAvailableExceptionFromProvider()
        {
            PathSelectorUnderTest.AddProvider(new NoPathAvailableDummy());
            PathSelectorUnderTest.SilenceNoPathAvailableExceptions = false;

            PathSelectorUnderTest.SelectPath();   
        }

        [TestMethod]
        [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsForThisEntry))]
        public void SilenceNoPathAvailableExceptionFromProvider()
        {
            PathSelectorUnderTest.AddProvider(new NoPathAvailableDummy());

            PathSelectorUnderTest.SelectPath();
        }

        [TestMethod]
        [ExpectedException(typeof(PathProviderExceptions.InvalidPath))]
        public void InvalidPathExceptionFromProvider()
        {
            PathSelectorUnderTest.AddProvider(new InvalidPathDummy());

            PathSelectorUnderTest.SelectPath();
        }

        [TestMethod]
        [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsForThisEntry))]
        public void SilenceInvalidPathExceptionFromProvider()
        {
            PathSelectorUnderTest.AddProvider(new InvalidPathDummy());
            PathSelectorUnderTest.SilenceInvalidPathExceptions = true;

            PathSelectorUnderTest.SelectPath();
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
