using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public interface ICandidateFoldersQuery
    {
        CandidateFolder GetFolder(IEmployer employer, Guid id);
        IList<CandidateFolder> GetFolders(IEmployer employer);

        CandidateFolder GetShortlistFolder(IEmployer employer);
        CandidateFolder GetMobileFolder(IEmployer employer);
        IList<CandidateFolder> GetPrivateFolders(IEmployer employer);
        IList<CandidateFolder> GetSharedFolders(IEmployer employer);

        bool IsInFolder(IEmployer employer, Guid candidateId);
        bool IsInFolder(IEmployer employer, Guid folderId, Guid candidateId);
        bool IsInMobileFolder(IEmployer employer, Guid candidateId);

        IList<Guid> GetInFolderCandidateIds(IEmployer employer);
        IList<Guid> GetInFolderCandidateIds(IEmployer employer, Guid folderId);
        IList<Guid> GetInMobileFolderCandidateIds(IEmployer employer);
        IList<Guid> GetInMobileFolderCandidateIds(IEmployer employer, IEnumerable<Guid> candidateIds);

        int GetInFolderCount(IEmployer employer, Guid folderId);
        IDictionary<Guid, int> GetInFolderCounts(IEmployer employer);
        IDictionary<Guid, DateTime?> GetLastUsedTimes(IEmployer employer);

        int GetFolderCount(IEmployer employer, Guid candidateId);
        IDictionary<Guid, int> GetFolderCounts(IEmployer employer, IEnumerable<Guid> candidateIds);
    }
}
