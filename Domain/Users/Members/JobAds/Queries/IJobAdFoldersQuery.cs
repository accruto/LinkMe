using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IJobAdFoldersQuery
    {
        JobAdFolder GetFolder(IMember member, Guid id);
        IList<JobAdFolder> GetFolders(IMember member);

        JobAdFolder GetMobileFolder(IMember member);

        bool IsInFolder(IMember member, Guid folderId, Guid jobAdId);
        bool IsInMobileFolder(IMember member, Guid jobAdId);

        IList<Guid> GetInFolderJobAdIds(IMember member, Guid folderId);
        IList<Guid> GetInMobileFolderJobAdIds(IMember member);
        IList<Guid> GetInMobileFolderJobAdIds(IMember member, IEnumerable<Guid> jobAdIds);

        int GetInFolderCount(IMember member, Guid folderId);
        IDictionary<Guid, int> GetInFolderCounts(IMember member);
        IDictionary<Guid, DateTime?> GetLastUsedTimes(IMember member);
    }
}
