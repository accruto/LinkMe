using System;
using System.Xml;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Donations;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Web.Members.Friends;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InviteFriends=LinkMe.Web.Members.Friends.InviteFriends;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class InviteFriendsTests
        : WebFormDataTestCase
    {
        private const string ValidEmail = "linkme10@test.linkme.net.au";
        private const string ValidEmail2 = "10linkme10@test.linkme.net.au";
        private const string Password = "password";
        private const string DefaultMessage = "";
        private const string ChangedMessage = "I have changed the body of the text.";

        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();
        private readonly IMemberViewsQuery _memberViewsQuery = Resolve<IMemberViewsQuery>();
        private readonly IMemberFriendsQuery _memberFriendsQuery = Resolve<IMemberFriendsQuery>();

        private HtmlButtonTester _btnFindFriends;
        private HtmlButtonTester _btnCancelFindFriends;

        private HtmlButtonTester _btnAddToInvitation;
        private HtmlButtonTester _btnCancelAddToInvitation;

        private HtmlButtonTester _btnUploadCsv;
        private HtmlButtonTester _btnCancelUploadCsv;

        private HtmlButtonTester _btnSendInvitations;
        private HtmlButtonTester _btnCancelSendInvitations;

        private HtmlButtonTester _btnNetworkInvite;
        private HtmlButtonTester _btnCancelNetworkInvite;

        private HtmlTextBoxTester _txtEmailAddress;
        private HtmlPasswordTester _txtProviderPassword;
        private HtmlDropDownListTester _ddlProviders;

        private HtmlFileTester _txtFilePath;

        private HtmlTextAreaTester _txtEmailAddresses;
        private HtmlTextAreaTester _txtBody;
        private HtmlCheckBoxTester _chkDonationRecipient;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        private ReadOnlyUrl _activationUrl;
        private ReadOnlyUrl _notActivatedUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _btnFindFriends = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnFindFriends");
            _btnCancelFindFriends = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnCancelFindFriends");

            _btnAddToInvitation = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnAddToInvitation");
            _btnCancelAddToInvitation = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnCancelAddToInvitation");

            _btnSendInvitations = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnSendInvitations");
            _btnCancelSendInvitations = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnCancelSendInvitations");

            _btnUploadCsv = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnUploadCsv");
            _btnCancelUploadCsv = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnCancelUploadCsv");

            _btnNetworkInvite = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnNetworkInvite");
            _btnCancelNetworkInvite = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnCancelNetworkInvite");

            _txtEmailAddress = new HtmlTextBoxTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFromAddressBook_txtEmailAddress");
            _txtProviderPassword = new HtmlPasswordTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFromAddressBook_txtPassword");
            _ddlProviders = new HtmlDropDownListTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFromAddressBook_ddlProviders");

            _txtFilePath = new HtmlFileTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteEmailClientContacts_txtFilePath");

            _txtEmailAddresses = new HtmlTextAreaTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFriends_txtEmailAddresses");
            _txtBody = new HtmlTextAreaTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFriends_txtBody");
            _chkDonationRecipient = new HtmlCheckBoxTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFriends_chkDonationRecipient");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
        }

        [TestMethod]
        public void TestPageVisibility()
        {
            // Assert that you have to be logged on to access the page.

            AssertSecureUrl(NavigationManager.GetUrlForPage<InviteFriends>(), LogInUrl);

            // Assert that you have to be activated to access the page.

            var member = _memberAccountsCommand.CreateTestMember(0, false);
            LogIn(member);
            GetPage<InviteFriends>();
            AssertUrlWithoutQuery(_notActivatedUrl);

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            member = _memberAccountsCommand.CreateTestMember(0, true);
            LogIn(member);
            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();

            // LogIn.

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            LogIn();

            // Assert that the form is shown and that initial controls are visible.

            AssertVisibility(false, false, true, false, false);
        }

        [TestMethod]
        public void TestCancel()
        {
            LogIn();

            // Cancel.

            _btnCancelSendInvitations.Click();
            AssertUrl(LoggedInMemberHomeUrl);
        }

        [TestMethod]
        public void TestSendInvitation()
        {
            // LogIn.

            var member = LogIn();

            // Send invitations.

            _txtEmailAddresses.Text = ValidEmail;
            Assert.AreEqual(DefaultMessage, _txtBody.Text.Trim());
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(ValidEmail);

            // Assert.

            member = _membersQuery.GetMember(member.GetBestEmailAddress().Address);
            var request = AssertDonation();
            AssertInvitations(member.Id, DefaultMessage, request, ValidEmail);

            // Look for an InvitationEmail.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, ValidEmail);
            email.AssertHtmlViewContains(member.FullName + " has sent you a request to join their personal network.");
            email.AssertHtmlViewContains(DefaultMessage);

            // Send again.

            _txtEmailAddresses.Text = ValidEmail;
            Assert.AreEqual(DefaultMessage, _txtBody.Text.Trim());
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains(ValidationInfoMessages.ALREADYINVITED_TEXT);
            AssertPageContains(ValidEmail);

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestSendInvitationNewBody()
        {
            // LogIn.

            var member = LogIn();

            // Send.

            _txtEmailAddresses.Text = ValidEmail;
            _txtBody.Text = ChangedMessage;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(ValidEmail);

            // Assert.

            member = _membersQuery.GetMember(member.GetBestEmailAddress().Address);
            var request = AssertDonation();
            AssertInvitations(member.Id, ChangedMessage, request, ValidEmail);

            // Look for an InvitationEmail.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, ValidEmail);
            email.AssertHtmlViewContains(member.FullName + " has sent you a request to join their personal network.");
            email.AssertHtmlViewContains(ChangedMessage);
        }

        [TestMethod, Ignore]
        public void TestSendInvitationWithDonation()
        {
            // LogIn.

            var member = LogIn();

            // Send with a donation.

            _txtEmailAddresses.Text = ValidEmail;
            Assert.AreEqual(DefaultMessage, _txtBody.Text);
            _chkDonationRecipient.IsChecked = true;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(ValidEmail);

            // Assert.

            member = _membersQuery.GetMember(member.GetBestEmailAddress().Address);
            var request = AssertDonation();
            AssertInvitations(member.Id, DefaultMessage, request, ValidEmail);
        }

        [TestMethod, Ignore]
        public void TestMultipleSendInvitationsWithDonation()
        {
            // LogIn.

            var member = LogIn();

            // Send without a donation.

            _txtEmailAddresses.Text = ValidEmail;
            Assert.AreEqual(DefaultMessage, _txtBody.Text);
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(ValidEmail);

            // Assert.

            member = _membersQuery.GetMember(member.GetBestEmailAddress().Address);
            var request = AssertDonation();
            AssertInvitations(member.Id, DefaultMessage, request, ValidEmail);

            // Send with a donation.

            _txtEmailAddresses.Text = ValidEmail2;
            Assert.AreEqual(DefaultMessage, _txtBody.Text);
            _chkDonationRecipient.IsChecked = true;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(ValidEmail2);

            // Assert.

            member = _membersQuery.GetMember(member.GetBestEmailAddress().Address);
            request = AssertDonation();
            AssertInvitations(member.Id, DefaultMessage, request, ValidEmail2);
        }

        [TestMethod]
        public void TestInvalidEmail()
        {
            // LogIn.

            LogIn();

            // Send.

            _txtEmailAddresses.Text = TestEmailToInvalidFormat;
            Assert.AreEqual(DefaultMessage, _txtBody.Text.Trim());
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains(TestEmailToInvalidFormat);
        }

        [TestMethod]
        public void TestNoEmail()
        {
            // LogIn.

            LogIn();

            // Send.

            Assert.AreEqual(DefaultMessage, _txtBody.Text.Trim());
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertErrorMessage(ValidationErrorMessages.REQUIRED_FIELD_TO);
        }

        [TestMethod]
        public void TestExistingMember()
        {
            // Create another member.

            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // LogIn.

            var inviter = LogIn();

            // Send.

            _txtEmailAddresses.Text = invitee.GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(invitee.GetBestEmailAddress().Address);

            // Look for an ExistingMemberInvitationEmail.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, new EmailRecipient(invitee.GetBestEmailAddress().Address, invitee.FullName));
            email.AssertHtmlViewContains("<p>Hi " + invitee.FirstName + "</p>");
            var withLink = email.GetHtmlView().Body.Contains("<p>\r\n  " + inviter.FullName + " has sent you a request to join their personal network");
            var withoutLink = email.GetHtmlView().Body.Contains(inviter.FullName + "\r\n  </a>\r\n  has sent you a request to join their personal network");
            Assert.IsTrue(withoutLink ^ withLink);
            var invitationUrl = GetInvitationUrl(email.GetHtmlView().Body);

            // Send it again.

            _txtEmailAddresses.Text = invitee.GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains(ValidationInfoMessages.ALREADYINVITED_TEXT);
            AssertPageContains(invitee.GetBestEmailAddress().Address);

            _emailServer.AssertNoEmailSent();

            // Follow the link.

            LogOut();
            Get(invitationUrl);
            AssertUrlWithoutQuery(LogInUrl);
            SubmitLogIn(invitee);

            AssertPage<Invitations>();
            AssertPageContains(inviter.FullName + " has asked to be your friend");
        }

        [TestMethod]
        public void TestDailyLimitMember()
        {
            // Create other members.

            const int count = 11;
            var invitees = new Member[count];
            for (var index = 1; index <= count; ++index)
                invitees[index - 1] = _memberAccountsCommand.CreateTestMember(index);

            // LogIn.

            var inviter = LogIn();

            for (var index = 0; index < count - 1; ++index)
            {
                var invitee = invitees[index];

                // Send.

                GetPage<InviteFriends>();
                _txtEmailAddresses.Text = invitee.GetBestEmailAddress().Address;
                _btnSendInvitations.Click();
                AssertPage<InviteFriends>();
                AssertPageContains("Invites sent:");
                AssertPageContains(invitee.GetBestEmailAddress().Address);

                // Look for an ExistingMemberInvitationEmail.

                var email = _emailServer.AssertEmailSent();
                email.AssertAddresses(inviter, Return, new EmailRecipient(invitee.GetBestEmailAddress().Address, invitee.FullName));
                email.AssertHtmlViewContains("<p>Hi " + invitee.FirstName + "</p>");
                var withLink = email.GetHtmlView().Body.Contains("<p>\r\n  " + inviter.FullName + " has sent you a request to join their personal network");
                var withoutLink = email.GetHtmlView().Body.Contains(inviter.FullName + "\r\n  </a>\r\n  has sent you a request to join their personal network");
                Assert.IsTrue(withoutLink ^ withLink);
            }

            // Invite one more.

            GetPage<InviteFriends>();
            _txtEmailAddresses.Text = invitees[count - 1].GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertErrorMessage("This functionality is temporarily unavailable. Please try again later.");
        }

        [TestMethod]
        public void TestExistingInactiveMember()
        {
            // Create another member.

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            invitee.IsActivated = false;
            _memberAccountsCommand.UpdateMember(invitee);

            // LogIn.

            var inviter = LogIn();

            // Send.

            _txtEmailAddresses.Text = invitee.GetBestEmailAddress().Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Invites sent:");
            AssertPageContains(invitee.GetBestEmailAddress().Address);

            // Look for an ExistingMemberInvitationEmail.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(inviter, Return, new EmailRecipient(invitee.GetBestEmailAddress().Address, invitee.FullName));
            email.AssertHtmlViewContains("<p>Hi " + invitee.FirstName + "</p>");
            var withoutLink = email.GetHtmlView().Body.Contains("<p>\r\n  " + inviter.FullName + " has sent you a request to join their personal network.");
            var withLink = email.GetHtmlView().Body.Contains(inviter.FullName + "\r\n  </a>\r\n  has sent you a request to join their personal network.");
            Assert.IsTrue(withoutLink ^ withLink);
            var invitationUrl = GetInvitationUrl(email.GetHtmlView().Body);

            // Follow the link.

            LogOut();
            Get(invitationUrl);
            AssertUrlWithoutQuery(_activationUrl);
            _loginIdTextBox.Text = invitee.GetLoginId();
            _passwordTextBox.Text = Password;
            _loginButton.Click();
            AssertPage<Invitations>();
            AssertPageContains(inviter.FullName + " has asked to be your friend");
        }

        [TestMethod]
        public void TestExistingContact()
        {
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            // LogIn.

            var member = LogIn();
            _networkingCommand.CreateFirstDegreeLink(member.Id, invitee.Id);

            // Send.

            var contactIds = _memberContactsQuery.GetFirstDegreeContacts(member.Id);
            var views = _memberViewsQuery.GetPersonalViews(member.Id, contactIds);
            var emailAddress = views[contactIds[0]].EmailAddresses[0];
            _txtEmailAddresses.Text = emailAddress.Address;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains("Already in your friends list:");
            AssertPageContains(emailAddress.Address);
        }

        [TestMethod]
        public void TestInvalidMultipleEmails()
        {
            // LogIn.

            LogIn();

            // Send.

            _txtEmailAddresses.Text = TestEmailMultipleInvalidFormat;
            Assert.AreEqual(DefaultMessage, _txtBody.Text.Trim());
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            AssertPageContains(TestEmailMultipleInvalidFormat.Split(new[]{',', ';'})[0]);
        }

        private Member LogIn()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            // Browse to the page.

            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();
            return member;
        }

        private void AssertVisibility(bool findFriends, bool findFriendsResults, bool inviteFriends, bool inviteFromEmailClient, bool networkInvite)
        {
            Assert.AreEqual(_btnFindFriends.IsVisible, findFriends);
            Assert.AreEqual(_btnCancelFindFriends.IsVisible, findFriends);
            Assert.AreEqual(_txtEmailAddress.IsVisible, findFriends);
            Assert.AreEqual(_txtProviderPassword.IsVisible, findFriends);
            Assert.AreEqual(_ddlProviders.IsVisible, findFriends);

            Assert.AreEqual(_btnAddToInvitation.IsVisible, findFriendsResults);
            Assert.AreEqual(_btnCancelAddToInvitation.IsVisible, findFriendsResults);

            Assert.AreEqual(_txtFilePath.IsVisible, inviteFromEmailClient);
            Assert.AreEqual(_btnUploadCsv.IsVisible, inviteFromEmailClient);
            Assert.AreEqual(_btnCancelUploadCsv.IsVisible, inviteFromEmailClient);

            Assert.AreEqual(_btnSendInvitations.IsVisible, inviteFriends);
            Assert.AreEqual(_txtEmailAddresses.IsVisible, inviteFriends);
            Assert.AreEqual(_txtBody.IsVisible, inviteFriends);

            // No longer used so never visible.
            
            Assert.IsFalse(_chkDonationRecipient.IsVisible);

            Assert.AreEqual(_btnCancelSendInvitations.IsVisible, inviteFriends);

            Assert.AreEqual(_btnNetworkInvite.IsVisible, networkInvite);
            Assert.AreEqual(_btnCancelNetworkInvite.IsVisible, networkInvite);
        }

        private static DonationRequest AssertDonation()
        {
            // No longer supported.

            return null;

            /*
            var recipient = _donationsQuery.GetRecipient(_donationsQuery.GetRecipients()[0].Name);
            Assert.IsNotNull(recipient);
            DonationRequest request = _donationsQuery.GetRequest(recipient.Id, decimal.Parse(ApplicationContext.Instance.GetProperty(ApplicationContext.DONATION_AMOUNT)));
            if (expected)
                Assert.IsNotNull(request);
            else
                Assert.IsNull(request);
            return request;
            */
        }

        private void AssertInvitations(Guid memberId, string body, DonationRequest request, params string[] emailAddresses)
        {
            foreach (var emailAddress in emailAddresses)
            {
                var invitation = _memberFriendsQuery.GetFriendInvitation(memberId, emailAddress);
                Assert.IsNotNull(invitation);
                Assert.AreEqual(body, invitation.Text);
                Assert.AreEqual(request == null ? (Guid?)null : request.Id, invitation.DonationRequestId);
            }
        }

        private static Url GetInvitationUrl(string body)
        {
            var doc = new XmlDocument();
            doc.LoadXml(body); 
            var links = doc.SelectNodes("html/body/div//a");
            foreach (XmlNode link in links)
            {
                if (link.Attributes["class"] != null &&
                    link.Attributes["class"].Value != null &&
                        link.Attributes["class"].Value.Contains("major-action"))
                {
                    return new ApplicationUrl(link.Attributes["href"].Value);
                }
            }

            throw new ArgumentException("body did not contain a link with class \"major-action\"", "body");
        }
    }
}