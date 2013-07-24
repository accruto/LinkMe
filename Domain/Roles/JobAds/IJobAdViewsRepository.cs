using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds
{
    public interface IJobAdViewsRepository
    {
        void CreateJobAdViewing(JobAdViewing viewing);

        int GetViewedCount(Guid jobAdId);
        int GetDistinctViewedCount(Guid jobAdId);
        bool HasViewedJobAd(Guid viewerId, Guid jobAdId);
        IList<Guid> GetViewedJobAdIds(Guid viewerId);
        IList<Guid> GetViewedJobAdIds(Guid viewerId, IEnumerable<Guid> jobAdIds);
    }
}
