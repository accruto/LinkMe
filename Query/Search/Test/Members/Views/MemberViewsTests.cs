using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Views
{
    [TestClass]
    public class MemberViewsTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMemberViewsQuery _memberViewsQuery = Resolve<IMemberViewsQuery>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        private readonly IRepresentativeInvitationsCommand _representativeInvitationsCommand = Resolve<IRepresentativeInvitationsCommand>();

        [TestMethod]
        public void TestSelf()
        {
            var member = _membersCommand.CreateTestMember(1);

            var views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Self, PersonalContactDegree.Self));
            AssertViews(member.Id, views, member);
        }

        [TestMethod]
        public void TestAnonymous()
        {
            var other = _membersCommand.CreateTestMember(1);

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.Anonymous, PersonalContactDegree.Anonymous));
            AssertViews(null, views, other);
        }

        [TestMethod]
        public void TestPublic()
        {
            var member = _membersCommand.CreateTestMember(0);
            var other = _membersCommand.CreateTestMember(1);

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(member.Id, views, other);
        }

        [TestMethod]
        public void TestFriend()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);

            // Member views friend.

            var views = new PersonalViews(new PersonalView(friend, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree));
            AssertViews(member.Id, views, friend);

            // Friend views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.FirstDegree, PersonalContactDegree.FirstDegree));
            AssertViews(friend.Id, views, member);
        }

        [TestMethod]
        public void TestFriendsFriend()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            var friendsFriend = _membersCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friend.Id);

            // Member views friend's friend.

            var views = new PersonalViews(new PersonalView(friendsFriend, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(member.Id, views, friendsFriend);

            // Friend's friend views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(friendsFriend.Id, views, member);
        }

        [TestMethod]
        public void TestRepresentative()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _representativesCommand.CreateRepresentative(member.Id, friend.Id);

            // Member views representative.

            var views = new PersonalViews(new PersonalView(friend, PersonalContactDegree.Representative, PersonalContactDegree.FirstDegree));
            AssertViews(member.Id, views, friend);

            // Representative views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Representee, PersonalContactDegree.FirstDegree));
            AssertViews(friend.Id, views, member);
        }

        [TestMethod]
        public void TestRepresentee()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _representativesCommand.CreateRepresentative(friend.Id, member.Id);

            // Member views representee.

            var views = new PersonalViews(new PersonalView(friend, PersonalContactDegree.Representee, PersonalContactDegree.FirstDegree));
            AssertViews(member.Id, views, friend);

            // Representee views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Representative, PersonalContactDegree.FirstDegree));
            AssertViews(friend.Id, views, member);
        }

        [TestMethod]
        public void TestFriendsFriendExpiredFriendInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            var friendsFriend = _membersCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friend.Id);

            // Create invitation.

            _networkingInvitationsCommand.CreateInvitation(
                new FriendInvitation
                    {
                        InviterId = friendsFriend.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-50)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(friendsFriend, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(member.Id, views, friendsFriend);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(friendsFriend.Id, views, member);
        }

        [TestMethod]
        public void TestFriendsFriendFriendInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            var friendsFriend = _membersCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friend.Id);

            // Create invitation.

            _networkingInvitationsCommand.CreateInvitation(
                new FriendInvitation
                    {
                        InviterId = friendsFriend.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-2)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(friendsFriend, PersonalContactDegree.FirstDegree, PersonalContactDegree.SecondDegree));
            AssertViews(member.Id, views, friendsFriend);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(friendsFriend.Id, views, member);
        }

        [TestMethod]
        public void TestPublicExpiredFriendInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var other = _membersCommand.CreateTestMember(1);

            // Create invitation.

            _networkingInvitationsCommand.CreateInvitation(
                new FriendInvitation
                    {
                        InviterId = other.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-50)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(member.Id, views, other);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(other.Id, views, member);
        }

        [TestMethod]
        public void TestPublicFriendInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var other = _membersCommand.CreateTestMember(1);

            // Create invitation.

            _networkingInvitationsCommand.CreateInvitation(
                new FriendInvitation
                    {
                        InviterId = other.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-2)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.FirstDegree, PersonalContactDegree.Public));
            AssertViews(member.Id, views, other);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(other.Id, views, member);
        }

        [TestMethod]
        public void TestFriendsFriendExpiredRepresentativeInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            var friendsFriend = _membersCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friend.Id);

            // Create invitation.

            _representativeInvitationsCommand.CreateInvitation(
                new RepresentativeInvitation
                    {
                        InviterId = friendsFriend.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-50)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(friendsFriend, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(member.Id, views, friendsFriend);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(friendsFriend.Id, views, member);
        }

        [TestMethod]
        public void TestFriendsFriendRepresentativeInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = _membersCommand.CreateTestMember(1);
            var friendsFriend = _membersCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);
            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friend.Id);

            // Create invitation.

            _representativeInvitationsCommand.CreateInvitation(
                new RepresentativeInvitation
                    {
                        InviterId = friendsFriend.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-2)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(friendsFriend, PersonalContactDegree.Representee, PersonalContactDegree.SecondDegree));
            AssertViews(member.Id, views, friendsFriend);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.SecondDegree, PersonalContactDegree.SecondDegree));
            AssertViews(friendsFriend.Id, views, member);
        }

        [TestMethod]
        public void TestPublicExpiredRepresentativeInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var other = _membersCommand.CreateTestMember(1);

            // Create invitation.

            _representativeInvitationsCommand.CreateInvitation(
                new RepresentativeInvitation
                    {
                        InviterId = other.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-50)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(member.Id, views, other);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(other.Id, views, member);
        }

        [TestMethod]
        public void TestPublicRepresentativeInvitation()
        {
            var member = _membersCommand.CreateTestMember(0);
            var other = _membersCommand.CreateTestMember(1);

            // Create invitation.

            _representativeInvitationsCommand.CreateInvitation(
                new RepresentativeInvitation
                    {
                        InviterId = other.Id,
                        InviteeId = member.Id,
                        Status = RequestStatus.Pending,
                        LastSentTime = DateTime.Now.AddDays(-2)
                    });

            // Member views inviter.

            var views = new PersonalViews(new PersonalView(other, PersonalContactDegree.Representee, PersonalContactDegree.Public));
            AssertViews(member.Id, views, other);

            // Inviter views member.

            views = new PersonalViews(new PersonalView(member, PersonalContactDegree.Public, PersonalContactDegree.Public));
            AssertViews(other.Id, views, member);
        }

        private void AssertViews(Guid? fromId, PersonalViews expectedViews, params Member[] members)
        {
            var views = _memberViewsQuery.GetPersonalViews(fromId, from m in members select m.Id);
            foreach (var member in members)
            {
                Assert.AreEqual(expectedViews[member.Id].EffectiveContactDegree, views[member.Id].EffectiveContactDegree);
                Assert.AreEqual(expectedViews[member.Id].ActualContactDegree, views[member.Id].ActualContactDegree);
            }

            views = _memberViewsQuery.GetPersonalViews(fromId, members);
            foreach (var member in members)
            {
                Assert.AreEqual(expectedViews[member.Id].EffectiveContactDegree, views[member.Id].EffectiveContactDegree);
                Assert.AreEqual(expectedViews[member.Id].ActualContactDegree, views[member.Id].ActualContactDegree);
            }

            foreach (var member in members)
            {
                var view = _memberViewsQuery.GetPersonalView(fromId, member.Id);
                Assert.AreEqual(expectedViews[member.Id].EffectiveContactDegree, view.EffectiveContactDegree);
                Assert.AreEqual(expectedViews[member.Id].ActualContactDegree, view.ActualContactDegree);

                view = _memberViewsQuery.GetPersonalView(fromId, member);
                Assert.AreEqual(expectedViews[member.Id].EffectiveContactDegree, view.EffectiveContactDegree);
                Assert.AreEqual(expectedViews[member.Id].ActualContactDegree, view.ActualContactDegree);
            }
        }
    }
}