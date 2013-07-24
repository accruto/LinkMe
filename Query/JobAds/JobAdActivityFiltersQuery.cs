using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;

namespace LinkMe.Query.JobAds
{
    public class JobAdActivityFiltersQuery
        : FiltersQueryCore, IJobAdActivityFiltersQuery
    {
        private readonly IJobAdViewsQuery _jobAdViewsQuery;
        private readonly IJobAdApplicationSubmissionsQuery _jobAdApplicationSubmissionsQuery;
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;
        private readonly IMemberJobAdNotesQuery _memberJobAdNotesQuery;
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery;

        public JobAdActivityFiltersQuery(IJobAdViewsQuery jobAdViewsQuery, IJobAdApplicationSubmissionsQuery jobAdApplicationSubmissionsQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberJobAdNotesQuery memberJobAdNotesQuery, IJobAdBlockListsQuery jobAdBlockListsQuery)
        {
            _jobAdViewsQuery = jobAdViewsQuery;
            _jobAdApplicationSubmissionsQuery = jobAdApplicationSubmissionsQuery;
            _jobAdFlagListsQuery = jobAdFlagListsQuery;
            _memberJobAdNotesQuery = memberJobAdNotesQuery;
            _jobAdBlockListsQuery = jobAdBlockListsQuery;
        }

        IList<Guid> IJobAdActivityFiltersQuery.GetIncludeJobAdIds(IMember member, JobAdSearchQuery query)
        {
            return GetIncludeJobAdIds(member, query, null);
        }

        IList<Guid> IJobAdActivityFiltersQuery.GetFlaggedIncludeJobAdIds(IMember member, JobAdSearchQuery query)
        {
            // Include those that are flagged.
            
            var jobAdIds = GetIncludeList(null, _jobAdFlagListsQuery.GetFlaggedJobAdIds(member));
            
            // Include filters.
            
            return GetIncludeJobAdIds(member, query, jobAdIds);
        }

        IList<Guid> IJobAdActivityFiltersQuery.GetExcludeJobAdIds(IMember member, JobAdSearchQuery query)
        {
            // Exclude filters.

            var jobAdIds = GetExcludeJobAdIds(member, query, null);

            // Exclude all blocked job ads.

            jobAdIds = GetExcludeList(jobAdIds, _jobAdBlockListsQuery.GetBlockedJobAdIds(member));
            return jobAdIds == null ? null : jobAdIds.ToList();
        }

        IList<Guid> IJobAdActivityFiltersQuery.GetFlaggedExcludeJobAdIds(IMember member, JobAdSearchQuery query)
        {
            // Exclude filters.

            var jobAdIds = GetExcludeJobAdIds(member, query, null);

            // Exclude all blocked job ads.

            jobAdIds = GetExcludeList(jobAdIds, _jobAdBlockListsQuery.GetBlockedJobAdIds(member));
            return jobAdIds == null ? null : jobAdIds.ToList();
        }

        private IList<Guid> GetIncludeJobAdIds(IMember member, JobAdSearchQuery query, IEnumerable<Guid> jobAdIds)
        {
            // These filters only apply for logged in users.

            if (member != null)
            {
                // Include filters.

                if (query.IsFlagged != null && query.IsFlagged.Value)
                    jobAdIds = GetIncludeList(jobAdIds, _jobAdFlagListsQuery.GetFlaggedJobAdIds(member));

                if (query.HasNotes != null && query.HasNotes.Value)
                    jobAdIds = GetIncludeList(jobAdIds, _memberJobAdNotesQuery.GetHasNotesJobAdIds(member));

                if (query.HasViewed != null && query.HasViewed.Value)
                    jobAdIds = GetIncludeList(jobAdIds, _jobAdViewsQuery.GetViewedJobAdIds(member.Id));

                if (query.HasApplied != null && query.HasApplied.Value)
                    jobAdIds = GetIncludeList(jobAdIds, _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(member.Id));
            }

            return jobAdIds == null ? null : jobAdIds.ToList();
        }

        private IEnumerable<Guid> GetExcludeJobAdIds(IMember member, JobAdSearchQuery query, IEnumerable<Guid> jobAdIds)
        {
            // Exclude filters.

            if (member != null)
            {
                if (query.IsFlagged != null && !query.IsFlagged.Value)
                    jobAdIds = GetExcludeList(jobAdIds, _jobAdFlagListsQuery.GetFlaggedJobAdIds(member));

                if (query.HasNotes != null && !query.HasNotes.Value)
                    jobAdIds = GetExcludeList(jobAdIds, _memberJobAdNotesQuery.GetHasNotesJobAdIds(member));

                if (query.HasViewed != null && !query.HasViewed.Value)
                    jobAdIds = GetExcludeList(jobAdIds, _jobAdViewsQuery.GetViewedJobAdIds(member.Id));

                if (query.HasApplied != null && !query.HasApplied.Value)
                    jobAdIds = GetExcludeList(jobAdIds, _jobAdApplicationSubmissionsQuery.GetSubmittedApplicationJobAdIds(member.Id));
            }

            return jobAdIds;
        }
    }
}