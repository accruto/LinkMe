using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Employers.Models.Settings
{
    [Passwords]
    public class LoginCredentialsModel
        : IHavePasswords
    {
        [Required, LoginId]
        public string LoginId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}