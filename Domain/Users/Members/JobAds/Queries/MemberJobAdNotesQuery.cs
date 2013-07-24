using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public class MemberJobAdNotesQuery
        : IMemberJobAdNotesQuery
    {
        private readonly IJobAdNotesQuery _jobAdNotesQuery;

        public MemberJobAdNotesQuery(IJobAdNotesQuery jobAdNotesQuery)
        {
            _jobAdNotesQuery = jobAdNotesQuery;
        }

        MemberJobAdNote IMemberJobAdNotesQuery.GetNote(IMember member, Guid id)
        {
            if (member == null)
                return null;
            var note = _jobAdNotesQuery.GetNote<MemberJobAdNote>(id);
            return CanAccessNote(member, note) ? note : null;
        }

        IList<MemberJobAdNote> IMemberJobAdNotesQuery.GetNotes(IMember member, Guid jobAdId)
        {
            return member == null
                ? new List<MemberJobAdNote>()
                : _jobAdNotesQuery.GetNotes<MemberJobAdNote>(member.Id, jobAdId);
        }

        bool IMemberJobAdNotesQuery.HasNotes(IMember member, Guid jobAdId)
        {
            return member != null
                && _jobAdNotesQuery.HasNotes(member.Id, jobAdId);
        }

        IList<Guid> IMemberJobAdNotesQuery.GetHasNotesJobAdIds(IMember member)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdNotesQuery.GetHasNotesJobAdIds(member.Id);
        }

        private static bool CanAccessNote(IHasId<Guid> member, MemberJobAdNote note)
        {
            if (member == null || note == null)
                return false;

            // Must be the owner.
            
            return note.MemberId == member.Id;
        }
    }
}