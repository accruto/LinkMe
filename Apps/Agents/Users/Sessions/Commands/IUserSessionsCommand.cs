namespace LinkMe.Apps.Agents.Users.Sessions.Commands
{
    public interface IUserSessionsCommand
    {
        void CreateUserLogin(UserLogin login);
        void CreateUserLogout(UserLogout logout);
        void CreateUserSessionEnd(UserSessionEnd sessionEnd);
    }
}