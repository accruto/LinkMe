using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Registration
{
    public class ExternalReferralSource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
