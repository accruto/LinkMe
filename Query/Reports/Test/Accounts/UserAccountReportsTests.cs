using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Accounts
{
    [TestClass]
    public class UserAccountReportsTests
        : AccountReportsTests
    {
        [TestMethod]
        public void TestLongTimeAgo()
        {
            CreateMember(DateTime.Now.AddDays(-10));

            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Yesterday));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Today));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, new DayRange(DateTime.Now.Date.AddDays(1))));
        }

        [TestMethod]
        public void TestYesterday()
        {
            CreateMember(DateTime.Now.AddDays(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
            Assert.AreEqual(1, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Yesterday));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Today));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, new DayRange(DateTime.Now.Date.AddDays(1))));
        }

        [TestMethod]
        public void TestToday()
        {
            CreateMember(DateTime.Now);

            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Yesterday));
            Assert.AreEqual(1, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Today));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, new DayRange(DateTime.Now.Date.AddDays(1))));
        }

        [TestMethod]
        public void TestTomorrow()
        {
            CreateMember(DateTime.Now.AddDays(1));

            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Yesterday));
            Assert.AreEqual(0, _accountReportsQuery.GetNewUsers(UserType.Member, DayRange.Today));
            Assert.AreEqual(1, _accountReportsQuery.GetNewUsers(UserType.Member, new DayRange(DateTime.Now.Date.AddDays(1))));
        }
    }
}