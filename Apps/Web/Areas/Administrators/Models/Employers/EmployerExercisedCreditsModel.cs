using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Web.Models.Credits;

namespace LinkMe.Web.Areas.Administrators.Models.Employers
{
    public class EmployerExercisedCreditsModel
        : ExercisedCreditsModel
    {
        public IEmployer Employer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IDictionary<Guid, Organisation> Organisations { get; set; }
    }

    public class EmployerAllocationExercisedCreditsModel
        : ExercisedCreditsModel
    {
        public IEmployer Employer { get; set; }
    }
}