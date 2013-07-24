using System;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Mobile
{
    [TestClass]
    public class IsInMobileFolderTests
        : DisplayTests
    {
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        [TestMethod]
        public void TestAnonymousIsInMobileFolder()
        {
            var jobAd = CreateJobAd();

            // Get the job.

            Search(Keywords);
            var node = GetResult(jobAd.Id);

            AssertIsInMobileFolder(node, false);
        }

        [TestMethod]
        public void TestIsInMobileFolder()
        {
            var jobAd = CreateJobAd();

            var member = CreateMember(0);
            var folder = _jobAdFoldersQuery.GetMobileFolder(member);
            _memberJobAdListsCommand.AddJobAdToFolder(member, folder, jobAd.Id);
            LogIn(member);

            // Get the job.

            Search(Keywords);
            var node = GetResult(jobAd.Id);

            AssertIsInMobileFolder(node, true);
        }

        [TestMethod]
        public void TestIsNotInMobileFolder()
        {
            var jobAd = CreateJobAd();

            var member = CreateMember(0);
            LogIn(member);

            // Get the job.

            Search(Keywords);
            var node = GetResult(jobAd.Id);

            AssertIsInMobileFolder(node, false);
        }

        private static void AssertIsInMobileFolder(HtmlNode node, bool isInMobileFolder)
        {
            node = node.SelectSingleNode(".//div[starts-with(@class, 'icon saved')]");
            Assert.IsNotNull(node);
            var classes = node.Attributes["class"].Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(isInMobileFolder, classes.Contains("active"));
        }

        private JobAd CreateJobAd()
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
