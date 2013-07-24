using System.Collections.Generic;

namespace LinkMe.Domain.Location.Queries
{
    public interface ILocationQuery
    {
        IList<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountry(string name);
        Country GetCountryByIsoCode(string isoCode);

        NamedLocation GetNamedLocation(int id);
        NamedLocation GetNamedLocation(int id, bool throwIfNotFound);

        GeographicalArea GetGeographicalArea(int id, bool throwIfNotFound);

        CountrySubdivision GetCountrySubdivision(int id);
        CountrySubdivision GetCountrySubdivision(int id, bool throwIfNotFound);
        CountrySubdivision GetCountrySubdivision(Country country, string nameOrAlias);
        CountrySubdivision GetCountrySubdivision(GeoCoordinates coordinates);
        IList<CountrySubdivision> GetCountrySubdivisions(Country country);

        Region GetRegion(int id);
        Region GetRegion(Country country, string nameOrAlias);
        IList<Region> GetRegions(Country country);

        Locality GetLocality(int id);
        Locality GetLocality(int id, bool throwIfNotFound);
        Locality GetLocality(GeoCoordinates coordinates);
        Locality GetClosestLocality(Country country, GeoCoordinates coordinates);
        IList<Locality> GetLocalities(Country country);
        IList<Locality> GetLocalities(Region region);

        PostalCode GetPostalCode(Country country, string postcode);
        IList<PostalCode> GetPostalCodes(Locality locality);

        PostalSuburb GetPostalSuburb(PostalCode postalCode, string suburb);
        IList<PostalSuburb> GetPostalSuburbs(PostalCode postalCode);

        bool IsLocationSynonym(Country country, string location, NamedLocation namedLocation);

        IList<PartialMatch> FindPartialMatchedLocations(Country country, string location, int maximum);
        IList<PartialMatch> FindPartialMatchedPostalSuburbs(Country country, string location, int maximum);

        void ResolveLocation(LocationReference locationReference, Country country);
        void ResolveLocation(LocationReference locationReference, Country country, string location);
        LocationReference ResolveLocation(Country country, string location);
        LocationReference ResolveLocation(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions);

        void ResolvePostalSuburb(LocationReference locationReference, Country country, string location);
        LocationReference ResolvePostalSuburb(Country country, string location);
        LocationReference ResolvePostalSuburb(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions);

        IUrlNamedLocation ResolveUrlNamedLocation(Country country, string urlName);

        bool Contains(LocationReference location1, LocationReference location2);
        bool Contains(CountrySubdivision subdivision, LocationReference location);
        bool Contains(Region region, LocationReference location);
    }
}