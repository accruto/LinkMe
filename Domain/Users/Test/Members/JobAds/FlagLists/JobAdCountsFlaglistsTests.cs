using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.FlagLists
{
    [TestClass]
    public class JobAdCountsFlaglistsTests
        : JobAdFlagListsTests
    {
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();

        [TestMethod]
        public void TestJobAdCount()
        {
            const int count = 5;

            var member = CreateMember(1);
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);

            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd1.Id);
            for (var i = 1; i < count; i++)
                _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, _jobAdsCommand.PostTestJobAd(employer).Id);

            Assert.AreEqual(count, _jobAdFlagListsQuery.GetFlaggedCount(member));

            // Now block the member permanently.

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            Assert.AreEqual(1, _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAd1.Id));

            Assert.AreEqual(1, _jobAdBlockListsQuery.GetBlockedCount(member));
            Assert.AreEqual(count - 1, _jobAdFlagListsQuery.GetFlaggedCount(member));
        }
    }
}