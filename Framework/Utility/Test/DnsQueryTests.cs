using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Framework.Utility.Test
{
    [TestClass]
    public class DnsQueryTests
    {
        [TestMethod]
        public void GoodDomainTest()
        {
            var names = DnsQuery.GetMxNames("microsoft.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("live.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("live.com.au");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("hotmail.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("gmail.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("yahoo.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("bigpond.com.au");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("bigpond.com");
            Assert.AreNotEqual(0, names.Count);

            names = DnsQuery.GetMxNames("student.monash.edu");
            Assert.AreNotEqual(0, names.Count);
        }

        [TestMethod]
        public void BadDomainTest()
        {
            var names = DnsQuery.GetMxNames("hotmai.com");
            Assert.AreEqual(0, names.Count);

            names = DnsQuery.GetMxNames("yahoo.co.au");
            Assert.AreEqual(0, names.Count);

            names = DnsQuery.GetMxNames("bigpand.net");
            Assert.AreEqual(0, names.Count);
        }
    }
}