using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications
{
    public abstract class MemberToMemberNotification
        : MemberEmail
    {
        protected MemberToMemberNotification(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected MemberToMemberNotification(ICommunicationUser to)
            : base(to)
        {
        }
    }
}