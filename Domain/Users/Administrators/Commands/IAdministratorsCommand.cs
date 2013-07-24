using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Administrators.Commands
{
    public interface IAdministratorsCommand
    {
        void CreateAdministrator(Administrator administrator);
        void UpdateAdministrator(Administrator administrator);
    }
}
