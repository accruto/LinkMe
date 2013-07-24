using System;
using System.Collections.Specialized;
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
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Configuration;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts.Api
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
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IPasswordsCommand _passwordsCommand = Resolve<IPasswordsCommand>();

        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _mustChangePasswordUrl;
        private ReadOnlyUrl _visibilityUrl;
        private ReadOnlyUrl _notesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            ApplicationContext.Instance.Reset();
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
            _visibilityUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/visibility");
            _notesUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/notes/api");
        }

        [TestMethod]
        public void TestLoginFail()
        {
            const string loginId = "invalidUser";
            const string password = "invalidPassword";

            // Log in.

            AssertNotLoggedIn();
            AssertJsonError(ApiLogIn(loginId, password, false), null, "101", "Login failed. Please try again.");

            // Assert that the login failed.

            AssertNotLoggedIn();
        }

        [TestMethod]
        public void TestMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(member));

            // Assert that the user is directed to the networker home page after login.

            Get(HomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(employer));

            // Assert that the user was directed to the employer home page after login.

            Get(HomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);
        }

        [TestMethod]
        public void TestAdministrator()
        {
            // Create the administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(administrator));

            // Assert that the user was directed to the administrator home page after login.

            Get(HomeUrl);
            AssertUrl(LoggedInAdministratorHomeUrl);
            AssertPageContains(administrator.FullName);
        }

        [TestMethod]
        public void TestCustodian()
        {
            // Create the custodian.

            var community = _communitiesCommand.CreateTestCommunity(0);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(custodian));

            // Assert that the user was directed to the administrator home page after login.

            Get(HomeUrl);
            AssertUrl(LoggedInCustodianHomeUrl);
            AssertPageContains(custodian.FullName);
        }

        [TestMethod]
        public void TestDisabledMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());

            // Log in.

            AssertNotLoggedIn();
            AssertJsonError(ApiLogIn(member), null, "102", "The user is disabled.");

            // Check not logged in.

            AssertNotLoggedIn();
        }

        [TestMethod]
        public void TestDeactivatedMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Login.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(member));

            // Assert that the user was redirected to the account not activated form.

            Get(HomeUrl);
            AssertPath(_notActivatedUrl);
        }

        [TestMethod]
        public void TestDisabledAndDeactivatedMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            _userAccountsCommand.DisableUserAccount(member, Guid.NewGuid());
            _userAccountsCommand.DeactivateUserAccount(member, Guid.NewGuid());

            // Log in.

            AssertNotLoggedIn();
            AssertJsonError(ApiLogIn(member), null, "102", "The user is disabled.");

            // Check not logged in.

            AssertNotLoggedIn();
        }

        [TestMethod]
        public void TestDisabledEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());

            // Log in.

            AssertNotLoggedIn();
            AssertJsonError(ApiLogIn(employer), null, "102", "The user is disabled.");

            // Check not logged in.

            AssertNotLoggedIn();
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

            AssertNotLoggedIn();
            AssertJsonError(ApiLogIn(member), null, "101", "Login failed. Please try again.");
            AssertNotLoggedIn();

            // Assert that they can log in with the new password.  In the test environment this is always the same password.

            var password = _passwordsCommand.GenerateRandomPassword();
            AssertJsonSuccess(ApiLogIn(member.GetLoginId(), password, false));

            Get(HomeUrl);
            AssertUrlWithoutQuery(_mustChangePasswordUrl);
            AssertPageContains(member.FullName);
        }

        [TestMethod]
        public void TestRememberMeMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Assert that the user is not already logged in.

            AssertNotLoggedIn();

            // Log in.

            AssertJsonSuccess(ApiLogIn(member.GetLoginId(), member.GetPassword(), true));
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");

            // Assert that the user is logged in.

            Get(HomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);

            // Logout.

            LogOut();
            AssertUrl(HomeUrl);
            AssertNotLoggedIn();

            // Set the cookies and try to access the home page.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(HomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertPageContains(member.FullName);

            // Try to call the login API.

            LogOut();
            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            AssertJsonError(ApiLogIn(member.GetLoginId(), member.GetPassword(), false), null, "104", "The user is already logged in.");

            // Try to call an API requiring authorisation.

            LogOut();
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_visibilityUrl, JsonContentType, Serialize(new VisibilityModel()))), null, "100", "The user is not logged in.");

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(_visibilityUrl, JsonContentType, Serialize(new VisibilityModel()))));
        }

        [TestMethod]
        public void TestRememberMeEmployer()
        {
            // Create the employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(employer.GetLoginId(), employer.GetPassword(), true));
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");

            // Assert that the user was directed to the networker home page after login.

            Get(HomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);

            // Set the cookies and try to access the home page.

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            Get(HomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertPageContains(employer.FullName);

            // Try to call the login API.

            LogOut();
            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            AssertJsonError(ApiLogIn(employer.GetLoginId(), employer.GetPassword(), false), null, "104", "The user is already logged in.");

            // Try to call an API requiring authorisation.

            LogOut();
            AssertJsonError(Deserialize<JsonResponseModel>(Post(_notesUrl, new NameValueCollection { { "candidateId", Guid.NewGuid().ToString() } })), null, "100", "The user is not logged in.");

            SetRememberMeCookies(userCookieValue, passwordCookieValue);
            AssertJsonSuccess(Deserialize<JsonResponseModel>(Post(_notesUrl, new NameValueCollection { { "candidateId", Guid.NewGuid().ToString() } })));
        }

        [TestMethod]
        public void TestInvalidAuthorisationCookie()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Log in.

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(member));

            Get(HomeUrl);
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

            AssertNotLoggedIn();
            AssertJsonSuccess(ApiLogIn(memberLoginIdWithSpaces, member.GetPassword(), false));

            Get(HomeUrl);
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
