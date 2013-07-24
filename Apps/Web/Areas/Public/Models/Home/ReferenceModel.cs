using System.Collections.Generic;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Resources;
using LinkMe.Web.Areas.Shared.Models;

namespace LinkMe.Web.Areas.Public.Models.Home
{
    public class ReferenceModel
    {
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public int StepSalary { get; set; }
        public int MinHourlySalary { get; set; }
        public int MaxHourlySalary { get; set; }
        public int StepHourlySalary { get; set; }
        public IList<Industry> Industries { get; set; }
        public FeaturedStatistics FeaturedStatistics { get; set; }
        public IList<FeaturedEmployerModel> FeaturedEmployers { get; set; }
        public IList<FeaturedLinkModel> FeaturedJobAds { get; set; }
        public IList<FeaturedLinkModel> FeaturedCandidateSearches { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
        public Country DefaultCountry { get; set; }
        public QnA FeaturedAnsweredQuestion { get; set; }
        public IList<Category> Categories { get; set; }
    }
}
