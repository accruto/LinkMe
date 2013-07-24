using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class MemberServicesEmail
        : InternalEmail
    {
        protected MemberServicesEmail(ICommunicationUser from)
            : base(null, from)
        {
        }

        protected MemberServicesEmail()
            : base(null)
        {
        }

        public override bool RequiresActivation
        {
            get { return false; }
        }
    }
}
