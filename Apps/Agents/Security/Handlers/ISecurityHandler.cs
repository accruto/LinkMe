using System;

namespace LinkMe.Apps.Agents.Security.Handlers
{
    public interface ISecurityHandler
    {
        void OnPasswordReset(bool isGenerated, Guid userId, string loginId, string password);
    }
}
