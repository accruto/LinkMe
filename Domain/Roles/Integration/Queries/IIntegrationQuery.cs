using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Integration.Queries
{
    public interface IIntegrationQuery
    {
        T GetIntegrationSystem<T>(Guid id) where T : IntegrationSystem, new();
        IntegratorUser GetIntegratorUser(Guid id);
        IntegratorUser GetIntegratorUser(string loginId);
        IList<IntegratorUser> GetIntegratorUsers();
    }
}