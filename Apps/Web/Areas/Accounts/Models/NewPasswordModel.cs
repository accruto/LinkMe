using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Accounts.Models
{
    [Passwords]
    public class ChangePasswordModel
        : IHavePasswords
    {
        public bool MustChange { get; set; }
        public bool IsAdministrator { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Password]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }

        string IHavePasswords.Password
        {
            get { return NewPassword; }
        }

        string IHavePasswords.ConfirmPassword
        {
            get { return ConfirmNewPassword; }
        }
    }
}
