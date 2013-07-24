using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Security
{
    [Passwords]
    public class AccountLoginCredentials
        : IHavePasswords
    {
        [Required, Password]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required, LoginId]
        public string LoginId { get; set; }
    }
}
