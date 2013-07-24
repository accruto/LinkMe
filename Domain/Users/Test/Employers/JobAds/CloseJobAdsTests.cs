using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds
{
    [TestClass]
    public class CloseJobAdsTests
        : JobAdsTests
    {
        [TestMethod]
        public void TestCloseLotsOfJobAds()
        {
            var employer = CreateEmployer();

            // Create a lot of job ads.

            var jobAdIds = new List<Guid>();
            for (var index = 0; index < 100; ++index)
                jobAdIds.Add(_jobAdsCommand.PostTestJobAd(employer).Id);

            // Make sure they are all open.

            for (var index = 0; index < 100; ++index)
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdIds[index]);
                Assert.AreEqual(JobAdStatus.Open, jobAd.Status);
            }

            // Now close them.

            foreach (var id in jobAdIds)
            {
                var closeJobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(id);
                _jobAdsCommand.CloseJobAd(closeJobAd);
            }

            // Make sure they are all closed.

            for (var index = 0; index < 100; ++index)
            {
                var jobAd = _jobAdsQuery.GetJobAd<JobAdEntry>(jobAdIds[index]);
                Assert.AreEqual(JobAdStatus.Closed, jobAd.Status);
            }
        }
    }
}