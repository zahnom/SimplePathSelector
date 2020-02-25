using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplePathSelectorNamespace;

namespace SimplePathSelectorTestsNamespace
{
    [TestClass]
    public class SimplePathSelectorTests
    {
        [TestClass]
        public class NoPathsAvailableTests
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

                PathSelectorUnderTest.AddProvider(new NoPathAvailableDummy());
            }

            [TestMethod]
            [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsAvailable))]
            public void NoPathsAvailable()
            {
                PathSelectorUnderTest.SelectPath();
            }

            [TestMethod]
            public void PathsListShouldBeEmpty()
            {
                Assert.AreEqual(0, PathSelectorUnderTest.Paths.Count);
            }

            [TestMethod]
            [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsAvailable))]
            public void SilenceInvalidPathExceptionFromProvider()
            {
                PathSelectorUnderTest.AddProvider(new InvalidPathDummy());
                PathSelectorUnderTest.SilenceInvalidPathExceptions = true;

                PathSelectorUnderTest.SelectPath();
            }
        }

        [TestClass]
        public class PathSelectionTests
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
        }

        [TestClass]
        public class NoPathAvailableFromProvider
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

                PathSelectorUnderTest.AddProvider(new NoPathAvailableDummy());
            }

            [TestMethod]
            [ExpectedException(typeof(PathProviderExceptions.NoPathAvailable))]
            public void NoPathAvailableExceptionFromProvider1()
            {
                PathSelectorUnderTest.SilenceNoPathAvailableExceptions = false;
                PathSelectorUnderTest.SelectPath();
            }

            [TestMethod]
            [ExpectedException(typeof(PathProviderExceptions.NoPathAvailable))]
            public void NoPathAvailableExceptionFromProvider2()
            {
                PathSelectorUnderTest.SilenceNoPathAvailableExceptions = false;
                var result = PathSelectorUnderTest.Paths;
            }

            [TestMethod]
            [ExpectedException(typeof(SimplePathSelectorExceptions.NoPathsAvailable))]
            public void IgnoreNoPathAvailableException1()
            {
                PathSelectorUnderTest.SelectPath();
            }

            [TestMethod]
            public void IgnoreNoPathAvailableException2()
            {
                var result = PathSelectorUnderTest.Paths;
            }
        }

        [TestClass]
        public class InvalidPathTests
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
            [ExpectedException(typeof(PathProviderExceptions.InvalidPath))]
            public void InvalidPathExceptionFromProvider()
            {
                PathSelectorUnderTest.AddProvider(new InvalidPathDummy());
                PathSelectorUnderTest.SelectPath();
            }
        }

        [TestClass]
        public class CastingTests
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

                PathSelectorUnderTest.AddProvider(new NoPathAvailableDummy());
                PathSelectorUnderTest.AddProvider(new DummyClass3());
                PathSelectorUnderTest.AddProvider(new DummyClass2());
                PathSelectorUnderTest.AddProvider(new DummyClass1());
            }

            [TestMethod]
            public void ImplicitCasting()
            {
                Assert.AreEqual("DummyClass1", PathSelectorUnderTest);
            }

            [TestMethod]
            public void ToStringTest()
            {
                Assert.AreEqual("DummyClass1", PathSelectorUnderTest.ToString());
            }
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
