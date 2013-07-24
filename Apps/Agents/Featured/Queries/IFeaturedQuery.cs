using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Featured.Queries
{
    public interface IFeaturedQuery
    {
        FeaturedStatistics GetFeaturedStatistics();
        IList<FeaturedEmployer> GetFeaturedEmployers();
        IList<FeaturedItem> GetFeaturedJobAds();
        IList<FeaturedItem> GetFeaturedCandidateSearches();
    }
}
