using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public interface IJobAdBlockListsQuery
    {
        JobAdBlockList GetBlockList(IMember member);

        bool IsBlocked(IMember member, Guid jobAdId);
        IList<Guid> GetBlockedJobAdIds(IMember member);
        int GetBlockedCount(IMember member);
    }
}
