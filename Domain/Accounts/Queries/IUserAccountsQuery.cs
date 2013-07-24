using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Accounts.Queries
{
    public interface IUserAccountsQuery
    {
        bool IsEnabled(Guid userId);
        bool IsActive(Guid userId);
        IList<Guid> GetEnabledAccountIds(IEnumerable<Guid> userIds);
        IList<Guid> GetCreatedAccountIds(DateTimeRange dateTimeRange);

        DateTime? GetCreatedTime(Guid userId);
        IList<Guid> GetCreatedAccountIds(IEnumerable<Guid> userIds, DateTimeRange dateTimeRange);

        IList<UserAccountAction> GetEnablements(Guid userId);
        IList<UserAccountAction> GetDisablements(Guid userId);
        IList<UserAccountAction> GetActivations(Guid userId);
        IList<UserAccountAction> GetDeactivations(Guid userId);
    }
}
