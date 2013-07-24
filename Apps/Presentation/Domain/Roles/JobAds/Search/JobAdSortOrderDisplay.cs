using System.Collections.Generic;
using System.Linq;
using LinkMe.Query.JobAds;

namespace LinkMe.Apps.Presentation.Domain.Roles.JobAds.Search
{
    public static class JobAdSortOrderDisplay
    {
        private static readonly IDictionary<JobAdSortOrder, string> Texts = new Dictionary<JobAdSortOrder, string>
        {
            {JobAdSortOrder.Relevance, "Relevance"},
            {JobAdSortOrder.CreatedTime, "Date posted"},
            {JobAdSortOrder.Distance, "Distance"},
            {JobAdSortOrder.Flagged, "Flagged"},
            {JobAdSortOrder.JobType, "Job type"},
            {JobAdSortOrder.Salary, "Salary"},
        };

        public static JobAdSortOrder[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this JobAdSortOrder sortOrder)
        {
            string text;
            return Texts.TryGetValue(sortOrder, out text) ? text : null;
        }
    }
}