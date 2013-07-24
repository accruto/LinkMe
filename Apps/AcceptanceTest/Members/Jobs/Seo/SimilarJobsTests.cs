using System;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public class SimilarJobsTests
        : SeoTests
    {
        private const string JobTitle = "Manager";
        private ReadOnlyUrl _jobsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _jobsUrl = new ReadOnlyApplicationUrl("~/jobs");
        }

        [TestMethod]
        public void TestSimilarJobs()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer, JobTitle, null, null, null);

            var expectedUrl = GetSimilarJobsUrl(jobAd.Id);
            TestUrl(expectedUrl);

            var url = GetOldSimilarJobsUrl(jobAd.Id);
            TestUrl(expectedUrl, url);
        }

        [TestMethod]
        public void TestSimilarJobsNoJobId()
        {
            // Some how google got it.

            var url = GetOldSimilarJobsUrl();
            TestUrl(_jobsUrl, url);
        }

        private static ReadOnlyUrl GetSimilarJobsUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/members/jobs/similar/" + jobAdId);
        }

        private static ReadOnlyUrl GetOldSimilarJobsUrl()
        {
            return new ReadOnlyApplicationUrl("~/ui/unregistered/SimilarJobs.aspx");
        }

        private static ReadOnlyUrl GetOldSimilarJobsUrl(Guid jobAdId)
        {
            return new ReadOnlyApplicationUrl("~/ui/unregistered/SimilarJobs.aspx", new ReadOnlyQueryString("jobAdId", jobAdId.ToString()));
        }
    }
}
