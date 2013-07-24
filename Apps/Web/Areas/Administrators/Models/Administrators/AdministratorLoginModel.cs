using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Administrators.Models.Administrators
{
    public class AdministratorLoginModel
    {
        public string LoginId { get; set; }
        [Required, AdministratorPassword]
        public string Password { get; set; }
    }
}