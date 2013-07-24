using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Administrators.Commands;
using LinkMe.Domain.Users.Administrators.Queries;

namespace LinkMe.Apps.Agents.Users.Administrators.Commands
{
    public class AdministratorAccountsCommand
        : IAdministratorAccountsCommand
    {
        private readonly IAdministratorsCommand _administratorsCommand;
        private readonly IAdministratorsQuery _administratorsQuery;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;

        public AdministratorAccountsCommand(IAdministratorsCommand administratorsCommand, IAdministratorsQuery administratorsQuery, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery)
        {
            _administratorsCommand = administratorsCommand;
            _administratorsQuery = administratorsQuery;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
        }

        void IAdministratorAccountsCommand.CreateAdministrator(Administrator administrator, LoginCredentials credentials)
        {
            // Check login credentials.

            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            administrator.IsEnabled = true;
            administrator.IsActivated = true;

            // Always make sure the email is verified when created.

            if (administrator.EmailAddress != null)
                administrator.EmailAddress.IsVerified = true;

            // Save.

            _administratorsCommand.CreateAdministrator(administrator);
            _loginCredentialsCommand.CreateCredentials(administrator.Id, credentials);
        }

        void IAdministratorAccountsCommand.UpdateAdministrator(Administrator administrator)
        {
            // Maintain the state of the email address.

            if (administrator.EmailAddress != null)
            {
                var originalAdministrator = _administratorsQuery.GetAdministrator(administrator.Id);
                administrator.EmailAddress.IsVerified = originalAdministrator.EmailAddress == null
                    || administrator.EmailAddress.Address != originalAdministrator.EmailAddress.Address
                    || originalAdministrator.EmailAddress.IsVerified;
            }

            _administratorsCommand.UpdateAdministrator(administrator);
        }
    }
}