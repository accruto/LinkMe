using System;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Service;
using LinkMe.Web.UI.Controls.Networkers.OverlayPopups;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.service
{
	[TestClass]
    public class InvitationPopupContentsTest : WebFormDataTestCase
	{
		private Member _member;
		private Member _firstDegreeContact;
		private Member _secondDegreeContact;
		private Member _publicContact;
		private Member _invitedContact;

	    private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();

        private const string AlreadyFriendsFormat = "You are already friends with {0}.";
	    private const string CannotAddSelfToFriends = "Sorry. We can't add you to your own friends list.";
        private const string NotAcceptingInvitations = "Sorry, that member is not currently accepting invitations.";
        private const string InviteAlreadyExistsFormat = "You have already sent an invitation to {0}. Please give that member some time to respond. If they have not responded by {1}, you will be able to send another invitation as a reminder.";

        [TestInitialize]
        public void TestInitialize()
		{
            PreLoadTestUserProfiles();

            _member = _membersQuery.GetMember(TestNetworkerUserId);
            _firstDegreeContact = _membersQuery.GetMember(TestUserTwoId);
            _secondDegreeContact = _membersQuery.GetMember(TestUserFiveId);
            _publicContact = _membersQuery.GetMember(TestUserNineId);
            _invitedContact = _membersQuery.GetMember(TestUserSevenId);
            LogIn(_member);
		}

		[TestMethod]
		public void TestInviteSelf()
		{
			RequestInvitation(_member.Id);

			AssertPageContains(CannotAddSelfToFriends);
		}

		[TestMethod]
		public void TestInviteFirstDegreeContact()
		{
			RequestInvitation(_firstDegreeContact.Id);

			AssertPageContains(string.Format(AlreadyFriendsFormat, _firstDegreeContact.FullName));
		}

		[TestMethod]
		public void TestInviteSecondDegreeContact()
		{
			_secondDegreeContact = _membersQuery.GetMember(_secondDegreeContact.Id);

			// With permission
            _secondDegreeContact.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.SendInvites);
            var view = new PersonalView(_secondDegreeContact, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree);
            Assert.IsTrue(view.CanAccess(PersonalVisibility.SendInvites));
            _memberAccountsCommand.UpdateMember(_secondDegreeContact);
			
			RequestInvitation(_secondDegreeContact.Id);
			AssertPageContains(string.Format(AddToFriends.HeadingFormat, _secondDegreeContact.FirstName));

			// Without permission
            _secondDegreeContact.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.SendInvites);
            Assert.IsTrue(!view.CanAccess(PersonalVisibility.SendInvites));
            _memberAccountsCommand.UpdateMember(_secondDegreeContact);

            RequestInvitation(_secondDegreeContact.Id);
			AssertPageContains(NotAcceptingInvitations);
		}

		[TestMethod]
		public void TestInvitePublicContact()
		{
			_publicContact = _membersQuery.GetMember(_publicContact.Id);

			// With permission
            _publicContact.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.SendInvites);
            _publicContact.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            var view = new PersonalView(_publicContact, PersonalContactDegree.Public, PersonalContactDegree.Public);
            Assert.IsTrue(view.CanAccess(PersonalVisibility.SendInvites));
            _memberAccountsCommand.UpdateMember(_publicContact);

            RequestInvitation(_publicContact.Id);
			AssertPageContains(string.Format(AddToFriends.HeadingFormat, _publicContact.FirstName));

			// Without permission
            _publicContact.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.SendInvites);
            Assert.IsTrue(!view.CanAccess(PersonalVisibility.SendInvites));
            _memberAccountsCommand.UpdateMember(_publicContact);

            RequestInvitation(_publicContact.Id);
			AssertPageContains(NotAcceptingInvitations);
		}

		[TestMethod]
		public void TestInviteInvitedContact()
		{
		    var invitation = new FriendInvitation {InviterId = _member.Id, InviteeId = _invitedContact.Id};
            _networkingInvitationsCommand.CreateInvitation(invitation);
            _networkingInvitationsCommand.SendInvitation(invitation);
            Assert.IsTrue(invitation.Status == RequestStatus.Pending && _memberFriendsCommand.GetAllowedSendingTime(invitation) > DateTime.Now);

            RequestInvitation(_invitedContact.Id);

            AssertPageContains(string.Format(InviteAlreadyExistsFormat, _invitedContact.FullName,
                                _memberFriendsCommand.GetAllowedSendingTime(invitation)));
		}

		private void RequestInvitation(Guid inviteeId)
		{
			GetPage<InvitationPopupContents>(InvitationPopupContents.InviteeIdParameter, inviteeId.ToString());
		}
	}
}
