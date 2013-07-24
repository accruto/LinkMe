using System;
using System.Collections.Generic;
using LinkMe.Domain;

namespace LinkMe.Query.Reports.Roles.Candidates.Queries
{
    public class CandidateReportsQuery
        : ICandidateReportsQuery
    {
        private readonly ICandidateReportsRepository _repository;

        public CandidateReportsQuery(ICandidateReportsRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> ICandidateReportsQuery.GetCandidateStatuses(CandidateStatus status)
        {
            return _repository.GetCandidateStatuses(status);
        }

        IList<Guid> ICandidateReportsQuery.GetEthnicStatuses(EthnicStatus status)
        {
            return _repository.GetEthnicStatuses(status);
        }
    }
}