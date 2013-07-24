using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class Administrator
        : RegisteredUser, IAdministrator, IHaveEmailAddress
    {
        [Required]
        public EmailAddress EmailAddress { get; set; }

        public override UserType UserType
        {
            get { return UserType.Administrator; }
        }

        protected override Guid? GetAffiliateId()
        {
            return null;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddress;
        }
    }

    public class UnregisteredAdministrator
        : RegisteredUser, IAdministrator
    {
        public EmailAddress EmailAddress { get; set; }

        public override UserType UserType
        {
            get { return UserType.Anonymous; }
        }

        protected override Guid? GetAffiliateId()
        {
            return null;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddress;
        }
    }
}
