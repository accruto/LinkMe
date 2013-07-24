using System;
using System.Collections.Generic;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members
{
    public interface IFilterMembersRepository
    {
        IList<Guid> Filter(IEnumerable<Guid> memberIds, MemberSortOrder sortOrder, DateTime? modifiedSince);
    }
}