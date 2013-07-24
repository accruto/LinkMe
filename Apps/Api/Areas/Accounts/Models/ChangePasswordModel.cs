using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Api.Areas.Accounts.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Password]
        public string NewPassword { get; set; }
    }
}