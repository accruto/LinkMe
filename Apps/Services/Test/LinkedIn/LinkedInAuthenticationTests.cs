using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.LinkedIn.Commands;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.LinkedIn
{
    [TestClass]
    public class LinkedInAuthenticationTests
        : TestClass
    {
        private readonly ILinkedInAuthenticationCommand _linkedInAuthenticationCommand = Resolve<ILinkedInAuthenticationCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ILinkedInCommand _linkedInCommand = Resolve<ILinkedInCommand>();

        private const string LinkedInId = "abc";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoProfile()
        {
            var result = _linkedInAuthenticationCommand.AuthenticateUser(LinkedInId);
            Assert.AreEqual(AuthenticationStatus.Failed, result.Status);
            Assert.IsNull(result.User);
        }

        [TestMethod]
        public void TestProfile()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = employer.Id,
            };

            _linkedInCommand.UpdateProfile(profile);

            var result = _linkedInAuthenticationCommand.AuthenticateUser(LinkedInId);
            Assert.AreEqual(AuthenticationStatus.Authenticated, result.Status);
            Assert.AreEqual(employer.Id, result.User.Id);
        }

        [TestMethod]
        public void TestDisabledProfile()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());

            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = employer.Id,
            };

            _linkedInCommand.UpdateProfile(profile);

            var result = _linkedInAuthenticationCommand.AuthenticateUser(LinkedInId);
            Assert.AreEqual(AuthenticationStatus.Disabled, result.Status);
            Assert.AreEqual(employer.Id, result.User.Id);
        }

        [TestMethod]
        public void TestDeactivatedProfile()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.DeactivateUserAccount(employer, Guid.NewGuid());

            var profile = new LinkedInProfile
            {
                Id = LinkedInId,
                UserId = employer.Id,
            };

            _linkedInCommand.UpdateProfile(profile);

            var result = _linkedInAuthenticationCommand.AuthenticateUser(LinkedInId);
            Assert.AreEqual(AuthenticationStatus.Deactivated, result.Status);
            Assert.AreEqual(employer.Id, result.User.Id);
        }
    }
}
