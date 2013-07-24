using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Users.Sessions
{
    public interface IUserSessionsRepository
    {
        void CreateUserLogin(UserLogin login);
        void CreateUserLogout(UserLogout logout);
        void CreateUserSessionEnd(UserSessionEnd sessionEnd);

        DateTime? GetLastLoginTime(Guid userId);
        IList<UserSessionActivity> GetUserActivity(Guid userId);
    }
}