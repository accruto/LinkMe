using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.BlockLists
{
    [TestClass]
    public class RemoveCandidatesFromCandidateBlockListsTests
        : CandidateBlockListsTests
    {
        [TestMethod]
        public void TestRemoveCandidatesTemporaryBlockList()
        {
            TestRemoveCandidates(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestRemoveCandidatesPermanentBlockList()
        {
            TestRemoveCandidates(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveFromOtherTemporaryBlockList()
        {
            TestRemoveFromOtherBlockList(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveFromOtherPermanentBlockList()
        {
            TestRemoveFromOtherBlockList(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveFromOtherTemporaryBlockListSameOrganisation()
        {
            TestRemoveFromOtherBlockListSameOrganisation(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveFromOtherPermanentBlockListSameOrganisation()
        {
            TestRemoveFromOtherBlockListSameOrganisation(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesTemporaryBlockList()
        {
            TestRemoveAllCandidates(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesPermanentBlockList()
        {
            TestRemoveAllCandidates(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherTemporaryBlockList()
        {
            TestRemoveAllFromOtherBlockList(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherPermanentBlockList()
        {
            TestRemoveAllFromOtherBlockList(GetPermanentBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherTemporaryBlockListSameOrganisation()
        {
            TestRemoveAllFromOtherBlockListSameOrganisation(GetTemporaryBlockList);
        }

        [TestMethod, ExpectedException(typeof(CandidateBlockListsPermissionsException))]
        public void TestCannotRemoveAllFromOtherPermanentBlockListSameOrganisation()
        {
            TestRemoveAllFromOtherBlockListSameOrganisation(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesTemporaryBlockListByType()
        {
            TestRemoveAllCandidatesByType(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesPermanentBlockListByType()
        {
            TestRemoveAllCandidatesByType(GetPermanentBlockList);
        }

        private void TestRemoveFromOtherBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(employer2);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, member.Id));

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidateFromBlockList(employer1, blockList, member.Id));
        }

        private void TestRemoveAllFromOtherBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var members = CreateMembers(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(employer2);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, members[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, members[1].Id));

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(1, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer1, blockList));
        }

        private void TestRemoveFromOtherBlockListSameOrganisation(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(employer2);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, member.Id));

            // If that succeeded then the entry should be there.

            AssertBlockListCandidates(employer2, blockList, new[] { member }, new Member[0]);
            AssertBlockListCandidates(employer2, new[] { member }, new Member[0]);

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(0, _candidateListsCommand.RemoveCandidateFromBlockList(employer1, blockList, member.Id));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertBlockListCandidates(employer1, blockList, new Member[0], new[] { member });
            AssertBlockListCandidates(employer1, new Member[0], new[] { member });
            AssertBlockListCandidates(employer2, blockList, new Member[0], new[] { member });
            AssertBlockListCandidates(employer2, new Member[0], new[] { member });
        }

        private void TestRemoveAllFromOtherBlockListSameOrganisation(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            // Create member and employers.

            var members = CreateMembers(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the blockList for employer 2 and have them add a candidate.

            var blockList = getBlockList(employer2);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, members[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToBlockList(employer2, blockList, members[1].Id));

            // If that succeeded then the entry should be there.

            AssertBlockListCandidates(employer2, blockList, members, new Member[0]);
            AssertBlockListCandidates(employer2, members, new Member[0]);

            // Employer 1 tries to remove candidate from that blockList.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer1, blockList));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertBlockListCandidates(employer1, blockList, new Member[0], members);
            AssertBlockListCandidates(employer1, new Member[0], members);
            AssertBlockListCandidates(employer2, blockList, new Member[0], members);
            AssertBlockListCandidates(employer2, new Member[0], members);
        }

        private void TestRemoveCandidates(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            var members = CreateMembers(3);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id));
            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromBlockList(employer, blockList, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertBlockListCandidates(employer, blockList, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
            AssertBlockListCandidates(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromBlockList(employer, blockList, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertBlockListCandidates(employer, blockList, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
            AssertBlockListCandidates(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllCandidates(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            var members = CreateMembers(3);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id));
            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList));

            // Assert.

            AssertBlockListCandidates(employer, blockList, new Member[0], members);
            AssertBlockListCandidates(employer, new Member[0], members);

            // Remove candidates again.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList));

            // Assert.

            AssertBlockListCandidates(employer, blockList, new Member[0], members);
            AssertBlockListCandidates(employer, new Member[0], members);
        }

        private void TestRemoveAllCandidatesByType(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            var members = CreateMembers(3);

            // Create blockList.

            var employer = CreateEmployer(1);
            var blockList = getBlockList(employer);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id));
            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList.BlockListType));

            // Assert.

            AssertBlockListCandidates(employer, blockList, new Member[0], members);
            AssertBlockListCandidates(employer, new Member[0], members);

            // Remove candidates again.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromBlockList(employer, blockList.BlockListType));

            // Assert.

            AssertBlockListCandidates(employer, blockList, new Member[0], members);
            AssertBlockListCandidates(employer, new Member[0], members);
        }

        private Member[] CreateMembers(int count)
        {
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);
            return members;
        }
    }
}