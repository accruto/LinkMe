using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Featured.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Featured
{
    [TestClass]
    public class FeaturedStatisticsTests
        : TestClass
    {
        private readonly IFeaturedCommand _featuredCommand = Resolve<IFeaturedCommand>();
        private readonly IFeaturedQuery _featuredQuery = Resolve<IFeaturedQuery>();

        private const string TitleFormat = "Job title {0}";
        private const string UrlFormat = "~/job/{0}";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUpdateStatistics()
        {
            var statistics = new FeaturedStatistics {CreatedJobAds = 1, Members = 2, MemberSearches = 3, MemberAccesses = 4};
            _featuredCommand.UpdateFeaturedStatistics(statistics);
            AssertStatistics(1, 2, 3, 4, _featuredQuery.GetFeaturedStatistics());
        }

        [TestMethod]
        public void TestUpdateJobAds()
        {
            var jobAd1 = CreateJobAd(1);
            var jobAd2 = CreateJobAd(2);
            var jobAd3 = CreateJobAd(3);

            _featuredCommand.UpdateFeaturedJobAds(new[] { jobAd1, jobAd2 });
            AssertJobAds(new[] { jobAd1, jobAd2 }, _featuredQuery.GetFeaturedJobAds());

            _featuredCommand.UpdateFeaturedJobAds(new[] { jobAd3, jobAd2 });
            AssertJobAds(new[] { jobAd3, jobAd2 }, _featuredQuery.GetFeaturedJobAds());
        }

        private static void AssertJobAds(ICollection<FeaturedItem> expectedJobAds, ICollection<FeaturedItem> jobAds)
        {
            Assert.AreEqual(expectedJobAds.Count, jobAds.Count);
            foreach (var expectedJobAd in expectedJobAds)
                AssertJobAd(expectedJobAd, (from j in jobAds where j.Id == expectedJobAd.Id select j).Single());
        }

        private static void AssertJobAd(FeaturedItem expectedJobAd, FeaturedItem jobAd)
        {
            Assert.AreEqual(expectedJobAd.Title, jobAd.Title);
            Assert.AreEqual(expectedJobAd.Url, jobAd.Url);
        }

        private static FeaturedItem CreateJobAd(int index)
        {
            return new FeaturedItem
            {
                Id = Guid.NewGuid(),
                Title = string.Format(TitleFormat, index),
                Url = string.Format(UrlFormat, index),
            };
        }

        private static void AssertStatistics(int expectedCreatedJobAds, int expectedMembers, int expectedMemberSearches, int expectedMemberAccesses, FeaturedStatistics statistics)
        {
            Assert.AreEqual(expectedCreatedJobAds, statistics.CreatedJobAds);
            Assert.AreEqual(expectedMembers, statistics.Members);
            Assert.AreEqual(expectedMemberSearches, statistics.MemberSearches);
            Assert.AreEqual(expectedMemberAccesses, statistics.MemberAccesses);
        }
    }
}
