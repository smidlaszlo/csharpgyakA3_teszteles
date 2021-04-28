using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

//[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]

namespace NegyzetNevter.Tests
{
    [TestClass]
    public class Initialize
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }
    }

    [TestClass()]
    public class NegyzetTests
    {
        static Negyzet target;
        const double oldal = 10F;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            target = new Negyzet(oldal);

            Assert.AreEqual(oldal, target.Oldalhossz);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        //// 1. Called once before each test
        //public NegyzetTests()
        //{
        //}

        ////  2. Called once before each test after the constructor
        //[TestInitialize]
        //public void TestInitialize()
        //{
        //}

        //[TestMethod]
        //public void Test____()
        //{
        //    // 3.
        //}

        //// 4. Called once after each test before the Dispose method
        //[TestCleanup]
        //public void TestCleanup()
        //{
        //}

        //[Ignore]
        [TestCategory("JellemvonasKategoriaTrait")]
        //[TestMethod()]
        [TestMethod("Az Oldalhossz tulajdonsag allitsa be az oldalhosszt")]
        public void OldalhosszTest()
        {
            target.Oldalhossz = oldal;
            double actual = target.Oldalhossz;
            //Assert.AreEqual(oldal, actual);
            //Assert.AreEqual(oldal, actual, delta);
            Assert.AreEqual(oldal, actual, "Az Oldalhossz tulajdonsagnak be kell allitania az oldalhosszt!");
            //Assert.AreEqual(oldal, actual, delta, "uzenet");
        }

        [TestMethod()]
        public void AtloTest()
        {
            double actual;
            double expected = Math.Sqrt(oldal * oldal * 2);

            actual = target.Atlo;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void KeruletTest()
        {
            double expected = oldal * 4;
            double actual;
            actual = target.Kerulet();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TeruletTest()
        {
            double expected = oldal * oldal;
            double actual;
            actual = target.Terulet();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void TeglatestTerfogataTest()
        {
            double magassag = 10;
            double expected = target.Terulet() * magassag;
            double actual;
            actual = target.TeglatestTerfogata(magassag);
            Assert.AreEqual(expected, actual);
            
            magassag = 0;
            actual = target.TeglatestTerfogata(magassag);
        }

        [DataTestMethod]
        [DataRow(1, 100)]
        [DataRow(10, 1000)]
        public void TeglatestTerfogataTestDataTest(double magassag, double terfogat)
        {
            double expected = terfogat;
            double actual;
            actual = target.TeglatestTerfogata(magassag);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAdatok), DynamicDataSourceType.Method)]
        public void TeglatestTerfogataTestDynamicDataMethod(double magassag, double terfogat)
        {
            double expected = terfogat;
            double actual;
            actual = target.TeglatestTerfogata(magassag);

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> GetAdatok()
        {
            yield return new object[] { 1, 100 };
            yield return new object[] { 10, 1000 };
        }

        [DataTestMethod]
        [DynamicData(nameof(Adatok), DynamicDataSourceType.Property)]
        public void TeglatestTerfogataTestDynamicDataProperty(double magassag, double terfogat)
        {
            double expected = terfogat;
            double actual;
            actual = target.TeglatestTerfogata(magassag);

            Assert.AreEqual(expected, actual);
        }

        public static IEnumerable<object[]> Adatok
        {
            get
            {
                yield return new object[] { 1, 100 };
                yield return new object[] { 10, 1000 };
            }
        }
        
        [DataTestMethod]
        [SajatAdatForras]
        public void TeglatestTerfogataTestSajatAdatForras(double magassag, double terfogat)
        {
            double expected = terfogat;
            double actual;
            actual = target.TeglatestTerfogata(magassag);

            Assert.AreEqual(expected, actual);
        }

        #region parhuzamos teszteles
        //[TestMethod]
        //[DoNotParallelize]
        //public void Test1() => Thread.Sleep(5000);

        //[TestMethod]
        //public void Test2() => Thread.Sleep(5000);

        //[TestMethod]
        //public void Test3() => Thread.Sleep(5000);
        #endregion
    }

    public class SajatAdatForrasAttribute : Attribute, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            yield return new object[] { 1, 100 };
            yield return new object[] { 10, 1000 };
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data != null)
                return string.Format(CultureInfo.CurrentCulture, "Custom - {0} ({1})", methodInfo.Name, string.Join(",", data));

            return null;
        }
    }

}