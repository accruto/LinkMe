using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Common.Domain.Users.Administrators
{
    public interface IAdministratorsCommand
    {
        void CreateAdministrator(Administrator administrator, LoginCredentials credentials);
        void UpdateAdministrator(Administrator administrator);
        Administrator GetAdministrator(Guid id);
    }
}
