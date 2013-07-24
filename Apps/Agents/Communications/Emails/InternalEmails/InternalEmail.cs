using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.InternalEmails
{
    public abstract class InternalEmail
        : TemplateEmail
    {
        protected InternalEmail(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected InternalEmail(ICommunicationUser to)
            : base(to, null)
        {
        }

        protected static ICommunicationUser GetUser(string emailAddress, string firstName, string lastName, UserType userType)
        {
            return new EmailUser(emailAddress, firstName, lastName, userType);
        }

        protected static ICommunicationUser GetUser(string emailAddress)
        {
            return new EmailUser(emailAddress);
        }
    }
}