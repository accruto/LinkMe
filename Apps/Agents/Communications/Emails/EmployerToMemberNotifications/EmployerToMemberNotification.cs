using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications
{
    public abstract class EmployerToMemberNotification
        : MemberEmail
    {
        protected EmployerToMemberNotification(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }
    }
}