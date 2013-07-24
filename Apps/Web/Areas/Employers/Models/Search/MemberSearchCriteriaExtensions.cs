using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Presentation.Query.Search.Members;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public static class MemberSearchCriteriaExtensions
    {
        public static ReadOnlyUrl GetSearchUrl(this MemberSearchCriteria criteria)
        {
            var url = SearchRoutes.Search.GenerateUrl().AsNonReadOnly();
            url.QueryString.Add(criteria.GetQueryString());
            return url;
        }

        public static string GetHash(this MemberSearchCriteria criteria)
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

            // These flags correspond to those shown in the filter.

            if (criteria.CandidateStatusFlags == (CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.Unspecified))
            {
                Clone(ref isAlreadyCloned, ref criteria);
                criteria.CandidateStatusFlags = CandidateStatusFlags.All;
            }

            var queryString = criteria.GetQueryString();
            return queryString.Count > 0 ? queryString.ToString() : null;
        }

        private static void Clone(ref bool isAlreadyCloned, ref MemberSearchCriteria criteria)
        {
            if (!isAlreadyCloned)
                criteria = criteria.Clone();
            isAlreadyCloned = true;
        }
    }
}
