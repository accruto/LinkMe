using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Sort
{
    [TestClass]
    public class FlaggedRangeTests
        : RangeTests
    {
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();

        protected override void UpdateJobAds(IMember member, IEnumerable<Guid> jobAdIds)
        {
            _memberJobAdListsCommand.AddJobAdsToFlagList(member, _jobAdFlagListsQuery.GetFlagList(member), jobAdIds);
        }

        protected override JobAdSortExecution Sort(IMember member, JobAdSearchSortCriteria criteria, Range range)
        {
            return _executeJobAdSortCommand.SortFlagged(member, criteria, range);
        }
    }
}