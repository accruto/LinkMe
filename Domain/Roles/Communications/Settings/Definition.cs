using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Communications.Settings
{
    public class Definition
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}
