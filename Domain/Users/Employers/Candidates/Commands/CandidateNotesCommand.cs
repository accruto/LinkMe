using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public class CandidateNotesCommand
        : ICandidateNotesCommand
    {
        private readonly IContenderNotesCommand _contenderNotesCommand;
        private readonly IContenderNotesQuery _contenderNotesQuery;

        public CandidateNotesCommand(IContenderNotesCommand contenderNotesCommand, IContenderNotesQuery contenderNotesQuery)
        {
            _contenderNotesCommand = contenderNotesCommand;
            _contenderNotesQuery = contenderNotesQuery;
        }

        void ICandidateNotesCommand.CreatePrivateNote(IEmployer employer, CandidateNote note)
        {
            Prepare(employer, note, false);
            _contenderNotesCommand.CreateNote(note);
        }

        void ICandidateNotesCommand.CreateSharedNote(IEmployer employer, CandidateNote note)
        {
            Prepare(employer, note, true);
            _contenderNotesCommand.CreateNote(note);
        }

        bool ICandidateNotesCommand.CanDeleteNote(IEmployer employer, CandidateNote note)
        {
            // Same thing at the moment.

            return CanUpdateNote(employer, note);
        }

        bool ICandidateNotesCommand.CanUpdateNote(IEmployer employer, CandidateNote note)
        {
            return CanUpdateNote(employer, note);
        }

        void ICandidateNotesCommand.UpdateNote(IEmployer employer, CandidateNote note)
        {
            if (!CanUpdateNote(employer, note))
                throw new NoteOwnerPermissionsException(employer, note.Id);

            _contenderNotesCommand.UpdateNote(note);
        }

        void ICandidateNotesCommand.DeleteNote(IEmployer employer, Guid id)
        {
            var note = _contenderNotesQuery.GetNote<CandidateNote>(id);
            if (note != null)
            {
                if (!CanUpdateNote(employer, note))
                    throw new NoteOwnerPermissionsException(employer, note.Id);
                _contenderNotesCommand.DeleteNote(id);
            }
        }

        void ICandidateNotesCommand.ShareNote(IEmployer employer, CandidateNote note)
        {
            // Must be able to update the note.

            if (!CanUpdateNote(employer, note))
                throw new NoteOwnerPermissionsException(employer, note.Id);

            Prepare(employer, note, true);
            _contenderNotesCommand.UpdateNote(note);
        }

        void ICandidateNotesCommand.UnshareNote(IEmployer employer, CandidateNote note)
        {
            // Must be able to update the note.

            if (!CanUpdateNote(employer, note))
                throw new NoteOwnerPermissionsException(employer, note.Id);

            Prepare(employer, note, false);
            _contenderNotesCommand.UpdateNote(note);
        }

        private static void Prepare(IEmployer employer, CandidateNote note, bool isShared)
        {
            note.RecruiterId = employer.Id;
            note.OrganisationId = isShared ? employer.Organisation.Id : (Guid?)null;
        }

        private static bool CanUpdateNote(IHasId<Guid> employer, CandidateNote note)
        {
            // Must own the note to update it.

            if (employer == null || note == null)
                return false;
            return note.RecruiterId == employer.Id;
        }
    }
}