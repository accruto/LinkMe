using System;

namespace LinkMe.Domain.Roles.Registration.Commands
{
    public interface IEmailVerificationsCommand
    {
        void CreateEmailVerification(EmailVerification emailVerification);
        void DeleteEmailVerification(Guid id);

        void VerifyEmailAddress(Guid userId, string emailAddress);
        void UnverifyEmailAddress(string emailAddress, string reason);
    }
}
