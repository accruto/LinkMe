using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public interface IMemberJobAdNotesCommand
    {
        void CreateNote(IMember member, MemberJobAdNote note);

        bool CanDeleteNote(IMember member, MemberJobAdNote note);
        bool CanUpdateNote(IMember member, MemberJobAdNote note);

        void DeleteNote(IMember member, Guid id);
        void UpdateNote(IMember member, MemberJobAdNote note);
    }
}