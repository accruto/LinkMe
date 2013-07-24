using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Custodians;
using LinkMe.Domain.Users.Custodians.Commands;
using LinkMe.Domain.Users.Custodians.Queries;

namespace LinkMe.Apps.Agents.Users.Custodians.Commands
{
    public class CustodianAccountsCommand
        : ICustodianAccountsCommand
    {
        private readonly ICustodiansCommand _custodiansCommand;
        private readonly ICustodiansQuery _custodiansQuery;
        private readonly ILoginCredentialsCommand _loginCredentialsCommand;
        private readonly ILoginCredentialsQuery _loginCredentialsQuery;
        private readonly ICustodianAffiliationsCommand _custodianAffiliationsCommand;

        public CustodianAccountsCommand(ICustodiansCommand custodiansCommand, ICustodiansQuery custodiansQuery, ILoginCredentialsCommand loginCredentialsCommand, ILoginCredentialsQuery loginCredentialsQuery, ICustodianAffiliationsCommand custodianAffiliationsCommand)
        {
            _custodiansCommand = custodiansCommand;
            _custodiansQuery = custodiansQuery;
            _loginCredentialsCommand = loginCredentialsCommand;
            _loginCredentialsQuery = loginCredentialsQuery;
            _custodianAffiliationsCommand = custodianAffiliationsCommand;
        }

        void ICustodianAccountsCommand.CreateCustodian(Custodian custodian, LoginCredentials credentials, Guid affiliateId)
        {
            // Check login credentials.

            if (_loginCredentialsQuery.DoCredentialsExist(credentials))
                throw new DuplicateUserException();

            // Set some defaults.

            custodian.IsEnabled = true;
            custodian.IsActivated = true;

            // Always make sure the email is verified when created.

            if (custodian.EmailAddress != null)
                custodian.EmailAddress.IsVerified = true;

            // Save.

            _custodiansCommand.CreateCustodian(custodian);
            _loginCredentialsCommand.CreateCredentials(custodian.Id, credentials);
            _custodianAffiliationsCommand.SetAffiliation(custodian.Id, affiliateId);
            custodian.AffiliateId = affiliateId;
        }

        void ICustodianAccountsCommand.UpdateCustodian(Custodian custodian)
        {
            // Maintain the state of the email address.

            if (custodian.EmailAddress != null)
            {
                var originalCustodian = _custodiansQuery.GetCustodian(custodian.Id);
                custodian.EmailAddress.IsVerified = originalCustodian.EmailAddress == null
                    || custodian.EmailAddress.Address != originalCustodian.EmailAddress.Address
                    || originalCustodian.EmailAddress.IsVerified;
            }

            _custodiansCommand.UpdateCustodian(custodian);
        }
    }
}