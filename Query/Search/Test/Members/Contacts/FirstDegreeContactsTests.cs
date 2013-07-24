using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Contacts
{
    [TestClass]
    public class FirstDegreeContactsTests
        : TestClass
    {
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();

        private const string EmailFormat = "user{0}@test.linkme.net.au";

        [TestMethod]
        public void TestNoFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            Assert.AreEqual(0, _memberContactsQuery.GetFirstDegreeContacts(member.Id).Count);
        }

        [TestMethod]
        public void TestFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);
            AssertFriends(member.Id, friends, new Member[0]);
        }

        [TestMethod]
        public void TestDisabledFriend()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);

            // Disable.

            friends[1].IsEnabled = false;
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));

            // Disable another.

            friends[0].IsEnabled = false;
            _membersCommand.UpdateMember(friends[0]);
            AssertFriends(member.Id, friends.Skip(2).ToList(), friends.Take(2));

            // Enable.

            friends[1].IsEnabled = true;
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Skip(1).ToList(), friends.Take(1));
        }

        [TestMethod]
        public void TestDeactivatedFriend()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);

            // Deactivate.

            friends[1].IsActivated = false;
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));

            // Deactivate another.

            friends[0].IsActivated = false;
            _membersCommand.UpdateMember(friends[0]);
            AssertFriends(member.Id, friends.Skip(2).ToList(), friends.Take(2));

            // Activate.

            friends[1].IsActivated = true;
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Skip(1).ToList(), friends.Take(1));
        }

        [TestMethod]
        public void TestNameHidden()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);

            // Hide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));

            // Hide another.

            friends[0].VisibilitySettings.Personal.FirstDegreeVisibility = friends[0].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[0]);
            AssertFriends(member.Id, friends.Skip(2).ToList(), friends.Take(2));

            // Unhide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.SetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Skip(1).ToList(), friends.Take(1));
        }

        [TestMethod]
        public void TestRepresentative()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);

            // Hide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));

            // Make a representative.

            _representativesCommand.CreateRepresentative(member.Id, friends[1].Id);
            AssertFriends(member.Id, friends, new Member[0]);

            // Unhide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.SetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends, new Member[0]);

            // Hide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends, new Member[0]);

            // Delete representative.

            _representativesCommand.DeleteRepresentative(member.Id, friends[1].Id);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));
        }

        [TestMethod]
        public void TestRepresentee()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);

            // Hide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));

            // Make a representee.

            _representativesCommand.CreateRepresentative(friends[1].Id, member.Id);
            AssertFriends(member.Id, friends, new Member[0]);

            // Unhide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.SetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends, new Member[0]);

            // Hide.

            friends[1].VisibilitySettings.Personal.FirstDegreeVisibility = friends[1].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[1]);
            AssertFriends(member.Id, friends, new Member[0]);

            // Delete representative.

            _representativesCommand.DeleteRepresentative(friends[1].Id, member.Id);
            AssertFriends(member.Id, friends.Take(1).Concat(friends.Skip(2).Take(1)).ToList(), friends.Skip(1).Take(1));
        }

        [TestMethod]
        public void TestSearch()
        {
            var member = CreateMember();
            var friends1 = CreateFriends(member.Id, "Barney", "Gumble", 0, 3);
            var friends2 = CreateFriends(member.Id, "Lenny", "Leonard", 3, 2);

            // Search.

            PerformSearch(member.Id, "Barney Gumble", false, friends1.ToArray());
            PerformSearch(member.Id, "Lenny Leonard", false, friends2.ToArray());
        }

        [TestMethod]
        public void TestSearchDeactivated()
        {
            var member = CreateMember();
            var friends1 = CreateFriends(member.Id, "Barney", "Gumble", 0, 3);
            var deactivatedFriend = CreateFriends(member.Id, "Lenny", "Leonard", 3, 1)[0];
            deactivatedFriend.IsActivated = false;
            _membersCommand.UpdateMember(deactivatedFriend);

            // Deactivated user should not come up in search.

            PerformSearch(member.Id, "Barney Gumble", false, friends1.ToArray());
            PerformSearch(member.Id, "Lenny Leonard", false);
        }

        [TestMethod]
        public void TestSearchFriendsFriends()
        {
            var member = CreateMember();
            var friend = CreateFriends(member.Id, "Carl", "Carlson", 0, 1)[0];
            CreateFriends(friend.Id, "Barney", "Gumble", 1, 3);
            CreateFriends(friend.Id, "Lenny", "Leonard", 4, 2);

            // Search.

            PerformSearch(member.Id, "Barney Gumble", false);
            PerformSearch(member.Id, "Lenny Leonard", false);
        }

        [TestMethod]
        public void TestSearchNames()
        {
            var member = CreateMember();
            var friends1 = CreateFriends(member.Id, "Barney", "Gumble", 0, 3);
            var friends2 = CreateFriends(member.Id, "Barn", "Gum", 3, 2);
            var friends3 = CreateFriends(member.Id, "Space First", "Space Last", 5, 1);

            // Full name.

            PerformSearch(member.Id, "Barney Gumble", false, friends1.ToArray());

            // Partial first and last name.

            PerformSearch(member.Id, "barne gumb", false, friends1.ToArray());
            PerformSearch(member.Id, "bar gum", false, friends1.Concat(friends2).ToArray());

            // Partial first name.

            PerformSearch(member.Id, "barne", false, friends1.ToArray());
            PerformSearch(member.Id, "bar", false, friends1.Concat(friends2).ToArray());

            // Partial last name.

            PerformSearch(member.Id, "gumb", false, friends1.ToArray());
            PerformSearch(member.Id, "gum", false, friends1.Concat(friends2).ToArray());

            // Some extra symbols.

            PerformSearch(member.Id, "gumb!", false);
            PerformSearch(member.Id, "  barne  $%  gumb", false);

            // - is a valid name character, so no matches.

            PerformSearch(member.Id, "barne-gumb", false);

            // A person with spaces in their first and last name.

            PerformSearch(member.Id, "Space First Space Last", false, friends3.ToArray());
            PerformSearch(member.Id, "Space First Space", false, friends3.ToArray());
            PerformSearch(member.Id, "Space First", false, friends3.ToArray());
            PerformSearch(member.Id, "Space", false, friends3.ToArray());
            PerformSearch(member.Id, "Space Last", false, friends3.ToArray());
            PerformSearch(member.Id, "Space Space Last", false, friends3.ToArray());
            PerformSearch(member.Id, "Space First Space", false, friends3.ToArray());
        }

        [TestMethod]
        public void TestExactMatch()
        {
            var member = CreateMember();
            var friends = CreateFriends(member.Id, "Barney", "Gumble", 0, 3);

            // Search exactly.

            PerformSearch(member.Id, "Barney Gumble", true, friends.ToArray());

            // Search with partial names but look for exact match.

            PerformSearch(member.Id, "bar gum", true);

            // Search with partial names but look for non-exact match.

            PerformSearch(member.Id, "bar gum", false, friends.ToArray());
        }

        [TestMethod]
        public void TestSearchForYourself()
        {
            var member = CreateMember();

            // Check that you can't search for yourself.

            PerformSearch(member.Id, member.FullName, false);
        }

        [TestMethod]
        public void TestSearchByEmailAddress()
        {
            var member = CreateMember();
            var friend = _membersCommand.CreateTestMember("findMe@test.linkme.net.au");
            friend.VisibilitySettings.Personal.Set(PersonalContactDegree.FirstDegree, PersonalVisibility.EmailAddress);
            _membersCommand.UpdateMember(friend);
            _networkingCommand.CreateFirstDegreeLink(member.Id, friend.Id);

            PerformSearch(member.Id, friend.GetBestEmailAddress().Address, friend);

            // Remove access to name so member should not be found.

            friend.VisibilitySettings.Personal.FirstDegreeVisibility = friend.VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friend);

            PerformSearch(member.Id, friend.GetBestEmailAddress().Address);
        }

        private void PerformSearch(Guid memberId, string name, bool exactMatch, params Member[] expectedMembers)
        {
            AssertMembers(_memberContactsQuery.GetFirstDegreeContacts(memberId, name, exactMatch), expectedMembers);
        }

        private void PerformSearch(Guid memberId, string emailAddress, params Member[] expectedMembers)
        {
            var contact = _memberContactsQuery.GetFirstDegreeContact(memberId, emailAddress);
            switch (expectedMembers.Length)
            {
                case 0:
                    Assert.IsNull(contact);
                    break;

                case 1:
                    Assert.IsNotNull(contact);
                    Assert.AreEqual(expectedMembers[0].Id, contact);
                    break;

                default:
                    Assert.Fail("Should not be more than 1 expected member.");
                    break;
            }
        }

        private static void AssertMembers(ICollection<Guid> members, params Member[] expectedMembers)
        {
            if (expectedMembers.Length > 0)
            {
                Assert.AreEqual(expectedMembers.Length, members.Count);
                foreach (var expectedResult in expectedMembers)
                    Assert.IsTrue(members.Contains(expectedResult.Id));
            }
            else
            {
                Assert.AreEqual(0, members.Count);
            }
        }

        private void AssertFriends(Guid memberId, ICollection<Member> expected, IEnumerable<Member> notExpected)
        {
            var firstDegreeContacts = _memberContactsQuery.GetFirstDegreeContacts(memberId);
            Assert.AreEqual(expected.Count, firstDegreeContacts.Count);
            foreach (var expectedFriend in expected)
                Assert.IsTrue(firstDegreeContacts.Contains(expectedFriend.Id));

            foreach (var expectedMember in expected)
                Assert.IsTrue(_memberContactsQuery.AreFirstDegreeContacts(memberId, expectedMember.Id));
            foreach (var notExpectedMember in notExpected)
                Assert.IsFalse(_memberContactsQuery.AreFirstDegreeContacts(memberId, notExpectedMember.Id));
        }

        private Member CreateMember()
        {
            return _membersCommand.CreateTestMember(1000);
        }

        private IList<Member> CreateFriends(Guid memberId, int start, int count)
        {
            var friends = new List<Member>();
            for (var index = start; index < start + count; ++index)
            {
                var friend = _membersCommand.CreateTestMember(index);
                _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);
                friends.Add(friend);
            }

            return friends;
        }

        private IList<Member> CreateFriends(Guid memberId, string firstNameFormat, string lastNameFormat, int startIndex, int count)
        {
            var friends = new List<Member>();

            for (var index = startIndex; index < startIndex + count; ++index)
            {
                var friend = _membersCommand.CreateTestMember(string.Format(EmailFormat, index), string.Format(firstNameFormat, index), string.Format(lastNameFormat, index));
                _networkingCommand.CreateFirstDegreeLink(memberId, friend.Id);

                friends.Add(friend);
            }

            return friends;
        }
    }
}