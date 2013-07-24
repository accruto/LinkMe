using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Search
{
    [TestClass]
    public class RangeTests
        : ExecuteJobAdSearchTests
    {
        [TestMethod]
        public void TestNoRange()
        {
            var jobAds = CreateJobAds(127);

            var criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime }};
            var execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.SequenceEqual(execution.Results.JobAdIds));

            criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true } };
            execution = _executeJobAdSearchCommand.Search(null, criteria, null);

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.Reverse().SequenceEqual(execution.Results.JobAdIds));
        }

        [TestMethod]
        public void TestOpenRange()
        {
            var jobAds = CreateJobAds(127);

            var criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime } };
            var execution = _executeJobAdSearchCommand.Search(null, criteria, new Range());

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.SequenceEqual(execution.Results.JobAdIds));

            criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true } };
            execution = _executeJobAdSearchCommand.Search(null, criteria, new Range());

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.Reverse().SequenceEqual(execution.Results.JobAdIds));
        }

        [TestMethod]
        public void TestMaximum()
        {
            var jobAds = CreateJobAds(127);

            for (var max = 0; max < 200; max += 10)
            {
                var criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime } };
                var execution = _executeJobAdSearchCommand.Search(null, criteria, new Range(0, max));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Take(max).SequenceEqual(execution.Results.JobAdIds));

                criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true } };
                execution = _executeJobAdSearchCommand.Search(null, criteria, new Range(0, max));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Reverse().Take(max).SequenceEqual(execution.Results.JobAdIds));
            }
        }

        [TestMethod]
        public void TestPaging()
        {
            var jobAds = CreateJobAds(127);

            const int page = 25;
            for (var index = 0; index * page < 200; ++index)
            {
                var criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime } };
                var execution = _executeJobAdSearchCommand.Search(null, criteria, new Range(index * page, page));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Skip(index * page).Take(page).SequenceEqual(execution.Results.JobAdIds));

                criteria = new JobAdSearchCriteria { SortCriteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true } };
                execution = _executeJobAdSearchCommand.Search(null, criteria, new Range(index * page, page));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Reverse().Skip(index * page).Take(page).SequenceEqual(execution.Results.JobAdIds));
            }
        }

        private IList<Guid> CreateJobAds(int count)
        {
            var jobPoster = CreateJobPoster();
            var now = DateTime.Now;
            return (from i in Enumerable.Range(0, count)
                    select CreateJobAd(jobPoster, i, j => j.CreatedTime = now.AddDays(-1*i)).Id).ToList();
        }
    }
}
