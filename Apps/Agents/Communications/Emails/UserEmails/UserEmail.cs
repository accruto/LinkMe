using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Agents.Communications.Emails.UserEmails
{
    public abstract class UserEmail
        : TemplateEmail
    {
        protected UserEmail(ICommunicationUser to, ICommunicationUser from)
            : base(to, from)
        {
        }

        protected UserEmail(ICommunicationUser to)
            : base(to, null)
        {
        }

        protected static ICommunicationUser GetMember(IMember member, EmailAddress emailAddress)
        {
            return new EmailMember(member, emailAddress);
        }

        protected static ICommunicationUser GetEmployer(IEmployer employer, string emailAddress)
        {
            return employer == null
                ? GetUnregisteredEmployer(emailAddress, null, null)
                : new EmailEmployer(employer, emailAddress);
        }

        protected static ICommunicationUser GetUnregisteredEmployer(string emailAddress, string firstName, string lastName)
        {
            return new UnregisteredEmployer
            {
                EmailAddress = new EmailAddress { Address = emailAddress, IsVerified = true },
                FirstName = firstName,
                LastName = lastName,
                IsEnabled = true,
            };
        }

        protected static ICommunicationUser GetUnregisteredEmployer(string emailAddress)
        {
            return new UnregisteredEmployer
            {
                EmailAddress = new EmailAddress { Address = emailAddress, IsVerified = true },
                IsEnabled = true,
            };
        }

        protected static ICommunicationUser GetUnregisteredMember(string emailAddress, string fullName)
        {
            return new UnregisteredMember
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = true } },
                FirstName = fullName,
                IsEnabled = true,
            };
        }

        protected static ICommunicationUser GetUnregisteredMember(string emailAddress)
        {
            return new UnregisteredMember
            {
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = true } },
                IsEnabled = true,
            };
        }
    }
}