using LinkMe.Apps.Agents.Security;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Administrators.Models.Members
{
    public class MemberLoginModel
    {
        public string LoginId { get; set; }
        [Required, Password]
        public string Password { get; set; }
        public bool SendPasswordEmail { get; set; }
    }
}