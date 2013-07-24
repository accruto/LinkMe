using System;

namespace LinkMe.Apps.Agents.Security
{
    public static class PublishedEvents
    {
        public const string PasswordReset = "LinkMe.Apps.Agents.Security.PasswordReset";
    }

    public class UserCredentialsEventArgs
        : EventArgs
    {
        public Guid UserId { get; private set; }
        public Guid ActorId { get; private set; }

        public UserCredentialsEventArgs(Guid userId, Guid actorId)
        {
            UserId = userId;
            ActorId = actorId;
        }
    }

    public class PasswordResetEventArgs
        : EventArgs
    {
        public Guid UserId { get; private set; }
        public string LoginId { get; private set; }
        public string Password { get; private set; }
        public bool IsGenerated { get; private set; }

        public PasswordResetEventArgs(Guid userId, string loginId, string password, bool isGenerated)
        {
            UserId = userId;
            LoginId = loginId;
            Password = password;
            IsGenerated = isGenerated;
        }
    }
}