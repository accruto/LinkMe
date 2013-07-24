using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Settings
{
    [Passwords]
    public class PasswordsModel
        : IHavePasswords
    {
        [Required, Password]
        public string CurrentPassword { get; set; }
        [Required, Password]
        public string Password { get; set; }
        [Required, Password]
        public string ConfirmPassword { get; set; }
    }
}