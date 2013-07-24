using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.CustodianEmails
{
    public abstract class CustodianEmail
        : UserEmail
    {
        protected CustodianEmail(ICommunicationUser to)
            : base(to)
        {
        }
    }
}