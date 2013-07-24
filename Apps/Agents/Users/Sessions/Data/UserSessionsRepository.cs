using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Domain.Data;
using LinkMe.Framework.Utility.Data.Linq;

namespace LinkMe.Apps.Agents.Users.Sessions.Data
{
    public class UserSessionsRepository
        : Repository, IUserSessionsRepository
    {
        private static readonly Func<UsersDataContext, Guid, DateTime?> GetLastLoginTime
            = CompiledQuery.Compile((UsersDataContext dc, Guid userId)
                => (from e in dc.UserLoginEntities
                    where e.userId == userId
                    && (e.authenticationStatus == (int?)AuthenticationStatus.Authenticated || e.authenticationStatus == (int?)AuthenticationStatus.AuthenticatedAutomatically || e.authenticationStatus == (int?)AuthenticationStatus.AuthenticatedMustChangePassword || e.authenticationStatus == (int?) AuthenticationStatus.Deactivated)
                    select (DateTime?)e.time).Max());

        private static readonly Func<UsersDataContext, Guid, IQueryable<UserSessionActivity>> GetUserActivity
            = CompiledQuery.Compile((UsersDataContext dc, Guid userId)
                => from e in dc.UserLoginEntities
                   where e.userId == userId
                   orderby e.time
                   select e.Map());

        public UserSessionsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        void IUserSessionsRepository.CreateUserLogin(UserLogin login)
        {
            using (var dc = CreateContext())
            {
                dc.UserLoginEntities.InsertOnSubmit(login.Map());
                dc.SubmitChanges();
            }
        }

        void IUserSessionsRepository.CreateUserLogout(UserLogout logout)
        {
            using (var dc = CreateContext())
            {
                dc.UserLoginEntities.InsertOnSubmit(logout.Map());
                dc.SubmitChanges();
            }
        }

        void IUserSessionsRepository.CreateUserSessionEnd(UserSessionEnd sessionEnd)
        {
            using (var dc = CreateContext())
            {
                dc.UserLoginEntities.InsertOnSubmit(sessionEnd.Map());
                dc.SubmitChanges();
            }
        }

        DateTime? IUserSessionsRepository.GetLastLoginTime(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetLastLoginTime(dc, userId);
            }
        }

        IList<UserSessionActivity> IUserSessionsRepository.GetUserActivity(Guid userId)
        {
            using (var dc = CreateContext().AsReadOnly())
            {
                return GetUserActivity(dc, userId).ToList();
            }
        }

        private UsersDataContext CreateContext()
        {
            return CreateContext(c => new UsersDataContext(c));
        }
    }
}