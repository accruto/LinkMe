using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Validation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Web.Areas.Public.Models.Join
{
    public class JobDetailsReferenceModel
    {
        public IList<Country> Countries { get; set; }
        public Country CurrentCountry { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
        public IList<Industry> Industries { get; set; }
        public IList<int?> Months { get; set; }
        public IList<int?> Years { get; set; }
        public IList<ExternalReferralSource> ExternalReferralSources { get; set; }
    }

    public class JobDetailsMemberModel
    {
        public string JobTitle { get; set; }
        public string JobCompany { get; set; }
        public IList<Guid> IndustryIds { get; set; }
        public Profession? RecentProfession { get; set; }
        public Seniority? RecentSeniority { get; set; }
        public EducationLevel? HighestEducationLevel { get; set; }
        public string DesiredJobTitle { get; set; }
        public JobTypes DesiredJobTypes { get; set; }
        public RelocationPreference RelocationPreference { get; set; }
        public IList<int> RelocationCountryIds { get; set; }
        public IList<int> RelocationCountryLocationIds { get; set; }
        public EthnicStatus EthnicStatus { get; set; }
        public Gender Gender { get; set; }
        public PartialDate? DateOfBirth { get; set; }
        public string Citizenship { get; set; }
        public VisaStatus? VisaStatus { get; set; }
        [EmailAddress(true)]
        public string SecondaryEmailAddress { get; set; }
        [PhoneNumber]
        public string SecondaryPhoneNumber { get; set; }
        public PhoneNumberType SecondaryPhoneNumberType { get; set; }
    }

    public class JobDetailsModel
        : JoinModel
    {
        public JobDetailsMemberModel Member { get; set; }
        public bool SendSuggestedJobs { get; set; }
        public int? ExternalReferralSourceId { get; set; }
        public JobDetailsReferenceModel Reference { get; set; }
    }
}