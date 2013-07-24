using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.FlagLists
{
    [TestClass]
    public class AddCandidatesToCandidateFlagListsTests
        : CandidateFlagListsTests
    {
        [TestMethod]
        public void TestAddCandidateFlaggedFlagList()
        {
            TestAddCandidate(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestAddCandidatesFlaggedFlagList()
        {
            TestAddCandidates(GetFlaggedFlagList);
        }

        [TestMethod]
        public void TestAddExistingCandidatesFlaggedFlagList()
        {
            TestAddExistingCandidates(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotAddToOtherFlaggedFlagList()
        {
            TestAddToOtherFlagList(GetFlaggedFlagList);
        }

        [TestMethod, ExpectedException(typeof(CandidateFlagListsPermissionsException))]
        public void TestCannotAddToOtherFlaggedFlagListSameOrganisation()
        {
            TestAddToOtherFlagListSameOrganisation(GetFlaggedFlagList);
        }

        private void TestAddToOtherFlagList(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the flagList for employer 2.

            var flagList = getFlagList(employer2, 1);

            // Employer 1 tries to add candidate to that flagList.

            _candidateListsCommand.AddCandidateToFlagList(employer1, flagList, member.Id);
        }

        private void TestAddToOtherFlagListSameOrganisation(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the flagList for employer 2.

            var flagList = getFlagList(employer2, 1);

            // Employer 1 tries to add candidate to that flagList.

            var count = _candidateListsCommand.AddCandidateToFlagList(employer1, flagList, member.Id);

            // If that succeeded then the entry should be there and visible to both.

            Assert.AreEqual(1, count);
            AssertFlagListEntries(employer1, new[] { member }, new Member[0]);
            AssertFlagListEntries(employer2, new[] { member }, new Member[0]);
        }

        private void TestAddCandidate(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create flagList.

            var employer = CreateEmployer(1);
            var flagList = getFlagList(employer, 1);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var flagListCount = _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[index].Id);
                Assert.AreEqual(index + 1, flagListCount);
            }

            // Assert.

            AssertFlagListEntries(employer, members, new Member[0]);
        }

        private void TestAddCandidates(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create flagList.

            var employer = CreateEmployer(1);
            var flagList = getFlagList(employer, 1);

            // Add candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id));

            // Assert.

            AssertFlagListEntries(employer, members, new Member[0]);
        }

        private void TestAddExistingCandidates(Func<IEmployer, int, CandidateFlagList> getFlagList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create flagList.

            var employer = CreateEmployer(1);
            var flagList = getFlagList(employer, 1);

            // Add a couple of candidates.

            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[1].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[0].Id));

            // Add all again.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id));

            // Assert.

            AssertFlagListEntries(employer, members, new Member[0]);
        }
    }
}