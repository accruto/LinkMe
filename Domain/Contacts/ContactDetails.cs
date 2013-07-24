using System;
using System.Linq;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Contacts
{
    public class ContactDetails
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [FirstName]
        public string FirstName { get; set; }
        [LastName]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        [EmailAddress(EmailAddressValidationMode.MultipleEmails)]
        public string SecondaryEmailAddresses { get; set; }
        /*[PhoneNumber]*/
        public string FaxNumber { get; set; }
        /*[PhoneNumber]*/
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get { return FirstName.CombineLastName(LastName); }
        }

        public bool IsEmpty
        {
            get
            {
                return new[]
                {
                    FirstName,
                    LastName,
                    EmailAddress,
                    PhoneNumber,
                    FaxNumber,
                    CompanyName,
                    SecondaryEmailAddresses
                }.All(string.IsNullOrEmpty);
            }
        }
    }
}
