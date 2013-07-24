using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Security
{
    public class Login
    {
        [Required, Trim]
        public string LoginId { get; set; }
        [Required, Trim]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(LoginId)
                    && string.IsNullOrEmpty(Password);
            }
        }
    }
}