using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IMemberJobAdNotesQuery
    {
        MemberJobAdNote GetNote(IMember member, Guid id);
        IList<MemberJobAdNote> GetNotes(IMember member, Guid candidateId);

        bool HasNotes(IMember member, Guid jobAdId);
        IList<Guid> GetHasNotesJobAdIds(IMember member);
    }
}