using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public interface ICandidatesQuery
    {
        Candidate GetCandidate(Guid id);
        IList<Candidate> GetCandidates(IEnumerable<Guid> ids);
    }
}
