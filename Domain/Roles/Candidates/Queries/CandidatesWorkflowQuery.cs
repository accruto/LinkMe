using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public class CandidatesWorkflowQuery
        : ICandidatesWorkflowQuery
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesWorkflowQuery(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        IList<Tuple<Guid, CandidateStatus>> ICandidatesWorkflowQuery.GetCandidatesWithoutStatusWorkflow()
        {
            return _repository.GetCandidatesWithoutStatusWorkflow();
        }

        IList<Tuple<Guid, CandidateStatus>> ICandidatesWorkflowQuery.GetCandidatesWithoutSuggestedJobsWorkflow()
        {
            return _repository.GetCandidatesWithoutSuggestedJobsWorkflow();
        }
    }
}