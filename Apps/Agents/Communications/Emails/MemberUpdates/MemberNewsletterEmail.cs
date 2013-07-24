using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberUpdates
{
    public class MemberNewsletterEmail
        : MemberUpdate
    {
        public MemberNewsletterEmail(ICommunicationUser member)
            : base(member)
        {
        }

        public override bool RequiresActivation
        {
            get { return true; }
        }
    }
}