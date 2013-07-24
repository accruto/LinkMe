using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.JobAds.Queries
{
    public interface IJobAdViewsQuery
    {
        int GetViewedCount(Guid jobAdId);
        int GetDistinctViewedCount(Guid jobAdId);
        bool HasViewedJobAd(Guid viewerId, Guid jobAdId);

        /// <summary>
        /// For performance reasons you should use this method over the one without jobadids
        /// </summary>
        /// <param name="viewerId">The viewer to check</param>
        /// <param name="jobAdIds">The jobAdIds to check</param>
        /// <returns>The viewed jobAdIds from the list supplied</returns>
        IList<Guid> GetViewedJobAdIds(Guid viewerId, IEnumerable<Guid> jobAdIds);
        IList<Guid> GetViewedJobAdIds(Guid viewerId);
    }
}
