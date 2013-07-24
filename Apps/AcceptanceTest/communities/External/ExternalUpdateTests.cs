using System;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities.External
{
    [TestClass]
    public class ExternalUpdateTests
        : ExternalCredentialsTests
    {
        private const string ExternalId = "abcdefgh";
        private const string EmailAddress = "member@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        private HtmlTextBoxTester _settingsEmailAddressTextBox;
        private HtmlTextBoxTester _settingsFirstNameTextBox;
        private HtmlTextBoxTester _settingsLastNameTextBox;
        private HtmlButtonTester _settingsSaveButton;
        private HtmlTextBoxTester _profileFirstNameTextBox;
        private HtmlTextBoxTester _profileLastNameTextBox;
        private HtmlTextBoxTester _profileEmailAddressTextBox;
        private HtmlTextBoxTester _profileSecondaryEmailAddressTextBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _settingsEmailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _settingsFirstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _settingsLastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _settingsSaveButton = new HtmlButtonTester(Browser, "save");
            _profileFirstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _profileLastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _profileEmailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _profileSecondaryEmailAddressTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddress");
        }

        [TestMethod]
        public void TestCannotUpdateSettings()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");

            // Login.

            CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);

            url = vertical.GetVerticalHostUrl("").AsNonReadOnly();
            url = new ApplicationUrl(url, "members/settings") { Scheme = Uri.UriSchemeHttps };
            Get(url);

            // Check.

            Assert.IsTrue(_settingsEmailAddressTextBox.IsVisible);
            Assert.IsTrue(_settingsEmailAddressTextBox.IsReadOnly);
            Assert.IsTrue(_settingsFirstNameTextBox.IsVisible);
            Assert.IsTrue(_settingsFirstNameTextBox.IsReadOnly);
            Assert.IsTrue(_settingsLastNameTextBox.IsVisible);
            Assert.IsTrue(_settingsLastNameTextBox.IsReadOnly);
            Assert.IsFalse(_settingsSaveButton.IsVisible);

            AssertPageDoesNotContain("Change my password");
            AssertPageDoesNotContain("Deactivate my account");
        }

        [TestMethod]
        public void TestCannotUpdateProfile()
        {
            var vertical = CreateVertical();
            var url = vertical.GetVerticalHostUrl("");

            // Login.

            CreateMember(EmailAddress, FirstName, LastName, vertical.Id, true, ExternalId);

            CreateExternalCookies(url, ExternalId, EmailAddress, FirstName + " " + LastName, vertical.ExternalCookieDomain);
            Get(url);

            url = vertical.GetVerticalHostUrl("").AsNonReadOnly();
            Get(new ApplicationUrl(url, "members/profile/contactdetails") { Scheme = Uri.UriSchemeHttps });

            // Check.

            Assert.IsTrue(_profileFirstNameTextBox.IsVisible);
            Assert.IsTrue(_profileFirstNameTextBox.IsReadOnly);
            Assert.AreEqual(FirstName, _profileFirstNameTextBox.Text);

            Assert.IsTrue(_profileLastNameTextBox.IsVisible);
            Assert.IsTrue(_profileLastNameTextBox.IsReadOnly);
            Assert.AreEqual(LastName, _profileLastNameTextBox.Text);

            Assert.IsTrue(_profileEmailAddressTextBox.IsVisible);
            Assert.IsTrue(_profileEmailAddressTextBox.IsReadOnly);
            Assert.AreEqual(EmailAddress, _profileEmailAddressTextBox.Text);

            Assert.IsFalse(_profileSecondaryEmailAddressTextBox.IsVisible);
        }
    }
}
