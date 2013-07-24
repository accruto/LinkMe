using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Roles.Representatives.Commands;
using LinkMe.Domain.Test.Files;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Contacts
{
    [TestClass]
    public class SecondDegreeContactsTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();
        private readonly IMemberFriendsCommand _memberFriendsCommand = Resolve<IMemberFriendsCommand>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IRepresentativesCommand _representativesCommand = Resolve<IRepresentativesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestGetSecondDegreeContacts()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // start.

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] { friend1.Id, friend2.Id, friend3.Id }},
                        {second2.Id, new[] { friend2.Id, friend3.Id }},
                        {second3.Id, new[] { friend3.Id }}
                    });
        }

        [TestMethod]
        public void TestFriendDisabled()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // Disable.

            friend3.IsEnabled = false;
            _membersCommand.UpdateMember(friend3);

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] {friend1.Id, friend2.Id}},
                        {second2.Id, new[] {friend2.Id}}
                    });
        }

        [TestMethod]
        public void TestFriendsList()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // Hide friends list.

            friend3.VisibilitySettings.Personal.FirstDegreeVisibility = friend3.VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.FriendsList);
            _membersCommand.UpdateMember(friend3);

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] {friend1.Id, friend2.Id}},
                        {second2.Id, new[] {friend2.Id}}
                    });
        }

        [TestMethod]
        public void TestFirstDegreeName()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // Hide name.

            friend3.VisibilitySettings.Personal.FirstDegreeVisibility = friend3.VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friend3);

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] {friend1.Id, friend2.Id}},
                        {second2.Id, new[] {friend2.Id}}
                    });
        }

        [TestMethod]
        public void TestSecondDegreeName()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // Hide name.

            second2.VisibilitySettings.Personal.Set(PersonalContactDegree.Self, PersonalVisibility.Name);
            _membersCommand.UpdateMember(second2);

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] { friend1.Id, friend2.Id, friend3.Id }},
                        {second3.Id, new[] { friend3.Id }}
                    });
        }

        [TestMethod]
        public void TestSecondDegreeDisabled()
        {
            Member start, friend1, friend2, friend3, second1, second2, second3;
            CreateMembers(out start, out friend1, out friend2, out friend3, out second1, out second2, out second3);

            // Hide name.

            second2.IsEnabled = false;
            _membersCommand.UpdateMember(second2);

            var contacts = _memberContactsQuery.GetSecondDegreeContacts(start.Id, 0);
            AssertAreEqual(
                contacts,
                new Dictionary<Guid, Guid[]>
                    {
                        {second1.Id, new[] { friend1.Id, friend2.Id, friend3.Id }},
                        {second3.Id, new[] { friend3.Id }}
                    });
        }

        [TestMethod]
        public void TestNoFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            Assert.AreEqual(0, _memberContactsQuery.GetSecondDegreeContacts(member.Id, 0).Count);
        }

        [TestMethod]
        public void TestNoFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            CreateFriends(member.Id, 1, 3);
            Assert.AreEqual(0, _memberContactsQuery.GetSecondDegreeContacts(member.Id, 0).Count);
        }

        [TestMethod]
        public void TestFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friend = CreateFriends(member.Id, 1, 1)[0];
            var friendsFriend = CreateFriends(friend.Id, 2, 1)[0];
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friend.Id}}
                    },
                new[] { friend.Id });
        }

        [TestMethod]
        public void TestDisabledFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Disable.

            friends[0].IsEnabled = false;
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add another link.

            _networkingCommand.CreateFirstDegreeLink(friends[1].Id, friendsFriend.Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestDeactivatedFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Deactivate.

            friends[0].IsActivated = false;
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add another link.

            _networkingCommand.CreateFirstDegreeLink(friends[1].Id, friendsFriend.Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestHiddenNameFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Hide.

            friends[0].VisibilitySettings.Personal.FirstDegreeVisibility = friends[0].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add another link.

            _networkingCommand.CreateFirstDegreeLink(friends[1].Id, friendsFriend.Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestHiddenListFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Hide.

            friends[0].VisibilitySettings.Personal.FirstDegreeVisibility = friends[0].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.FriendsList);
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add another link.

            _networkingCommand.CreateFirstDegreeLink(friends[1].Id, friendsFriend.Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestRepresentativeFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Hide.

            friends[0].VisibilitySettings.Personal.FirstDegreeVisibility = friends[0].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add as representative.

            _representativesCommand.CreateRepresentative(member.Id, friends[0].Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestRepresenteeFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Hide.

            friends[0].VisibilitySettings.Personal.FirstDegreeVisibility = friends[0].VisibilitySettings.Personal.FirstDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friends[0]);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });

            // Add as representative.

            _representativesCommand.CreateRepresentative(friends[0].Id, member.Id);
            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });
        }

        [TestMethod]
        public void TestFriendsDisabledFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Disable.

            friendsFriend.IsEnabled = false;
            _membersCommand.UpdateMember(friendsFriend);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });
        }

        [TestMethod]
        public void TestFriendsDeactivatedFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Deactivate.

            friendsFriend.IsActivated = false;
            _membersCommand.UpdateMember(friendsFriend);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });
        }

        [TestMethod]
        public void TestFriendsHiddenNameFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Hide.

            friendsFriend.VisibilitySettings.Personal.SecondDegreeVisibility = friendsFriend.VisibilitySettings.Personal.SecondDegreeVisibility.ResetFlag(PersonalVisibility.Name);
            _membersCommand.UpdateMember(friendsFriend);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });
        }

        [TestMethod]
        public void TestFriendsIgnoredFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 2);
            var friendsFriend = CreateFriends(friends[0].Id, 3, 1)[0];

            AssertFriendsFriends(
                member.Id,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id });

            // Ignore.

            _memberFriendsCommand.IgnoreFriend(member.Id, friendsFriend.Id);
            AssertFriendsFriends(member.Id, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friendsFriend.Id });
        }

        [TestMethod]
        public void TestMinimumFriendsFriends()
        {
            var member = _membersCommand.CreateTestMember(0);
            var friends = CreateFriends(member.Id, 1, 3);
            var friendsFriend = _membersCommand.CreateTestMember(4);

            AssertFriendsFriends(member.Id, 0, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id, friendsFriend.Id });
            AssertFriendsFriends(member.Id, 1, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id, friendsFriend.Id });
            AssertFriendsFriends(member.Id, 2, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id, friendsFriend.Id });
            AssertFriendsFriends(member.Id, 3, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id, friendsFriend.Id });

            // 1 friend.

            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friends[0].Id);

            AssertFriendsFriends(
                member.Id, 0,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 1,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(member.Id, 2, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id });
            AssertFriendsFriends(member.Id, 3, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            // 2 friends.

            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friends[1].Id);

            AssertFriendsFriends(
                member.Id, 0,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 1,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 2,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(member.Id, 3, new Dictionary<Guid, Guid[]>(), new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            // 3 friends.

            _networkingCommand.CreateFirstDegreeLink(friendsFriend.Id, friends[2].Id);

            AssertFriendsFriends(
                member.Id, 0,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id, friends[2].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 1,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id, friends[2].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 2,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id, friends[2].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });

            AssertFriendsFriends(
                member.Id, 3,
                new Dictionary<Guid, Guid[]>
                    {
                        {friendsFriend.Id, new[] {friends[0].Id, friends[1].Id, friends[2].Id}}
                    },
                new[] { friends[0].Id, friends[1].Id, friends[2].Id });
        }

        private void CreateMembers(out Member start, out Member friend1, out Member friend2, out Member friend3, out Member second1, out Member second2, out Member common3)
        {
            start = _membersCommand.CreateTestMember(0);
            friend1 = _membersCommand.CreateTestMember(1);
            friend2 = _membersCommand.CreateTestMember(2);
            friend3 = _membersCommand.CreateTestMember(3);

            second1 = _membersCommand.CreateTestMember(4);
            second1.PhotoId = _filesCommand.CreateTestFile(FileType.ProfilePhoto).Id;
            _membersCommand.UpdateMember(second1);

            second2 = _membersCommand.CreateTestMember(5);
            second2.PhotoId = _filesCommand.CreateTestFile(FileType.ProfilePhoto).Id;
            _membersCommand.UpdateMember(second2);

            common3 = _membersCommand.CreateTestMember(6);
            common3.PhotoId = _filesCommand.CreateTestFile(FileType.ProfilePhoto).Id;
            _membersCommand.UpdateMember(common3);

            _networkingCommand.CreateFirstDegreeLink(start.Id, friend1.Id);
            _networkingCommand.CreateFirstDegreeLink(start.Id, friend2.Id);
            _networkingCommand.CreateFirstDegreeLink(start.Id, friend3.Id);
            _networkingCommand.CreateFirstDegreeLink(friend1.Id, second1.Id);
            _networkingCommand.CreateFirstDegreeLink(friend2.Id, second1.Id);
            _networkingCommand.CreateFirstDegreeLink(friend2.Id, second2.Id);
            _networkingCommand.CreateFirstDegreeLink(friend3.Id, second1.Id);
            _networkingCommand.CreateFirstDegreeLink(friend3.Id, second2.Id);
            _networkingCommand.CreateFirstDegreeLink(friend3.Id, common3.Id);
        }

        private static void AssertAreEqual(ICollection<KeyValuePair<Guid, IList<Guid>>> contacts, ICollection<KeyValuePair<Guid, Guid[]>> expected)
        {
            Assert.AreEqual(expected.Count, contacts.Count);

            // Go one way...

            foreach (var expectedContact in expected)
            {
                var expectedContactKey = expectedContact.Key;
                var contact = (from c in contacts where c.Key == expectedContactKey select c).SingleOrDefault();
                Assert.AreNotEqual(Guid.Empty, contact.Key);
                var firstDegreeIds = (from l in contacts where l.Key == contact.Key select l.Value).SingleOrDefault();
                Assert.IsNotNull(firstDegreeIds);
                Assert.IsTrue(expectedContact.Value.NullableCollectionEqual(firstDegreeIds));
            }

            // Go the other ...

            foreach (var contact in contacts)
            {
                var contactKey = contact.Key;
                var expectedContact = (from c in expected where c.Key == contactKey select c).SingleOrDefault();
                Assert.IsNotNull(expectedContact);
                var firstDegreeIds = (from l in contacts where l.Key == contactKey select l.Value).SingleOrDefault();
                Assert.IsNotNull(firstDegreeIds);
                Assert.IsTrue(expectedContact.Value.NullableCollectionEqual(firstDegreeIds));
            }
        }

        protected IList<Member> CreateFriends(Guid memberId, int start, int count)
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

        private void AssertFriendsFriends(Guid memberId, ICollection<KeyValuePair<Guid, Guid[]>> expected, IEnumerable<Guid> notExpected)
        {
            AssertFriendsFriends(memberId, 0, expected, notExpected);
        }

        private void AssertFriendsFriends(Guid memberId, int minFirstDegreeContacts, ICollection<KeyValuePair<Guid, Guid[]>> expected, IEnumerable<Guid> notExpected)
        {
            var secondDegreeContacts = _memberContactsQuery.GetSecondDegreeContacts(memberId, minFirstDegreeContacts);
            Assert.AreEqual(expected.Count, secondDegreeContacts.Count);

            // Go one way...

            foreach (var contact in expected)
            {
                var contactKey = contact.Key;
                var secondDegreeContact = (from c in secondDegreeContacts where c.Key == contactKey select c).SingleOrDefault();
                Assert.IsNotNull(secondDegreeContact);
                Assert.IsTrue(ListsEqualIgnoreOrder(contact.Value, secondDegreeContact.Value));
            }

            // Go the other ...

            foreach (var secondDegreeContact in secondDegreeContacts)
            {
                var secondDegreeContactKey = secondDegreeContact.Key;
                var contact = (from c in expected where c.Key == secondDegreeContactKey select c).SingleOrDefault();
                Assert.IsNotNull(contact);
                Assert.IsTrue(ListsEqualIgnoreOrder(contact.Value, secondDegreeContact.Value));
            }

            // Are? ...

            foreach (var expectedId in expected)
                Assert.IsTrue(_memberContactsQuery.AreSecondDegreeContacts(memberId, expectedId.Key));
            foreach (var notExpectedId in notExpected)
                Assert.IsFalse(_memberContactsQuery.AreSecondDegreeContacts(memberId, notExpectedId));
        }

        private static bool ListsEqualIgnoreOrder(ICollection<Guid> a, ICollection<Guid> b)
        {
            if (ReferenceEquals(a, b))
                return true;
            if (a == null || b == null)
                return false;
            if (a.Count != b.Count)
                return false;

            var bList = new List<Guid>(b);
            foreach (var aItem in a)
            {
                var found = false;

                for (var i = 0; i < bList.Count; i++)
                {
                    if (Equals(aItem, bList[i]))
                    {
                        bList.RemoveAt(i); // Found an item equal to aItem in list b - remove it.
                        found = true;
                        break;
                    }
                }

                if (!found)
                    return false; // No item equal to aItem found in list b.
            }

            return true; // All items from list a found in list b.
        }
    }
}