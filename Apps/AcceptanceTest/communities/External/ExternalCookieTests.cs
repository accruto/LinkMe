using System;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.External
{
    [TestClass]
    public class ExternalCookieTests
        : ExternalCredentialsTests
    {
        private const string ExternalId = "abcdefgh";
        private const string EmailAddress = "member@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string NewEmailAddress = "member2@test.linkme.net.au";
        private const string NewLastName = "Gumble";
        private const string NewFirstName = "Barney";

        [TestMethod]
        public void TestCreateAccount()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            Get(url);
            AssertUrl(new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));

            // Set the cookie and try again.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);

            // Check.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, member);
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestLogin()
        {
            var vertical = CreateVertical();

            // Create the member first.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Try with no cookie, should be redirected.

            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            Get(url);
            AssertUrl(new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri)));

            // Set the cookie and try again, should be logged in.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            // Check.

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestLoginReturnUrl()
        {
            var vertical = CreateVertical();

            // Create the member first.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Try with no cookie to a general page, should be redirected with a return url.

            var url = vertical.GetVerticalHostUrl("").AsNonReadOnly();
            url = new ApplicationUrl(url, "members/friends/Invitations.aspx") { Scheme = Uri.UriSchemeHttps };
            Get(url);

            var externalLoginUrl = new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri));
            AssertUrl(externalLoginUrl);

            // Set the cookie and try again, should be logged in.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            AssertPage<Invitations>();
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            // Check.

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestLogout()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            // Create the member.

            CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Set the cookie to login.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            // Logout, should be redirected to the external login page.

            var externalLoginUrl = new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri));

            Get(LogOutUrl);
            AssertUrl(externalLoginUrl);

            Get(url);
            AssertUrl(externalLoginUrl);
        }

        [TestMethod]
        public void TestChangeFirstName()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            // Create the member.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Login with old details.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));

            // Set the cookie with new name and try again.

            CreateExternalCookies(url, ExternalId, EmailAddress, NewFirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + NewFirstName + " " + LastName + "</div>");

            AssertMember(EmailAddress, NewFirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestChangeLastName()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            // Create the member.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Login with old details.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));

            // Set the cookie with new name and try again.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + NewLastName, vertical.ExternalCookieDomain);
            Get(url);
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + NewLastName + "</div>");

            AssertMember(EmailAddress, FirstName, NewLastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestChangeEmailAddress()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            // Create the member.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            // Login with old details.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));

            // Set the cookie with new email address and try again.

            CreateExternalCookies(url, ExternalId, NewEmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            Assert.IsNull(_membersQuery.GetMember(EmailAddress));
            AssertMember(NewEmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(NewEmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }

        [TestMethod]
        public void TestMissingAffiliation()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;

            // Create the member.

            var member = CreateMember(EmailAddress, FirstName, LastName, vertical.Id, false, ExternalId);

            // Set the cookie with new name and try again.

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            AssertPageContains("Logged in as <div class=\"user-name\">" + FirstName + " " + LastName + "</div>");

            // Check.

            AssertMember(EmailAddress, FirstName, LastName, vertical.Id, _membersQuery.GetMember(EmailAddress));
            Assert.IsNull(_loginCredentialsQuery.GetCredentials(member.Id));
            AssertCredentials(vertical.Id, ExternalId, _externalCredentialsQuery.GetCredentials(member.Id, vertical.Id));
        }
    }
}
