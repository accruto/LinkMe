using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Linq;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Sort
{
    [TestClass]
    public class CreatedTimeSortTests
        : JobAdSortTests
    {
        [TestMethod]
        public void TestSort()
        {
            var member = new Member { Id = Guid.NewGuid() };
            var jobAds = (from i in Enumerable.Range(1, 5)
                          select new JobAd { Id = Guid.NewGuid(), CreatedTime = DateTime.Now.AddDays(-1 * i) }).ToList();

            foreach (var jobAd in jobAds.Randomise())
            {
                _memberJobAdListsCommand.AddJobAdToFlagList(member, _jobAdFlagListsQuery.GetFlagList(member), jobAd.Id);
                IndexJobAd(jobAd, "LinkMe", false);
            }

            var query = new JobAdSortQuery { SortOrder = JobAdSortOrder.CreatedTime };
            var results = SortFlagged(member, query);
            Assert.IsTrue(jobAds.Select(j => j.Id).SequenceEqual(results.JobAdIds));

            query = new JobAdSortQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = false };
            results = SortFlagged(member, query);
            Assert.IsTrue(jobAds.Select(j => j.Id).SequenceEqual(results.JobAdIds));

            query = new JobAdSortQuery { SortOrder = JobAdSortOrder.CreatedTime, ReverseSortOrder = true };
            results = SortFlagged(member, query);
            Assert.IsTrue(jobAds.Select(j => j.Id).Reverse().SequenceEqual(results.JobAdIds));
        }
    }
}
