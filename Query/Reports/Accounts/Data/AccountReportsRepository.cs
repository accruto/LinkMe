using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Accounts.Data
{
    public class AccountReportsRepository
        : ReportsRepository<AccountsDataContext>, IAccountReportsRepository
    {
        [Flags]
        private enum UserFlags
        {
            Disabled = 0x04,
            Activated = 0x20,
        }

        private enum ActivityType
        {
            Login,
        }

        public AccountReportsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        int IAccountReportsRepository.GetUsers(UserType userType, DateTime time)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in GetUsers(dc, userType)
                        where u.createdTime <= time
                        select u).Count();
            }
        }

        int IAccountReportsRepository.GetEnabledUsers(UserType userType, DateTime time)
        {
            using (var dc = CreateDataContext(true))
            {
                var now = DateTime.Now;

                // The assumption here is that time < now and that all enablements/disablements occur between the 2.

                return (from u in GetUsers(dc, userType)
                        let e = (from e in dc.UserEnablementEntities where e.time >= time && e.time < now && e.userId == u.id select e).Count()
                        let d = (from d in dc.UserDisablementEntities where d.time >= time && d.time < now && d.userId == u.id select e).Count()
                        where u.createdTime <= time
                        &&
                        (
                            (
                                // Currently enabled.

                                (u.flags & (int)UserFlags.Disabled) == 0
                                &&
                                (e - d) % 2 == 0
                            )
                            ||
                            (
                                // Currently disabled.

                                (u.flags & (int)UserFlags.Disabled) != 0
                                &&
                                ((e - d) % 2 == 1 || (e - d) % 2 == -1)
                            )
                        )
                        select u).Count();
            }
        }

        int IAccountReportsRepository.GetActiveUsers(UserType userType, DateTime time)
        {
            using (var dc = CreateDataContext(true))
            {
                var now = DateTime.Now;

                // The assumption here is that time < now and that all enablements/disablements activations/deactivations occur between the 2.

                return (from u in GetUsers(dc, userType)
                        let e = (from e in dc.UserEnablementEntities where e.time >= time && e.time < now && e.userId == u.id select e).Count()
                        let d = (from d in dc.UserDisablementEntities where d.time >= time && d.time < now && d.userId == u.id select e).Count()
                        let a = (from a in dc.UserActivationEntities where a.time >= time && a.time < now && a.userId == u.id select e).Count()
                        let b = (from b in dc.UserDeactivationEntities where b.time >= time && b.time < now && b.userId == u.id select e).Count()
                        where u.createdTime <= time
                        &&
                        (
                            (
                                // Currently enabled.

                                (u.flags & (int)UserFlags.Disabled) == 0
                                &&
                                (e - d) % 2 == 0
                            )
                            ||
                            (
                                // Currently disabled.

                                (u.flags & (int)UserFlags.Disabled) != 0
                                &&
                                ((e - d) % 2 == 1 || (e - d) % 2 == -1)
                            )
                        )
                        &&
                        (
                            (
                                // Currently activated.

                                (u.flags & (int)UserFlags.Activated) != 0
                                &&
                                (a - b) % 2 == 0
                            )
                            ||
                            (
                                // Currently deactivated.

                                (u.flags & (int)UserFlags.Activated) == 0
                                &&
                                ((a - b) % 2 == 1 || (a - b) % 2 == -1)
                            )
                        )
                        select u).Count();
            }
        }

        int IAccountReportsRepository.GetNewUsers(UserType userType, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in GetUsers(dc, userType)
                        where u.createdTime >= timeRange.Start && u.createdTime < timeRange.End
                        select u).Count();
            }
        }

        IList<Guid> IAccountReportsRepository.GetNewUserIds(UserType userType, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in GetUsers(dc, userType)
                        where u.createdTime >= timeRange.Start && u.createdTime < timeRange.End
                        select u.id).ToList();
            }
        }

        int IAccountReportsRepository.GetLogIns(UserType userType, DateTimeRange timeRange)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in GetUsers(dc, userType)
                        join e in dc.UserLoginEntities on u.id equals e.userId
                        where e.time >= timeRange.Start && e.time < timeRange.End
                        && e.activityType == (int) ActivityType.Login
                        select u).Distinct().Count();
            }
        }

        IList<Guid> IAccountReportsRepository.GetLastLogIns(UserType userType, DayRange day)
        {
            using (var dc = CreateDataContext(true))
            {
                return (from u in GetEnabledUsers(dc, userType)
                        let lastLogInDate = (from v in dc.UserLoginEntities
                                             where v.userId == u.id
                                             select v.time).Max()
                        where lastLogInDate >= day.Start.Value && lastLogInDate < day.End.Value
                        select u.id).Distinct().ToList();
            }
        }

        private static IQueryable<RegisteredUserEntity> GetUsers(AccountsDataContext dc, UserType userType)
        {
            switch (userType)
            {
                case UserType.Member:
                    return from u in dc.RegisteredUserEntities
                           join m in dc.MemberEntities on u.id equals m.id
                           select u;

                case UserType.Employer:
                    return from u in dc.RegisteredUserEntities
                           join m in dc.EmployerEntities on u.id equals m.id
                           select u;

                case UserType.Administrator:
                    return from u in dc.RegisteredUserEntities
                           join a in dc.AdministratorEntities on u.id equals a.id
                           select u;

                case UserType.Custodian:
                    return from u in dc.RegisteredUserEntities
                           join a in dc.CommunityAdministratorEntities on u.id equals a.id
                           select u;

                default:
                    return from u in dc.RegisteredUserEntities
                           where u.id == Guid.Empty
                           select u;
            }
        }

        private static IQueryable<RegisteredUserEntity> GetEnabledUsers(AccountsDataContext dc, UserType userType)
        {
            return from u in GetUsers(dc, userType)
                   where (u.flags & (int) UserFlags.Disabled) == 0
                   select u;
        }

        protected override AccountsDataContext CreateDataContext(IDbConnection connection)
        {
            return new AccountsDataContext(connection);
        }
    }
}