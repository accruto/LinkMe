using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using Constants = LinkMe.Domain.Contacts.Constants;

namespace LinkMe.Web.Areas.Employers.Models.Settings
{
    public class SettingsModel
    {
        [Required, FirstName, Trim]
        public string FirstName { get; set; }
        [Required, LastName, Trim]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required, OrganisationName]
        public string OrganisationName { get; set; }
        public bool CanEditOrganisationName { get; set; }

        public EmployerSubRole Role { get; set; }
        [Required, StringLength(Constants.MaxJobTitleLength)]
        public string JobTitle { get; set; }

        [Required, PhoneNumber]
        public string PhoneNumber { get; set; }

        public IList<Guid> IndustryIds { get; set; }
        public IList<Industry> Industries { get; set; }

        public bool HasLoginCredentials { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool UseLinkedInProfile { get; set; }

        public IDictionary<Credit, Allocation> Allocations { get; set; }

        public bool ShowSuggestedCandidates { get; set; }
        public bool SendSuggestedCandidates { get; set; }
        public bool ReceiveSuggestedCandidates { get; set; }
        public bool EmailEmployerUpdate { get; set; }
        public bool EmailCampaign { get; set; }
    }
}