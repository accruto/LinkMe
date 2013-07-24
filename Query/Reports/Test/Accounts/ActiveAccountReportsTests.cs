using System;
using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Accounts
{
    [TestClass]
    public class ActiveAccountReportsTests
        : AccountReportsTests
    {
        [TestMethod]
        public void TestCreatedLongTimeAgo()
        {
            CreateMember(DateTime.Now.AddDays(-10));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedYesterday()
        {
            CreateMember(DateTime.Now.AddDays(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedToday()
        {
            CreateMember(DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestActivatedLongTimeAgo()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-10).AddMinutes(1));

            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestActivatedYesterday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-1));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-1).AddMinutes(1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestActivatedToday()
        {
            var member = CreateMember(DateTime.Now.AddMinutes(-1));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1).AddSeconds(10));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoActivatedYesterday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddDays(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoActivatedToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoActivatedDeactivatedToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-3));
            _userAccountsRepository.DeactivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-2));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }

        [TestMethod]
        public void TestCreatedLongTimeAgoActivatedDeactivatedActivatedToday()
        {
            var member = CreateMember(DateTime.Now.AddDays(-10));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-3));
            _userAccountsRepository.DeactivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-2));
            _userAccountsRepository.ActivateUserAccount(member.Id, Guid.NewGuid(), DateTime.Now.AddMinutes(-1));

            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(-1)));
            Assert.AreEqual(0, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date));
            Assert.AreEqual(1, _accountReportsQuery.GetActiveUsers(UserType.Member, DateTime.Now.Date.AddDays(1)));
        }
    }
}