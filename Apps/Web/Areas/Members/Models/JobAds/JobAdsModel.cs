using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Areas.Members.Models.JobAds
{
    public class JobAdsModel
    {
        public Country Country { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
        public IList<Industry> Industries { get; set; }
    }

    public class LocationsJobAdsModel
    {
        public Industry Industry { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
    }

    public class IndustriesJobAdsModel
    {
        public IUrlNamedLocation Location { get; set; }
        public IList<Industry> Industries { get; set; }
    }
}
