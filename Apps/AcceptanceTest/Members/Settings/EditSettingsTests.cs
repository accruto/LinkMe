using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.UI.Registered.Networkers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class EditSettingsTests
        : SettingsTests
    {
        private const string FirstName = "Ned";
        private const string LastName = "Flanders";
        private const string EmailAddress = "nflanders@test.linkme.net.au";
        private const string SecondaryEmailAddress = "mflanders@test.linkme.net.au";

        [TestMethod]
        public void TestDefaults()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            Assert.AreEqual(member.EmailAddresses[0].Address, _emailAddressTextBox.Text);
            Assert.AreEqual("", _secondaryEmailAddressTextBox.Text);
            Assert.AreEqual(member.FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(member.LastName, _lastNameTextBox.Text);
        }

        [TestMethod]
        public void TestDefaultsWithSecondaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);

            LogIn(member);
            Get(_settingsUrl);

            Assert.AreEqual(member.EmailAddresses[0].Address, _emailAddressTextBox.Text);
            Assert.AreEqual(member.EmailAddresses[1].Address, _secondaryEmailAddressTextBox.Text);
            Assert.AreEqual(member.FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(member.LastName, _lastNameTextBox.Text);
        }

        [TestMethod]
        public void TestErrors()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            // Required fields.

            _emailAddressTextBox.Text = string.Empty;
            _firstNameTextBox.Text = string.Empty;
            _lastNameTextBox.Text = string.Empty;
            _saveButton.Click();

            AssertUrl(_settingsUrl);
            AssertErrorMessages(
                "The first name is required.",
                "The last name is required.",
                "The email address is required.");

            // Invalid.

            _emailAddressTextBox.Text = "thisIsNotAnEmail";
            _secondaryEmailAddressTextBox.Text = "thisIsNotEither";
            _firstNameTextBox.Text = "@12";
            _lastNameTextBox.Text = "34#";
            _saveButton.Click();

            AssertUrl(_settingsUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The email address must be valid and have less than 320 characters.",
                "The secondary email address must be valid and have less than 320 characters.");
        }

        [TestMethod]
        public void TestDuplicateUser()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            // Create another user.

            var otherMember = _memberAccountsCommand.CreateTestMember(2);

            _emailAddressTextBox.Text = otherMember.EmailAddresses[0].Address;
            _saveButton.Click();

            AssertUrl(_settingsUrl);
            AssertErrorMessages("The username is already being used.");
        }

        [TestMethod]
        public void TestUpdate()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            var oldName = member.FullName;
            AssertPageContains("Logged in as <div class=\"user-name\">" + oldName);

            // Change.

            _emailAddressTextBox.Text = EmailAddress;
            _secondaryEmailAddressTextBox.Text = SecondaryEmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _saveButton.Click();

            // Check.

            AssertUrlWithoutQuery(LoggedInMemberHomeUrl);

            var updatedMember = _membersQuery.GetMember(member.Id);
            Assert.AreEqual(FirstName, updatedMember.FirstName);
            Assert.AreEqual(LastName, updatedMember.LastName);
            Assert.AreEqual(EmailAddress, updatedMember.GetLoginId());
            Assert.AreEqual(EmailAddress, updatedMember.EmailAddresses[0].Address);
            Assert.AreEqual(SecondaryEmailAddress, updatedMember.EmailAddresses[1].Address);
            Assert.AreEqual(true, updatedMember.IsActivated);

            AssertPageDoesNotContain("Logged in as <div class=\"user-name\">" + oldName);
            AssertPageContains("Logged in as <div class=\"user-name\">" + updatedMember.FullName);
        }

        [TestMethod]
        public void TestNameFieldsAreTrimmed()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            _firstNameTextBox.Text = " " + FirstName + " ";
            _lastNameTextBox.Text = " " + LastName + " ";
            _saveButton.Click();

            var updatedMember = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(updatedMember.FirstName == FirstName);
            Assert.IsTrue(updatedMember.LastName == LastName);

            AssertPageContains("Logged in as <div class=\"user-name\">" + updatedMember.FullName);
        }

        [TestMethod]
        public void TestEmailAddressesAreTrimmed()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            _emailAddressTextBox.Text = " " + EmailAddress + " ";
            _secondaryEmailAddressTextBox.Text = " " + SecondaryEmailAddress + " ";
            _saveButton.Click();

            var updatedMember = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(EmailAddress == updatedMember.GetLoginId());
            Assert.IsTrue(EmailAddress == updatedMember.EmailAddresses[0].Address);
            Assert.IsTrue(SecondaryEmailAddress == updatedMember.EmailAddresses[1].Address);

            AssertPageDoesNotContain("The email address you entered is not valid.");
            AssertPageContains("Logged in as <div class=\"user-name\">" + updatedMember.FullName);
        }

        [TestMethod]
        public void TestVisibilityLink()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(_settingsUrl);

            // Follow the link.

            var url = new ReadOnlyApplicationUrl(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='visibility-link']/a").Attributes["href"].Value);
            Get(url);

            // Should be directed to the visibility settings page.

            AssertPage<VisibilitySettingsBasic>();
        }
    }
}