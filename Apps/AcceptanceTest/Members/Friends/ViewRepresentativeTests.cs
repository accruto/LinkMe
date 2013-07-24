using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Framework.Utility;
using LinkMe.Web.Members.Friends;
using LinkMe.Web.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class ViewRepresentativeTests
        : RepresentativesTests
    {
        private const string CannotYourselfBeRepresentative = "Sorry. You can't be your own representative.";
        private const string CouldNotFindMember = "The member you selected could not be found.";
        private const string AlreadyRepresentativeFormat = "{0} is already your representative.";
        private const string InviteAlreadyExistsFormat = "You have already sent an invitation to {0}.";
        private const string AlreadyDeclinedFormat = "{0} has already declined an invitation from you.";
        private const string NotAcceptingInvitations = "Sorry, that member is not currently accepting invitations.";
        private const string InvitationTextFormat = "A message will be sent to {0} who can choose to accept your request";
        private const string SuccessfulSendInvitation = "Your invitation was sent successfully.";

        private HtmlButtonTester _btnRemoveRepresentative;

        [TestInitialize]
        public void TestInitialize()
        {
            _btnRemoveRepresentative = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_TopContent") + "_btnRemoveRepresentative");
        }

        [TestMethod]
        public void TestNoRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            CreateFriends(member.Id, 1, 1);

            LogIn(member);
            GetPage<ViewRepresentative>();

            AssertNoRepresentative();

            Assert.IsTrue(_txtName.IsVisible);
            Assert.IsTrue(_txtEmailAddress.IsVisible);
            Assert.IsTrue(_btnSearch.IsVisible);
        }

        [TestMethod]
        public void TestInvitedRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];

            var invitation = new RepresentativeInvitation
            {
                InviterId = member.Id,
                InviteeId = friend.Id
            };
            _memberFriendsCommand.SendInvitation(invitation);

            LogIn(member);
            GetPage<ViewRepresentative>();

            AssertNoRepresentative();

            Assert.IsTrue(_txtName.IsVisible);
            Assert.IsTrue(_txtEmailAddress.IsVisible);
            Assert.IsTrue(_btnSearch.IsVisible);
        }

        [TestMethod]
        public void TestRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);

            LogIn(member);
            GetPage<ViewRepresentative>();

            AssertRepresentative(friend.Id);

            Assert.IsTrue(_txtName.IsVisible);
            Assert.IsTrue(_txtEmailAddress.IsVisible);
            Assert.IsTrue(_btnSearch.IsVisible);
        }

        [TestMethod]
        public void TestRemoveRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);

            LogIn(member);
            GetPage<ViewRepresentative>();

            AssertRepresentative(friend.Id);
            _btnRemoveRepresentative.Click();

            AssertPage<ViewRepresentative>();
            AssertNoRepresentative();

            Assert.IsNull(_representativesQuery.GetRepresentativeId(member.Id));

            // Should still be friends.

            _memberContactsQuery.AreFirstDegreeContacts(member.Id, friend.Id);
        }

        [TestMethod]
        public void TestSearchRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 1);

            LogIn(member);
            GetPage<ViewRepresentative>();
            AssertPageDoesNotContain(friends[0].FullName);

            // Search by name.

            PerformSearch(friends[0].FullName, null, friends[0]);

            // Search by email address.

            PerformSearch(null, friends[0].GetBestEmailAddress().Address, friends[0]);

            // Search by other name.

            PerformSearch("something", null);

            // Search by other email address.

            PerformSearch(null, "something@test.linkme.net.au");
        }

        [TestMethod]
        public void TestInviteSelf()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            TestInvitation(member, member.Id, CannotYourselfBeRepresentative);

        }

        [TestMethod]
        public void TestInviteUnknownMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            TestInvitation(member, Guid.NewGuid(), CouldNotFindMember);
        }

        [TestMethod]
        public void TestInviteRepresentative()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);

            TestInvitation(member, friend.Id, string.Format(AlreadyRepresentativeFormat, friend.FullName));
        }

        [TestMethod]
        public void TestInvitePendingInvitation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            var invitation = new RepresentativeInvitation
            {
                InviterId = member.Id,
                InviteeId = friend.Id
            };
            _memberFriendsCommand.SendInvitation(invitation);
            
            TestInvitation(member, friend.Id, string.Format(InviteAlreadyExistsFormat, friend.FullName));
        }

        [TestMethod]
        public void TestInviteRejectedInvitation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            var invitation = new RepresentativeInvitation
            {
                InviterId = member.Id,
                InviteeId = friend.Id
            };
            _memberFriendsCommand.SendInvitation(invitation);
            _memberFriendsCommand.RejectInvitation(invitation);

            TestInvitation(member, friend.Id, string.Format(AlreadyDeclinedFormat, friend.FullName));
        }

        [TestMethod]
        public void TestInviteNoPublicPermissions()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = _memberAccountsCommand.CreateTestMember(1);
            friend.VisibilitySettings.Personal.PublicVisibility = friend.VisibilitySettings.Personal.PublicVisibility.ResetFlag(PersonalVisibility.SendInvites);
            _memberAccountsCommand.UpdateMember(friend);

            TestInvitation(member, friend.Id, NotAcceptingInvitations);
        }

        [TestMethod]
        public void TestInviteNoFriendPermissions()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            friend.VisibilitySettings.Personal.FirstDegreeVisibility = friend.VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.SendInvites);
            _memberAccountsCommand.UpdateMember(friend);

            TestInvitation(member, friend.Id, NotAcceptingInvitations);
        }

        [TestMethod]
        public void TestInviteFriend()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            TestInvitation(member, friend.Id, string.Format(InvitationTextFormat, friend.FirstName));
        }

        [TestMethod]
        public void TestInvitePublic()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = _memberAccountsCommand.CreateTestMember(1);
            friend.VisibilitySettings.Personal.PublicVisibility = friend.VisibilitySettings.Personal.PublicVisibility.SetFlag(PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(friend);
            TestInvitation(member, friend.Id, string.Format(InvitationTextFormat, friend.FirstName));
        }

        [TestMethod]
        public void TestSendInvitationFriend()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            TestSendInvitation(member, friend.Id, Message, SuccessfulSendInvitation);

            // Check.

            AssertInvitation(member.Id, friend.Id, Message);
        }

        private void PerformSearch(string name, string emailAddress, params Member[] expectedMembers)
        {
            _txtName.Text = name;
            _txtEmailAddress.Text = emailAddress;
            _btnSearch.Click();

            foreach (var expectedMember in expectedMembers)
                AssertPageContains(expectedMember.FullName);
        }

        private void TestInvitation(IUser member, Guid inviteeId, string expectedText)
        {
            LogIn(member);
            GetPage<RepresentativePopupContents>(RepresentativePopupContents.InviteeIdParameter, inviteeId.ToString());
            AssertPageContains(expectedText);
        }

        private void TestSendInvitation(IUser member, Guid inviteeId, string message, string expectedText)
        {
            LogIn(member);
            GetPage<RepresentativePopupContents>(
                RepresentativePopupContents.InviteeIdParameter, inviteeId.ToString(),
                RepresentativePopupContents.SendInvitationParameter, "true",
                RepresentativePopupContents.MessageParameter, message);
            AssertPageContains(expectedText);
        }

        private IList<Member> CreateFriends(Guid memberId, int start, int count)
        {
            var friends = new List<Member>();

            for (var index = start; index < start + count; index++)
            {
                var friend = _memberAccountsCommand.CreateTestMember(index);
                friends.Add(friend);
                _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);
            }

            return friends;
        }
    }
}
