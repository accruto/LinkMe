using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public interface ICandidateFlagListsQuery
    {
        CandidateFlagList GetFlagList(IEmployer employer);

        bool IsFlagged(IEmployer employer, Guid candidateId);

        IList<Guid> GetFlaggedCandidateIds(IEmployer employer);
        IList<Guid> GetFlaggedCandidateIds(IEmployer employer, IEnumerable<Guid> candidateIds);

        int GetFlaggedCount(IEmployer employer);
    }
}
