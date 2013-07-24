using System;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public class ExternalCredentialsCommand
        : IExternalCredentialsCommand
    {
        private readonly ISecurityRepository _repository;

        public ExternalCredentialsCommand(ISecurityRepository repository)
        {
            _repository = repository;
        }

        void IExternalCredentialsCommand.CreateCredentials(Guid userId, ExternalCredentials credentials)
        {
            _repository.UpdateCredentials(userId, credentials);
        }

        void IExternalCredentialsCommand.UpdateCredentials(Guid userId, ExternalCredentials credentials, Guid updatedById)
        {
            _repository.UpdateCredentials(userId, credentials);
        }

        void IExternalCredentialsCommand.DeleteCredentials(Guid userId, Guid providerId)
        {
            _repository.DeleteCredentials(userId, providerId);
        }
    }
}