using System;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Accounts
{
    [TestClass]
    public class AccountsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
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
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.EnableUserAccount(employer, Guid.NewGuid());
            _userAccountsCommand.ActivateUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(employer.IsEnabled);
            Assert.IsTrue(employer.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(employer.Id));

            // Disable.

            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsFalse(employer.IsEnabled);
            Assert.IsTrue(employer.IsActivated);
            Assert.IsFalse(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsFalse(_userAccountsQuery.IsActive(employer.Id));

            // Enable.

            _userAccountsCommand.EnableUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(employer.IsEnabled);
            Assert.IsTrue(employer.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(employer.Id));
        }

        [TestMethod]
        public void TestActivated()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.EnableUserAccount(employer, Guid.NewGuid());
            _userAccountsCommand.ActivateUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(employer.IsEnabled);
            Assert.IsTrue(employer.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(employer.Id));

            // Deactivate.

            _userAccountsCommand.DeactivateUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(employer.IsEnabled);
            Assert.IsFalse(employer.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsFalse(_userAccountsQuery.IsActive(employer.Id));

            // Activate.

            _userAccountsCommand.ActivateUserAccount(employer, Guid.NewGuid());

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(employer.IsEnabled);
            Assert.IsTrue(employer.IsActivated);
            Assert.IsTrue(_userAccountsQuery.IsEnabled(employer.Id));
            Assert.IsTrue(_userAccountsQuery.IsActive(employer.Id));
        }
    }
}