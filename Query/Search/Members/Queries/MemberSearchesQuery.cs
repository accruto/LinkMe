using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.Members.Queries
{
    public class MemberSearchesQuery
        : IMemberSearchesQuery
    {
        private readonly IMembersRepository _repository;

        public MemberSearchesQuery(IMembersRepository repository)
        {
            _repository = repository;
        }

        MemberSearch IMemberSearchesQuery.GetMemberSearch(Guid id)
        {
            return _repository.GetMemberSearch(id);
        }

        MemberSearch IMemberSearchesQuery.GetMemberSearch(Guid ownerId, string name)
        {
            return _repository.GetMemberSearch(ownerId, name);
        }

        IList<MemberSearch> IMemberSearchesQuery.GetMemberSearches(IEnumerable<Guid> ids)
        {
            return _repository.GetMemberSearches(ids);
        }

        IList<MemberSearch> IMemberSearchesQuery.GetMemberSearches(Guid ownerId)
        {
            return _repository.GetMemberSearches(ownerId);
        }

        RangeResult<MemberSearch> IMemberSearchesQuery.GetMemberSearches(Guid ownerId, Range range)
        {
            return _repository.GetMemberSearches(ownerId, range);
        }

        IList<MemberSearch> IMemberSearchesQuery.GetMemberSearches(IEnumerable<Guid> ownerIds, Range range)
        {
            return _repository.GetMemberSearches(ownerIds, range);
        }

        MemberSearchExecution IMemberSearchesQuery.GetMemberSearchExecution(Guid id)
        {
            return _repository.GetMemberSearchExecution(id);
        }

        IList<MemberSearchExecution> IMemberSearchesQuery.GetMemberSearchExecutions(Guid ownerId)
        {
            return _repository.GetMemberSearchExecutions(ownerId);
        }

        IList<MemberSearchExecution> IMemberSearchesQuery.GetMemberSearchExecutions(IEnumerable<Guid> ownerIds, Range range)
        {
            return _repository.GetMemberSearchExecutions(ownerIds, range);
        }
    }
}