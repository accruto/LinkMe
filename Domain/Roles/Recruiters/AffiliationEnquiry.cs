using System;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Recruiters
{
    [Serializable]
    public class AffiliationEnquiry
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}