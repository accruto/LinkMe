using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberEmails
{
    public abstract class MemberEmail
        : UserEmail
    {
        protected MemberEmail(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected MemberEmail(ICommunicationUser to)
            : base(to)
        {
        }
    }
}