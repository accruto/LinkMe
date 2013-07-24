using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Users.Employers;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Asp.Security
{
    public class EmployerAccount
    {
        public EmployerAccount()
        {
            SubRole = Defaults.SubRole;
        }

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
        public IList<Guid> IndustryIds { get; set; }
    }
}
