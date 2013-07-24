using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Candidates.Queries
{
    public interface ICandidateResumeFilesQuery
    {
        ResumeFileReference GetResumeFile(Guid candidateId, Guid fileReferenceId);
        ResumeFileReference GetResumeFile(Guid resumeId);

        bool HasResumeFiles(Guid candidateId);
        IList<ResumeFileReference> GetResumeFiles(Guid candidateId);
        ResumeFileReference GetLastUsedResumeFile(Guid candidateId);
    }
}
