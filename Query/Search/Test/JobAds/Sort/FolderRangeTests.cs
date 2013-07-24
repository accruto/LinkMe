using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Sort
{
    [TestClass]
    public class FolderRangeTests
        : RangeTests
    {
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();

        protected override void UpdateJobAds(IMember member, IEnumerable<Guid> jobAdIds)
        {
            _memberJobAdListsCommand.AddJobAdsToFolder(member, _jobAdFoldersQuery.GetFolders(member).OrderBy(f => f.Name).ToList()[0], jobAdIds);
        }

        protected override JobAdSortExecution Sort(IMember member, JobAdSearchSortCriteria criteria, Range range)
        {
            return _executeJobAdSortCommand.SortFolder(member, _jobAdFoldersQuery.GetFolders(member).OrderBy(f => f.Name).ToList()[0].Id, criteria, range);
        }
    }
}