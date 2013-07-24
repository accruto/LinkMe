using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Sort
{
    [TestClass]
    public abstract class RangeTests
        : ExecuteJobAdSortTests
    {
        protected readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        protected abstract void UpdateJobAds(IMember member, IEnumerable<Guid> jobAdIds);
        protected abstract JobAdSortExecution Sort(IMember member, JobAdSearchSortCriteria criteria, Range range);

        [TestMethod]
        public void TestNoRange()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = CreateJobAds(member, 127);

            var criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            var execution = Sort(member, criteria, null);

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.SequenceEqual(execution.Results.JobAdIds));

            criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            execution = Sort(member, criteria, null);

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.Reverse().SequenceEqual(execution.Results.JobAdIds));
        }

        [TestMethod]
        public void TestOpenRange()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = CreateJobAds(member, 127);

            var criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
            var execution = Sort(member, criteria, new Range());

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.SequenceEqual(execution.Results.JobAdIds));

            criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            execution = Sort(member, criteria, new Range());

            Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
            Assert.IsTrue(jobAds.Reverse().SequenceEqual(execution.Results.JobAdIds));
        }

        [TestMethod]
        public void TestMaximum()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = CreateJobAds(member, 127);

            for (var max = 0; max < 200; max += 10)
            {
                var criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
                var execution = Sort(member, criteria, new Range(0, max));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Take(max).SequenceEqual(execution.Results.JobAdIds));

                criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
                execution = Sort(member, criteria, new Range(0, max));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Reverse().Take(max).SequenceEqual(execution.Results.JobAdIds));
            }
        }

        [TestMethod]
        public void TestPaging()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = CreateJobAds(member, 127);

            const int page = 25;
            for (var index = 0; index * page < 200; ++index)
            {
                var criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime };
                var execution = Sort(member, criteria, new Range(index * page, page));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Skip(index * page).Take(page).SequenceEqual(execution.Results.JobAdIds));

                criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
                execution = Sort(member, criteria, new Range(index * page, page));

                Assert.AreEqual(jobAds.Count, execution.Results.TotalMatches);
                Assert.IsTrue(jobAds.Reverse().Skip(index * page).Take(page).SequenceEqual(execution.Results.JobAdIds));
            }
        }

        private IList<Guid> CreateJobAds(IMember member, int count)
        {
            var jobPoster = CreateJobPoster();
            var now = DateTime.Now;
            var jobAds = (from i in Enumerable.Range(0, count)
                          select CreateJobAd(jobPoster, i, j => j.CreatedTime = now.AddDays(-1*i)).Id).ToList();
            UpdateJobAds(member, jobAds);
            return jobAds;
        }
    }
}