using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Featured.Queries
{
    public class FeaturedQuery
        : IFeaturedQuery
    {
        private readonly IFeaturedRepository _repository;

        public FeaturedQuery(IFeaturedRepository repository)
        {
            _repository = repository;
        }

        FeaturedStatistics IFeaturedQuery.GetFeaturedStatistics()
        {
            return _repository.GetFeaturedStatistics();
        }

        IList<FeaturedEmployer> IFeaturedQuery.GetFeaturedEmployers()
        {
            return _repository.GetFeaturedEmployers();
        }

        IList<FeaturedItem> IFeaturedQuery.GetFeaturedJobAds()
        {
            return _repository.GetFeaturedJobAds();
        }

        IList<FeaturedItem> IFeaturedQuery.GetFeaturedCandidateSearches()
        {
            return _repository.GetFeaturedCandidateSearches();
        }
    }
}