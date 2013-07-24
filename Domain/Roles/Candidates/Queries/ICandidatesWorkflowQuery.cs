using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public interface ICandidatesWorkflowQuery
    {
        IList<Tuple<Guid, CandidateStatus>> GetCandidatesWithoutStatusWorkflow();
        IList<Tuple<Guid, CandidateStatus>> GetCandidatesWithoutSuggestedJobsWorkflow();
    }
}