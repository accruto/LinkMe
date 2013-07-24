using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.JobAds
{
    public interface IJobAdSortFiltersQuery
    {
        IList<Guid> GetFolderIncludeJobAdIds(IMember member, Guid folderId);
        IList<Guid> GetFlaggedIncludeJobAdIds(IMember member);
        IList<Guid> GetBlockedIncludeJobAdIds(IMember member);

        IList<Guid> GetFolderExcludeJobAdIds(IMember member, Guid folderId);
        IList<Guid> GetFlaggedExcludeJobAdIds(IMember member);
        IList<Guid> GetBlockedExcludeJobAdIds(IMember member);
    }
}