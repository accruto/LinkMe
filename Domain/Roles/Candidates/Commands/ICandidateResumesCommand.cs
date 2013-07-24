using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Resumes;

namespace LinkMe.Domain.Roles.Candidates.Commands
{
    public interface ICandidateResumesCommand
    {
        void CreateResumeFile(Candidate candidate, FileReference fileReference);
        void CreateResume(Candidate candidate, Resume resume, FileReference fileReference);
        void CreateResume(Candidate candidate, Resume resume);
        void UpdateResume(Candidate candidate, Resume resume);
    }
}
