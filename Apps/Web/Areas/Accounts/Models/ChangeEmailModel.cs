using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Accounts.Models
{
    public class ChangeEmailModel
    {
        [Required, EmailAddress]
        public string EmailAddress { get; set; }
        [EmailAddress]
        public string SecondaryEmailAddress { get; set; }
    }
}
