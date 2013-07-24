using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public class ActivationEmail
        : EmailVerificationEmail
    {
        public ActivationEmail(IMember to, EmailVerification emailVerification)
            : base(to, emailVerification)
        {
        }
    }
}