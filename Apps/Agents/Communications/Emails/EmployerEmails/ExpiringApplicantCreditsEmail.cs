using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class ExpiringApplicantCreditsEmail
        : ExpiringCreditsEmail
    {
        public ExpiringApplicantCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager, int quantity)
            : base(to, accountManager, quantity)
        {
        }
    }
}
