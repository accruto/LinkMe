using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Domain.Users.Members.Communications.Queries
{
    public class MemberCommunicationsQuery
        : IMemberCommunicationsQuery
    {
        private readonly IMemberCommunicationsRepository _repository;

        public MemberCommunicationsQuery(IMemberCommunicationsRepository repository)
        {
            _repository = repository;
        }

        IList<Guid> IMemberCommunicationsQuery.GetNotSentMemberIds(Guid definitionId, DateTime createdBefore, Range range)
        {
            return _repository.GetNotSentMemberIds(definitionId, createdBefore, range);
        }

        IList<Guid> IMemberCommunicationsQuery.GetSentMemberIds(Guid definitionId, DateTime lastSentBefore, Range range)
        {
            return _repository.GetSentMemberIds(definitionId, lastSentBefore, range);
        }
    }
}
