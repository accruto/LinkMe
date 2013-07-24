using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Location;

namespace LinkMe.Web.Areas.Employers.Models.Candidates
{
    public class CandidatesModel
    {
        public Country Country { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
        public IList<Salary> SalaryBands { get; set; }
    }

    public class LocationsCandidatesModel
    {
        public Salary SalaryBand { get; set; }
        public IList<CountrySubdivision> CountrySubdivisions { get; set; }
        public IList<Region> Regions { get; set; }
    }

    public class SalaryBandsCandidatesModel
    {
        public IUrlNamedLocation Location { get; set; }
        public IList<Salary> SalaryBands { get; set; }
    }
}
