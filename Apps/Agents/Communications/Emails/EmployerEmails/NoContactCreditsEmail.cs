using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public class NoContactCreditsEmail
        : CreditsEmail
    {
        public NoContactCreditsEmail(ICommunicationUser to, ICommunicationUser accountManager)
            : base(to, accountManager)
        {
        }
    }
}
