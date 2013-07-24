using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class LastRefreshTimeTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestRefresh()
        {
            var now = DateTime.Now;

            // Create jobAds.

            var jobAd1 = CreateJobAd(now.AddDays(-5));
            IndexJobAd(jobAd1, null);

            var jobAd2 = CreateJobAd(now.AddDays(-10));
            IndexJobAd(jobAd2, null);

            var jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("good") };
            var results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[1]);

            // Sorted.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = false, Keywords = Expression.Parse("good") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[1]);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true, Keywords = Expression.Parse("good") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);

            // Refresh 2.

            IndexJobAd(jobAd2, null, now.AddDays(-1), false);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("good") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);

            // Sorted.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = false, Keywords = Expression.Parse("good") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true, Keywords = Expression.Parse("good") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[1]);
        }

        private JobAd CreateJobAd(DateTime createdTime)
        {
            return new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = "Best Job in the World",
                CreatedTime = createdTime,
                Features = JobAdFeatures.Refresh,
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "good time management" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Salary = new Salary { LowerBound = 20000, UpperBound = 40000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Consulting & Corporate Strategy") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(_locationQuery.GetCountry("Australia"), "VIC")),
                },
            };
        }
    }
}
