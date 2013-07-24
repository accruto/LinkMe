using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders.Commands
{
    public class ContenderNotesCommand
        : IContenderNotesCommand
    {
        private readonly IContenderNotesRepository _repository;

        public ContenderNotesCommand(IContenderNotesRepository repository)
        {
            _repository = repository;
        }

        void IContenderNotesCommand.CreateNote(ContenderNote note)
        {
            note.Prepare();
            note.Validate();
            _repository.CreateNote(note);
        }

        void IContenderNotesCommand.UpdateNote(ContenderNote note)
        {
            note.Validate();
            note.UpdatedTime = DateTime.Now;
            _repository.UpdateNote(note);
        }

        void IContenderNotesCommand.DeleteNote(Guid id)
        {
            _repository.DeleteNote(id);
        }
    }
}