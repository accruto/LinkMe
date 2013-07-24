using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class LogoutTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        [TestMethod]
        public void TestMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            TestLogout(member, LoggedInMemberHomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestLogout(employer, LoggedInEmployerHomeUrl, EmployerHomeUrl);
        }

        [TestMethod]
        public void TestAdministrator()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            TestLogout(administrator, LoggedInAdministratorHomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestCustodian()
        {
            var community = _communitiesCommand.CreateTestCommunity(0);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);
            TestLogout(custodian, LoggedInCustodianHomeUrl, HomeUrl);
        }

        [TestMethod]
        public void TestMemberReturnUrl()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            TestLogoutReturnUrl(member, LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestEmployerReturnUrl()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            TestLogoutReturnUrl(employer, LoggedInEmployerHomeUrl);
        }

        [TestMethod]
        public void TestAdministratorReturnUrl()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            TestLogoutReturnUrl(administrator, LoggedInAdministratorHomeUrl);
        }

        [TestMethod]
        public void TestCustodianReturnUrl()
        {
            var community = _communitiesCommand.CreateTestCommunity(0);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);
            TestLogoutReturnUrl(custodian, LoggedInCustodianHomeUrl);
        }

        [TestMethod]
        public void TestHttp()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);

            // Logout.

            var logOutUrl = LogOutUrl.AsNonReadOnly();
            logOutUrl.Scheme = Uri.UriSchemeHttp;
            Get(logOutUrl);

            AssertUrl(HomeUrl);
        }

        private void TestLogout(IUser user, ReadOnlyUrl loggedInUrl, ReadOnlyUrl loggedOutUrl)
        {
            LogIn(user);
            AssertUrl(loggedInUrl);

            // Logout.

            LogOut();
            AssertUrl(loggedOutUrl);
        }

        private void TestLogoutReturnUrl(IUser user, ReadOnlyUrl loggedInUrl)
        {
            LogIn(user);
            AssertUrl(loggedInUrl);

            // Logout.

            var returnUrl = new ReadOnlyApplicationUrl(true, "~/privacy");
            var logOutUrl = LogOutUrl.AsNonReadOnly();
            logOutUrl.QueryString["returnUrl"] = returnUrl.Path;
            Get(logOutUrl);

            AssertUrl(returnUrl);
        }
    }
}
