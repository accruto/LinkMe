using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Security
{
    public interface ISecurityRepository
    {
        void UpdateCredentials(Guid userId, LoginCredentials credentials);
        bool DoCredentialsExist(LoginCredentials credentials);
        LoginCredentials GetLoginCredentials(Guid userId);
        Guid? GetLoginUserId(string loginId);
        string GetLoginId(Guid userId);
        IDictionary<Guid, string> GetLoginIds(IEnumerable<Guid> userIds);

        void UpdateCredentials(Guid userId, ExternalCredentials credentials);
        void DeleteCredentials(Guid userId, Guid providerId);
        bool DoCredentialsExist(ExternalCredentials credentials);
        ExternalCredentials GetExternalCredentials(Guid userId, Guid providerId);
        Guid? GetExternalUserId(Guid providerId, string externalId);
    }
}