using LinkMe.Domain.Users.Members.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.BlockLists
{
    [TestClass]
    public class CreateJobAdBlockListsTests
        : JobAdBlockListsTests
    {
        [TestMethod]
        public void TestGetBlockList()
        {
            var member = CreateMember(1);
            var blockList = new JobAdBlockList { MemberId = member.Id, BlockListType = BlockListType.Permanent };
            AssertBlockList(member, blockList, _jobAdBlockListsQuery.GetBlockList(member));
        }
    }
}