using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Reports.Accounts
{
    public interface IAccountReportsRepository
    {
        int GetUsers(UserType userType, DateTime time);
        int GetEnabledUsers(UserType userType, DateTime time);
        int GetActiveUsers(UserType userType, DateTime time);

        int GetNewUsers(UserType userType, DateTimeRange timeRange);
        IList<Guid> GetNewUserIds(UserType userType, DateTimeRange timeRange);

        int GetLogIns(UserType type, DateTimeRange timeRange);
        IList<Guid> GetLastLogIns(UserType userType, DayRange day);
    }
}
