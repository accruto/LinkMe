using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class CreateEmployerModel
    {
        [Required, LoginId]
        public string LoginId { get; set; }
        [Required, Password]
        public string Password { get; set; }
        [Required, EmailAddress(true)]
        public string EmailAddress { get; set; }
        [Required, FirstName]
        public string FirstName { get; set; }
        [Required, LastName]
        public string LastName { get; set; }
        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public EmployerSubRole SubRole { get; set; }
        public Guid? IndustryId { get; set; }
        public IList<Industry> Industries { get; set; }
    }
}