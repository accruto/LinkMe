using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.MemberAlerts
{
    public abstract class MemberAlertEmail
        : MemberEmail
    {
        protected MemberAlertEmail(ICommunicationUser to)
            : base(to)
        {
        }
    }
}