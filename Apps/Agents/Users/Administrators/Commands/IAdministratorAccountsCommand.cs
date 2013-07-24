using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Users.Administrators.Commands
{
    public interface IAdministratorAccountsCommand
    {
        void CreateAdministrator(Administrator administrator, LoginCredentials credentials);
        void UpdateAdministrator(Administrator administrator);
    }
}