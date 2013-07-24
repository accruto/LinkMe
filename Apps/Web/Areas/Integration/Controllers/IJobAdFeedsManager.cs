using System;
using System.Collections.Generic;
using LinkMe.Apps.Services.JobAds;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Web.Areas.Integration.Controllers
{
    public interface IJobAdFeedsManager
    {
        IList<JobAdFeedElement> GetJobAdFeed(IEnumerable<Guid> industryIds, DateTime? modifiedSince);
        IList<JobAdFeedElement> GetJobAdFeed(IEnumerable<JobAdSearchCriteria> criteria, IEnumerable<Guid> industryIds, DateTime? modifiedSince);
        IList<Guid> GetJobAdIdFeed();
    }
}
