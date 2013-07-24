using System;
using System.Net;
using System.Web.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts.Authentication
{
    [TestClass]
    public class AuthenticationCookiesTests
        : CookiesTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly ILoginCredentialsCommand _loginCredentialsCommand = Resolve<ILoginCredentialsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string FormsCookieName = "LinkMeAuth";
        private const string Host = "localhost.test.linkme.com.au";
        private const string Domain = ".linkme.com.au";
        private const char UserDataSeparator = '\n';

        private ReadOnlyUrl _mustChangePasswordUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _mustChangePasswordUrl = new ReadOnlyApplicationUrl(true, "~/accounts/mustchangepassword");
        }

        [TestMethod]
        public void TestCookie()
        {
            var member = CreateMember();

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            var cookie = GetCookie(FormsCookieName);
            var cookieValue = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(LoggedInMemberHomeUrl));
            cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(Domain, cookie.Domain);

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the domain.

            TestAddCookie(Domain, cookieValue, LoggedInMemberHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the host.

            TestAddCookie(Host, cookieValue, LoggedInMemberHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the domain and host.

            TestAddCookies(Host, cookieValue, Domain, cookieValue, LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestBothCookies()
        {
            var member = CreateMember();

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            var cookie = GetCookie(FormsCookieName);
            var cookieValue1 = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            LogOut();
            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            cookie = GetCookie(FormsCookieName);
            var cookieValue2 = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            // Delete the cookies.

            TestDeleteCookie();

            // Add it back in with the domain.

            TestAddCookie(Domain, cookieValue1, LoggedInMemberHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the host.

            TestAddCookie(Host, cookieValue2, LoggedInMemberHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the domain and host.

            TestAddCookies(Host, cookieValue2, Domain, cookieValue1, LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestCookieWithPasswordReset()
        {
            var member = CreateMember();

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            var cookie = GetCookie(FormsCookieName);
            var cookieValue = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(LoggedInMemberHomeUrl));
            cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(Domain, cookie.Domain);

            // Delete the cookie.

            TestDeleteCookie();

            // Reset the member's password.

            _loginCredentialsCommand.ResetPassword(member.Id, _loginCredentialsQuery.GetCredentials(member.Id));

            // Add it back in with the domain, should still be logged in.

            TestAddCookie(Domain, cookieValue, GetTestUrl(_mustChangePasswordUrl));

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the host.

            TestAddCookie(Host, cookieValue, GetTestUrl(_mustChangePasswordUrl));

            // Delete the cookie.

            TestDeleteCookie();

            // Add it back in with the domain and host.

            TestAddCookies(Host, cookieValue, Domain, cookieValue, GetTestUrl(_mustChangePasswordUrl));
        }

        [TestMethod]
        public void TestCookieWithExpiredTicket()
        {
            var member = CreateMember();

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            var cookie = GetCookie(FormsCookieName);
            var cookieValue = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            // Delete the cookie.

            TestDeleteCookie();

            // Create an expired ticket for the domain.

            TestExpiredCookie(member, Domain);

            // Create an expired ticket for the host.

            TestExpiredCookie(member, Host);

            // Add the original back in.

            TestAddCookie(Domain, cookieValue, LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestLoginWithExpiredTicket()
        {
            var member = CreateMember();

            // Create an expired ticket for the domain.

            var expiredCookieValue = CreateExpiredCookieValue(member);

            // Add the expired cookie to the host.

            Browser.Cookies = new CookieContainer();
            Browser.Cookies.Add(new Cookie(FormsCookieName, expiredCookieValue, "/", Domain));

            // Not logged in.

            Get(GetTestUrl(HomeUrl));
            AssertUrl(GetTestUrl(HomeUrl));
            var cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(Domain, cookie.Domain);

            // Log in.

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            AssertUrl(GetTestUrl(LoggedInMemberHomeUrl));
            cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(Domain, cookie.Domain);
        }

        [TestMethod]
        public void TestMemberAndEmployerCookie()
        {
            var member = CreateMember();
            var employer = CreateEmployer();

            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(member);
            var cookie = GetCookie(FormsCookieName);
            var memberCookieValue = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            LogOut();
            Get(GetTestUrl(HomeUrl));
            SubmitLogIn(employer);
            cookie = GetCookie(FormsCookieName);
            var employerCookieValue = cookie.Value;
            Assert.AreEqual(Domain, cookie.Domain);

            // Delete the cookie.

            TestDeleteCookie();

            // Add back the member cookie.

            TestAddCookie(Domain, memberCookieValue, LoggedInMemberHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add back the employer cookie.

            TestAddCookie(Domain, employerCookieValue, LoggedInEmployerHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add both cookies, employer using host, member using domain.

            TestAddCookies(Host, employerCookieValue, Domain, memberCookieValue, LoggedInEmployerHomeUrl);

            // Delete the cookie.

            TestDeleteCookie();

            // Add both cookies, employer using host, member using domain.

            TestAddCookies(Host, memberCookieValue, Domain, employerCookieValue, LoggedInMemberHomeUrl);
        }

        private void TestDeleteCookie()
        {
            Browser.Cookies = new CookieContainer();
            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(GetLoginUrl(LogInUrl, LoggedInMemberHomeUrl)));
            Assert.IsNull(GetCookie(FormsCookieName));
        }

        private void TestAddCookie(string domain, string cookieValue, ReadOnlyUrl expectedUrl)
        {
            Browser.Cookies = new CookieContainer();
            Browser.Cookies.Add(new Cookie(FormsCookieName, cookieValue, "/", domain));
            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(expectedUrl));
            var cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(domain, cookie.Domain);
        }

        private void TestAddCookies(string domain1, string cookieValue1, string domain2, string cookieValue2, ReadOnlyUrl expectedUrl)
        {
            Browser.Cookies = new CookieContainer();
            Browser.Cookies.Add(new Cookie(FormsCookieName, cookieValue1, "/", domain1));
            Browser.Cookies.Add(new Cookie(FormsCookieName, cookieValue2, "/", domain2));
            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(expectedUrl));
            var cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(domain1, cookie.Domain);
        }

        private void TestExpiredCookie(IRegisteredUser member, string domain)
        {
            var cookieValue = CreateExpiredCookieValue(member);

            Browser.Cookies = new CookieContainer();
            Browser.Cookies.Add(new Cookie(FormsCookieName, cookieValue, "/", domain));

            Get(GetTestUrl(LoggedInMemberHomeUrl));
            AssertUrl(GetTestUrl(GetLoginUrl(LogInUrl, LoggedInMemberHomeUrl)));

            // The cookie is not null but it doesn't log the user in because it is expired.

            var cookie = GetCookie(FormsCookieName);
            Assert.AreEqual(domain, cookie.Domain);
        }

        private static string CreateExpiredCookieValue(IRegisteredUser member)
        {
            var issueDate = DateTime.Now.AddYears(-2);
            var expirationDate = DateTime.Now.AddYears(-1);

            var ticket = new FormsAuthenticationTicket(
                1,
                member.Id.ToString("n"),
                issueDate,
                expirationDate,
                false,
                CreateAuthenticationUserData(member, false),
                FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

        private static ReadOnlyUrl GetLoginUrl(ReadOnlyUrl url, ReadOnlyUrl returnUrl)
        {
            var loginUrl = url.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = returnUrl.Path;
            return loginUrl;
        }

        private static ReadOnlyUrl GetTestUrl(ReadOnlyUrl url)
        {
            var hostUrl = url.AsNonReadOnly();
            hostUrl.Host = Host;
            return hostUrl;
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
        }

        private static string CreateAuthenticationUserData(IRegisteredUser user, bool needsReset)
        {
            return ((int)user.UserType).ToString()
                + UserDataSeparator + user.FullName
                + UserDataSeparator + needsReset
                + UserDataSeparator + user.IsActivated;
        }
    }
}
