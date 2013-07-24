using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class ExpiringContactCreditsEmail
        : ExpiringCreditsEmail
    {
        public ExpiringContactCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager, int quantity)
            : base(to, accountManager, quantity)
        {
        }
    }
}
