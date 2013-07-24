using LinkMe.Apps.Agents.Security.Commands;

namespace LinkMe.Apps.Agents.Users.LinkedIn.Commands
{
    public interface ILinkedInAuthenticationCommand
    {
        AuthenticationResult AuthenticateUser(string linkedInId);
    }
}
