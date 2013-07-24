using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;

namespace LinkMe.Query.Reports.Accounts.Queries
{
    public class AccountReportsQuery
        : IAccountReportsQuery
    {
        private readonly IAccountReportsRepository _repository;

        public AccountReportsQuery(IAccountReportsRepository repository)
        {
            _repository = repository;
        }

        int IAccountReportsQuery.GetUsers(UserType userType, DateTime time)
        {
            return _repository.GetUsers(userType, time);
        }

        int IAccountReportsQuery.GetEnabledUsers(UserType userType, DateTime time)
        {
            return _repository.GetEnabledUsers(userType, time);
        }

        int IAccountReportsQuery.GetActiveUsers(UserType userType, DateTime time)
        {
            return _repository.GetActiveUsers(userType, time);
        }

        int IAccountReportsQuery.GetNewUsers(UserType userType, DateTimeRange timeRange)
        {
            return _repository.GetNewUsers(userType, timeRange);
        }

        IList<Guid> IAccountReportsQuery.GetNewUserIds(UserType userType, DateTimeRange timeRange)
        {
            return _repository.GetNewUserIds(userType, timeRange);
        }

        int IAccountReportsQuery.GetLogIns(UserType userType, DateTimeRange timeRange)
        {
            return _repository.GetLogIns(userType, timeRange);
        }

        IList<Guid> IAccountReportsQuery.GetLastLogIns(UserType userType, DayRange day)
        {
            return _repository.GetLastLogIns(userType, day);
        }
    }
}