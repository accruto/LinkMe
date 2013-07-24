using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Administrators
{
    public interface IAdministratorsRepository
    {
        void CreateAdministrator(Administrator administrator);
        void UpdateAdministrator(Administrator administrator);
        Administrator GetAdministrator(Guid id);
        IList<Administrator> GetAdministrators(bool enabledOnly);
    }
}
