using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Communities
{
    public class Community
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool HasMembers { get; set; }
        public bool HasOrganisations { get; set; }
        public bool OrganisationsCanSearchAllMembers { get; set; }
        public bool OrganisationsAreBranded { get; set; }
        public string EmailDomain { get; set; }
    }
}
