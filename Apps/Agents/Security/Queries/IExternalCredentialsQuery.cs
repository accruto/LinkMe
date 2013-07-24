using System;

namespace LinkMe.Apps.Agents.Security.Queries
{
    public interface IExternalCredentialsQuery
    {
        bool DoCredentialsExist(ExternalCredentials credentials);
        Guid? GetUserId(Guid providerId, string externalId);
        ExternalCredentials GetCredentials(Guid userId, Guid providerId);
    }
}
