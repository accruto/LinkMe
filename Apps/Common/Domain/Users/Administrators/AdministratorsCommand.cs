using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;

namespace LinkMe.Common.Domain.Users.Administrators
{
    public class AdministratorsCommand
        : IAdministratorsCommand
    {
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;

        public AdministratorsCommand(ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery)
        {
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
        }

        void IAdministratorsCommand.CreateAdministrator(Administrator administrator, LoginCredentials credentials)
        {
            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            administrator.IsEnabled = true;
            administrator.IsActivated = true;

            // Save.

            //_userProfileBroker.SaveNewUser(administrator);
            _loginCredentialsCommand.CreateCredentials(administrator.Id, credentials);
        }

        void IAdministratorsCommand.UpdateAdministrator(Administrator administrator)
        {
            //_userProfileBroker.SaveUser(administrator);
        }

        Administrator IAdministratorsCommand.GetAdministrator(Guid id)
        {
            return null; // _userProfileBroker.FindById(id) as Administrator;
        }
    }
}
