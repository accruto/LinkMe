using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public class MemberJobAdNotesCommand
        : IMemberJobAdNotesCommand
    {
        private readonly IJobAdNotesCommand _jobAdNotesCommand;
        private readonly IJobAdNotesQuery _jobAdNotesQuery;

        public MemberJobAdNotesCommand(IJobAdNotesCommand jobAdNotesCommand, IJobAdNotesQuery jobAdNotesQuery)
        {
            _jobAdNotesCommand = jobAdNotesCommand;
            _jobAdNotesQuery = jobAdNotesQuery;
        }

        void IMemberJobAdNotesCommand.CreateNote(IMember member, MemberJobAdNote note)
        {
            Prepare(member, note);
            _jobAdNotesCommand.CreateNote(note);
        }

        bool IMemberJobAdNotesCommand.CanDeleteNote(IMember member, MemberJobAdNote note)
        {
            // Same thing at the moment.

            return CanUpdateNote(member, note);
        }

        bool IMemberJobAdNotesCommand.CanUpdateNote(IMember member, MemberJobAdNote note)
        {
            return CanUpdateNote(member, note);
        }

        void IMemberJobAdNotesCommand.UpdateNote(IMember member, MemberJobAdNote note)
        {
            if (!CanUpdateNote(member, note))
                throw new NoteOwnerPermissionsException(member, note.Id);

            _jobAdNotesCommand.UpdateNote(note);
        }

        void IMemberJobAdNotesCommand.DeleteNote(IMember member, Guid id)
        {
            var note = _jobAdNotesQuery.GetNote<MemberJobAdNote>(id);
            if (note != null)
            {
                if (!CanUpdateNote(member, note))
                    throw new NoteOwnerPermissionsException(member, note.Id);
                _jobAdNotesCommand.DeleteNote(id);
            }
        }

        private static void Prepare(IHasId<Guid> member, MemberJobAdNote note)
        {
            note.MemberId = member.Id;
        }

        private static bool CanUpdateNote(IHasId<Guid> member, MemberJobAdNote note)
        {
            // Must own the note to update it.

            if (member == null || note == null)
                return false;
            return note.MemberId == member.Id;
        }
    }
}