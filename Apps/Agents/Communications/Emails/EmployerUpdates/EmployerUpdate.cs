using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerUpdates
{
    public abstract class EmployerUpdate
        : EmployerEmail
    {
        protected EmployerUpdate(ICommunicationUser to)
            : base(to)
        {
        }
    }
}