using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Integration.Queries
{
    public class IntegrationQuery
        : IIntegrationQuery
    {
        private readonly IIntegrationRepository _repository;

        public IntegrationQuery(IIntegrationRepository repository)
        {
            _repository = repository;
        }

        T IIntegrationQuery.GetIntegrationSystem<T>(Guid id)
        {
            return _repository.GetIntegrationSystem<T>(id);
        }

        IntegratorUser IIntegrationQuery.GetIntegratorUser(Guid id)
        {
            return _repository.GetIntegratorUser(id);
        }

        IntegratorUser IIntegrationQuery.GetIntegratorUser(string loginId)
        {
            return _repository.GetIntegratorUser(loginId);
        }

        IList<IntegratorUser> IIntegrationQuery.GetIntegratorUsers()
        {
            return _repository.GetIntegratorUsers();
        }
    }
}