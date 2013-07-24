using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.BlockLists
{
    [TestClass]
    public class CreateCandidateBlockListsTests
        : CandidateBlockListsTests
    {
        [TestMethod]
        public void TestGetTemporaryBlockList()
        {
            var employer = CreateEmployer(1);
            var blockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Temporary };

            var shortlistBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            AssertBlockList(employer, blockList, shortlistBlockList);
            AssertBlockList(employer, blockList, _candidateBlockListsQuery.GetBlockList(employer, shortlistBlockList.Id));
            AssertBlockLists(employer, _candidateBlockListsQuery.GetBlockLists(employer));
        }

        [TestMethod]
        public void TestGetPermanentBlockList()
        {
            var employer = CreateEmployer(1);
            var blockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Permanent };

            var flaggedBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            AssertBlockList(employer, blockList, flaggedBlockList);
            AssertBlockList(employer, blockList, _candidateBlockListsQuery.GetBlockList(employer, flaggedBlockList.Id));
            AssertBlockLists(employer, _candidateBlockListsQuery.GetBlockLists(employer));
        }

        [TestMethod]
        public void TestGetTemporaryPermanentBlockLists()
        {
            var employer = CreateEmployer(1);
            var blockList1 = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Permanent };
            var blockList2 = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Temporary };

            var flaggedBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            AssertBlockList(employer, blockList1, flaggedBlockList);
            AssertBlockList(employer, blockList1, _candidateBlockListsQuery.GetBlockList(employer, flaggedBlockList.Id));

            var shortlistBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            AssertBlockList(employer, blockList2, shortlistBlockList);
            AssertBlockList(employer, blockList2, _candidateBlockListsQuery.GetBlockList(employer, shortlistBlockList.Id));

            AssertBlockLists(employer, _candidateBlockListsQuery.GetBlockLists(employer));
        }
    }
}