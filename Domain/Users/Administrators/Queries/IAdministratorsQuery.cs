using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Administrators.Queries
{
    public interface IAdministratorsQuery
    {
        Administrator GetAdministrator(Guid id);
        IList<Administrator> GetAdministrators();
        IList<Administrator> GetAdministrators(bool enabledOnly);
    }
}