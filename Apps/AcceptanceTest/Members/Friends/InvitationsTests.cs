using System;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Utility.Validation;
using LinkMe.Web;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.UI.Controls.Networkers;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class InvitationsTests
        : WebTestClass
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IRepresentativeInvitationsCommand _representativeInvitationsCommand = Resolve<IRepresentativeInvitationsCommand>();

        private const int TemporaryFriendAccessDays = 28;
        private const string FriendInviteAcceptedFormat = "You are now linked to <a href=\"{0}\">{1}</a>{2} network.";
        private const string RepresentativeInviteAcceptedFormat = "You are now <a href=\"{0}\">{1}</a>{2} representative.";

        private HtmlButtonTester _btnAcceptFriend;
        private HtmlButtonTester _btnIgnoreFriend;
        private HtmlButtonTester _btnAcceptRepresentative;
        private HtmlButtonTester _btnIgnoreRepresentative;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _btnAcceptFriend = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptInvitations_ctl00_btnAccept");
            _btnIgnoreFriend = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptInvitations_ctl00_btnIgnore");
            _btnAcceptRepresentative = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptRepresentativeInvitations_ctl00_btnRepresentativeAccept");
            _btnIgnoreRepresentative = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptRepresentativeInvitations_ctl00_btnRepresentativeIgnore");
        }

        [TestMethod]
        public void TestReceivedFriendInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create invitations.

            var inviter1 = _memberAccountsCommand.CreateTestMember(1);
            var invitation1 = new FriendInvitation { InviterId = inviter1.Id, InviteeEmailAddress = member.GetBestEmailAddress().Address };
            _memberFriendsCommand.SendInvitation(invitation1);

            var inviter2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation2 = new FriendInvitation { InviterId = inviter2.Id, InviteeEmailAddress = member.GetBestEmailAddress().Address };
            _memberFriendsCommand.SendInvitation(invitation2);

            var inviter3 = _memberAccountsCommand.CreateTestMember(3);
            var invitation3 = new FriendInvitation { InviterId = inviter3.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation3);

            // Assert all invites appear

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(inviter1.FullName);
            AssertPageContains(inviter2.FullName);
            AssertPageContains(inviter3.FullName);
        }

        [TestMethod]
        public void TestReceivedRepresentativeInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create invitations.

            var inviter1 = _memberAccountsCommand.CreateTestMember(1);
            var invitation1 = new RepresentativeInvitation { InviterId = inviter1.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation1);

            var inviter2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation2 = new RepresentativeInvitation { InviterId = inviter2.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation2);

            var inviter3 = _memberAccountsCommand.CreateTestMember(3);
            var invitation3 = new RepresentativeInvitation { InviterId = inviter3.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation3);

            // Assert all invites appear

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(inviter1.FullName);
            AssertPageContains(inviter2.FullName);
            AssertPageContains(inviter3.FullName);
        }

        [TestMethod]
        public void TestReceivedFriendAndRepresentativeInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Create representative invitations.

            var inviter1 = _memberAccountsCommand.CreateTestMember(1);
            var invitation1 = new RepresentativeInvitation { InviterId = inviter1.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation1);

            var inviter2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation2 = new RepresentativeInvitation { InviterId = inviter2.Id, InviteeId = member.Id };
            _memberFriendsCommand.SendInvitation(invitation2);

            // Create friend invitations.

            var inviter3 = _memberAccountsCommand.CreateTestMember(3);
            var invitation3 = new FriendInvitation { InviterId = inviter3.Id, InviteeEmailAddress = member.GetBestEmailAddress().Address };
            _memberFriendsCommand.SendInvitation(invitation3);

            var inviter4 = _memberAccountsCommand.CreateTestMember(4);
            var invitation4 = new FriendInvitation { InviterId = inviter4.Id, InviteeEmailAddress = member.GetBestEmailAddress().Address };
            _memberFriendsCommand.SendInvitation(invitation4);

            // Assert all invites appear

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(inviter1.FullName);
            AssertPageContains(inviter2.FullName);
            AssertPageContains(inviter3.FullName);
            AssertPageContains(inviter4.FullName);
        }

        [TestMethod]
        public void TestOldReceivedFriendInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendFriendInvitation(member2.Id, member1.Id);

            // Make the invitation old.

            var time = DateTime.Now.AddDays(-1 * TemporaryFriendAccessDays);
            invitation.FirstSentTime = invitation.LastSentTime = time;
            _networkingInvitationsCommand.UpdateInvitation(invitation);

            // Assert invitation doesn't appear.

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageDoesNotContain(_membersQuery.GetMember(invitation.InviterId).FullName);
        }

        [TestMethod]
        public void TestOldReceivedRepresentativeInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendRepresentativeInvitation(member2.Id, member1.Id);

            // Make the invitation old.

            var time = DateTime.Now.AddDays(-1 * TemporaryFriendAccessDays);
            invitation.FirstSentTime = invitation.LastSentTime = time;
            _representativeInvitationsCommand.UpdateInvitation(invitation);

            // Assert invitation doesn't appear.

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageDoesNotContain(_membersQuery.GetMember(invitation.InviterId).FullName);
        }

        [TestMethod]
        public void TestRejectedReceivedFriendInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendFriendInvitation(member2.Id, member1.Id);

            _memberFriendsCommand.RejectInvitation(invitation);

            // Assert invitation doesn't appear

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageDoesNotContain(_membersQuery.GetMember(invitation.InviterId).FullName);
        }

        [TestMethod]
        public void TestRejectedReceivedRepresentativeInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendRepresentativeInvitation(member2.Id, member1.Id);

            _memberFriendsCommand.RejectInvitation(invitation);

            // Assert invitation doesn't appear

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageDoesNotContain(_membersQuery.GetMember(invitation.InviterId).FullName);
        }

        [TestMethod]
        public void TestIgnoreFriendInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendFriendInvitation(member2.Id, member1.Id);

            LogIn(member1);

            GetPage<Invitations>();
            AssertPageContains(_membersQuery.GetMember(invitation.InviterId).FullName);
            AssertPageContains("has asked to be your friend");

            _btnIgnoreFriend.Click();
            AssertPage<Invitations>();
            AssertPageContains(string.Format(ValidationInfoMessages.INVITE_IGNORED, HtmlUtil.TextToHtml(_membersQuery.GetMember(invitation.InviterId).FullName.MakeNamePossessive())));
            AssertPageDoesNotContain("has asked to be your friend");
        }

        [TestMethod]
        public void TestAcceptFriendInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendFriendInvitation(member2.Id, member1.Id);

            LogIn(member1);

            GetPage<Invitations>();
            AssertPageContains(_membersQuery.GetMember(invitation.InviterId).FullName);
            AssertPageContains("has asked to be your friend");

            _btnAcceptFriend.Click();
            AssertPage<Invitations>();

            var profileUrl = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, invitation.InviterId.ToString()).PathAndQuery;
            var name = _membersQuery.GetMember(invitation.InviterId).FullName;
            AssertPageContains(string.Format(FriendInviteAcceptedFormat, profileUrl, name, name.GetNamePossessiveSuffix()), true);
            AssertPageDoesNotContain("has asked to be your friend");
        }

        [TestMethod]
        public void TestIgnoreRepresentativeInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendRepresentativeInvitation(member2.Id, member1.Id);

            LogIn(member1);

            GetPage<Invitations>();
            AssertPageContains(_membersQuery.GetMember(invitation.InviterId).FullName);
            AssertPageContains("has asked you to be their representative");

            _btnIgnoreRepresentative.Click();
            AssertPage<Invitations>();
            AssertPageContains(string.Format(ValidationInfoMessages.INVITE_IGNORED, HtmlUtil.TextToHtml(_membersQuery.GetMember(invitation.InviterId).FullName.MakeNamePossessive())));
            AssertPageDoesNotContain("has asked you to be their representative");
        }

        [TestMethod]
        public void TestAcceptRepresentativeInvitation()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var invitation = SendRepresentativeInvitation(member2.Id, member1.Id);

            LogIn(member1);

            GetPage<Invitations>();
            AssertPageContains(_membersQuery.GetMember(invitation.InviterId).FullName);
            AssertPageContains("has asked you to be their representative");

            _btnAcceptRepresentative.Click();
            AssertPage<Invitations>();

            var profileUrl = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, invitation.InviterId.ToString()).PathAndQuery;
            var name = _membersQuery.GetMember(invitation.InviterId).FullName;
            AssertPageContains(string.Format(RepresentativeInviteAcceptedFormat, profileUrl, name, name.GetNamePossessiveSuffix()), true);
            AssertPageDoesNotContain("has asked you to be their representative");
        }

        [TestMethod]
        public void TestNoInvitations()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageContains(ReceivedNetworkInvitationList.NoPendingInvitations);
        }

        [TestMethod]
        public void TestNoSentInvitations()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);

            LogIn(member1);
            GetPage<Invitations>();
            AssertPageContains(WebTextConstants.TEXT_NO_INVITATIONS_SENT_1);
        }

        [TestMethod]
        public void TestSentFriendInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            var invitation = new FriendInvitation { InviterId = member.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(invitee.FullName);
        }

        [TestMethod]
        public void TestSentRepresentativeInvitationToFriend()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var invitee = _memberAccountsCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(member.Id, invitee.Id);
            
            var invitation = new RepresentativeInvitation { InviterId = member.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(invitee.FullName);
        }

        [TestMethod]
        public void TestSentRepresentativeInvitationToNonFriend()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            var invitation = new RepresentativeInvitation { InviterId = member.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(invitee.FullName);
        }

        [TestMethod]
        public void TestSentRepresentativeAndFriendInvitations()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var invitee1 = _memberAccountsCommand.CreateTestMember(1);
            var invitee2 = _memberAccountsCommand.CreateTestMember(2);

            _memberFriendsCommand.SendInvitation(new RepresentativeInvitation { InviterId = member.Id, InviteeId = invitee1.Id });
            _memberFriendsCommand.SendInvitation(new FriendInvitation { InviterId = member.Id, InviteeId = invitee2.Id });

            LogIn(member);
            GetPage<Invitations>();
            AssertPageContains(invitee1.FullName);
            AssertPageContains(invitee2.FullName);
        }

        [TestMethod]
        public void TestAcceptedSentRepresentativeInvitation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            var invitation = new RepresentativeInvitation { InviterId = member.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);
            _memberFriendsCommand.AcceptInvitation(invitee.Id, invitation);

            LogIn(member);
            GetPage<Invitations>();
            AssertPageDoesNotContain(invitee.FullName);
        }

        [TestMethod]
        public void TestRejectedSentRepresentativeInvitation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            var invitee = _memberAccountsCommand.CreateTestMember(1);
            var invitation = new RepresentativeInvitation { InviterId = member.Id, InviteeId = invitee.Id };
            _memberFriendsCommand.SendInvitation(invitation);
            _memberFriendsCommand.RejectInvitation(invitation);

            LogIn(member);
            GetPage<Invitations>();
            AssertPageDoesNotContain(invitee.FullName);
        }

        private FriendInvitation SendFriendInvitation(Guid inviterId, Guid inviteeId)
        {
            var invitation = new FriendInvitation { InviterId = inviterId, InviteeId = inviteeId };
            _memberFriendsCommand.SendInvitation(invitation);
            return invitation;
        }

        private RepresentativeInvitation SendRepresentativeInvitation(Guid inviterId, Guid inviteeId)
        {
            var invitation = new RepresentativeInvitation { InviterId = inviterId, InviteeId = inviteeId };
            _memberFriendsCommand.SendInvitation(invitation);
            return invitation;
        }
    }
}