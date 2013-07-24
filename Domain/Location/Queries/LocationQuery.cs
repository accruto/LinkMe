using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Location.Queries
{
    public class LocationQuery
        : ILocationQuery
    {
        private readonly ILocationRepository _repository;
        private volatile bool _initialised;

        public LocationQuery(ILocationRepository repository)
        {
            _repository = repository;

            try
            {
                Initialise();
            }
            catch (LocalityInitialisationException)
            {
            }
        }

        IList<Country> ILocationQuery.GetCountries()
        {
            CheckInitialised();
            return (from c in World.GetCountries() select c.Clone()).ToList();
        }

        Country ILocationQuery.GetCountry(int id)
        {
            CheckInitialised();
            return World.GetCountry(id).Clone();
        }

        Country ILocationQuery.GetCountry(string name)
        {
            CheckInitialised();
            return World.GetCountry(name).Clone();
        }

        Country ILocationQuery.GetCountryByIsoCode(string isoCode)
        {
            CheckInitialised();
            return World.GetCountryByIsoCode(isoCode).Clone();
        }

        NamedLocation ILocationQuery.GetNamedLocation(int id)
        {
            CheckInitialised();
            return World.GetNamedLocation(id).Clone();
        }

        NamedLocation ILocationQuery.GetNamedLocation(int id, bool throwIfNotFound)
        {
            CheckInitialised();
            return World.GetNamedLocation(id, throwIfNotFound).Clone();
        }

        GeographicalArea ILocationQuery.GetGeographicalArea(int id, bool throwIfNotFound)
        {
            CheckInitialised();
            return World.GetGeographicalArea(id, throwIfNotFound).Clone();
        }

        CountrySubdivision ILocationQuery.GetCountrySubdivision(int id)
        {
            CheckInitialised();
            return World.GetCountrySubdivision(id).Clone();
        }

        CountrySubdivision ILocationQuery.GetCountrySubdivision(int id, bool throwIfNotFound)
        {
            CheckInitialised();
            return World.GetCountrySubdivision(id, throwIfNotFound).Clone();
        }

        CountrySubdivision ILocationQuery.GetCountrySubdivision(Country country, string nameOrAlias)
        {
            CheckInitialised();
            return country.GetCountrySubdivision(nameOrAlias).Clone();
        }

        CountrySubdivision ILocationQuery.GetCountrySubdivision(GeoCoordinates coordinates)
        {
            CheckInitialised();
            return World.GetCountrySubdivision(coordinates).Clone();
        }

        IList<CountrySubdivision> ILocationQuery.GetCountrySubdivisions(Country country)
        {
            CheckInitialised();
            return (from s in country.GetCountrySubdivisions() select s.Clone()).ToList();
        }

        Region ILocationQuery.GetRegion(int id)
        {
            CheckInitialised();
            return World.GetRegion(id).Clone();
        }

        Region ILocationQuery.GetRegion(Country country, string nameOrAlias)
        {
            CheckInitialised();
            return country.GetRegion(nameOrAlias).Clone();
        }

        IList<Region> ILocationQuery.GetRegions(Country country)
        {
            CheckInitialised();
            return (from r in country.GetRegions() select r.Clone()).ToList();
        }

        Locality ILocationQuery.GetLocality(int id)
        {
            CheckInitialised();
            return World.GetLocality(id).Clone();
        }

        Locality ILocationQuery.GetLocality(int id, bool throwIfNotFound)
        {
            CheckInitialised();
            return World.GetLocality(id, throwIfNotFound).Clone();
        }

        Locality ILocationQuery.GetClosestLocality(Country country, GeoCoordinates coordinates)
        {
            CheckInitialised();
            return country.GetClosestLocality(coordinates).Clone();
        }

        IList<Locality> ILocationQuery.GetLocalities(Country country)
        {
            CheckInitialised();
            return (from l in country.GetLocalities() select l.Clone()).ToList();
        }

        IList<Locality> ILocationQuery.GetLocalities(Region region)
        {
            CheckInitialised();
            return (from l in region.GetLocalities() select l.Clone()).ToList();
        }

        Locality ILocationQuery.GetLocality(GeoCoordinates coordinates)
        {
            CheckInitialised();
            return World.GetLocality(coordinates).Clone();
        }

        PostalCode ILocationQuery.GetPostalCode(Country country, string postcode)
        {
            CheckInitialised();
            return country.GetPostalCode(postcode).Clone();
        }

        IList<PostalCode> ILocationQuery.GetPostalCodes(Locality locality)
        {
            CheckInitialised();
            return (from p in locality.GetPostalCodes() select p.Clone()).ToList();
        }

        PostalSuburb ILocationQuery.GetPostalSuburb(PostalCode postalCode, string suburb)
        {
            CheckInitialised();
            return postalCode.GetPostalSuburb(suburb).Clone();
        }

        IList<PostalSuburb> ILocationQuery.GetPostalSuburbs(PostalCode postalCode)
        {
            CheckInitialised();
            return (from p in postalCode.GetPostalSuburbs() select p.Clone()).ToList();
        }

        bool ILocationQuery.IsLocationSynonym(Country country, string location, NamedLocation namedLocation)
        {
            CheckInitialised();
            return country.IsLocationSynonym(location, namedLocation);
        }

        IList<PartialMatch> ILocationQuery.FindPartialMatchedLocations(Country country, string location, int maximum)
        {
            return LocationFinder.FindPartialMatchedLocations(country, location, maximum);
        }

        IList<PartialMatch> ILocationQuery.FindPartialMatchedPostalSuburbs(Country country, string location, int maximum)
        {
            return LocationFinder.FindPartialMatchedPostalSuburbs(country, location, maximum);
        }

        void ILocationQuery.ResolveLocation(LocationReference reference, Country country)
        {
            LocationResolver.ResolveLocation(reference, country, string.Empty, null, 0);
        }

        void ILocationQuery.ResolveLocation(LocationReference reference, Country country, string location)
        {
            LocationResolver.ResolveLocation(reference, country, location, null, 0);
        }

        LocationReference ILocationQuery.ResolveLocation(Country country, string location)
        {
            var reference = new LocationReference();
            LocationResolver.ResolveLocation(reference, country, location, null, 0);
            return reference;
        }

        LocationReference ILocationQuery.ResolveLocation(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var reference = new LocationReference();
            LocationResolver.ResolveLocation(reference, country, location, suggestions, maximumSuggestions);
            return reference;
        }

        void ILocationQuery.ResolvePostalSuburb(LocationReference reference, Country country, string location)
        {
            LocationResolver.ResolvePostalSuburb(reference, country, location, null, 0);
        }

        LocationReference ILocationQuery.ResolvePostalSuburb(Country country, string location)
        {
            var reference = new LocationReference();
            LocationResolver.ResolvePostalSuburb(reference, country, location, null, 0);
            return reference;
        }

        LocationReference ILocationQuery.ResolvePostalSuburb(Country country, string location, ICollection<NamedLocation> suggestions, int maximumSuggestions)
        {
            var reference = new LocationReference();
            LocationResolver.ResolvePostalSuburb(reference, country, location, suggestions, maximumSuggestions);
            return reference;
        }

        IUrlNamedLocation ILocationQuery.ResolveUrlNamedLocation(Country country, string urlName)
        {
            // The lists shouldn't be too big so just iterate for now.

            var finder = World.GetCountryFinder(country.Id);
            foreach (var subdivision in finder.GetCountrySubdivisions())
            {
                if (subdivision.UrlName == urlName)
                    return subdivision.Clone();
            }

            return (from region in finder.GetRegions()
                    where region.UrlName == urlName
                    select region.Clone()).FirstOrDefault();
        }

        bool ILocationQuery.Contains(LocationReference location1, LocationReference location2)
        {
            // Do some special cases first: Country.
            
            if (location1.IsCountry && location1.Country.Equals(location2.Country))
                return true;
            if (!location1.Country.Equals(location2.Country))
                return false;
            
            // Country subdivision.
            
            if (location1.NamedLocation is CountrySubdivision)
                return Contains(location1.CountrySubdivision, location2);
                
            // Region.

            if (location1.NamedLocation is Region)
                return Contains(location1.Region, location2);
            
            // For now simply compare.

            return location1.Equals(location2);
        }

        bool ILocationQuery.Contains(CountrySubdivision subdivision, LocationReference location)
        {
            return Contains(subdivision, location);
        }

        bool ILocationQuery.Contains(Region region, LocationReference location)
        {
            return Contains(region, location);
        }

        private void CheckInitialised()
        {
            if (!_initialised)
                Initialise();
        }
        
        private void Initialise()
        {
            World.Initialise(_repository);
            _initialised = true;
        }

        private static bool Contains(CountrySubdivision subdivision, LocationReference location)
        {
            return subdivision.Equals(location.CountrySubdivision);
        }

        private static bool Contains(Region region, LocationReference location)
        {
            if (location.NamedLocation is Locality)
                return Contains(region, location.Locality);
            if (location.NamedLocation is PostalCode)
                return Contains(region, location.PostalCode);
            if (location.NamedLocation is PostalSuburb)
                return Contains(region, location.PostalSuburb);
            return false;
        }

        private static bool Contains(Region region, PostalSuburb postalSuburb)
        {
            return (from regionLocality in region.GetLocalities()
                    from regionPostalCode in regionLocality.GetPostalCodes()
                    from regionPostalSuburb in regionPostalCode.GetPostalSuburbs()
                    select regionPostalSuburb).Contains(postalSuburb);
        }

        private static bool Contains(Region region, PostalCode postalCode)
        {
            return region.GetLocalities().SelectMany(regionLocality => regionLocality.GetPostalCodes()).Contains(postalCode);
        }

        private static bool Contains(Region region, Locality locality)
        {
            return Enumerable.Contains(region.GetLocalities(), locality);
        }
    }
}