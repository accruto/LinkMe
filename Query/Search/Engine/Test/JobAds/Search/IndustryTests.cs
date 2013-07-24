using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class IndustryTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestIndustry()
        {
            var accounting = _industriesQuery.GetIndustry("Accounting");
            var administration = _industriesQuery.GetIndustry("Administration");
            var construction = _industriesQuery.GetIndustry("Construction");
            var other = _industriesQuery.GetIndustry("Other");

            // One each in each industry.

            var jobAd0 = CreateJobAd(0, accounting);
            IndexJobAd(jobAd0, "LinkMe");

            var jobAd1 = CreateJobAd(0, administration);
            IndexJobAd(jobAd1, "LinkMe");

            // One in all.

            var jobAd2 = CreateJobAd(0, accounting, administration, construction);
            IndexJobAd(jobAd2, "LinkMe");

            // One in none.

            var jobAd3 = CreateJobAd(0);
            IndexJobAd(jobAd3, "LinkMe");

            // Search.

            var jobQuery = new JobAdSearchQuery();
            AssertJobAds(Search(jobQuery), jobAd0, jobAd1, jobAd2, jobAd3);

            jobQuery.IndustryIds = new[] { accounting.Id };
            AssertJobAds(Search(jobQuery), jobAd0, jobAd2);

            jobQuery.IndustryIds = new[] { administration.Id };
            AssertJobAds(Search(jobQuery), jobAd1, jobAd2);

            jobQuery.IndustryIds = new[] { construction.Id };
            AssertJobAds(Search(jobQuery), jobAd2);

            jobQuery.IndustryIds = new[] { other.Id };
            AssertJobAds(Search(jobQuery), jobAd3);

            jobQuery.IndustryIds = new[] { accounting.Id, administration.Id };
            AssertJobAds(Search(jobQuery), jobAd0, jobAd1, jobAd2);

            jobQuery.IndustryIds = new[] { accounting.Id, administration.Id, construction.Id };
            AssertJobAds(Search(jobQuery), jobAd0, jobAd1, jobAd2);

            jobQuery.IndustryIds = new[] { accounting.Id, administration.Id, other.Id };
            AssertJobAds(Search(jobQuery), jobAd0, jobAd1, jobAd2, jobAd3);

            jobQuery.IndustryIds = (from i in _industriesQuery.GetIndustries() select i.Id).ToList();
            AssertJobAds(Search(jobQuery), jobAd0, jobAd1, jobAd2, jobAd3);
        }

        private static void AssertJobAds(JobAdSearchResults results, params JobAd[] jobAds)
        {
            Assert.AreEqual(jobAds.Length, results.JobAdIds.Count);
            Assert.IsTrue(results.JobAdIds.CollectionEqual(from j in jobAds select j.Id));
        }

        private static JobAd CreateJobAd(int index, params Industry[] industries)
        {
            return new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = string.Format("Job Ad {0}", index),
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Salary = new Salary { LowerBound = 20000, UpperBound = 40000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                    Industries = industries == null || industries.Length == 0 ? null : industries.ToList(),
                }
            };
        }
    }
}
