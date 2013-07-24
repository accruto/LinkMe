using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Users.Employers;
using LinkMe.Apps.Asp.Json;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Api.Areas.Employers.Models.Accounts
{
    public class EmployerJoinModel
        : JsonRequestModel
    {
        public EmployerJoinModel()
        {
            SubRole = Defaults.SubRole;
        }

        [Required, LoginId]
        public string LoginId { get; set; }
        [Required, Password]
        public string Password { get; set; }

        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }
        [Required, OrganisationName]
        public string OrganisationName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public EmployerSubRole SubRole { get; set; }
    }
}