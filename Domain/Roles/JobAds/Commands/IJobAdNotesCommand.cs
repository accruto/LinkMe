using System;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public interface IJobAdNotesCommand
    {
        void CreateNote(JobAdNote note);
        void UpdateNote(JobAdNote note);
        void DeleteNote(Guid id);
    }
}
