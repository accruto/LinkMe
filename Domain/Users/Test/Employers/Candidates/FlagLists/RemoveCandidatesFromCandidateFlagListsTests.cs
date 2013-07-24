using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.FlagLists
{
    [TestClass]
    public class RemoveCandidatesFromCandidateFlagListsTests
        : CandidateFlagListsTests
    {
        [TestMethod]
        public void TestRemoveCandidatesFlaggedFlagList()
        {
            TestRemoveCandidates(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotRemoveFromOtherFlaggedFlagList()
        {
            TestRemoveFromOtherFlagList(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotRemoveFromOtherFlaggedFlagListSameOrganisation()
        {
            TestRemoveFromOtherFlagListSameOrganisation(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesFlaggedFlagList()
        {
            TestRemoveAllCandidates(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherFlaggedFlagList()
        {
            TestRemoveAllFromOtherFlagList(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherFlaggedFlagListSameOrganisation()
        {
            TestRemoveAllFromOtherFlagListSameOrganisation(GetFlaggedFlagList);
        }

        private void TestRemoveFromOtherFlagList(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member.Id));

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidateFromFlagList(employer1, flagList, member.Id));
        }

        private void TestRemoveAllFromOtherFlagList(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member1.Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member2.Id));

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(1, _candidateListsCommand.RemoveAllCandidatesFromFlagList(employer1, flagList));
        }

        private void TestRemoveFromOtherFlagListSameOrganisation(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member.Id));

            // If that succeeded then the entry should be there.

            AssertFlagListEntries(employer2, new[] { member }, new Member[0]);

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(0, _candidateListsCommand.RemoveCandidateFromFlagList(employer1, flagList, member.Id));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertFlagListEntries(employer1, new Member[0], new[] { member });
            AssertFlagListEntries(employer2, new Member[0], new[] { member });
        }

        private void TestRemoveAllFromOtherFlagListSameOrganisation(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the flagList for employer 2 and have them add a candidate.

            var flagList = getFlagList(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member1.Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFlagList(employer2, flagList, member2.Id));

            // If that succeeded then the entry should be there.

            AssertFlagListEntries(employer2, new[] { member1, member2 }, new Member[0]);

            // Employer 1 tries to remove candidate from that flagList.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFlagList(employer1, flagList));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertFlagListEntries(employer1, new Member[0], new[] { member1, member2 });
            AssertFlagListEntries(employer2, new Member[0], new[] { member1, member2 });
        }

        private void TestRemoveCandidates(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create flagList.

            var employer = CreateEmployer(1);
            var flagList = getFlagList(employer, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id));
            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromFlagList(employer, flagList, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertFlagListEntries(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromFlagList(employer, flagList, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertFlagListEntries(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllCandidates(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create flagList.

            var employer = CreateEmployer(1);
            var flagList = getFlagList(employer, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id));
            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFlagList(employer, flagList));

            // Assert.

            AssertFlagListEntries(employer, new Member[0], members);

            // Remove candidates again.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFlagList(employer, flagList));

            // Assert.

            AssertFlagListEntries(employer, new Member[0], members);
        }
    }
}