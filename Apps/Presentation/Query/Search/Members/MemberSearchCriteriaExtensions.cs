using LinkMe.Apps.Presentation.Converters;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public static class MemberSearchCriteriaExtensions
    {
        private static readonly ILocationQuery LocationQuery = Container.Current.Resolve<ILocationQuery>();
        private static readonly IIndustriesQuery IndustriesQuery = Container.Current.Resolve<IIndustriesQuery>();

        public static ReadOnlyQueryString GetQueryString(this MemberSearchCriteria criteria)
        {
            return new QueryStringGenerator(new MemberSearchCriteriaConverter(LocationQuery, IndustriesQuery)).GenerateQueryString(criteria);
        }

        public static void PrepareCriteria(this MemberSearchCriteria criteria)
        {
            // This is not really a good place for this. TBD.

            // MaxRecency means infinity.

            if (criteria.Recency != null && criteria.Recency.Value.Days == MemberSearchCriteria.MaxRecency)
                criteria.Recency = null;

            // If include relocating is not set then set the exclude international to its default.

            if (!criteria.IncludeRelocating)
                criteria.IncludeInternational = MemberSearchCriteria.DefaultIncludeInternational;

            if (criteria.Distance == null)
                criteria.Distance = criteria.EffectiveDistance;

            // Ensure NotLooking is not included in the search.

            if (criteria.CandidateStatusFlags != null)
                criteria.CandidateStatusFlags = criteria.CandidateStatusFlags.Value.ResetFlag(CandidateStatusFlags.NotLooking);
            if (criteria.CandidateStatusFlags == null)
                criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers | CandidateStatusFlags.Unspecified;
        }
    }
}