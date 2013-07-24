using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class FilterTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void BasicSearchTest()
        {
            //Create jobAds

            var testIntegratorId = Guid.NewGuid();
            var australia = _locationQuery.GetCountry("Australia");

            var jobAd1 = new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = "Best Job in the World",
                Integration = { IntegratorUserId = testIntegratorId },
                CreatedTime = DateTime.Now,
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Salary = new Salary { LowerBound = 20000, UpperBound = 40000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Consulting & Corporate Strategy") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "VIC")),
                },
            };

            IndexJobAd(jobAd1, "LinkMe");

            var jobAd2 = new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = "Not so good Job",
                CreatedTime = DateTime.Now,
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Salary = new Salary { LowerBound = 200000, UpperBound = 400000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "NSW")),
                }
            };

            IndexJobAd(jobAd2, "LinkMe");

            var jobAd3 = new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = "You really don't want this",
                Integration = { IntegratorUserId = testIntegratorId },
                CreatedTime = DateTime.Now,
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "QLD")),
                }
            };

            IndexJobAd(jobAd3, "LinkMe");
            
            // Search without filter.

            var jobQuery = new JobAdSearchQuery();
            var results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);

            //Search for a salary range

            jobQuery = new JobAdSearchQuery
            {
                Salary = new Salary { LowerBound = 10000, UpperBound = 30000, Rate = SalaryRate.Year, Currency = Currency.AUD }
            };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.IsTrue(new[] {jobAd1.Id, jobAd3.Id}.CollectionEqual(results.JobAdIds));

            //Search for a salary range; exclude empty

            jobQuery = new JobAdSearchQuery
            {
                Salary = new Salary { LowerBound = 10000, UpperBound = 30000, Rate = SalaryRate.Year, Currency = Currency.AUD },
                ExcludeNoSalary = true,
            };
            results = Search(jobQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);

            //search on exact bullet point

            jobQuery = new JobAdSearchQuery
            {
                IncludeSynonyms = false,
                Keywords = Expression.ParseExactPhrase("good verbal"),
            };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);

            //search on exact bullet point again with a phrase that doesn't exist

            jobQuery = new JobAdSearchQuery
            {
                IncludeSynonyms = false,
                Keywords = Expression.ParseExactPhrase("bad verbal"),
            };
            results = Search(jobQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);

            //search on bullet point with any words

            jobQuery = new JobAdSearchQuery
            {
                IncludeSynonyms = false,
                Keywords = Expression.Parse("bad verbal", BinaryOperator.Or),
            };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);

            //search on bullet point with all words

            jobQuery = new JobAdSearchQuery
            {
                IncludeSynonyms = false,
                Keywords = Expression.Parse("bad verbal", BinaryOperator.And),
            };
            results = Search(jobQuery);
            Assert.AreEqual(0, results.JobAdIds.Count);
            
            // search on industry

            jobQuery = new JobAdSearchQuery
            {
                IndustryIds = new List<Guid>{_industriesQuery.GetIndustryByAnyName("Engineering").Id},
            };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.IsTrue(new[] { jobAd2.Id, jobAd3.Id }.CollectionEqual(results.JobAdIds));

            // search on specific job title

            jobQuery = new JobAdSearchQuery {AdTitle = Expression.Parse("good job", BinaryOperator.Or)};
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.IsTrue(new[] { jobAd1.Id, jobAd2.Id }.CollectionEqual(results.JobAdIds));

            // search by location
            jobQuery = new JobAdSearchQuery {Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "QLD")), Distance = 50};
            results = Search(jobQuery);
            Assert.AreEqual(1, results.JobAdIds.Count);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[0]);

            // search by location with large distance to include neighbouring state
            jobQuery = new JobAdSearchQuery { Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "VIC")), Distance = 2000 };
            results = Search(jobQuery);
            Assert.AreEqual(2, results.JobAdIds.Count);
            Assert.IsTrue(new[] { jobAd1.Id, jobAd2.Id }.CollectionEqual(results.JobAdIds));

            //search by minCreatedTime
            jobQuery = new JobAdSearchQuery {Recency = DateTime.Now - DateTime.Now.AddHours(-1) };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
        }
    }
}
