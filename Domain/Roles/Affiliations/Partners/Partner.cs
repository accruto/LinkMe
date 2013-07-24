using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Affiliations.Partners
{
    /// <summary>
    /// This supports an early implementation of Autopeople.
    /// Really should be removed and whatever functionality depends on it
    /// should be linked into the Autopeople community.
    /// </summary>
    public class Partner
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}