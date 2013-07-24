using System;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Accounts
{
    [TestClass]
    public class AccountsTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IUserAccountsQuery _userAccountsQuery = Resolve<IUserAccountsQuery>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();

        [TestInitialize]
        public void AccountsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestEnabled()
        {
            var member = _membersCommand.CreateTestMember(0);
            _userAccountsCommand.EnableUserAccount(member, Guid.NewGuid());
            _userAccountsCommand.ActivateUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsTrue(member.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(member.Id));

            // Disable.

            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsFalse(member.IsEnabled);
            Assert.IsTrue(member.IsActivated);
            Assert.IsFalse(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsFalse(_userAccountsQuery.IsActive(member.Id));

            // Enable.

            _userAccountsCommand.EnableUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsTrue(member.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(member.Id));
        }

        [TestMethod]
        public void TestActivated()
        {
            var member = _membersCommand.CreateTestMember(0);
            _userAccountsCommand.EnableUserAccount(member, Guid.NewGuid());
            _userAccountsCommand.ActivateUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsTrue(member.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(member.Id));

            // Deactivate.

            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsFalse(member.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsFalse(_userAccountsQuery.IsActive(member.Id));

            // Activate.

            _userAccountsCommand.ActivateUserAccount(member, Guid.NewGuid());

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);
            Assert.IsTrue(member.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(member.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(member.Id));
        }
    }
}
