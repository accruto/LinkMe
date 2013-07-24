using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Models.Accounts
{
    public abstract class Join
        : IHavePasswords
    {
        [Required, Password]
        public string JoinPassword { get; set; }
        [Required]
        public string JoinConfirmPassword { get; set; }

        string IHavePasswords.Password
        {
            get { return JoinPassword; }
        }

        string IHavePasswords.ConfirmPassword
        {
            get { return JoinConfirmPassword; }
        }

        protected bool IsJoinEmpty
        {
            get
            {
                return string.IsNullOrEmpty(JoinPassword)
                    && string.IsNullOrEmpty(JoinConfirmPassword);
            }
        }
    }
}
