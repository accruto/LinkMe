using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Users.Sessions.Queries
{
    public interface IUserSessionsQuery
    {
        IList<UserSessionActivity> GetUserActivity(Guid userId);
        DateTime? GetLastLoginTime(Guid userId);
    }
}