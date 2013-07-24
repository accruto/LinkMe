namespace LinkMe.Apps.Agents.Security.Commands
{
    public interface IExternalAuthenticationCommand
    {
        AuthenticationResult AuthenticateUser(ExternalCredentials credentials);
    }
}