using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public interface IJobAdsQuery
    {
        T GetJobAd<T>(Guid id) where T : JobAdEntry;
        DateTime? GetLastRefreshTime(Guid id);

        IList<T> GetJobAds<T>(IEnumerable<Guid> ids) where T : JobAdEntry;

        IList<Guid> GetJobAdIds();
        IList<Guid> GetJobAdIds(Guid posterId);
        IList<Guid> GetJobAdIds(Guid posterId, JobAdStatus status);

        IList<Guid> GetOpenJobAdIds();
        IList<Guid> GetOpenJobAdIds(Guid posterId);

        IList<Guid> GetOpenJobAdIds(DateTimeRange createdTimeRange);

        IList<Guid> GetExpiredJobAdIds();
        IList<Guid> GetRecentOpenJobAdIds(Range range);
        IList<Guid> GetJobAdIdsRequiringRefresh(DateTime lastRefreshTime);

        IList<Guid> GetJobAdsWithoutSalaries();
        IList<Guid> GetOpenJobAdsWithoutSalaries();
        IList<Guid> GetOpenJobAdsWithoutSeniorityIndex();

        Tuple<int, int> GetOpenJobAdCounts(Guid posterId);
    }
}
