using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberUpdates
{
    public abstract class MemberUpdate
        : MemberEmail
    {
        protected MemberUpdate(ICommunicationUser to)
            : base(to)
        {
        }
    }
}