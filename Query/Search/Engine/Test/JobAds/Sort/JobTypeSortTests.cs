using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Sort
{
    [TestClass]
    public class JobTypeSortTests
        : JobAdSortTests
    {
        [TestMethod]
        public void TestSort()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = (from i in Enumerable.Range(1, 5)
                          select new JobAd { Id = Guid.NewGuid() }).ToList();

            jobAds[0].Description.JobTypes = JobTypes.PartTime;
            jobAds[1].Description.JobTypes = JobTypes.JobShare;
            jobAds[2].Description.JobTypes = JobTypes.Contract;
            jobAds[3].Description.JobTypes = JobTypes.FullTime;
            jobAds[4].Description.JobTypes = JobTypes.Temp;

            foreach (var jobAd in jobAds.Randomise())
            {
                _memberJobAdListsCommand.AddJobAdToFlagList(member, _jobAdFlagListsQuery.GetFlagList(member), jobAd.Id);
                IndexJobAd(jobAd, "LinkMe", false);
            }

            // Order should be FullTime, PartTime, Contract, Temp, JobShare.

            var query = new JobAdSortQuery { SortOrder = JobAdSortOrder.JobType };
            var results = SortFlagged(member, query);
            Assert.IsTrue(new[] { jobAds[3].Id, jobAds[0].Id, jobAds[2].Id, jobAds[4].Id, jobAds[1].Id }.SequenceEqual(results.JobAdIds));

            query = new JobAdSortQuery { SortOrder = JobAdSortOrder.JobType, ReverseSortOrder = false };
            results = SortFlagged(member, query);
            Assert.IsTrue(new[] { jobAds[3].Id, jobAds[0].Id, jobAds[2].Id, jobAds[4].Id, jobAds[1].Id }.SequenceEqual(results.JobAdIds));

            query = new JobAdSortQuery { SortOrder = JobAdSortOrder.JobType, ReverseSortOrder = true };
            results = SortFlagged(member, query);
            Assert.IsTrue(new[] { jobAds[1].Id, jobAds[4].Id, jobAds[2].Id, jobAds[0].Id, jobAds[3].Id }.SequenceEqual(results.JobAdIds));
        }
    }
}
