using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds.Commands
{
    public class JobAdNotesCommand
        : IJobAdNotesCommand
    {
        private readonly IJobAdNotesRepository _repository;

        public JobAdNotesCommand(IJobAdNotesRepository jobAdsRepository)
        {
            _repository = jobAdsRepository;
        }

        void IJobAdNotesCommand.CreateNote(JobAdNote note)
        {
            note.Prepare();
            note.Validate();
            _repository.CreateNote(note);
        }

        void IJobAdNotesCommand.UpdateNote(JobAdNote note)
        {
            note.Validate();
            note.UpdatedTime = DateTime.Now;
            _repository.UpdateNote(note);
        }

        void IJobAdNotesCommand.DeleteNote(Guid id)
        {
            _repository.DeleteNote(id);
        }
    }
}
