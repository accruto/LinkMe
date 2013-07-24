using System;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public interface ICandidatesCommand
    {
        void CreateCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        Candidate GetCandidate(Guid id);
    }
}