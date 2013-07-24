using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Security.Commands
{
    public enum AuthenticationStatus
    {
        Failed,
        Authenticated,
        AuthenticatedMustChangePassword,
        AuthenticatedWithOverridePassword,
        Disabled,
        Deactivated,
        AuthenticatedAutomatically,
    }

    public class AuthenticationResult
    {
        public AuthenticationStatus Status { get; set; }
        public IRegisteredUser User { get; set; }
    }
}