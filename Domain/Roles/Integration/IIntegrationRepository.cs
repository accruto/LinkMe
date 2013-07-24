using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Integration
{
    public interface IIntegrationRepository
    {
        void CreateIntegrationSystem(IntegrationSystem system);
        T GetIntegrationSystem<T>(Guid id) where T : IntegrationSystem, new();

        void CreateIntegratorUser(IntegratorUser user);
        IntegratorUser GetIntegratorUser(Guid id);
        IntegratorUser GetIntegratorUser(string loginId);
        IList<IntegratorUser> GetIntegratorUsers();
    }
}
