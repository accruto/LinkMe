using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public class VerificationEmail
        : EmailVerificationEmail
    {
        public VerificationEmail(IMember to, EmailVerification emailVerification)
            : base(to, emailVerification)
        {
        }
    }
}