using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Web.Models.Credits;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class OrganisationExercisedCreditsModel
        : ExercisedCreditsModel
    {
        public Organisation Organisation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IDictionary<Guid, Employer> Employers { get; set; }
    }

    public class OrganisationAllocationExercisedCreditsModel
        : ExercisedCreditsModel
    {
        public Organisation Organisation { get; set; }
        public IDictionary<Guid, Employer> Employers { get; set; }
    }
}