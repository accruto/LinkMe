using System;
using LinkMe.Domain.Files;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public interface ICandidateResumeFilesCommand
    {
        void ValidateFile(string fileName, FileContents contents);

        void CreateResumeFile(Guid candidateId, ResumeFileReference resumeFileReference);
        void UpdateLastUsedTime(ResumeFileReference resumeFileReference);
    }
}