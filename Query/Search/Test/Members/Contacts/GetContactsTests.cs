using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Contacts
{
    [TestClass]
    public class GetContactsTests
        : TestClass
    {
        private readonly IMembersCommand _memberAccountsCommand = Resolve<IMembersCommand>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly INetworkingInvitationsCommand _networkingInvitationsCommand = Resolve<INetworkingInvitationsCommand>();
        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();

        private const string FirstName = "John";
        private const string LastName1 = "Smith";
        private const string LastName2 = "Brown";
        private const string EmailAddress1 = "member1@test.linkme.net.au";
        private const string EmailAddress2 = "member2@test.linkme.net.au";

        [TestMethod]
        public void TestByName()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);
            var deactivated = _memberAccountsCommand.CreateTestMember(EmailAddress2, FirstName, LastName2);

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            deactivated.IsActivated = false;
            deactivated.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(deactivated);

            TestByName(member.Id, activated.Id, deactivated.Id);
        }

        [TestMethod]
        public void TestByNameWithFirstDegreeContact()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);
            var deactivated = _memberAccountsCommand.CreateTestMember(EmailAddress2, FirstName, LastName2);

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            deactivated.IsActivated = false;
            deactivated.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(deactivated);

            // Create first degree contacts.

            _networkingCommand.CreateFirstDegreeLink(member.Id, activated.Id);
            _networkingCommand.CreateFirstDegreeLink(member.Id, deactivated.Id);

            TestByName(member.Id, activated.Id, deactivated.Id);
        }

        [TestMethod]
        public void TestByNameWithSecondDegreeContact()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);
            var deactivated = _memberAccountsCommand.CreateTestMember(EmailAddress2, FirstName, LastName2);

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            deactivated.IsActivated = false;
            deactivated.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(deactivated);

            // Create second degree contact.

            var friend = _memberAccountsCommand.CreateTestMember(3);
            _networkingCommand.CreateFirstDegreeLink(friend.Id, activated.Id);
            _networkingCommand.CreateFirstDegreeLink(friend.Id, deactivated.Id);
            _networkingCommand.CreateFirstDegreeLink(friend.Id, member.Id);

            TestByName(member.Id, activated.Id, deactivated.Id);
        }

        [TestMethod]
        public void TestByNameWithInvitation()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);
            var deactivated = _memberAccountsCommand.CreateTestMember(EmailAddress2, FirstName, LastName2);

            member.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.SendInvites);
            _memberAccountsCommand.UpdateMember(member);

            deactivated.IsActivated = false;
            _memberAccountsCommand.UpdateMember(deactivated);

            // Create invitations.

            _networkingInvitationsCommand.SendInvitation(new FriendInvitation { InviterId = activated.Id, InviteeId = member.Id });
            _networkingInvitationsCommand.SendInvitation(new FriendInvitation { InviterId = deactivated.Id, InviteeId = member.Id });

            TestByName(member.Id, activated.Id, deactivated.Id);
        }

        [TestMethod]
        public void TestByEmailWithFirstDegreeContact()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.EmailAddress);
            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            // Create contact.

            _networkingCommand.CreateFirstDegreeLink(activated.Id, member.Id);
            Assert.AreEqual(activated.Id, _memberContactsQuery.GetContact(member.Id, activated.GetBestEmailAddress().Address));
        }

        [TestMethod]
        public void TestByEmailWithSecondDegreeContact()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);

            var secondDegreeFriend = _memberAccountsCommand.CreateTestMember(3);
            _networkingCommand.CreateFirstDegreeLink(member.Id, activated.Id);
            _networkingCommand.CreateFirstDegreeLink(activated.Id, secondDegreeFriend.Id);

            Assert.AreEqual(secondDegreeFriend.Id, _memberContactsQuery.GetContact(member.Id, secondDegreeFriend.GetBestEmailAddress().Address));

            secondDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            secondDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.EmailAddress);
            _memberAccountsCommand.UpdateMember(secondDegreeFriend);

            Assert.AreEqual(secondDegreeFriend.Id, _memberContactsQuery.GetContact(member.Id, secondDegreeFriend.GetBestEmailAddress().Address));

            // Remove access to name flag for second degree so member shouldn't be found.

            secondDegreeFriend.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(secondDegreeFriend);
            Assert.IsNull(_memberContactsQuery.GetContact(member.Id, secondDegreeFriend.GetBestEmailAddress().Address));
        }

        [TestMethod]
        public void TestByEmail()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var activated = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName, LastName1);

            Assert.IsFalse(_memberContactsQuery.AreFirstDegreeContacts(member.Id, activated.Id));

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            Assert.AreEqual(activated.Id, _memberContactsQuery.GetContact(member.Id, activated.GetBestEmailAddress().Address));

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.EmailAddress);
            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.Public, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);

            Assert.AreEqual(activated.Id, _memberContactsQuery.GetContact(member.Id, activated.GetBestEmailAddress().Address));

            // Remove access to name flag for public so member shouldn't be found.

            activated.VisibilitySettings.Personal.Set(PersonalContactDegree.SecondDegree, PersonalVisibility.Name);
            _memberAccountsCommand.UpdateMember(activated);
            Assert.IsNull(_memberContactsQuery.GetContact(member.Id, activated.GetBestEmailAddress().Address));
        }

        private void TestByName(Guid memberId, Guid activatedId, Guid deactivatedId)
        {
            var ids = _memberContactsQuery.GetContacts(memberId, FirstName, false);
            Assert.IsTrue(ids.Contains(activatedId));
            Assert.IsFalse(ids.Contains(deactivatedId));
        }
    }
}