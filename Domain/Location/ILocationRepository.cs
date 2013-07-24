using System.Collections.Generic;

namespace LinkMe.Domain.Location
{
    public class LocationData
    {
        public IDictionary<int, Country> Countries { get; set; }
        public IDictionary<int, IList<LocationAbbreviation>> LocationAbbreviations { get; set; }
        public IDictionary<int, IList<RelativeLocation>> RelativeLocations { get; set; }
        public IDictionary<int, Region> Regions { get; set; }
        public IDictionary<int, IList<int>> RegionLocalities { get; set; }
        public IDictionary<int, CountrySubdivision> CountrySubdivisions { get; set; }
        public IDictionary<int, Locality> Localities { get; set; }
        public IDictionary<int, IList<int>> LocalityPostalCodes { get; set; }
        public IDictionary<int, PostalCode> PostalCodes { get; set; }
        public IDictionary<int, IList<int>> PostalCodeSuburbs { get; set; }
        public IDictionary<int, PostalSuburb> PostalSuburbs { get; set; }
    }

    public interface ILocationRepository
    {
        LocationData GetLocationData();
    }
}
