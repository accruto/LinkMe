using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Communications
{
    public interface IMemberCommunicationsRepository
    {
        IList<Guid> GetNotSentMemberIds(Guid definitionId, DateTime createdBefore, Range range);
        IList<Guid> GetSentMemberIds(Guid definitionId, DateTime lastSentBefore, Range range);
    }
}
