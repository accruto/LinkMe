using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public class CandidatesQuery
        : ICandidatesQuery
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesQuery(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        Candidate ICandidatesQuery.GetCandidate(Guid id)
        {
            return _repository.GetCandidate(id);
        }

        IList<Candidate> ICandidatesQuery.GetCandidates(IEnumerable<Guid> ids)
        {
            return _repository.GetCandidates(ids);
        }
    }
}
