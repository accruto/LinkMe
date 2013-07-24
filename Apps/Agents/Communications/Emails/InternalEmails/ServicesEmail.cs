using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class ServicesEmail
        : InternalEmail
    {
        protected ServicesEmail(ICommunicationUser from)
            : base(null, from)
        {
        }

        protected ServicesEmail()
            : base(null)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}
