using System;
using System.Collections.Generic;
using System.Net;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Members
{
    [TestClass]
    public class EditExternalMemberTests
        : MembersTests
    {
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        private const string ExternalId = "abcdefgh";
        private const string ExternalCookieName = "LinkMeAuthExt";
        private const string EmailAddress = "member@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Create the member.

            var vertical = CreateVertical();
            var member = CreateMember(true, true, vertical.Id);

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Assert.

            AssertMember(member);
        }

        [TestMethod]
        public void TestDisable()
        {
            var host = HomeUrl.Host;

            // Create the member.

            var vertical = CreateVertical();
            var member = CreateMember(true, true, vertical.Id);

            // Log in as the member.

            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;
            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
            LogOut();

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            homeUrl = HomeUrl.AsNonReadOnly();
            homeUrl.Host = host;
            Get(homeUrl);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Disable.

            _disableButton.Click();

            // Check the member details.

            member.IsEnabled = false;
            AssertMember(member);

            // Check the member login.

            LogOut();
            url = vertical.GetVerticalHostUrl("");
            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);

            var externalLoginUrl = new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri));
            AssertUrl(externalLoginUrl);
        }

        [TestMethod]
        public void TestEnable()
        {
            var host = HomeUrl.Host;

            // Create the member.

            var vertical = CreateVertical();
            var member = CreateMember(false, true, vertical.Id);

            // Log in as the member.

            var url = vertical.GetVerticalHostUrl("");
            var verticalHost = url.Host;
            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);

            var externalLoginUrl = new ReadOnlyUrl(vertical.ExternalLoginUrl, new ReadOnlyQueryString("returnUrl", url.AbsoluteUri));
            AssertUrl(externalLoginUrl);

            // Login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var homeUrl = HomeUrl.AsNonReadOnly();
            homeUrl.Host = host;
            Get(homeUrl);
            LogIn(administrator);
            Get(GetMemberUrl(member));

            // Enable the member.

            _enableButton.Click();

            // Check the member details.

            member.IsEnabled = true;
            AssertMember(member);

            // Check the user login.

            LogOut();
            url = vertical.GetVerticalHostUrl("");
            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);
            homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);
        }

        private void AssertMember(IMember member)
        {
            AssertMember(member, true);
        }

        private Vertical CreateVertical()
        {
            var community = TestCommunity.LiveInAustralia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            return _verticalsCommand.GetVertical(community);
        }

        private Member CreateMember(bool enabled, bool activated, Guid verticalId)
        {
            var member = new Member
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } },
                FirstName = FirstName,
                LastName = LastName,
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Sydney 2000 NSW") },
                IsActivated = true,
            };

            var externalCredentals = new ExternalCredentials { ProviderId = verticalId, ExternalId = ExternalId };
            _memberAccountsCommand.CreateMember(member, externalCredentals, verticalId);

            member.IsEnabled = enabled;
            member.IsActivated = activated;
            _memberAccountsCommand.UpdateMember(member);

            return member;
        }

        protected void CreateExternalCookies(ReadOnlyUrl url, string externalId, string emailAddress, string name, string cookieDomain)
        {
            var cookieValue = new ExternalCookieData(externalId, emailAddress, name).CookieValue;
            var cookie = new Cookie(ExternalCookieName, cookieValue) { Domain = cookieDomain };

            // Cookie Uri should be with a path "/".

            var cookieUrl = url.AsNonReadOnly();
            cookieUrl.Path = "/";
            var uri = new Uri(cookieUrl.AbsoluteUri);

            Browser.Cookies.Add(uri, cookie);
        }
    }
}