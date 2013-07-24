using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;

namespace LinkMe.Common.Domain.Users.Custodians
{
    public class CustodiansCommand
        : ICustodiansCommand
    {
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;

        public CustodiansCommand(ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery)
        {
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
        }

        void ICustodiansCommand.CreateCustodian(Custodian custodian, LoginCredentials credentials)
        {
            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            custodian.IsEnabled = true;
            custodian.IsActivated = true;

            // Save.

            //_userProfileBroker.SaveNewUser(custodian);
            _loginCredentialsCommand.CreateCredentials(custodian.Id, credentials);
        }

        void ICustodiansCommand.UpdateCustodian(Custodian custodian)
        {
            //_userProfileBroker.SaveUser(custodian);
        }

        Custodian ICustodiansCommand.GetCustodian(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
