using System;

namespace LinkMe.Domain.Roles.Registration.Queries
{
    public interface IEmailVerificationsQuery
    {
        EmailVerification GetEmailVerification(Guid userId, string emailAddress);
        EmailVerification GetEmailVerification(string verificationCode);
    }
}
