using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdActivityFiltersQuery
    {
        IList<Guid> GetIncludeJobAdIds(IMember member, JobAdSearchQuery query);
        IList<Guid> GetFlaggedIncludeJobAdIds(IMember member, JobAdSearchQuery query);

        IList<Guid> GetExcludeJobAdIds(IMember member, JobAdSearchQuery query);
        IList<Guid> GetFlaggedExcludeJobAdIds(IMember member, JobAdSearchQuery query);
    }
}