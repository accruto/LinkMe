using System.Linq;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Web.Areas.Employers.Models.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds
{
    [TestClass]
    public class ApiGetJobAdsTests
        : ApiJobAdsTests
    {
        [TestMethod]
        public void TestAnonymous()
        {
            var employer = CreateEmployer();
            _jobAdsCommand.PostTestJobAd(employer);

            var model = JobAds();
            AssertModel(model);
        }

        [TestMethod]
        public void TestOpenJobAds()
        {
            var employer = CreateEmployer();

            // Create some job ads.

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);

            // Test.

            LogIn(employer);
            var model = JobAds();
            AssertModel(model, jobAd1, jobAd2);
        }

        [TestMethod]
        public void TestClosedJobAds()
        {
            var employer = CreateEmployer();

            // Create some job ads.

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Closed);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Closed);

            // Test.

            LogIn(employer);
            var model = JobAds();
            AssertModel(model, jobAd1, jobAd2);
        }

        [TestMethod]
        public void TestJobAdsOrdering()
        {
            var employer = CreateEmployer();

            // Create some job ads.

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Closed);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);
            var jobAd3 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Closed);
            var jobAd4 = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);

            // Test.

            LogIn(employer);
            var model = JobAds();
            AssertModel(model, jobAd1, jobAd2, jobAd3, jobAd4);
        }

        private static void AssertModel(JsonJobAdsModel model, params JobAd[] expectedJobAds)
        {
            AssertJsonSuccess(model);

            // Ensure that if there are both open and closed ads the open appear before the closed.

            JobAdApplicantsModel lastOpenJobAd = null;
            JobAdApplicantsModel firstClosedJobAd =null ;

            try
            {
                lastOpenJobAd = model.JobAds.Last(jobAd => jobAd.Status == JobAdStatus.Open.ToString());
                firstClosedJobAd = model.JobAds.First(jobAd => jobAd.Status == JobAdStatus.Closed.ToString());
            }
            catch
            {
                //suppress errors from executing .last and .first on empty sequences
            }

            if (lastOpenJobAd != null && firstClosedJobAd != null)
            {
                Assert.IsTrue(model.JobAds.IndexOf(lastOpenJobAd) < model.JobAds.IndexOf(firstClosedJobAd));
            }

            Assert.AreEqual(expectedJobAds.Length, model.JobAds.Count);
            foreach (var expectedJobAd in expectedJobAds)
            {
                var expectedJobAdId = expectedJobAd.Id;
                var jobAdModel = (from j in model.JobAds where j.Id == expectedJobAdId select j).Single();
                AssertModel(jobAdModel, expectedJobAd);
            }
        }

        private static void AssertModel(JobAdApplicantsModel model, JobAdEntry jobAd)
        {
            Assert.AreEqual(string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId) ? jobAd.Title : jobAd.Integration.ExternalReferenceId + ": " + jobAd.Title, model.Title);
            Assert.AreEqual(jobAd.Status.ToString(), model.Status);
        }
    }
}
