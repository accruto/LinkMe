using System;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public interface IContenderNotesCommand
    {
        void CreateNote(ContenderNote note);
        void UpdateNote(ContenderNote note);
        void DeleteNote(Guid id);
    }
}