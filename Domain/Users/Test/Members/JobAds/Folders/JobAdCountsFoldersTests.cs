using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.Folders
{
    [TestClass]
    public class JobAdCountsFoldersTests
        : JobAdFoldersTests
    {
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();

        [TestMethod]
        public void TestJobAdCount()
        {
            const int count = 5;

            var member = CreateMember(1);
            var folder = GetFolder(member, 1);

            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            _candidateListsCommand.AddJobAdToFolder(member, folder, jobAd1.Id);

            for (var i = 1; i < count; i++)
                _candidateListsCommand.AddJobAdToFolder(member, folder, _jobAdsCommand.PostTestJobAd(employer).Id);

            Assert.AreEqual(count, _jobAdFoldersQuery.GetInFolderCounts(member)[folder.Id]);

            // Now block the member permanently.

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            Assert.AreEqual(1, _candidateListsCommand.AddJobAdToBlockList(member, blockList, jobAd1.Id));

            Assert.AreEqual(1, _jobAdBlockListsQuery.GetBlockedCount(member));
            Assert.AreEqual(count - 1, _jobAdFoldersQuery.GetInFolderCounts(member)[folder.Id]);
        }
    }
}