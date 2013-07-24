using System;
using System.Net;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.JobAds.SuggestedCandidates
{
    [TestClass]
    public class JobAdsTests
        : SuggestedCandidatesTests
    {
        [TestMethod]
        public void TestUnknownJobAd()
        {
            var employer = CreateEmployer(0, null);
            LogIn(employer);

            Get(HttpStatusCode.NotFound, GetSuggestedCandidatesUrl(Guid.NewGuid()));
        }

        [TestMethod]
        public void TestDraftJobAd()
        {
            var employer = CreateEmployer(0, null);
            var jobAd = CreateJobAd(employer, 0);
            Assert.AreEqual(JobAdStatus.Draft, jobAd.Status);

            LogIn(employer);
            TestSuggestedCandidates(jobAd);
        }

        [TestMethod]
        public void TestOpenJobAd()
        {
            var employer = CreateEmployer(0, null);
            var jobAd = CreateJobAd(employer, 0);
            OpenJobAd(employer, jobAd);
            Assert.AreEqual(JobAdStatus.Open, jobAd.Status);

            LogIn(employer);
            TestSuggestedCandidates(jobAd);
        }

        [TestMethod]
        public void TestOthersJobAd()
        {
            var employer1 = CreateEmployer(1, null);
            var employer2 = CreateEmployer(2, null);
            var jobAd = CreateJobAd(employer2, 0);

            LogIn(employer1);

            // Another employer can still see the job ad.

            TestSuggestedCandidates(jobAd);
            OpenJobAd(employer2, jobAd);
            TestSuggestedCandidates(jobAd);
        }

        [TestMethod]
        public void TestAnonymousJobAd()
        {
            var employer = CreateEmployer(1, null);
            var jobAd = CreateJobAd(employer, 0);
            Assert.AreEqual(JobAdStatus.Draft, jobAd.Status);

            // An anonymous user can still see the job ad.

            TestSuggestedCandidates(jobAd);
            OpenJobAd(employer, jobAd);
            TestSuggestedCandidates(jobAd);
        }

        private void TestSuggestedCandidates(JobAdEntry jobAd)
        {
            var url = GetSuggestedCandidatesUrl(jobAd.Id);
            Get(url);
            AssertUrl(url);
            AssertJobAdTitle(jobAd.Title);
            AssertNoCandidates();

            if (!string.IsNullOrEmpty(jobAd.Integration.ExternalReferenceId))
            {
                // Use the external reference id to check get to the same place.

                var externalReferenceUrl = GetSuggestedCandidatesUrl(jobAd.Integration.ExternalReferenceId);
                Get(externalReferenceUrl);
                AssertUrl(url);
                AssertJobAdTitle(jobAd.Title);
                AssertNoCandidates();
            }
        }
    }
}
