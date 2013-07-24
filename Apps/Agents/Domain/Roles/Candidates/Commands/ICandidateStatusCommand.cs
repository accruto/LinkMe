using System;
using LinkMe.Domain;

namespace LinkMe.Apps.Agents.Domain.Roles.Candidates.Commands
{
    public interface ICandidateStatusCommand
    {
        void ConfirmStatus(Guid candidateId, CandidateStatus status);
    }
}
