using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Accounts
{
    [TestClass]
    public class EnabledAccountReportsTests
        : AccountReportsTests
    {
        [TestMethod]
        public void TestCreatedLongTimeAgo()
        {
            CreateMember(DateTime.Now.AddDays(-10));

            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedYesterday()
        {
            CreateMember(DateTime.Now.AddDays(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedToday()
        {
            CreateMember(DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestDisabledLongTimeAgo()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-10).AddMinutes(1));

            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestDisabledYesterday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-1));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-1).AddMinutes(1));

            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestDisabledToday()
        {
            var member = CreateMember(DateTime.Now.AddMinutes(-1));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1).AddSeconds(10));

            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoDisabledYesterday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-1));

            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoDisabledToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoDisabledEnabledToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-3));
            _userAccountsRepository.EnableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-2));

            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoDisabledEnabledDisabledToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-3));
            _userAccountsRepository.EnableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-2));
            _userAccountsRepository.DisableUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetEnabledUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }
    }
}