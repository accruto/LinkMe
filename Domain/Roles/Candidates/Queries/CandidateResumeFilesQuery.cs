using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public class CandidateResumeFilesQuery
        : ICandidateResumeFilesQuery
    {
        private readonly ICandidatesRepository _repository;

        public CandidateResumeFilesQuery(ICandidatesRepository repository)
        {
            _repository = repository;
        }

        ResumeFileReference ICandidateResumeFilesQuery.GetResumeFile(Guid candidateId, Guid fileReferenceId)
        {
            return _repository.GetResumeFile(candidateId, fileReferenceId);
        }

        ResumeFileReference ICandidateResumeFilesQuery.GetResumeFile(Guid resumeId)
        {
            return _repository.GetResumeFile(resumeId);
        }

        bool ICandidateResumeFilesQuery.HasResumeFiles(Guid candidateId)
        {
            return _repository.HasResumeFiles(candidateId);
        }

        IList<ResumeFileReference> ICandidateResumeFilesQuery.GetResumeFiles(Guid candidateId)
        {
            return _repository.GetResumeFiles(candidateId);
        }

        ResumeFileReference ICandidateResumeFilesQuery.GetLastUsedResumeFile(Guid candidateId)
        {
            return _repository.GetLastUsedResumeFile(candidateId);
        }
    }
}
