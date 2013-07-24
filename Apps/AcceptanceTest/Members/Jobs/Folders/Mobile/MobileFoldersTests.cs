using System;
using System.Linq;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Folders.Mobile
{
    [TestClass]
    public class MobileFoldersTests
        : FoldersTests
    {
        private ReadOnlyUrl _mobileFolderUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
            _mobileFolderUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/folders/mobile");
        }

        [TestMethod]
        public void TestNoJobAds()
        {
            var member = CreateMember();
            LogIn(member);

            Get(_mobileFolderUrl);
            AssertUrl(_mobileFolderUrl);

            AssertPageContains("You have no saved jobs.");
        }

        [TestMethod]
        public void TestSomeJobAds()
        {
            var employer = CreateEmployer();

            const int count = 3;
            var jobAds = new JobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = CreateJobAd(employer, j => j.CreatedTime = DateTime.Now.AddDays(-1 * index));

            // Create employer and folder.

            var member = CreateMember();
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            _memberJobAdListsCommand.AddJobAdsToFolder(member, folder, from j in jobAds select j.Id);

            LogIn(member);
            Get(_mobileFolderUrl);
            AssertUrl(_mobileFolderUrl);

            AssertPageDoesNotContain("You have no saved jobs.");
            AssertMobileJobAds(jobAds);
        }
    }
}
