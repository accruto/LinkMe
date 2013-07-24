using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Orders
{
    public class Purchaser
    {
        [IsSet]
        public Guid Id { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public string EmailAddress { get; set; }
    }
}
