using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Accounts.Models
{
    public class NewPasswordModel
    {
        [Required]
        public string LoginId { get; set; }
    }
}
