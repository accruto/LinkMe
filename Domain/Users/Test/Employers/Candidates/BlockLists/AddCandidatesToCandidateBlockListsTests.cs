using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.BlockLists
{
    [TestClass]
    public class AddCandidatesToCandidateBlockListsTests
        : CandidateBlockListsTests
    {
        [TestMethod]
        public void TestAddCandidateTemporaryBlockList()
        {
            TestAddCandidate(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestAddCandidatePermanentBlockList()
        {
            TestAddCandidate(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestAddCandidatesTemporaryBlockList()
        {
            TestAddCandidates(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestAddCandidatesPermanentBlockList()
        {
            TestAddCandidates(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestAddExistingCandidatesTemporaryBlockList()
        {
            TestAddExistingCandidates(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestAddExistingCandidatesPermanentBlockList()
        {
            TestAddExistingCandidates(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotAddToOtherTemporaryBlockList()
        {
            TestAddToOtherBlockList(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotAddToOtherPermanentBlockList()
        {
            TestAddToOtherBlockList(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotAddToOtherTemporaryBlockListSameOrganisation()
        {
            TestAddToOtherBlockListSameOrganisation(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotAddToOtherPermanentBlockListSameOrganisation()
        {
            TestAddToOtherBlockListSameOrganisation(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestAddToMultipleBlockLists()
        {
            const int count = 10;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);
            var employer3 = CreateEmployer(3);

            var blockList1 = GetTemporaryBlockList(employer1);
            var blockList2 = GetPermanentBlockList(employer1);

            Assert.AreEqual(1, _candidateListsCommand.AddCandidatesToBlockList(employer1, blockList1, new[] { members[3].Id }));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidatesToBlockList(employer1, blockList2, new[] { members[4].Id, members[5].Id }));

            AssertBlockListCandidates(employer1, blockList1, new[] { members[3] }, new[] { members[0], members[1], members[2], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer2, blockList1, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer3, blockList1, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });

            AssertBlockListCandidates(employer1, blockList2, new[] { members[4], members[5] }, new[] { members[0], members[1], members[2], members[3], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer2, blockList2, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer3, blockList2, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            
            AssertBlockListCandidates(employer1, new[] { members[3], members[4], members[5] }, new[] { members[0], members[1], members[2], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer2, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertBlockListCandidates(employer3, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
        }

        private void TestAddToOtherBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the blockList for employer 2.

            var blockList = getBlockList(employer2);

            // Employer 1 tries to add candidate to that blockList.

            _candidateListsCommand.AddCandidateToBlockList(employer1, blockList, member.Id);
        }

        private void TestAddToOtherBlockListSameOrganisation(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the blockList for employer 2.

            var blockList = getBlockList(employer2);

            // Employer 1 tries to add candidate to that blockList.

            var count = _candidateListsCommand.AddCandidateToBlockList(employer1, blockList, member.Id);

            // If that succeeded then the entry should be there and visible to both.

            Assert.AreEqual(1, count);
            AssertBlockListCandidates(employer1, blockList, new[] { member }, new Member[0]);
            AssertBlockListCandidates(employer1, new[] { member }, new Member[0]);
            AssertBlockListCandidates(employer2, blockList, new[] { member }, new Member[0]);
            AssertBlockListCandidates(employer2, new[] { member }, new Member[0]);
        }

        private void TestAddCandidate(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var blockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, blockList, members[index].Id);
                Assert.AreEqual(index + 1, blockListCount);
            }

            // Assert.

            AssertBlockListCandidates(employer, blockList, members, new Member[0]);
            AssertBlockListCandidates(employer, members, new Member[0]);
        }

        private void TestAddCandidates(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id));

            // Assert.

            AssertBlockListCandidates(employer, blockList, members, new Member[0]);
            AssertBlockListCandidates(employer, members, new Member[0]);
        }

        private void TestAddExistingCandidates(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add a couple of candidates.

            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToBlockList(employer, blockList, members[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToBlockList(employer, blockList, members[1].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToBlockList(employer, blockList, members[0].Id));

            // Add all again.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id));

            // Assert.

            AssertBlockListCandidates(employer, blockList, members, new Member[0]);
            AssertBlockListCandidates(employer, members, new Member[0]);
        }
    }
}