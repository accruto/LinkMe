using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class RepresentativesFlowTests
        : RepresentativesTests
    {
        private const string Password = "password";

        private HtmlButtonTester _btnAccept;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        private ReadOnlyUrl _activationUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _emailServer.ClearEmails();

            _btnAccept = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptRepresentativeInvitations_ctl00_btnRepresentativeAccept");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");

            _activationUrl = new ReadOnlyApplicationUrl("~/accounts/activation");
        }

        [TestMethod]
        public void TestInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create some other members.

            var member1 = _memberAccountsCommand.CreateTestMember(1);
            member1.VisibilitySettings.Personal.PublicVisibility = member1.VisibilitySettings.Personal.PublicVisibility.SetFlag(PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member1);

            var member2 = _memberAccountsCommand.CreateTestMember(2);
            member2.VisibilitySettings.Personal.PublicVisibility = member2.VisibilitySettings.Personal.PublicVisibility.SetFlag(PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member2);

            LogIn(member);
            GetPage<ViewRepresentative>();

            // Search for and invite member 1.

            _txtName.Text = member1.FullName;
            _btnSearch.Click();
            AssertPageContains(member1.FullName);
            AssertPageDoesNotContain(member2.FullName);

            // Send invitation.

            GetPage<RepresentativePopupContents>(
                RepresentativePopupContents.InviteeIdParameter, member1.Id.ToString(),
                RepresentativePopupContents.SendInvitationParameter, "true",
                RepresentativePopupContents.MessageParameter, Message);

            AssertInvitation(member.Id, member1.Id, Message);

            GetPage<ViewRepresentative>();
            AssertNoRepresentative();

            // Search for and invite member 2.

            _txtName.Text = member2.FullName;
            _btnSearch.Click();
            AssertPageContains(member2.FullName);

            // Send invitation.

            GetPage<RepresentativePopupContents>(
                RepresentativePopupContents.InviteeIdParameter, member2.Id.ToString(),
                RepresentativePopupContents.SendInvitationParameter, "true",
                RepresentativePopupContents.MessageParameter, Message);

            AssertInvitation(member.Id, member2.Id, Message);

            GetPage<ViewRepresentative>();
            AssertNoRepresentative();
        }

        [TestMethod]
        public void TestInviteFriend()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, invitee.Id);

            TestInvite(inviter, invitee);
        }

        [TestMethod]
        public void TestInviteNonFriend()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            TestInvite(inviter, invitee);
        }

        [TestMethod]
        public void TestEmailsFriend()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, invitee.Id);

            TestEmails(inviter, invitee, true);
        }

        [TestMethod]
        public void TestEmailsFriendNotActivated()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(inviter.Id, invitee.Id);

            invitee.IsActivated = false;
            _memberAccountsCommand.UpdateMember(invitee);

            TestEmails(inviter, invitee, false);
        }

        [TestMethod]
        public void TestEmailsNonFriend()
        {
            var inviter = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);

            TestEmails(inviter, invitee, true);
        }

        private void TestInvite(IRegisteredUser inviter, IMember invitee)
        {
            Assert.IsNull(_memberContactsQuery.GetRepresentativeContact(inviter.Id));
            Assert.AreEqual(0, _memberContactsQuery.GetRepresenteeContacts(invitee.Id).Count);

            LogIn(inviter);

            // Invite.

            GetPage<RepresentativePopupContents>(
                RepresentativePopupContents.InviteeIdParameter, invitee.Id.ToString(),
                RepresentativePopupContents.SendInvitationParameter, "true",
                RepresentativePopupContents.MessageParameter, Message);

            GetPage<ViewRepresentative>();
            AssertNoRepresentative();

            GetPage<Invitations>();
            AssertPageContains(invitee.FullName);

            AssertInvitation(inviter.Id, invitee.Id, Message);

            // Accept.

            LogOut();
            LogIn(invitee);

            GetPage<Invitations>();
            AssertPageContains(inviter.FullName);
            AssertPageContains("has asked you to be their representative");

            _btnAccept.Click();
            AssertPage<Invitations>();
            var url = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, inviter.Id.ToString());
            AssertPageContains(string.Format("You are now <a href=\"{0}\">{1}</a>{2} representative.", url.PathAndQuery, inviter.FullName, inviter.FullName.GetNamePossessiveSuffix()), true);

            Assert.IsNull(_memberFriendsQuery.GetRepresentativeInvitation(inviter.Id, invitee.Id));
            Assert.IsNull(_memberFriendsQuery.GetRepresentativeInvitationByInviter(inviter.Id));
            Assert.AreEqual(0, _memberFriendsQuery.GetRepresentativeInvitations(invitee.Id, invitee.GetBestEmailAddress().Address).Count);

            Assert.AreEqual(invitee.Id, _memberContactsQuery.GetRepresentativeContact(inviter.Id));
            Assert.AreEqual(1, _memberContactsQuery.GetRepresenteeContacts(invitee.Id).Count);
            Assert.AreEqual(inviter.Id, _memberContactsQuery.GetRepresenteeContacts(invitee.Id)[0]);

            // Should also be friends.

            Assert.AreEqual(true, _memberContactsQuery.AreFirstDegreeContacts(inviter.Id, invitee.Id));
        }

        private void TestEmails(RegisteredUser inviter, Member invitee, bool isActivated)
        {
            Assert.IsNull(_memberContactsQuery.GetRepresentativeContact(inviter.Id));
            Assert.AreEqual(0, _memberContactsQuery.GetRepresenteeContacts(invitee.Id).Count);

            LogIn(inviter);

            // Invite.

            GetPage<RepresentativePopupContents>(
                RepresentativePopupContents.InviteeIdParameter, invitee.Id.ToString(),
                RepresentativePopupContents.SendInvitationParameter, "true",
                RepresentativePopupContents.MessageParameter, Message);

            GetPage<ViewRepresentative>();
            AssertNoRepresentative();

            var email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains(inviter.FullName + " has sent you a request to represent them");

            // Follow the link.

            LogOut();

            email.AssertAddresses(inviter, Return, invitee);
            var link = email.GetHtmlView().GetLinks()[1];
            Get(link);

            if (isActivated)
            {
                AssertUrlWithoutQuery(LogInUrl);
                SubmitLogIn(invitee);
            }
            else
            {
                AssertUrlWithoutQuery(_activationUrl);
                _loginIdTextBox.Text = invitee.GetLoginId();
                _passwordTextBox.Text = Password;
                _loginButton.Click();
            }

            AssertPage<Invitations>();

            // Accept.

            AssertPageContains(inviter.FullName);
            AssertPageContains("has asked you to be their representative");

            _btnAccept.Click();
            AssertPage<Invitations>();
            var url = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, inviter.Id.ToString());
            AssertPageContains(string.Format("You are now <a href=\"{0}\">{1}</a>{2} representative.", url.PathAndQuery, inviter.FullName, inviter.FullName.GetNamePossessiveSuffix()), true);

            Assert.IsNull(_memberFriendsQuery.GetRepresentativeInvitation(inviter.Id, invitee.Id));
            Assert.IsNull(_memberFriendsQuery.GetRepresentativeInvitationByInviter(inviter.Id));
            Assert.AreEqual(0, _memberFriendsQuery.GetRepresentativeInvitations(invitee.Id, invitee.GetBestEmailAddress().Address).Count);

            Assert.AreEqual(invitee.Id, _memberContactsQuery.GetRepresentativeContact(inviter.Id));
            Assert.AreEqual(1, _memberContactsQuery.GetRepresenteeContacts(invitee.Id).Count);
            Assert.AreEqual(inviter.Id, _memberContactsQuery.GetRepresenteeContacts(invitee.Id)[0]);

            // Should also be friends.

            Assert.AreEqual(true, _memberContactsQuery.AreFirstDegreeContacts(inviter.Id, invitee.Id));

            // Look for the confirmation email.

            email = _emailServer.AssertEmailSent();
            email.AssertHtmlViewContains(invitee.FullName + " has accepted your request for them to represent you");
            email.AssertAddresses(Return, Return, inviter);
        }
    }
}
