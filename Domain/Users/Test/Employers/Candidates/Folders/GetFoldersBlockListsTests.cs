using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class GetFoldersBlockListsTests
        : CandidateFoldersTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        [TestMethod]
        public void TestFoldersAndBlockLists()
        {
            const int count = 10;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = CreateSharedFolder(employer, 1);
            GetShortlistFolder(employer);
            GetMobileFolder(employer);
            _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            _candidateBlockListsQuery.GetPermanentBlockList(employer);

            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
            AssertBlockLists(employer, _candidateBlockListsQuery.GetBlockLists(employer));
        }

        protected static void AssertBlockLists(IEmployer employer, IList<CandidateBlockList> blockLists)
        {
            // Should always have a flagged and shortlist blockList.

            Assert.AreEqual(2, blockLists.Count);

            // Look for the temporary blockList.

            var flaggedBlockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Temporary };
            AssertBlockList(employer, flaggedBlockList, (from f in blockLists where f.BlockListType == BlockListType.Temporary select f).Single());

            // Look for the shortlist blockList.

            var shortlistBlockList = new CandidateBlockList { RecruiterId = employer.Id, BlockListType = BlockListType.Permanent };
            AssertBlockList(employer, shortlistBlockList, (from f in blockLists where f.BlockListType == BlockListType.Permanent select f).Single());
        }

        protected static void AssertBlockList(IEmployer employer, CandidateBlockList expectedBlockList, CandidateBlockList blockList)
        {
            Assert.AreNotEqual(Guid.Empty, blockList.Id);
            Assert.AreNotEqual(DateTime.MinValue, blockList.CreatedTime);

            if (!((expectedBlockList.BlockListType == BlockListType.Permanent || expectedBlockList.BlockListType == BlockListType.Temporary) && expectedBlockList.Id == Guid.Empty))
                Assert.AreEqual(expectedBlockList.Id, blockList.Id);
            Assert.AreEqual(expectedBlockList.Name, blockList.Name);
            Assert.AreEqual(expectedBlockList.BlockListType, blockList.BlockListType);
            Assert.IsNull(blockList.Name);
            Assert.AreEqual(employer.Id, blockList.RecruiterId);
            Assert.AreEqual(expectedBlockList.RecruiterId, blockList.RecruiterId);
        }
    }
}