using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Communications.Queries
{
    public interface IMemberCommunicationsQuery
    {
        IList<Guid> GetNotSentMemberIds(Guid definitionId, DateTime createdBefore, Range range);
        IList<Guid> GetSentMemberIds(Guid definitionId, DateTime lastSentBefore, Range range);
    }
}
