using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public abstract class NewJobAdFlowTests
        : MaintainJobAdTests
    {
        protected const string DefaultTitle = "Janitor";
        protected const string DefaultContent = "Sweeping and cleaning";
        protected const string DefaultLocation = "Camberwell VIC 3124";

        protected void AssertNoJobAd(Guid posterId)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(posterId));
            Assert.AreEqual(0, jobAds.Count);
        }

        protected JobAd AssertJobAd(Guid posterId, JobAdStatus expectedStatus, JobAdFeatures features)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(_jobAdsQuery.GetJobAdIds(posterId));
            Assert.AreEqual(1, jobAds.Count);

            var jobAd = jobAds[0];
            Assert.AreEqual(DefaultTitle, jobAd.Title);
            Assert.AreEqual(DefaultContent, jobAd.Description.Content);
            Assert.IsTrue(new[] {_accounting.Id}.CollectionEqual(jobAd.Description.Industries.Select(i => i.Id)));
            Assert.AreEqual(JobTypes.FullTime, jobAd.Description.JobTypes);
            Assert.AreEqual(expectedStatus, jobAd.Status);

            // Check features.

            Assert.AreEqual(features, jobAd.Features);
            var expectedExpiryTime = expectedStatus != JobAdStatus.Open
                ? (DateTime?)null
                : features.IsFlagSet(JobAdFeatures.ExtendedExpiry)
                    ? DateTime.Now.Date.AddDays(30).AddDays(1).AddSeconds(-1)
                    : DateTime.Now.Date.AddDays(14).AddDays(1).AddSeconds(-1);
            Assert.AreEqual(expectedExpiryTime, jobAd.ExpiryTime);

            return jobAd;
        }
    }
}
