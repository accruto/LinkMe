using System;

namespace LinkMe.Domain.Accounts.Commands
{
    public interface IUserAccountsCommand
    {
        void EnableUserAccount(IUserAccount user, Guid enabledById);
        void DisableUserAccount(IUserAccount user, Guid disabledById);

        void ActivateUserAccount(IUserAccount user, Guid activatedById);
        void DeactivateUserAccount(IUserAccount user, Guid deactivatedById);
        void DeactivateUserAccount(IUserAccount user, DeactivationReason reason, string comments);
    }
}