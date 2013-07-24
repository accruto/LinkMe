namespace LinkMe.Apps.Agents.Security.Commands
{
    public interface ILoginAuthenticationCommand
    {
        AuthenticationResult AuthenticateUser(LoginCredentials credentials);
    }
}