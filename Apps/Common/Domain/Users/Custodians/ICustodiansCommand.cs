using System;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;

namespace LinkMe.Common.Domain.Users.Custodians
{
    public interface ICustodiansCommand
    {
        void CreateCustodian(Custodian custodian, LoginCredentials credentials);
        void UpdateCustodian(Custodian custodian);

        Custodian GetCustodian(Guid id);
    }
}
