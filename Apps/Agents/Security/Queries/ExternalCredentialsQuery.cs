using System;

namespace LinkMe.Apps.Agents.Security.Queries
{
    public class ExternalCredentialsQuery
        : IExternalCredentialsQuery
    {
        private readonly ISecurityRepository _repository;

        public ExternalCredentialsQuery(ISecurityRepository repository)
        {
            _repository = repository;
        }

        bool IExternalCredentialsQuery.DoCredentialsExist(ExternalCredentials credentials)
        {
            return _repository.DoCredentialsExist(credentials);
        }

        Guid? IExternalCredentialsQuery.GetUserId(Guid providerId, string externalId)
        {
            return _repository.GetExternalUserId(providerId, externalId);
        }

        ExternalCredentials IExternalCredentialsQuery.GetCredentials(Guid userId, Guid providerId)
        {
            return _repository.GetExternalCredentials(userId, providerId);
        }
    }
}