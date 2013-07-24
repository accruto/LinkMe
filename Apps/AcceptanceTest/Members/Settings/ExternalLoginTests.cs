using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class ExternalLoginTests
        : SettingsTests
    {
        private const string ExternalCookieName = "LinkMeAuthExt";
        private const string ExternalId = "abcdefgh";
        private const string EmailAddress = "member@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCannotAccessUrls()
        {
            var community = TestCommunity.LiveInAustralia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var url = _verticalsCommand.GetCommunityHostUrl(community, "");
            var verticalHost = url.Host;

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);

            // Try to access some pages which should throw you back to the settings page.

            var settingsUrl = _settingsUrl.AsNonReadOnly();
            settingsUrl.Host = url.Host;

            var changePasswordUrl = _changePasswordUrl.AsNonReadOnly();
            changePasswordUrl.Host = url.Host;

            Get(changePasswordUrl);
            AssertUrl(settingsUrl);

            var deactivateUrl = _deactivateUrl.AsNonReadOnly();
            deactivateUrl.Host = url.Host;

            Get(deactivateUrl);
            AssertUrl(settingsUrl);
        }

        [TestMethod]
        public void TestCannotChangeSettings()
        {
            var community = TestCommunity.LiveInAustralia.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var url = _verticalsCommand.GetCommunityHostUrl(community, "");
            var verticalHost = url.Host;

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName);
            Get(url);
            var homeUrl = LoggedInMemberHomeUrl.AsNonReadOnly();
            homeUrl.Host = verticalHost;
            AssertUrl(homeUrl);

            var settingsUrl = _settingsUrl.AsNonReadOnly();
            settingsUrl.Host = url.Host;
            Get(settingsUrl);

            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.IsFalse(_secondaryEmailAddressTextBox.IsVisible);

            Assert.AreEqual(FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(LastName, _lastNameTextBox.Text);
            Assert.AreEqual(EmailAddress, _emailAddressTextBox.Text);

            Assert.IsTrue(_firstNameTextBox.IsReadOnly);
            Assert.IsTrue(_lastNameTextBox.IsReadOnly);
            Assert.IsTrue(_emailAddressTextBox.IsReadOnly);
        }

        private void CreateExternalCookies(ReadOnlyUrl url, string externalId, string emailAddress, string name)
        {
            Browser.Cookies = new CookieContainer();
            var uri = new Uri(url.AbsoluteUri);
            Browser.Cookies.Add(uri, new Cookie(ExternalCookieName, new ExternalCookieData(externalId, emailAddress, name).CookieValue));
        }
    }
}
