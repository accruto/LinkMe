using System;
using System.Net;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public class StatusTests
        : CustomizedDisplayTests
    {
        private const string TitleFormat = "This is the {0} title.";
        private const string ContentFormat = "This is the {0} content.";

        [TestMethod]
        public void TestDraftJobAd()
        {
            var poster = CreateEmployer();
            var jobAd = CreateJobAd(0, poster.Id);
            Assert.AreEqual(JobAdStatus.Draft, jobAd.Status);

            Get(HttpStatusCode.NotFound, GetJobUrl(jobAd));
        }

        [TestMethod]
        public void TestOpenJobAd()
        {
            var poster = CreateEmployer();
            var jobAd = CreateJobAd(0, poster.Id);
            _jobAdsCommand.OpenJobAd(jobAd);
            Assert.AreEqual(JobAdStatus.Open, jobAd.Status);

            Get(HttpStatusCode.OK, GetJobUrl(jobAd));
            AssertPageDoesNotContain("This job is no longer advertised.");
        }

        [TestMethod]
        public void TestDeletedJobAd()
        {
            var poster = CreateEmployer();
            var jobAd = CreateJobAd(0, poster.Id);
            _jobAdsCommand.DeleteJobAd(jobAd);
            Assert.AreEqual(JobAdStatus.Deleted, jobAd.Status);

            Get(HttpStatusCode.NotFound, GetJobUrl(jobAd));
        }

        [TestMethod]
        public void TestClosedJobAd()
        {
            var poster = CreateEmployer();
            var jobAd = CreateJobAd(0, poster.Id);
            _jobAdsCommand.OpenJobAd(jobAd);
            _jobAdsCommand.CloseJobAd(jobAd);
            Assert.AreEqual(JobAdStatus.Closed, jobAd.Status);

            Get(HttpStatusCode.OK, GetJobUrl(jobAd));
            AssertPageContains("This job is no longer advertised.");
        }

        private JobAd CreateJobAd(int index, Guid jobPosterId)
        {
            var jobAd = new JobAd
            {
                Id = Guid.NewGuid(),
                PosterId = jobPosterId,
                Title = string.Format(TitleFormat, index),
                Description =
                {
                    Content = string.Format(ContentFormat, index),
                },
            };

            _jobAdsCommand.CreateJobAd(jobAd);
            return jobAd;
        }
    }
}
