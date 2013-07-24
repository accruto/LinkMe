using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public abstract class LoginTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IPasswordsCommand _passwordsCommand = Resolve<IPasswordsCommand>();

        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _mustChangePasswordUrl;
        private ReadOnlyUrl _contactUsUrl;
        private ReadOnlyUrl _searchJobsUrl;
        private ReadOnlyUrl _searchCandidatesUrl;
        private ReadOnlyUrl _flagListUrl;

        [TestInitialize]
        public void LoginTestsInitialize()
        {
            ApplicationContext.Instance.Reset();
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
            _contactUsUrl = new ReadOnlyApplicationUrl("~/faqs/accessing-and-editing-your-profile/what-does-it-mean-that-my-account-is-disabled/7B7FAD42-E027-4586-843B-4D422F39E7EA");
            _searchJobsUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _searchCandidatesUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            _flagListUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
        }

        protected abstract void GetLoginUrl();
        protected abstract void AssertLoginUrl();
        protected abstract void AssertSecureLoginUrl();

        [TestMethod]
        public void TestLoginFail()
        {
            const string loginId = "invalidUser";
            const string password = "invalidPassword";

            // Log in.

            GetLoginUrl();
            SubmitLogIn(loginId, password, false);

            // Assert that the login failed.

            AssertSecureLoginUrl();
            AssertNotLoggedIn();
        }

        [TestMethod]
        public void TestMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(member.FullName);

            // Log in.

            SubmitLogIn(member);

            // Assert that the user was directed to the networker home page after login.

            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(employer.FullName);

            // Log in.

            SubmitLogIn(employer);

            // Assert that the user was directed to the employer home page after login.

            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);
        }

        [TestMethod]
        public void TestAdministrator()
        {
            // Create the administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(administrator.FullName);

            // Log in.

            SubmitLogIn(administrator);

            // Assert that the user was directed to the administrator home page after login.

            AssertUrl(LoggedInAdministratorHomeUrl);
            AssertPageContains(administrator.FullName);
        }

        [TestMethod]
        public void TestCustodian()
        {
            // Create the custodian.

            var community = _communitiesCommand.CreateTestCommunity(0);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(custodian.FullName);

            // Log in.

            SubmitLogIn(custodian);

            // Assert that the user was directed to the administrator home page after login.

            AssertUrl(LoggedInCustodianHomeUrl);
            AssertPageContains(custodian.FullName);
        }

        [TestMethod]
        public void TestDisabledMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(member.FullName);

            // Log in.

            SubmitLogIn(member);
            AssertPath(_contactUsUrl);

            // Check not logged in.

            GetLoginUrl();
            AssertLoginUrl();
            AssertPageDoesNotContain(member.FullName);
        }

        [TestMethod]
        public void TestDeactivatedMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(member.FullName);

            // Log in.

            SubmitLogIn(member);

            // Assert that the user was redirected to the account not activated form.

            AssertUrl(_notActivatedUrl);
        }

        [TestMethod]
        public void TestDisabledAndDeactivatedMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(member.FullName);

            // Log in.

            SubmitLogIn(member);
            AssertPath(_contactUsUrl);

            // Check not logged in.

            GetLoginUrl();
            AssertLoginUrl();
            AssertPageDoesNotContain(member.FullName);
        }

        [TestMethod]
        public void TestDisabledEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(employer.FullName);

            // Log in.

            SubmitLogIn(employer);
            AssertPath(_contactUsUrl);

            // Check not logged in.

            GetLoginUrl();
            AssertLoginUrl();
            AssertPageDoesNotContain(employer.FullName);
        }

        [TestMethod]
        public void TestPasswordReset()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Reset their password.

            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            _loginCredentialsCommand.ResetPassword(member.Id, credentials);

            // Assert that they cannot log in in with their old password.

            GetLoginUrl();
            SubmitLogIn(member);
            AssertPageDoesNotContain(member.FullName);
            AssertSecureLoginUrl();

            // Assert that they can log in with the new password.  In the test environment this is always the same password.

            var password = _passwordsCommand.GenerateRandomPassword();

            GetLoginUrl();
            SubmitLogIn(member, password);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);
        }

        [TestMethod]
        public void TestRememberMeMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(member.FullName);

            // Log in and get the cookies.

            SubmitLogIn(member, true);
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");

            // Assert that the user was directed to the member home page after login.

            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);

            // Logout.

            LogOut();
            AssertUrl(HomeUrl);
            AssertPageDoesNotContain(member.FullName);

            // Set the cookies and try to access the login page, should be automatically logged in.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            GetLoginUrl();
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);

            // Try to access a page that does not have a login form.

            LogOut();
            AssertPageDoesNotContain(member.FullName);

            Get(LoggedInMemberHomeUrl);
            var loginUrl = LogInUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = LoggedInMemberHomeUrl.Path;
            AssertUrl(loginUrl);

            // Try to access it again when cookies are set.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);

            // Access a page which does not require a login and doesn't have a login form.

            LogOut();
            Get(_searchJobsUrl);
            AssertUrl(_searchJobsUrl);
            AssertPageDoesNotContain(member.FullName);

            // Try to access it again when cookies are set, should be automatically logged in.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(_searchJobsUrl);
            AssertUrl(_searchJobsUrl);
            AssertPageContains(member.FullName);
        }

        [TestMethod]
        public void TestRememberMeEmployer()
        {
            // Create the employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Assert that the user is not already logged in.

            GetLoginUrl();
            AssertPageDoesNotContain(employer.FullName);

            // Log in.

            SubmitLogIn(employer, true);
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");

            // Assert that the user was directed to the home page after login.

            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);

            // Set the cookies and try to access the login page, should be automatically logged in.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            GetLoginUrl();
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);

            // Try to access a page that does not have a login form.

            LogOut();
            AssertPageDoesNotContain(employer.FullName);

            Get(_flagListUrl);
            AssertUrl(GetEmployerLoginUrl(_flagListUrl));

            // Try to access it again when cookies are set.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(_flagListUrl);
            AssertUrl(_flagListUrl);
            AssertPageContains(employer.FullName);

            // Access a page which does not require a login and doesn't have a login form.

            LogOut();
            Get(_searchCandidatesUrl);
            AssertUrl(_searchCandidatesUrl);
            AssertPageDoesNotContain(employer.FullName);

            // Try to access it again when cookies are set, should be automatically logged in.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(_searchCandidatesUrl);
            AssertUrl(_searchCandidatesUrl);
            AssertPageContains(employer.FullName);
        }

        [TestMethod]
        public void TestInvalidAuthorisationCookie()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Log in.

            GetLoginUrl();
            SubmitLogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);

            // Set the authorisation cookie and try to access a secure page.

            SetAuthCookie("12345");
            Get(LoggedInMemberHomeUrl);
            AssertUrlWithoutQuery(LogInUrl);
        }

        [TestMethod]
        public void TestLoginWithExtraSpaces()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var memberLoginIdWithSpaces = " \t " + member.GetLoginId() + " \t ";

            // Log in from the front page

            GetLoginUrl();
            SubmitLogIn(memberLoginIdWithSpaces, member.GetPassword());
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);
            LogOut();
        }

        private void SetAuthCookie(string value)
        {
            foreach (Cookie cookie in Browser.Cookies.GetCookies(new Uri(HomeUrl.ToString())))
            {
                if (cookie.Name == "LinkMeAuth")
                {
                    cookie.Value = value;
                    break;
                }
            }
        }

        private string GetCookieValue(string name)
        {
            var cookies = Browser.Cookies.GetCookies(Browser.CurrentUrl);
            var cookie = cookies[name];
            return cookie == null ? null : cookie.Value;
        }

        private void SetRememberMeCookies(string userCookieValue, string passwordCookieValue)
        {
            Browser.Cookies = new CookieContainer();
            var uri = new Uri(new ReadOnlyApplicationUrl("/").AbsoluteUri);
            Browser.Cookies.Add(uri, new Cookie("user_cookie", userCookieValue));
            Browser.Cookies.Add(uri, new Cookie("passwordCookie", passwordCookieValue));
        }
    }
}