using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Presentation.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Web.Areas.Members.Models.Search
{
    public static class JobAdSearchCriteriaExtensions
    {
        public static string GetHash(this JobAdSearchCriteria criteria)
        {
            if (criteria == null)
                return null;

            // Do not include certain criteria if they correspond to defaults.

            var isAlreadyCloned = false;

            if (criteria.Location != null && criteria.Location.IsCountry && criteria.Location.Country.Id == ActivityContext.Current.Location.Country.Id)
            {
                Clone(ref isAlreadyCloned, ref criteria);
                criteria.Location = null;
            }

            if (criteria.Distance != null && criteria.Distance.Value == 0)
            {
                Clone(ref isAlreadyCloned, ref criteria);
                criteria.Distance = null;
            }

            if (criteria.Recency != null && criteria.Recency.Value.Days == JobAdSearchCriteria.DefaultRecency)
            {
                Clone(ref isAlreadyCloned, ref criteria);
                criteria.Recency = null;
            }

            var queryString = criteria.GetQueryString();
            return queryString.Count > 0 ? queryString.ToString() : null;
        }

        private static void Clone(ref bool isAlreadyCloned, ref JobAdSearchCriteria criteria)
        {
            if (!isAlreadyCloned)
                criteria = criteria.Clone();
            isAlreadyCloned = true;
        }
    }
}
