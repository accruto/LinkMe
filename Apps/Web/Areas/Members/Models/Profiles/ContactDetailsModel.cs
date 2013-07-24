using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    [Serializable]
    public class ContactDetailsMemberModel
    {
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }

        public int? CountryId { get; set; }
        [Required]
        public string Location { get; set; }

        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
        [EmailAddress(true)]
        public string SecondaryEmailAddress { get; set; }

        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }
        public PhoneNumberType PhoneNumberType { get; set; }

        [PhoneNumber]
        public string SecondaryPhoneNumber { get; set; }
        public PhoneNumberType SecondaryPhoneNumberType { get; set; }

        public string Citizenship { get; set; }
        
        [Required]
        public VisaStatus? VisaStatus { get; set; }
        public EthnicStatus EthnicStatus { get; set; }
        public Gender Gender { get; set; }
        public PartialDate? DateOfBirth { get; set; }
        public Guid? PhotoId { get; set; }
    }

    public class ContactDetailsReferenceModel
    {
        public IList<int?> Months { get; set; }
        public IList<int?> Years { get; set; }
        public IList<Country> Countries { get; set; }
    }

    public class ContactDetailsModel
    {
        public ContactDetailsMemberModel Member { get; set; }
        public ContactDetailsReferenceModel Reference { get; set; }
        public bool CanEditContactDetails { get; set; }
    }
}