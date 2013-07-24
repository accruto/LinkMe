using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class Custodian
        : RegisteredUser, ICustodian, IHaveEmailAddress
    {
        [Required]
        public EmailAddress EmailAddress { get; set; }

        public override UserType UserType
        {
            get { return UserType.Custodian; }
        }

        public Guid? AffiliateId { get; set; }

        protected override Guid? GetAffiliateId()
        {
            return AffiliateId;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddress;
        }
    }
}
