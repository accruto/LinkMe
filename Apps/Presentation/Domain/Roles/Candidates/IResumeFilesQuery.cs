using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Views;

namespace LinkMe.Apps.Presentation.Domain.Roles.Candidates
{
    public interface IResumeFilesQuery
    {
        ResumeFile GetResumeFile(RegisteredUser recipient, Member member, Candidate candidate, Resume resume);
        ResumeFile GetResumeFile(EmployerMemberView view, Resume resume);
    }
}
