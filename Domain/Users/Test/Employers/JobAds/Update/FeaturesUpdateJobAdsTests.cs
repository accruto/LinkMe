using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class FeatureBoostUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestMethod]
        public void TestUpdateFeatureBoost()
        {
            var employer = CreateEmployer();

            // None.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = { Content = string.Format(ContentFormat, 1) },
                FeatureBoost = JobAdFeatureBoost.None
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertFeatureBoost(JobAdFeatureBoost.None, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id));
            AssertFeatureBoost(JobAdFeatureBoost.None, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id));

            // Update.

            jobAd.FeatureBoost = JobAdFeatureBoost.High;
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertFeatureBoost(JobAdFeatureBoost.High, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id));
            AssertFeatureBoost(JobAdFeatureBoost.High, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id));
        }

        private static void AssertFeatureBoost(JobAdFeatureBoost expectedBoost, JobAdEntry jobAd)
        {
            Assert.AreEqual(expectedBoost, jobAd.FeatureBoost);
        }
    }
}