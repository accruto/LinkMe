using System;
using System.Collections.Generic;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class Employer
        : RegisteredUser, IEmployer, ICreditOwner, IHaveEmailAddress, IHavePhoneNumber
    {
        [Required]
        public EmailAddress EmailAddress { get; set; }
        [Required, PhoneNumber, Prepare, Validate]
        public PhoneNumber PhoneNumber { get; set; }
        [Required]
        public IOrganisation Organisation { get; set; }
        public IList<Industry> Industries { get; set; }
        [StringLength(Constants.MaxJobTitleLength)]
        public string JobTitle { get; set; }
        public EmployerSubRole SubRole { get; set; }

        public override UserType UserType
        {
            get { return UserType.Employer; }
        }

        protected override Guid? GetAffiliateId()
        {
            return Organisation == null ? null : Organisation.AffiliateId;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddress;
        }
    }

    public class UnregisteredEmployer
        : RegisteredUser, IEmployer
    {
        public EmailAddress EmailAddress { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public IOrganisation Organisation { get; set; }
        public IList<Industry> Industries { get; set; }
        public string JobTitle { get; set; }
        public EmployerSubRole SubRole { get; set; }

        public override UserType UserType
        {
            get { return UserType.Anonymous; }
        }

        protected override Guid? GetAffiliateId()
        {
            return Organisation == null ? null : Organisation.AffiliateId;
        }

        protected override EmailAddress GetCommunicationEmailAddress()
        {
            return EmailAddress;
        }
    }
}
