using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Security.Queries
{
    public interface ILoginCredentialsQuery
    {
        Guid? GetUserId(string loginId);
        string GetLoginId(Guid userId);
        IDictionary<Guid, string> GetLoginIds(IEnumerable<Guid> userIds);

        bool DoCredentialsExist(LoginCredentials credentials);
        LoginCredentials GetCredentials(Guid userId);
    }
}
