using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Members.Models.Profiles
{
    public class DesiredJobMemberModel
    {
        public bool SendSuggestedJobs { get; set; }
        public string DesiredJobTitle { get; set; }
        public JobTypes DesiredJobTypes { get; set; }
        public CandidateStatus Status { get; set; }
        [Required]
        public decimal? DesiredSalaryLowerBound { get; set; }
        public SalaryRate DesiredSalaryRate { get; set; }
        public bool IsSalaryNotVisible { get; set; }
        public RelocationPreference RelocationPreference { get; set; }
        public IList<int> RelocationCountryIds { get; set; }
        public IList<int> RelocationCountryLocationIds { get; set; }
    }

    public class DesiredJobReferenceModel
    {
        public IList<Country> Countries { get; set; }
        public Country CurrentCountry { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
        public SalaryRate[] SalaryRates { get; set;}
    }

    public class DesiredJobModel
    {
        public DesiredJobMemberModel Member { get; set; }
        public DesiredJobReferenceModel Reference { get; set; }
    }
}