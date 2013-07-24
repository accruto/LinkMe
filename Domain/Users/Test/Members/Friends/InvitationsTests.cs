using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Friends
{
    [TestClass]
    public class InvitationsTests
        : TestClass
    {
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        private const string EmailAddressFormat = "member{0}@test.linkme.net.au";

        [TestInitialize]
        public void InvitationsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod, ExpectedException(typeof(DailyLimitException))]
        public void TestFriendDailyLimit()
        {
            var member = _membersCommand.CreateTestMember(0);

            // Create 10 invitations.

            const int count = 10;
            for (var index = 1; index <= count; ++index)
            {
                var friend = _membersCommand.CreateTestMember(index);
                _memberFriendsCommand.SendInvitation(new FriendInvitation { InviterId = member.Id, InviteeId = friend.Id });
            }

            // Try to send one more.

            var nextFriend = _membersCommand.CreateTestMember(count + 1);
            _memberFriendsCommand.SendInvitation(new FriendInvitation { InviterId = member.Id, InviteeId = nextFriend.Id });
        }

        [TestMethod, ExpectedException(typeof(DailyLimitException))]
        public void TestFriendContactDailyLimit()
        {
            var member = _membersCommand.CreateTestMember(0);

            // Create 10 invitations.

            const int count = 10;
            for (var index = 1; index <= count; ++index)
                _memberFriendsCommand.SendInvitation(new FriendInvitation { InviterId = member.Id, InviteeEmailAddress = string.Format(EmailAddressFormat, index) });

            // Try to send one more.

            _memberFriendsCommand.SendInvitation(new FriendInvitation { InviterId = member.Id, InviteeEmailAddress = string.Format(EmailAddressFormat, count + 1) });
        }

        [TestMethod, ExpectedException(typeof(DailyLimitException))]
        public void TestRepresentativeDailyLimit()
        {
            var member = _membersCommand.CreateTestMember(0);

            // Create 10 invitations.

            const int count = 10;
            for (var index = 1; index <= count; ++index)
            {
                var friend = _membersCommand.CreateTestMember(index);
                _memberFriendsCommand.SendInvitation(new RepresentativeInvitation { InviterId = member.Id, InviteeId = friend.Id });
            }

            // Try to send one more.

            var nextFriend = _membersCommand.CreateTestMember(count + 1);
            _memberFriendsCommand.SendInvitation(new RepresentativeInvitation { InviterId = member.Id, InviteeId = nextFriend.Id });
        }

        [TestMethod, ExpectedException(typeof(DailyLimitException))]
        public void TestRepresentativeContactDailyLimit()
        {
            var member = _membersCommand.CreateTestMember(0);

            // Create 10 invitations.

            const int count = 10;
            for (var index = 1; index <= count; ++index)
                _memberFriendsCommand.SendInvitation(new RepresentativeInvitation { InviterId = member.Id, InviteeEmailAddress = string.Format(EmailAddressFormat, index) });

            // Try to send one more.

            _memberFriendsCommand.SendInvitation(new RepresentativeInvitation { InviterId = member.Id, InviteeEmailAddress = string.Format(EmailAddressFormat, count + 1) });
        }
    }
}
