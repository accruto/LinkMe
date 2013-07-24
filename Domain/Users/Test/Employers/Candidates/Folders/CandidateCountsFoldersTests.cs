using LinkMe.Domain.Users.Employers.Candidates.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class CandidateCountsFoldersTests
        : CandidateFoldersTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        [TestMethod]
        public void TestCandidateCount()
        {
            const int count = 5;

            var employer = CreateEmployer(1);
            var folder = CreatePrivateFolder(employer, 1);
            var member1 = CreateMember(1);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            for (var i = 1; i < count; i++)
            {
                var member = CreateMember(i+1);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }

            var counts = _candidateFoldersQuery.GetInFolderCounts(employer);

            Assert.AreEqual(count, counts[folder.Id]);

            //Now block a member
            var tempBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

            // Add candidate to Temporary Blocklist - should have no effect on folder count
            var tempBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, tempBlockList, member1.Id);
            counts = _candidateFoldersQuery.GetInFolderCounts(employer);

            Assert.AreEqual(1, tempBlockListCount);
            Assert.AreEqual(count, counts[folder.Id]);

            //Now block the member permanently
            var permBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

            // Add candidate to Permanent Blocklist - should have effect on folder count
            var permBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, permBlockList, member1.Id);
            var tempBlockListCounts = _candidateBlockListsQuery.GetBlockedCounts(employer);
            counts = _candidateFoldersQuery.GetInFolderCounts(employer);

            //if the tempBlockListId isn't in the list there are zero entries
            Assert.AreEqual(false, tempBlockListCounts.ContainsKey(tempBlockList.Id));
            Assert.AreEqual(1, permBlockListCount);
            Assert.AreEqual(count - 1, counts[folder.Id]);

        }
    }
}
