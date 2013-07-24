using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Candidates
{
    public class NoteOwnerPermissionsException
        : PermissionsException
    {
        private readonly Guid _noteId;

        public NoteOwnerPermissionsException(IUser employer, Guid noteId)
            : base(employer)
        {
            _noteId = noteId;
        }

        public Guid NoteId
        {
            get { return _noteId; }
        }
    }
}
