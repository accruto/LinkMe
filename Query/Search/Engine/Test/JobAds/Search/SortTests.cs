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
    public class SortTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestSort()
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
                CreatedTime = DateTime.Now.AddDays(-2),
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "good time management" },
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
                    JobTypes = JobTypes.Temp,
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
                CreatedTime = DateTime.Now.AddDays(-100),
                Description =
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.PartTime,
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "QLD")),
                }
            };

            IndexJobAd(jobAd3, "LinkMe");

            // Sort by salary.

            var jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Salary };
            var results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[2]);

            // Sort by distance.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Distance, Distance = 3000, Location = new LocationReference(_locationQuery.GetCountrySubdivision(australia, "QLD")) };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[2]);

            // Sort by jobtype.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.JobType };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[2]);

            // Sort by jobtype II.

            jobAd2.Description.JobTypes = JobTypes.All;
            IndexJobAd(jobAd2, "LinkMe", null, false);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.JobType };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            //1 & 2 are equivalent - just make sure PT is last
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[2]);

            // Sort by jobtype III.

            jobAd1.Description.JobTypes |= JobTypes.Temp;
            IndexJobAd(jobAd1, "LinkMe", null, false);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.JobType };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            //1 & 2 are equivalent - just make sure PT is last
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[2]);

            // Sort by jobtype IV.

            jobAd1.Description.JobTypes = JobTypes.Temp | JobTypes.Contract;
            IndexJobAd(jobAd1, "LinkMe", null, false);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.JobType };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[2]);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.JobType, ReverseSortOrder = true };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[2]);

            // Sort by flagged list.

            //TODO: no support for flagging jobads yet

            // sort by relevance

            jobQuery = new JobAdSearchQuery {SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("good")};
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[2]);
           
            // Sort by createTime.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[2]);

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[2]);
        }
    }
}
