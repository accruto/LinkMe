using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class HasViewedTests
        : DisplayTests
    {
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();

        [TestMethod]
        public void TestHasViewed()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();
            _jobAdViewsCommand.ViewJobAd(member.Id, jobAd.Id);

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon viewed active']"));
            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon viewed ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(model.JobAds[0].HasViewed);
        }

        [TestMethod]
        public void TestHasNotViewed()
        {
            var member = CreateMember(0);
            var jobAd = CreateJobAd();

            LogIn(member);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            Assert.IsNull(node.SelectSingleNode(".//div[@class='icon viewed active']"));
            Assert.IsNotNull(node.SelectSingleNode(".//div[@class='icon viewed ']"));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsFalse(model.JobAds[0].HasViewed);
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
