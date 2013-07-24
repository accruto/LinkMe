using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration
{
    public class EmailVerification
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [IsSet]
        public Guid UserId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required, DefaultNewCode(false)]
        public string VerificationCode { get; set; }
    }
}
