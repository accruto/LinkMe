using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Integration
{
    public class IntegratorUser
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public Guid IntegrationSystemId { get; set; }
        public IntegratorPermissions Permissions { get; set; }
    }
}
