using System;
using System.Collections.Generic;

namespace LinkMe.Query.Search.Members
{
    public interface ISearchMembersRepository
    {
        IList<Guid> Search(AdministrativeMemberSearchCriteria criteria);
    }
}
