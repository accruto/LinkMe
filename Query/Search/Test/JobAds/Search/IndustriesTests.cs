using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public class IndustriesTests
        : ExecuteJobAdSearchTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private static Industry _accounting;
        private static Industry _engineering;
        private static Industry _other;

        [TestInitialize]
        public void TestInitialize()
        {
            _accounting = _industriesQuery.GetIndustry("Accounting");
            _engineering = _industriesQuery.GetIndustry("Engineering");
            _other = _industriesQuery.GetIndustry("Other");
        }

        [TestMethod]
        public void TestNoIndustries()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            var jobAdNone = CreateJobAd(jobPoster, 0, null);
            var jobAdOne = CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = null };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(4, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdNone.Id, jobAdOne.Id, jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestAllIndustries()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            var jobAdNone = CreateJobAd(jobPoster, 0, null);
            var jobAdOne = CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = _industriesQuery.GetIndustries().Select(i => i.Id).ToList() };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(4, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdNone.Id, jobAdOne.Id, jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestFirstIndustry()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            /*var jobAdNone =*/ CreateJobAd(jobPoster, 0, null);
            var jobAdOne = CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = new[] { _accounting.Id }};
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdOne.Id, jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestSecondIndustry()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            /*var jobAdNone =*/ CreateJobAd(jobPoster, 0, null);
            /*var jobAdOne =*/ CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = new[] { _engineering.Id } };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(2, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestMultipleIndustries()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            /*var jobAdNone =*/ CreateJobAd(jobPoster, 0, null);
            var jobAdOne = CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = new[] { _accounting.Id, _engineering.Id } };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdOne.Id, jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        [TestMethod]
        public void TestOtherIndustry()
        {
            var jobPoster = CreateJobPoster();

            // Various industries.

            var jobAdNone = CreateJobAd(jobPoster, 0, null);
            /*var jobAdOne =*/ CreateJobAd(jobPoster, 1, _accounting);
            var jobAdMultiple = CreateJobAd(jobPoster, 2, _accounting, _engineering, _other);
            var jobAdAll = CreateJobAd(jobPoster, 3, _industriesQuery.GetIndustries().ToArray());

            // Search.

            var criteria = new JobAdSearchCriteria { IndustryIds = new[] { _other.Id } };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(3, execution.Results.TotalMatches);
            Assert.IsTrue(execution.Results.JobAdIds.CollectionEqual(new[] { jobAdNone.Id, jobAdMultiple.Id, jobAdAll.Id }));
            AssertHits(execution.Results.IndustryHits, new[] { _accounting, _engineering, _other }, new[] { 3, 2, 3 }, 1);
        }

        private void AssertHits(IEnumerable<KeyValuePair<Guid, int>> hits, IList<Industry> industries, IList<int> expectedHits, int exceptExpectedHits)
        {
            // Check specific hits.

            for (var index = 0; index < industries.Count;  ++index)
                AssertHits(hits, industries[index].Id, expectedHits[index]);

            // Check all industries not passed in.

            foreach (var industry in (from i in _industriesQuery.GetIndustries() select i.Id).Except(from i in industries select i.Id))
                AssertHits(hits, industry, exceptExpectedHits);
        }

        private static void AssertHits(IEnumerable<KeyValuePair<Guid, int>> hits, Guid industryId, int expectedHits)
        {
            Assert.AreEqual(expectedHits, (from h in hits where h.Key == industryId select h.Value).Single());
        }

        private JobAd CreateJobAd(JobPoster jobPoster, int index, params Industry[] industries)
        {
            return CreateJobAd(jobPoster, index, j => j.Description.Industries = industries);
        }
    }
}
