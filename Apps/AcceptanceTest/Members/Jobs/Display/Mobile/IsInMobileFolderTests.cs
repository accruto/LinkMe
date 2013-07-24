using System;
using System.Linq;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Mobile
{
    [TestClass]
    public class IsInMobileFolderTests
        : MobileDisplayTests
    {
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        [TestMethod]
        public void TestAnonymousIsInMobileFolder()
        {
            var poster = CreateEmployer();
            var jobAd = PostJobAd(poster);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertIsInMobileFolder(false);
        }

        [TestMethod]
        public void TestIsInMobileFolder()
        {
            var poster = CreateEmployer();
            var jobAd = PostJobAd(poster);

            var member = CreateMember();
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            _memberJobAdListsCommand.AddJobAdToFolder(member, folder, jobAd.Id);
            LogIn(member);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertIsInMobileFolder(true);
        }

        [TestMethod]
        public void TestIsNotInMobileFolder()
        {
            var poster = CreateEmployer();
            var jobAd = PostJobAd(poster);

            var member = CreateMember();
            LogIn(member);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertIsInMobileFolder(false);
        }

        private void AssertIsInMobileFolder(bool isInMobileFolder)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[starts-with(@class, 'button saved')]");
            Assert.IsNotNull(node);
            var classes = node.Attributes["class"].Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(isInMobileFolder, classes.Contains("active"));
        }
    }
}
