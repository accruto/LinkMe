using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public interface ICandidateNotesCommand
    {
        void CreatePrivateNote(IEmployer employer, CandidateNote note);
        void CreateSharedNote(IEmployer employer, CandidateNote note);

        bool CanDeleteNote(IEmployer employer, CandidateNote note);
        bool CanUpdateNote(IEmployer employer, CandidateNote note);

        void DeleteNote(IEmployer employer, Guid id);
        void UpdateNote(IEmployer employer, CandidateNote note);

        void ShareNote(IEmployer employer, CandidateNote note);
        void UnshareNote(IEmployer employer, CandidateNote note);
    }
}