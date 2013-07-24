using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.AdministratorEmails
{
    public abstract class AdministratorEmail
        : UserEmail
    {
        protected AdministratorEmail(ICommunicationUser to)
            : base(to)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}