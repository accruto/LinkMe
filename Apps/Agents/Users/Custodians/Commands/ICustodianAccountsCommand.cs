using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Users.Custodians.Commands
{
    public interface ICustodianAccountsCommand
    {
        void CreateCustodian(Custodian custodian, LoginCredentials credentials, Guid affiliateId);
        void UpdateCustodian(Custodian custodian);
    }
}