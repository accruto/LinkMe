using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobAdViewsQuery
        : IJobAdViewsQuery
    {
        private readonly IJobAdViewsRepository _repository;

        public JobAdViewsQuery(IJobAdViewsRepository repository)
        {
            _repository = repository;
        }

        int IJobAdViewsQuery.GetViewedCount(Guid jobAdId)
        {
            return _repository.GetViewedCount(jobAdId);
        }

        int IJobAdViewsQuery.GetDistinctViewedCount(Guid jobAdId)
        {
            return _repository.GetDistinctViewedCount(jobAdId);
        }

        bool IJobAdViewsQuery.HasViewedJobAd(Guid viewerId, Guid jobAdId)
        {
            return _repository.HasViewedJobAd(viewerId, jobAdId);
        }

        /// <summary>
        /// For performance reasons you should use this method over the one without jobadids
        /// </summary>
        /// <param name="viewerId">The viewer to check</param>
        /// <param name="jobAdIds">The jobAdIds to check</param>
        /// <returns>The viewed jobAdIds from the list supplied</returns>
        IList<Guid> IJobAdViewsQuery.GetViewedJobAdIds(Guid viewerId, IEnumerable<Guid> jobAdIds)
        {
            return _repository.GetViewedJobAdIds(viewerId, jobAdIds);
        }

        IList<Guid> IJobAdViewsQuery.GetViewedJobAdIds(Guid viewerId)
        {
            return _repository.GetViewedJobAdIds(viewerId);
        }
    }
}
