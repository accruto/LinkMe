using System;
using System.Collections.Generic;

namespace LinkMe.Query.Search.Members.Commands
{
    public interface IAdministrativeMemberSearchCommand
    {
        IList<Guid> Search(AdministrativeMemberSearchCriteria criteria);
    }
}