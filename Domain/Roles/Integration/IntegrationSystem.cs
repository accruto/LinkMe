using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Integration
{
    public abstract class IntegrationSystem
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
