using System;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class EmailTests
        : SettingsTests
    {
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private const string NewEmailAddress = "bgumble@test.linkme.net.au";
        private const string SecondaryEmailAddress = "marge@test.linkme.net.au";
        private const string NewSecondaryEmailAddress = "moe@test.linkme.net.au";

        private ReadOnlyUrl _profileUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestChangePrimaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Should be activated and email verified.

            AssertEmailAddress(member.Id, member.EmailAddresses[0].Address, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[] { new EmailAddress { Address = NewEmailAddress } };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = NewEmailAddress;
            _secondaryEmailAddressTextBox.Text = null;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, true);
        }

        [TestMethod]
        public void TestAddSecondaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Should be activated and email verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = primaryEmailAddress;
            _secondaryEmailAddressTextBox.Text = NewSecondaryEmailAddress;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangePrimaryAddSecondaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Should be activated and email verified.

            var emailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, emailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = NewEmailAddress;
            _secondaryEmailAddressTextBox.Text = NewSecondaryEmailAddress;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, true);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeSecondaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = primaryEmailAddress;
            _secondaryEmailAddressTextBox.Text = NewSecondaryEmailAddress;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeBothEmailAddresses()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = NewEmailAddress },
                new EmailAddress { Address = NewSecondaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = NewEmailAddress;
            _secondaryEmailAddressTextBox.Text = NewSecondaryEmailAddress;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, true);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestRemoveSecondaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = primaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = primaryEmailAddress;
            _secondaryEmailAddressTextBox.Text = null;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, true, true);

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestMakeSecondaryPrimaryEmailAddress()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
            _memberAccountsCommand.UpdateMember(member);
            _emailVerificationsCommand.VerifyEmailAddress(member.Id, SecondaryEmailAddress);

            // Should be activated and emails verified.

            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, true, secondaryEmailAddress, true, true);

            LogIn(member);

            // Change.

            member.EmailAddresses = new[]
            {
                new EmailAddress { Address = secondaryEmailAddress },
            };
            Get(_settingsUrl);
            _emailAddressTextBox.Text = secondaryEmailAddress;
            _secondaryEmailAddressTextBox.Text = null;
            _saveButton.Click();

            // Check.

            AssertEmailAddress(member.Id, secondaryEmailAddress, true, true);

            _emailServer.AssertNoEmailSent();
        }

        private void AssertEmailAddress(Guid memberId, string emailAddress, bool isVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(memberId);

            Assert.AreEqual(1, member.EmailAddresses.Count);
            Assert.AreEqual(emailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(isVerified, member.EmailAddresses[0].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }

        private void AssertEmailAddress(Guid memberId, string primaryEmailAddress, bool primaryIsVerified, string secondaryEmailAddress, bool secondaryIsVerified, bool isActivated)
        {
            var member = _membersQuery.GetMember(memberId);

            Assert.AreEqual(2, member.EmailAddresses.Count);
            Assert.AreEqual(primaryEmailAddress, member.EmailAddresses[0].Address);
            Assert.AreEqual(primaryIsVerified, member.EmailAddresses[0].IsVerified);
            Assert.AreEqual(secondaryEmailAddress, member.EmailAddresses[1].Address);
            Assert.AreEqual(secondaryIsVerified, member.EmailAddresses[1].IsVerified);

            Assert.AreEqual(isActivated, member.IsActivated);
        }

        private static ReadOnlyUrl GetEmailUrl(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            return xmlNode != null ? new ReadOnlyApplicationUrl(xmlNode.Attributes["href"].InnerText) : null;
        }
    }
}