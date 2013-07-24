using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Featured.Commands
{
    public interface IFeaturedCommand
    {
        void UpdateFeaturedStatistics(FeaturedStatistics statistics);
        void UpdateFeaturedJobAds(IEnumerable<FeaturedItem> jobAds);
    }
}
