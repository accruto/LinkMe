using System;
using System.Collections.Generic;

namespace LinkMe.Apps.Agents.Users.Sessions.Queries
{
    public class UserSessionsQuery
        : IUserSessionsQuery
    {
        private readonly IUserSessionsRepository _repository;

        public UserSessionsQuery(IUserSessionsRepository repository)
        {
            _repository = repository;
        }

        IList<UserSessionActivity> IUserSessionsQuery.GetUserActivity(Guid userId)
        {
            return _repository.GetUserActivity(userId);
        }

        DateTime? IUserSessionsQuery.GetLastLoginTime(Guid userId)
        {
            return _repository.GetLastLoginTime(userId);
        }
    }
}