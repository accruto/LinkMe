using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Accounts.Queries
{
    public class UserAccountsQuery
        : IUserAccountsQuery
    {
        private readonly IUserAccountsRepository _repository;

        public UserAccountsQuery(IUserAccountsRepository repository)
        {
            _repository = repository;
        }

        bool IUserAccountsQuery.IsEnabled(Guid userId)
        {
            return _repository.IsEnabled(userId);
        }

        bool IUserAccountsQuery.IsActive(Guid userId)
        {
            return _repository.IsActive(userId);
        }

        IList<Guid> IUserAccountsQuery.GetEnabledAccountIds(IEnumerable<Guid> userIds)
        {
            return _repository.GetEnabledAccountIds(userIds);
        }

        IList<Guid> IUserAccountsQuery.GetCreatedAccountIds(DateTimeRange dateTimeRange)
        {
            return _repository.GetCreatedAccountIds(dateTimeRange);
        }

        DateTime? IUserAccountsQuery.GetCreatedTime(Guid userId)
        {
            return _repository.GetCreatedTime(userId);
        }

        IList<Guid> IUserAccountsQuery.GetCreatedAccountIds(IEnumerable<Guid> userIds, DateTimeRange dateTimeRange)
        {
            return _repository.GetCreatedAccountIds(userIds, dateTimeRange);
        }

        IList<UserAccountAction> IUserAccountsQuery.GetEnablements(Guid userId)
        {
            return _repository.GetEnablements(userId);
        }

        IList<UserAccountAction> IUserAccountsQuery.GetDisablements(Guid userId)
        {
            return _repository.GetDisablements(userId);
        }

        IList<UserAccountAction> IUserAccountsQuery.GetActivations(Guid userId)
        {
            return _repository.GetActivations(userId);
        }

        IList<UserAccountAction> IUserAccountsQuery.GetDeactivations(Guid userId)
        {
            return _repository.GetDeactivations(userId);
        }
    }
}
