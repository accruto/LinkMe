using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test
{
    [TestClass]
    public class LuceneExtensionsTests
    {
        [TestMethod]
        public void TestToDaysFieldValue()
        {
            Assert.AreEqual(734264, new DateTime(2011, 5, 8, 21, 29, 27, 603).ToFieldValue(TimeGranularity.Day));
            Assert.AreEqual(734258, new DateTime(2011, 5, 2, 20, 49, 18, 603).ToFieldValue(TimeGranularity.Day));
            Assert.AreEqual(734258, new DateTime(2011, 5, 2, 10, 54, 37, 727).ToFieldValue(TimeGranularity.Day));
        }

        [TestMethod]
        public void TestToHoursFieldValue()
        {
            Assert.AreEqual(17622357, new DateTime(2011, 5, 8, 21, 29, 27, 603).ToFieldValue(TimeGranularity.Hour));
            Assert.AreEqual(17622212, new DateTime(2011, 5, 2, 20, 49, 18, 603).ToFieldValue(TimeGranularity.Hour));
            Assert.AreEqual(17622212, new DateTime(2011, 5, 2, 20, 54, 37, 727).ToFieldValue(TimeGranularity.Hour));
        }

        [TestMethod]
        public void TestToMinutesFieldValue()
        {
            Assert.AreEqual(1057341449, new DateTime(2011, 5, 8, 21, 29, 27, 603).ToFieldValue(TimeGranularity.Minute));
            Assert.AreEqual(1057332769, new DateTime(2011, 5, 2, 20, 49, 18, 603).ToFieldValue(TimeGranularity.Minute));
            Assert.AreEqual(1057332769, new DateTime(2011, 5, 2, 20, 49, 37, 727).ToFieldValue(TimeGranularity.Minute));
        }
    }
}
