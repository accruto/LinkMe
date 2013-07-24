using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Agents.Users.Sessions.Commands
{
    public class UserSessionsCommand
        : IUserSessionsCommand
    {
        private readonly IUserSessionsRepository _repository;

        public UserSessionsCommand(IUserSessionsRepository repository)
        {
            _repository = repository;
        }

        void IUserSessionsCommand.CreateUserLogin(UserLogin login)
        {
            login.Prepare();
            login.Validate();
            _repository.CreateUserLogin(login);
        }

        void IUserSessionsCommand.CreateUserLogout(UserLogout logout)
        {
            logout.Prepare();
            logout.Validate();
            _repository.CreateUserLogout(logout);
        }

        void IUserSessionsCommand.CreateUserSessionEnd(UserSessionEnd sessionEnd)
        {
            sessionEnd.Prepare();
            sessionEnd.Validate();
            _repository.CreateUserSessionEnd(sessionEnd);
        }
    }
}