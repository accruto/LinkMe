using System;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public interface ILoginCredentialsCommand
    {
        void CreateCredentials(Guid userId, LoginCredentials credentials);
        void UpdateCredentials(Guid userId, LoginCredentials credentials, Guid updatedById);
        void ResetPassword(Guid userId, LoginCredentials credentials);
        void ChangePassword(Guid userId, LoginCredentials credentials, string newPassword);
    }
}