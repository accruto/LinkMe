using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public class JobAdsQuery
        : IJobAdsQuery
    {
        private readonly IJobAdsRepository _repository;

        public JobAdsQuery(IJobAdsRepository repository)
        {
            _repository = repository;
        }

        T IJobAdsQuery.GetJobAd<T>(Guid id)
        {
            return _repository.GetJobAd<T>(id);
        }

        DateTime? IJobAdsQuery.GetLastRefreshTime(Guid id)
        {
            return _repository.GetLastRefreshTime(id);
        }

        IList<T> IJobAdsQuery.GetJobAds<T>(IEnumerable<Guid> ids)
        {
            return _repository.GetJobAds<T>(ids);
        }

        IList<Guid> IJobAdsQuery.GetJobAdIds()
        {
            return _repository.GetJobAdIds();
        }

        IList<Guid> IJobAdsQuery.GetJobAdIds(Guid posterId)
        {
            return _repository.GetJobAdIds(posterId);
        }

        IList<Guid> IJobAdsQuery.GetJobAdIds(Guid posterId, JobAdStatus status)
        {
            return _repository.GetJobAdIds(posterId, status);
        }

        IList<Guid> IJobAdsQuery.GetOpenJobAdIds()
        {
            return _repository.GetOpenJobAdIds();
        }

        IList<Guid> IJobAdsQuery.GetOpenJobAdIds(Guid posterId)
        {
            return _repository.GetOpenJobAdIds(posterId);
        }

        IList<Guid> IJobAdsQuery.GetOpenJobAdIds(DateTimeRange createdTimeRange)
        {
            return _repository.GetOpenJobAdIds(createdTimeRange);
        }

        IList<Guid> IJobAdsQuery.GetExpiredJobAdIds()
        {
            return _repository.GetExpiredJobAdIds();
        }

        IList<Guid> IJobAdsQuery.GetRecentOpenJobAdIds(Range range)
        {
            return _repository.GetRecentOpenJobAdIds(range);
        }

        IList<Guid> IJobAdsQuery.GetJobAdIdsRequiringRefresh(DateTime lastRefreshTime)
        {
            return _repository.GetJobAdIdsRequiringRefresh(lastRefreshTime);
        }

        IList<Guid> IJobAdsQuery.GetJobAdsWithoutSalaries()
        {
            return _repository.GetJobAdIdsWithoutSalaries(false);
        }

        IList<Guid> IJobAdsQuery.GetOpenJobAdsWithoutSalaries()
        {
            return _repository.GetJobAdIdsWithoutSalaries(true);
        }

        IList<Guid> IJobAdsQuery.GetOpenJobAdsWithoutSeniorityIndex()
        {
            return _repository.GetJobAdIdsWithoutSeniorityIndex();
        }

        Tuple<int, int> IJobAdsQuery.GetOpenJobAdCounts(Guid posterId)
        {
            return _repository.GetOpenJobAdCounts(posterId);
        }
    }
}
