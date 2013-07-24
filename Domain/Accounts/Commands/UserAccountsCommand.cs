using System;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Domain.Accounts.Commands
{
    public class UserAccountsCommand
        : IUserAccountsCommand
    {
        private readonly IUserAccountsRepository _repository;

        public UserAccountsCommand(IUserAccountsRepository repository)
        {
            _repository = repository;
        }

        void IUserAccountsCommand.EnableUserAccount(IUserAccount user, Guid enabledById)
        {
            _repository.EnableUserAccount(user.Id, enabledById, DateTime.Now);

            // Fire events.

            var handlers = UserEnabled;
            if (handlers != null)
                handlers(this, new UserAccountEventArgs(user.Id, user.UserType, enabledById));
        }

        void IUserAccountsCommand.DisableUserAccount(IUserAccount user, Guid disabledById)
        {
            _repository.DisableUserAccount(user.Id, disabledById, DateTime.Now);

            // Fire events.

            var handlers = UserDisabled;
            if (handlers != null)
                handlers(this, new UserAccountEventArgs(user.Id, user.UserType, disabledById));
        }

        void IUserAccountsCommand.ActivateUserAccount(IUserAccount user, Guid activatedById)
        {
            _repository.ActivateUserAccount(user.Id, activatedById, DateTime.Now);

            // Fire events.

            var handlers = UserActivated;
            if (handlers != null)
                handlers(this, new UserAccountEventArgs(user.Id, user.UserType, activatedById));
        }

        void IUserAccountsCommand.DeactivateUserAccount(IUserAccount user, Guid deactivatedById)
        {
            _repository.DeactivateUserAccount(user.Id, deactivatedById, DateTime.Now);

            // Fire events.

            var handlers = UserDeactivated;
            if (handlers != null)
                handlers(this, new UserDeactivatedEventArgs(user.Id, user.UserType, deactivatedById, DeactivationReason.Other, null));
        }

        void IUserAccountsCommand.DeactivateUserAccount(IUserAccount user, DeactivationReason reason, string comments)
        {
            // Deactivated themselves.

            _repository.DeactivateUserAccount(user.Id, user.Id, DateTime.Now, reason, comments);

            // Fire events.

            var handlers = UserDeactivated;
            if (handlers != null)
                handlers(this, new UserDeactivatedEventArgs(user.Id, user.UserType, user.Id, reason, comments));
        }

        [Publishes(PublishedEvents.UserEnabled)]
        public event EventHandler<UserAccountEventArgs> UserEnabled;

        [Publishes(PublishedEvents.UserDisabled)]
        public event EventHandler<UserAccountEventArgs> UserDisabled;

        [Publishes(PublishedEvents.UserActivated)]
        public event EventHandler<UserAccountEventArgs> UserActivated;

        [Publishes(PublishedEvents.UserDeactivated)]
        public event EventHandler<UserDeactivatedEventArgs> UserDeactivated;
    }
}