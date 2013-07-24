using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class FeaturesUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestMethod]
        public void TestUpdateFeatures()
        {
            var employer = CreateEmployer();

            // None.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = { Content = string.Format(ContentFormat, 1) },
                Features = JobAdFeatures.None
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertFeatures(JobAdFeatures.None, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id));
            AssertFeatures(JobAdFeatures.None, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id));

            // Update.

            jobAd.Features = JobAdFeatures.Logo | JobAdFeatures.Highlight;
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertFeatures(JobAdFeatures.Logo | JobAdFeatures.Highlight, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id));
            AssertFeatures(JobAdFeatures.Logo | JobAdFeatures.Highlight, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id));
        }

        private static void AssertFeatures(JobAdFeatures expectedFeatures, JobAdEntry jobAd)
        {
            Assert.AreEqual(expectedFeatures, jobAd.Features);
        }
    }
}