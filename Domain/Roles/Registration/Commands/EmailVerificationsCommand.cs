using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.PublishedEvents;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration.Commands
{
    public class EmailVerificationsCommand
        : IEmailVerificationsCommand
    {
        private readonly IRegistrationRepository _repository;

        public EmailVerificationsCommand(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        void IEmailVerificationsCommand.CreateEmailVerification(EmailVerification emailVerification)
        {
            emailVerification.Prepare();
            Validate(emailVerification);

            _repository.CreateEmailVerification(emailVerification);
        }

        void IEmailVerificationsCommand.DeleteEmailVerification(Guid id)
        {
            _repository.DeleteEmailVerification(id);
        }

        void IEmailVerificationsCommand.VerifyEmailAddress(Guid userId, string emailAddress)
        {
            _repository.VerifyEmailAddress(userId, emailAddress);

            // Fire.

            var handlers = EmailAddressVerified;
            if (handlers != null)
                handlers(this, new EmailAddressVerifiedEventArgs(userId, emailAddress));
        }

        void IEmailVerificationsCommand.UnverifyEmailAddress(string emailAddress, string reason)
        {
            // Find the users.

            var userIds = _repository.GetUserIds(emailAddress);
            foreach (var userId in userIds)
            {
                // Update.

                _repository.UnverifyEmailAddress(userId, emailAddress);

                // Fire.

                var handlers = EmailAddressUnverified;
                if (handlers != null)
                    handlers(this, new EmailAddressUnverifiedEventArgs(userId, emailAddress, reason));
            }
        }

        [Publishes(PublishedEvents.EmailAddressUnverified)]
        public event EventHandler<EmailAddressUnverifiedEventArgs> EmailAddressUnverified;

        [Publishes(PublishedEvents.EmailAddressVerified)]
        public event EventHandler<EmailAddressVerifiedEventArgs> EmailAddressVerified;

        private void Validate(EmailVerification emailVerification)
        {
            emailVerification.Validate();

            // Check that another email verification does not already exist matching this one.

            var existingEmailVerification = _repository.GetEmailVerification(emailVerification.UserId, emailVerification.EmailAddress);
            if (existingEmailVerification != null)
                throw new ApplicationException(string.Format("An email verification already exists for user '{0}' and email address '{1}'.", emailVerification.UserId, emailVerification.EmailAddress));
        }
    }
}
