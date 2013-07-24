using System;
using System.Collections.Generic;
using LinkMe.Framework.Utility.Results;

namespace LinkMe.Query.Search.Members.Queries
{
    public interface IMemberSearchesQuery
    {
        MemberSearch GetMemberSearch(Guid id);
        MemberSearch GetMemberSearch(Guid ownerId, string name);
        IList<MemberSearch> GetMemberSearches(IEnumerable<Guid> ids);
        IList<MemberSearch> GetMemberSearches(Guid ownerId);
        RangeResult<MemberSearch> GetMemberSearches(Guid ownerId, Range range);
        IList<MemberSearch> GetMemberSearches(IEnumerable<Guid> ownerIds, Range range);

        MemberSearchExecution GetMemberSearchExecution(Guid id);
        IList<MemberSearchExecution> GetMemberSearchExecutions(Guid ownerId);
        IList<MemberSearchExecution> GetMemberSearchExecutions(IEnumerable<Guid> ownerIds, Range range);
    }
}