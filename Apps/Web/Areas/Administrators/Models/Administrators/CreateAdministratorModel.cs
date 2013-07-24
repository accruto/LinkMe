using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Administrators.Models.Administrators
{
    public class CreateAdministratorModel
    {
        [Required, LoginId]
        public string LoginId { get; set; }
        [Required, AdministratorPassword]
        public string Password { get; set; }
        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
    }
}