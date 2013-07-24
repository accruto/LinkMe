using System;
using System.Collections.Generic;
using System.Xml;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class ChangeEmailsTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();

        private HtmlButtonTester _sendButton;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _secondaryEmailAddressTextBox;
        private HtmlButtonTester _cancelButton;
        private HtmlLinkTester _changeEmailAddressLink;

        private ReadOnlyUrl _notActivatedUrl;
        private ReadOnlyUrl _activationSentUrl;
        private ReadOnlyUrl _changeEmailUrl;
        private ReadOnlyUrl _profileUrl;

        private const string EmailAddress = "moe@test.linkme.net.au";
        private const string SecondaryEmailAddress = "homer@test.linkme.net.au";
        private const string NewEmailAddress = "barney@test.linkme.net.au";
        private const string NewSecondaryEmailAddress = "waylon@test.linkme.net.au";

        [TestInitialize]
        public void TestInitialize()
        {
            _sendButton = new HtmlButtonTester(Browser, "send");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _secondaryEmailAddressTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddress");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
            _changeEmailAddressLink = new HtmlLinkTester(Browser, "ChangeEmailAddress");

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            _activationSentUrl = new ReadOnlyApplicationUrl(true, "~/accounts/activationsent");
            _changeEmailUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changeemail");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            var member = CreateMember(EmailAddress, null);
            TestPageVisibility(member);
        }

        [TestMethod]
        public void TestPageVisibilityWithSecondaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, SecondaryEmailAddress);
            TestPageVisibility(member);
        }

        [TestMethod]
        public void TestErrors()
        {
            // LogIn.

            var member0 = _memberAccountsCommand.CreateTestMember(0, false);
            var member1 = _memberAccountsCommand.CreateTestMember(1, false);

            LogIn(member0);

            // Change email.

            AssertUrl(_notActivatedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notActivatedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Try to change with an empty email address.

            _emailAddressTextBox.Text = string.Empty;
            _sendButton.Click();

            AssertUrl(changeEmailUrl);
            AssertErrorMessage("The email address is required.");

            // Try to change with the same email address.

            _emailAddressTextBox.Text = member1.EmailAddresses[0].Address;
            _sendButton.Click();
            AssertUrl(changeEmailUrl);
            AssertErrorMessage("The email address is already being used by another user.");
        }

        [TestMethod]
        public void TestChangePrimaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, null);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, false);

            ChangeEmail(member, NewEmailAddress, null);

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, false);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Logout and try to log in again with the old email.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            // Try to login in with the new email address.

            member.EmailAddresses[0].Address = NewEmailAddress;
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to the verification page.

            Get(url);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, true);
        }

        [TestMethod]
        public void TestAddSecondaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, null);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, primaryEmailAddress, NewSecondaryEmailAddress);

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, false, NewSecondaryEmailAddress, false, false);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(primaryEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Logout and try to log in again.

            LogOut();
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangePrimaryAddSecondaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, null);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, NewEmailAddress, NewSecondaryEmailAddress);

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, false);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Logout and try to log in again with the old email.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            // Try to login in with the new email address.

            member.EmailAddresses[0].Address = NewEmailAddress;
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeSecondaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, SecondaryEmailAddress);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, secondaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, primaryEmailAddress, NewSecondaryEmailAddress);

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, false, NewSecondaryEmailAddress, false, false);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(primaryEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Logout and try to log in again with the old email.

            LogOut();
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestChangeBothEmailAddresses()
        {
            var member = CreateMember(EmailAddress, SecondaryEmailAddress);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, secondaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, NewEmailAddress, NewSecondaryEmailAddress);

            // Check.

            AssertEmailAddress(member.Id, NewEmailAddress, false, NewSecondaryEmailAddress, false, false);

            var emails = _emailServer.AssertEmailsSent(2);
            emails[0].AssertAddresses(Return, Return, new EmailRecipient(NewEmailAddress, member.FullName));
            var url0 = GetEmailUrl(emails[0].GetHtmlView().Body);
            emails[1].AssertAddresses(Return, Return, new EmailRecipient(NewSecondaryEmailAddress, member.FullName));
            var url1 = GetEmailUrl(emails[1].GetHtmlView().Body);

            // Logout and try to log in again with the old email.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            // Try to login in with the new email address.

            member.EmailAddresses[0].Address = NewEmailAddress;
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Navigate to the verification page.

            Get(url0);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, false, true);

            Get(url1);
            AssertUrl(GetEmailUrl("VerificationEmail", _profileUrl));

            AssertEmailAddress(member.Id, NewEmailAddress, true, NewSecondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestRemoveSecondaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, SecondaryEmailAddress);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, secondaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, primaryEmailAddress, null);

            // Check.

            AssertEmailAddress(member.Id, primaryEmailAddress, false, false);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(primaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            // Logout and try to log in again.

            LogOut();
            LogIn(member);
            AssertUrl(_notActivatedUrl);

            Get(url);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, primaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestMakeSecondaryPrimaryEmailAddress()
        {
            var member = CreateMember(EmailAddress, SecondaryEmailAddress);
            var primaryEmailAddress = member.EmailAddresses[0].Address;
            var secondaryEmailAddress = member.EmailAddresses[1].Address;
            AssertEmailAddress(member.Id, primaryEmailAddress, false, secondaryEmailAddress, false, false);

            LogIn(member);

            // Change.

            ChangeEmail(member, secondaryEmailAddress, null);

            // Check.

            AssertEmailAddress(member.Id, secondaryEmailAddress, false, false);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(secondaryEmailAddress, member.FullName));
            var url = GetEmailUrl(email.GetHtmlView().Body);

            Get(url);
            AssertUrl(GetEmailUrl("ReactivationEmail", _profileUrl));

            AssertEmailAddress(member.Id, secondaryEmailAddress, true, true);
        }

        [TestMethod]
        public void TestResendVerificationEmail()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            LogIn(member);

            // Change email.

            AssertUrl(_notActivatedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notActivatedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Try to change with an empty email address.

            const string newEmail = "linkme2@test.linkme.net.au";
            _emailAddressTextBox.Text = newEmail;
            _sendButton.Click();
            AssertUrl(_activationSentUrl);

            // Logout and try to log in again with the old email.

            LogOut();
            LogIn(member);
            AssertNotLoggedIn();

            // Try to login in with the new email address.

            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = newEmail } };
            LogIn(member);
            AssertUrl(_notActivatedUrl);
        }

        private void TestPageVisibility(Member member)
        {
            LogIn(member);

            // Change email.

            AssertUrl(_notActivatedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notActivatedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            // Assert that selecting the cancel button returns the browser to this form.

            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.AreEqual(member.EmailAddresses[0].Address, _emailAddressTextBox.Text);
            Assert.IsTrue(_secondaryEmailAddressTextBox.IsVisible);
            Assert.AreEqual(member.EmailAddresses.Count > 1 ? member.EmailAddresses[1].Address : "", _secondaryEmailAddressTextBox.Text);
            Assert.IsTrue(_sendButton.IsVisible);
            Assert.IsTrue(_cancelButton.IsVisible);
        }

        private Member CreateMember(string emailAddress, string secondaryEmailAddress)
        {
            var member = _memberAccountsCommand.CreateTestMember(emailAddress, false);
            if (!string.IsNullOrEmpty(secondaryEmailAddress))
            {
                member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress });
                _memberAccountsCommand.UpdateMember(member);
            }

            return member;
        }

        private void ChangeEmail(IUser member, string newEmailAddress, string newSecondaryEmailAddress)
        {
            LogIn(member);

            // Change email.

            AssertUrl(_notActivatedUrl);
            _changeEmailAddressLink.Click();

            // Assert that the user was redirected to the appropriate page.

            var changeEmailUrl = _changeEmailUrl.AsNonReadOnly();
            changeEmailUrl.QueryString["returnUrl"] = _notActivatedUrl.PathAndQuery;
            AssertUrl(changeEmailUrl);

            _emailAddressTextBox.Text = newEmailAddress;
            _secondaryEmailAddressTextBox.Text = newSecondaryEmailAddress;
            _sendButton.Click();

            AssertUrl(_activationSentUrl);
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