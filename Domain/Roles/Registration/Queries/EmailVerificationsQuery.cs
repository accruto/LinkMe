using System;

namespace LinkMe.Domain.Roles.Registration.Queries
{
    public class EmailVerificationsQuery
        : IEmailVerificationsQuery
    {
        private readonly IRegistrationRepository _repository;

        public EmailVerificationsQuery(IRegistrationRepository repository)
        {
            _repository = repository;
        }

        EmailVerification IEmailVerificationsQuery.GetEmailVerification(Guid userId, string emailAddress)
        {
            return _repository.GetEmailVerification(userId, emailAddress);
        }

        EmailVerification IEmailVerificationsQuery.GetEmailVerification(string verificationCode)
        {
            return _repository.GetEmailVerification(verificationCode);
        }
    }
}