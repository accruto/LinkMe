using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Featured
{
    public interface IFeaturedRepository
    {
        FeaturedStatistics GetFeaturedStatistics();
        void UpdateFeaturedStatistics(FeaturedStatistics statistics);

        IList<FeaturedEmployer> GetFeaturedEmployers();
        IList<FeaturedItem> GetFeaturedJobAds();
        IList<FeaturedItem> GetFeaturedCandidateSearches();
        void UpdateFeaturedJobAds(IEnumerable<FeaturedItem> jobAds);
    }
}
