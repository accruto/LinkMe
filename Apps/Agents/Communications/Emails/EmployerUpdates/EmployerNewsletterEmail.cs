using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerUpdates
{
    public class EmployerNewsletterEmail
        : EmployerUpdate
    {
        public EmployerNewsletterEmail(ICommunicationUser employer)
            : base(employer)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}