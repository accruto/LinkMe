using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public interface ICandidateBlockListsQuery
    {
        CandidateBlockList GetBlockList(IEmployer employer, Guid id);
        IList<CandidateBlockList> GetBlockLists(IEmployer employer);
        CandidateBlockList GetTemporaryBlockList(IEmployer employer);
        CandidateBlockList GetPermanentBlockList(IEmployer employer);

        bool IsBlocked(IEmployer employer, Guid candidateId);
        bool IsBlocked(IEmployer employer, Guid blockListId, Guid candidateId);
        bool IsPermanentlyBlocked(IEmployer employer, Guid candidateId);

        IList<Guid> GetBlockedCandidateIds(IEmployer employer);
        IList<Guid> GetBlockedCandidateIds(IEmployer employer, Guid blockListId);
        IList<Guid> GetPermanentlyBlockedCandidateIds(IEmployer employer);

        int GetBlockedCount(IEmployer employer, Guid blockListId);
        IDictionary<Guid, int> GetBlockedCounts(IEmployer employer);
    }
}
