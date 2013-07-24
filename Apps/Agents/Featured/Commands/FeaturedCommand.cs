using System.Collections.Generic;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Featured.Commands
{
    public class FeaturedCommand
        : IFeaturedCommand
    {
        private readonly IFeaturedRepository _repository;

        public FeaturedCommand(IFeaturedRepository repository)
        {
            _repository = repository;
        }

        void IFeaturedCommand.UpdateFeaturedStatistics(FeaturedStatistics statistics)
        {
            statistics.Prepare();
            statistics.Validate();
            _repository.UpdateFeaturedStatistics(statistics);
        }

        void IFeaturedCommand.UpdateFeaturedJobAds(IEnumerable<FeaturedItem> jobAds)
        {
            foreach (var jobAd in jobAds)
            {
                jobAd.Prepare();
                jobAd.Validate();
            }

            _repository.UpdateFeaturedJobAds(jobAds);
        }
    }
}
