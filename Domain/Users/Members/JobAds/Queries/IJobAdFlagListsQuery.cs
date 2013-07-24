using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IJobAdFlagListsQuery
    {
        JobAdFlagList GetFlagList(IMember member);

        bool IsFlagged(IMember member, Guid jobAdId);

        IList<Guid> GetFlaggedJobAdIds(IMember member);
        IList<Guid> GetFlaggedJobAdIds(IMember member, IEnumerable<Guid> jobAdIds);

        int GetFlaggedCount(IMember member);
    }
}
