using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Verticals
{
    public class Vertical
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required]
        public string Name  { get; set; }

        public bool IsDeleted { get; set; }
        public string Url { get; set; }
        public string Host { get; set; }
        public string SecondaryHost { get; set; }
        public string TertiaryHost { get; set; }
        public int? CountryId { get; set; }

        public bool RequiresExternalLogin { get; set; }
        public string ExternalLoginUrl { get; set; }
        public string ExternalCookieDomain { get; set; }

        public string ReturnEmailAddress { get; set; }
        public string MemberServicesEmailAddress { get; set; }
        public string EmployerServicesEmailAddress { get; set; }
        public string EmailDisplayName { get; set; }
    }
}
