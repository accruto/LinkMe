using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Test.Employers.Candidates.FlagLists;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Flaglists
{
    [TestClass]
    public class CandidateCountsFlaglistsTests
        : CandidateFlagListsTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        [TestMethod]
        public void TestCandidateCount()
        {
            const int count = 5;

            var employer = CreateEmployer(1);
            var flaglist = _candidateFlagListsQuery.GetFlagList(employer);
            var member1 = CreateMember(1);
            _candidateListsCommand.AddCandidateToFlagList(employer, flaglist, member1.Id);

            for (var i = 1; i < count; i++)
            {
                var member = CreateMember(i+1);
                _candidateListsCommand.AddCandidateToFlagList(employer, flaglist, member.Id);
            }

            var flagListCount = _candidateFlagListsQuery.GetFlaggedCount(employer);
            Assert.AreEqual(count, flagListCount);

            //Now block a member
            var tempBlockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);

            // Add candidate to Temporary Blocklist - should have no effect on folder count
            var tempBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, tempBlockList, member1.Id);
            flagListCount = _candidateFlagListsQuery.GetFlaggedCount(employer);

            Assert.AreEqual(1, tempBlockListCount);
            Assert.AreEqual(count, flagListCount);

            //Now block the member permanently
            var permBlockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

            // Add candidate to Permanent Blocklist - should have effect on folder count
            var permBlockListCount = _candidateListsCommand.AddCandidateToBlockList(employer, permBlockList, member1.Id);
            var tempBlockListCounts = _candidateBlockListsQuery.GetBlockedCounts(employer);
            flagListCount = _candidateFlagListsQuery.GetFlaggedCount(employer);

            //if the tempBlockListId isn't in the list there are zero entries
            Assert.AreEqual(false, tempBlockListCounts.ContainsKey(tempBlockList.Id));
            Assert.AreEqual(1, permBlockListCount);
            Assert.AreEqual(count - 1, flagListCount);

        }
    }
}
