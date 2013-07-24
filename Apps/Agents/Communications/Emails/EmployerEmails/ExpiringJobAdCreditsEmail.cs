using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class ExpiringJobAdCreditsEmail
        : ExpiringCreditsEmail
    {
        public ExpiringJobAdCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager, int quantity)
            : base(to, accountManager, quantity)
        {
        }
    }
}
