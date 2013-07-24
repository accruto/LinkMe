using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Queries;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public static class JobAdSearchesExtensions
    {
        private const int RecentMonths = 6;

        public static IList<JobAdSearchExecution> GetRecentSearchExecutions(this IJobAdSearchesQuery jobAdSearchesQuery, Guid memberId)
        {
            // Consolidate searches with the same criteria, and then take the latest run instance of each.

            var now = DateTime.Now;
            return (from e in jobAdSearchesQuery.GetJobAdSearchExecutions(memberId, new DateTimeRange(now.AddMonths(-1 * RecentMonths), now))
                    group e by e.Criteria into g
                    select (from x in g orderby x.StartTime descending select x).First()).OrderByDescending(e => e.StartTime).ToList();
        }
    }
}
