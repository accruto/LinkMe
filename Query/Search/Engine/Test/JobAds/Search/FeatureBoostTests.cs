using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public class FeatureBoostTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestFeatureBoost()
        {
            // Create job ads.

            var jobAd1 = CreateJobAd(JobAdFeatureBoost.Low, "Best Job in the World", new[] { "good verbal communication", "self management and independency", "good time management" });
            IndexJobAd(jobAd1, "LinkMe");

            var jobAd2 = CreateJobAd(JobAdFeatureBoost.None, "Not so good Job", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd2, "LinkMe");

            var jobAd3 = CreateJobAd(JobAdFeatureBoost.High, "Not so good Job", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd3, "LinkMe");

            var jobAd4 = CreateJobAd(JobAdFeatureBoost.None, "You really don't want this", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd4, "LinkMe");

            var jobAd5 = CreateJobAd(JobAdFeatureBoost.Low, "You really don't want this", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd5, "LinkMe");

            var jobAd6 = CreateJobAd(JobAdFeatureBoost.High, "You really don't want this", new[] { "good verbal communication", "self management and independency", "bullet point 3" });
            IndexJobAd(jobAd6, "LinkMe");

            // High should appear first in all queries.

            var jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("good") };
            var results = Search(jobQuery);
            Assert.AreEqual(6, results.JobAdIds.Count);
            Assert.AreEqual(jobAd3.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd1.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd6.Id, results.JobAdIds[2]);
            Assert.AreEqual(jobAd2.Id, results.JobAdIds[3]);
            Assert.AreEqual(jobAd5.Id, results.JobAdIds[4]);
            Assert.AreEqual(jobAd4.Id, results.JobAdIds[5]);

            // Limited query.

            jobQuery = new JobAdSearchQuery { SortOrder = JobAdSortOrder.Relevance, Keywords = Expression.Parse("really") };
            results = Search(jobQuery);
            Assert.AreEqual(3, results.JobAdIds.Count);
            Assert.AreEqual(jobAd6.Id, results.JobAdIds[0]);
            Assert.AreEqual(jobAd5.Id, results.JobAdIds[1]);
            Assert.AreEqual(jobAd4.Id, results.JobAdIds[2]);
        }

        private static JobAd CreateJobAd(JobAdFeatureBoost boost, string title, IList<string> bulletPoints)
        {
            return new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = title,
                CreatedTime = DateTime.Now.AddDays(-2),
                Description =
                {
                    BulletPoints = bulletPoints,
                    Content = "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                },
                FeatureBoost = boost,
            };
        }
    }
}
