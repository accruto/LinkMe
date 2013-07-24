using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds.Queries;

namespace LinkMe.Query.JobAds
{
    public class JobAdSortFiltersQuery
        : FiltersQueryCore, IJobAdSortFiltersQuery
    {
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery;

        public JobAdSortFiltersQuery(IJobAdFlagListsQuery jobAdFlagListsQuery, IJobAdBlockListsQuery jobAdBlockListsQuery, IJobAdFoldersQuery jobAdFoldersQuery)
        {
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
            _jobAdFoldersQuery = jobAdFoldersQuery;
        }

        IList<Guid> IJobAdSortFiltersQuery.GetFolderIncludeJobAdIds(IMember member, Guid folderId)
        {
            return _jobAdFoldersQuery.GetInFolderJobAdIds(member, folderId);
        }

        IList<Guid> IJobAdSortFiltersQuery.GetFlaggedIncludeJobAdIds(IMember member)
        {
            return _jobAdFlagListsQuery.GetFlaggedJobAdIds(member);
        }

        IList<Guid> IJobAdSortFiltersQuery.GetBlockedIncludeJobAdIds(IMember member)
        {
            return _jobAdBlockListsQuery.GetBlockedJobAdIds(member);
        }

        IList<Guid> IJobAdSortFiltersQuery.GetFolderExcludeJobAdIds(IMember member, Guid folderId)
        {
            // Need to exclude blocked jobs.

            return _jobAdBlockListsQuery.GetBlockedJobAdIds(member);
        }

        IList<Guid> IJobAdSortFiltersQuery.GetFlaggedExcludeJobAdIds(IMember member)
        {
            // Need to exclude blocked jobs.

            return _jobAdBlockListsQuery.GetBlockedJobAdIds(member);
        }

        IList<Guid> IJobAdSortFiltersQuery.GetBlockedExcludeJobAdIds(IMember member)
        {
            return null;
        }
    }
}