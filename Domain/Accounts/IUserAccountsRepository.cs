using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Accounts
{
    public interface IUserAccountsRepository
    {
        void EnableUserAccount(Guid userId, Guid enabledById, DateTime time);
        void DisableUserAccount(Guid userId, Guid disabledById, DateTime time);
        void ActivateUserAccount(Guid userId, Guid activatedById, DateTime time);
        void DeactivateUserAccount(Guid userId, Guid deactivatedById, DateTime time);
        void DeactivateUserAccount(Guid userId, Guid deactivatedById, DateTime time, DeactivationReason reason, string comments);

        bool IsEnabled(Guid userId);
        bool IsActive(Guid userId);
        IList<Guid> GetEnabledAccountIds(IEnumerable<Guid> userIds);

        DateTime? GetCreatedTime(Guid userId);
        IList<Guid> GetCreatedAccountIds(DateTimeRange dateTimeRange);
        IList<Guid> GetCreatedAccountIds(IEnumerable<Guid> userIds, DateTimeRange dateTimeRange);

        IList<UserAccountAction> GetEnablements(Guid userId);
        IList<UserAccountAction> GetDisablements(Guid userId);
        IList<UserAccountAction> GetActivations(Guid userId);
        IList<UserAccountAction> GetDeactivations(Guid userId);
    }
}
