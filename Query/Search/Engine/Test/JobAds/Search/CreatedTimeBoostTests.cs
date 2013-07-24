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
    public class CreatedTimeBoostTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestCreatedTime()
        {
            var now = DateTime.Now;

            // Create jobAds.

            var jobAd1 = CreateJobAd(now.AddDays(-1), "Best Job in the World", new[] { "good verbal communication", "self management and independency", "good time management" });
            IndexJobAd(jobAd1, null);

            var jobAd2 = CreateJobAd(now.AddDays(-10), "Not so good Job", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd2, null);

            var jobAd3 = CreateJobAd(now.AddDays(-10), "You really don't want this", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd3, null);

            var jobAd4 = CreateJobAd(now.AddDays(-1), "You really don't want this", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd4, null);

            // 3 and 4 are equivalent but 4 should appear before 3 because it is featured.
            // 2 appears before 4 becuase it is a better match for the query.

            var jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("good") };
            var results = Search(jobQuery);
            Assert.AreEqual(4, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd4.Id, results.JobAdIds[2]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[3]);

            // Limited query.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("really") };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.AreEqual(jobAd4.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[1]);
        }

        private JobAd CreateJobAd(DateTime createdTime, string title, IList<string> bulletPoints)
        {
            return new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = title,
                CreatedTime = createdTime,
                Description =
                {
                    BulletPoints = bulletPoints,
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
