using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;

namespace LinkMe.Apps.Management.Test.EmailStatus
{
    internal class MockEmailVerificationsCommand
        : IEmailVerificationsCommand
    {
        public Member Member { get; private set; }
        public string EmailBounceReason { get; private set; }

        public void Reset()
        {
            Member = null;
            EmailBounceReason = null;
        }

        void IEmailVerificationsCommand.CreateEmailVerification(EmailVerification emailVerification)
        {
            throw new NotImplementedException();
        }

        void IEmailVerificationsCommand.DeleteEmailVerification(Guid id)
        {
            throw new NotImplementedException();
        }

        void IEmailVerificationsCommand.VerifyEmailAddress(Guid userId, string emailAddress)
        {
            throw new NotImplementedException();
        }

        void IEmailVerificationsCommand.UnverifyEmailAddress(string emailAddress, string reason)
        {
            Member = new Member { Id = Guid.NewGuid(), EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = false } } };
            EmailBounceReason = reason;
        }
    }
}