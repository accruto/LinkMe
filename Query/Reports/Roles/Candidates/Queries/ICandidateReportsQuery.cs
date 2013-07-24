using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Candidates.Queries
{
    public interface ICandidateReportsQuery
    {
        IList<Guid> GetCandidateStatuses(CandidateStatus status);
        IList<Guid> GetEthnicStatuses(EthnicStatus status);
    }
}