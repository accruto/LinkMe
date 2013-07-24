using System;
using System.Net;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Accounts
{
    [TestClass]
    public class LoginTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

        private ReadOnlyUrl _profileUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/v1/employers/profile");
        }

        [TestMethod]
        public void TestNotLoggedIn()
        {
            var model = Deserialize<JsonResponseModel>(Get(HttpStatusCode.Forbidden, _profileUrl));
            AssertJsonError(model, null, "100", "The user is not logged in.");
        }

        [TestMethod]
        public void TestSuccess()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            AssertJsonSuccess(LogIn(employer));
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Get(_profileUrl)));
        }

        [TestMethod]
        public void TestLogOut()
        {
            var model = Deserialize<JsonResponseModel>(Get(HttpStatusCode.Forbidden, _profileUrl));
            AssertJsonError(model, null, "100", "The user is not logged in.");

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            AssertJsonSuccess(LogIn(employer));
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Get(_profileUrl)));
            AssertJsonSuccess(LogOut());

            model = Deserialize<JsonResponseModel>(Get(HttpStatusCode.Forbidden, _profileUrl));
            AssertJsonError(model, null, "100", "The user is not logged in.");
        }

        [TestMethod]
        public void TestUnknownUser()
        {
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, "UnknownLoginId", "password"), null, "101", "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, member.GetLoginId(), member.GetPassword()), null, "101", "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestAdministrator()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, administrator.GetLoginId(), administrator.GetPassword()), null, "101", "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestCustodian()
        {
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, Guid.NewGuid());
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, custodian.GetLoginId(), custodian.GetPassword()), null, "101", "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestUnknownPassword()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, employer.GetLoginId(), "aaaaaa"), null, "101", "Login failed. Please try again.");
        }

        [TestMethod]
        public void TestDisabledUser()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());
            AssertJsonError(LogIn(HttpStatusCode.Forbidden, employer.GetLoginId(), employer.GetPassword()), null, "102", "The user is disabled.");
        }

        [TestMethod]
        public void TestUserMustChangePassword()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            var credentials = _loginCredentialsQuery.GetCredentials(employer.Id);
            credentials.MustChangePassword = true;
            _loginCredentialsCommand.UpdateCredentials(employer.Id, credentials, Guid.NewGuid());

            AssertJsonError(LogIn(HttpStatusCode.Forbidden, employer.GetLoginId(), employer.GetPassword()), null, "103", "The user must change their password.");
        }
    }
}