using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Create
{
    [TestClass]
    public class IndustryCreateJobAdTests
        : CreateJobAdTests
    {
        [TestMethod]
        public void TestNullIndustries()
        {
            TestIndustries(j => j.Description.Industries = null);
        }

        [TestMethod]
        public void TestEmptyIndustries()
        {
            TestIndustries(j => j.Description.Industries = new List<Industry>());
        }

        [TestMethod]
        public void TestNullIndustry()
        {
            TestIndustries(j => j.Description.Industries = new List<Industry> { null });
        }

        [TestMethod]
        public void TestDistinctIndustries()
        {
            var industry = _industriesQuery.GetIndustries()[0];
            TestIndustries(j => j.Description.Industries = new List<Industry> { industry, industry, industry }, industry);
        }

        private void TestIndustries(Action<JobAd> action, params Industry[] expectedIndustries)
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(poster.Id, 0);
            action(jobAd);
            _jobAdsCommand.CreateJobAd(jobAd);
            AssertIndustries(jobAd.Id, expectedIndustries);
        }

        private void AssertIndustries(Guid jobAdId, params Industry[] expectedIndustries)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (expectedIndustries.Length == 0)
            {
                Assert.IsNull(jobAd.Description.Industries);
            }
            else
            {
                Assert.IsNotNull(jobAd.Description.Industries);
                Assert.AreNotEqual(0, jobAd.Description.Industries.Count);
                Assert.AreEqual(expectedIndustries.Length, jobAd.Description.Industries.Count);
                foreach (var expectedIndustry in expectedIndustries)
                {
                    var expectedIndustryId = expectedIndustry.Id;
                    Assert.IsTrue((from i in jobAd.Description.Industries where i.Id == expectedIndustryId select i).Any());
                }
            }
        }
    }
}