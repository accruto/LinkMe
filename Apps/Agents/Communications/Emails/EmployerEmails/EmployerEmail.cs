using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerEmails
{
    public abstract class EmployerEmail
        : UserEmail
    {
        protected EmployerEmail(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected EmployerEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}