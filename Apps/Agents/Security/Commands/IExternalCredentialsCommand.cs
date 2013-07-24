using System;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public interface IExternalCredentialsCommand
    {
        void CreateCredentials(Guid userId, ExternalCredentials credentials);
        void UpdateCredentials(Guid userId, ExternalCredentials credentials, Guid updatedById);
        void DeleteCredentials(Guid userId, Guid providerId);
    }
}