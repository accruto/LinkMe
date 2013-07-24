using System;
using LinkMe.Domain.Roles.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public class ViewedCountTests
        : CustomizedDisplayTests
    {
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();

        [TestMethod]
        public void TestNotViewed()
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertViewed(0);
        }

        [TestMethod]
        public void Test1MemberViewed()
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer);

            // View.

            _jobAdViewsCommand.ViewJobAd(Guid.NewGuid(), jobAd.Id);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertViewed(1);
        }

        [TestMethod]
        public void Test2MembersViewed()
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer);

            // View.

            _jobAdViewsCommand.ViewJobAd(Guid.NewGuid(), jobAd.Id);
            _jobAdViewsCommand.ViewJobAd(Guid.NewGuid(), jobAd.Id);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertViewed(2);
        }

        [TestMethod]
        public void TestMultipleMembersViewed()
        {
            var employer = CreateEmployer();
            var jobAd = PostJobAd(employer);

            // View.

            var viewerId1 = Guid.NewGuid();
            var viewerId2 = Guid.NewGuid();
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAd.Id);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAd.Id);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAd.Id);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAd.Id);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAd.Id);

            // Get the job.

            Get(GetJobUrl(jobAd.Id));
            AssertViewed(2);
        }

        private void AssertViewed(int expectedCount)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='jobviewed']/div[@class='viewed']/div[@class='desc']/b");
            Assert.AreEqual(expectedCount.ToString(), node.InnerText);
        }
    }
}