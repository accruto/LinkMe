using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkMe.Domain.Location
{
    internal static class World
    {
        internal static readonly StringComparer StringComparer = StringComparer.InvariantCultureIgnoreCase;
        internal static readonly StringComparison StringComparison = StringComparison.InvariantCultureIgnoreCase;

        private class WorldData
        {
            public IList<Country> Countries;
            public IDictionary<int, CountryFinder> CountryFinders;
            public LocationFinder Finder;
        }

        private static WorldData _data;

        #region Countries

        public static IList<Country> GetCountries()
        {
            return _data.Countries;
        }

        public static CountryFinder GetCountryFinder(int id)
        {
            return _data.CountryFinders[id];
        }

        public static LocationFinder GetFinder()
        {
            return _data.Finder;
        }

        public static Country GetCountry(string name)
        {
            return (from c in GetCountries()
                    where StringComparer.Equals(name, c.Name)
                    select c).SingleOrDefault();
        }

        public static Country GetCountryByIsoCode(string isoCode)
        {
            return (from c in GetCountries()
                    where StringComparer.Equals(isoCode, c.IsoCode)
                    select c).SingleOrDefault();
        }

        public static Country GetCountry(int id)
        {
            return (from c in GetCountries()
                    where c.Id == id
                    select c).SingleOrDefault();
        }

        #endregion

        #region Get

        public static NamedLocation GetNamedLocation(int id, bool throwIfNotFound)
        {
            return GetFinder().GetNamedLocation(id, throwIfNotFound);
        }

        public static NamedLocation GetNamedLocation(int id)
        {
            return GetNamedLocation<NamedLocation>(id);
        }

        public static GeographicalArea GetGeographicalArea(int id, bool throwIfNotFound)
        {
            return GetNamedLocation<GeographicalArea>(id, throwIfNotFound);
        }

        public static CountrySubdivision GetCountrySubdivision(int id, bool throwIfNotFound)
        {
            return GetNamedLocation<CountrySubdivision>(id, throwIfNotFound);
        }

        public static CountrySubdivision GetCountrySubdivision(int id)
        {
            return GetNamedLocation<CountrySubdivision>(id);
        }

        public static CountrySubdivision GetCountrySubdivision(this Country country, string nameOrAlias)
        {
            return GetCountryFinder(country.Id).GetCountrySubdivision(nameOrAlias);
        }

        public static CountrySubdivision GetCountrySubdivision(GeoCoordinates coordinates)
        {
            return GetFinder().GetCountrySubdivision(coordinates);
        }

        public static CountrySubdivision GetCountrySubdivision(NamedLocation namedLocation)
        {
            if (namedLocation is Locality)
                return GetCountrySubdivision((Locality)namedLocation);
            if (namedLocation is PostalCode)
                return GetCountrySubdivision((PostalCode)namedLocation);
            if (namedLocation is Region)
                return GetCountrySubdivision((Region)namedLocation);
            if (namedLocation is PostalSuburb)
                return ((PostalSuburb)namedLocation).CountrySubdivision;
            if (namedLocation is CountrySubdivision)
                return (CountrySubdivision)namedLocation;
            return null;
        }

        public static CountrySubdivision GetCountrySubdivision(this Region region)
        {
            // If the localities in the region all belong to the same subdivision then return that.

            CountrySubdivision subdivision = null;
            foreach (var locality in GetLocalities(region))
            {
                var localitySubdivision = GetCountrySubdivision(locality);

                // If the locality returns the country subdivision then that is the best that can be obtained.

                if (localitySubdivision.IsCountry)
                    return localitySubdivision;

                // If the locality returns a subdivision that is different then just return the country subdivision.

                if (subdivision == null)
                    subdivision = localitySubdivision;
                else if (subdivision != localitySubdivision)
                    return GetCountryFinder(subdivision.Country.Id).GetCountrySubdivision(null);
            }

            return subdivision;
        }

        public static CountrySubdivision GetCountrySubdivision(this Locality locality)
        {
            switch (locality.CountrySubdivisions.Count)
            {
                case 0:
                    return null;

                case 1:
                    return locality.CountrySubdivisions[0];

                default:

                    // More than one subdivision so return the subdivision for
                    // the country to which the locality belongs.
                    // The assumption is that a locality is confied within one country.

                    return GetCountryFinder(locality.CountrySubdivisions[0].Country.Id).GetCountrySubdivision(null);
            }
        }

        public static CountrySubdivision GetCountrySubdivision(this PostalCode postalCode)
        {
            return GetCountrySubdivision(postalCode.Locality);
        }

        public static IList<CountrySubdivision> GetCountrySubdivisions(this Country country)
        {
            return GetCountryFinder(country.Id).GetCountrySubdivisions();
        }

        public static Region GetRegion(int id)
        {
            return GetNamedLocation<Region>(id);
        }

        public static Region GetRegion(this Country country, string nameOrAlias)
        {
            return GetCountryFinder(country.Id).GetRegion(nameOrAlias);
        }

        public static IList<Region> GetRegions(this Country country)
        {
            return GetCountryFinder(country.Id).GetRegions();
        }

        public static Locality GetLocality(int id)
        {
            return GetNamedLocation<Locality>(id);
        }

        public static Locality GetLocality(int id, bool throwIfNotFound)
        {
            return GetNamedLocation<Locality>(id, throwIfNotFound);
        }

        public static Locality GetLocality(GeoCoordinates coordinates)
        {
            return GetFinder().GetLocality(coordinates);
        }

        public static IList<Locality> GetLocalities(this Country country)
        {
            return GetCountryFinder(country.Id).GetLocalities();
        }

        public static Locality GetClosestLocality(this Country country, GeoCoordinates coordinates)
        {
            return GetCountryFinder(country.Id).GetClosestLocality(coordinates);
        }

        public static IList<Locality> GetLocalities(this Region region)
        {
            return GetFinder().GetLocalities(region);
        }

        public static PostalCode GetPostalCode(this Country country, string postcode)
        {
            return GetCountryFinder(country.Id).GetPostalCode(postcode);
        }

        public static IList<PostalCode> GetPostalCodes(this Locality locality)
        {
            return GetFinder().GetPostalCodes(locality);
        }

        public static PostalSuburb GetPostalSuburb(this PostalCode postalCode, string name)
        {
            foreach (var postalSuburb in postalCode.GetPostalSuburbs())
            {
                if (StringComparer.Compare(postalSuburb.Name, name) == 0)
                    return postalSuburb;
            }

            return null;
        }

        public static IList<PostalSuburb> GetPostalSuburbs(this PostalCode postalCode)
        {
            return GetFinder().GetPostalSuburbs(postalCode);
        }

        public static bool IsLocationSynonym(this Country country, string location, NamedLocation namedLocation)
        {
            return GetCountryFinder(country.Id).IsLocationSynonym(location, namedLocation);
        }

        private static T GetNamedLocation<T>(int id)
            where T : NamedLocation
        {
            return GetNamedLocation<T>(id, false);
        }

        private static T GetNamedLocation<T>(int id, bool throwIfNotFound)
            where T : NamedLocation
        {
            var namedLocation = GetNamedLocation(id, throwIfNotFound);
            if (namedLocation is T)
                return namedLocation as T;

            if (throwIfNotFound)
                throw new ApplicationException("There is no " + typeof(T).Name + " with id " + id + ".");
            return null;
        }

        #endregion

        #region Set

        public static void Set(this LocationReference locationReference, Country country, string unstructuredLocation)
        {
            locationReference.Set(
                unstructuredLocation,
                GetCountryFinder(country.Id).GetCountrySubdivision(null).Clone(),
                GetCountryFinder(country.Id).GetCountrySubdivision(null).Clone(),
                null);
        }

        public static void Set(this LocationReference locationReference, NamedLocation namedLocation)
        {
            if (namedLocation is CountrySubdivision)
                Set(locationReference, (CountrySubdivision)namedLocation);
            else if (namedLocation is Region)
                Set(locationReference, (Region)namedLocation);
            else if (namedLocation is Locality)
                Set(locationReference, (Locality)namedLocation);
            else if (namedLocation is PostalCode)
                Set(locationReference, (PostalCode)namedLocation);
            else if (namedLocation is PostalSuburb)
                Set(locationReference, (PostalSuburb)namedLocation);
        }

        public static void Set(this LocationReference locationReference, string unstructuredLocation, CountrySubdivision countrySubdivision)
        {
            locationReference.Set(
                unstructuredLocation == string.Empty ? null : unstructuredLocation,
                countrySubdivision.Clone(),
                countrySubdivision.Clone(),
                null);
        }

        private static void Set(this LocationReference locationReference, CountrySubdivision countrySubdivision)
        {
            locationReference.Set(
                null,
                countrySubdivision.Clone(),
                countrySubdivision.Clone(),
                null);
        }

        private static void Set(this LocationReference locationReference, Region region)
        {
            locationReference.Set(
                null,
                region.Clone(),
                region.GetCountrySubdivision().Clone(),
                null);
        }

        private static void Set(this LocationReference locationReference, Locality locality)
        {
            locationReference.Set(
                null,
                locality.Clone(),
                locality.GetCountrySubdivision().Clone(),
                locality.Clone());
        }

        public static void Set(this LocationReference locationReference, Country country, string unstructuredLocation, PostalCode postalCode)
        {
            Set(locationReference, unstructuredLocation, GetCountryFinder(country.Id).GetCountrySubdivision(null), postalCode);
        }

        public static void Set(this LocationReference locationReference, string unstructuredLocation, CountrySubdivision countrySubdivision, PostalCode postalCode)
        {
            locationReference.Set(
                unstructuredLocation,
                postalCode.Clone(),
                countrySubdivision.Clone(),
                postalCode.Locality.Clone());
        }

        private static void Set(this LocationReference locationReference, PostalCode postalCode)
        {
            locationReference.Set(
                null,
                postalCode.Clone(),
                postalCode.GetCountrySubdivision().Clone(),
                postalCode.Locality.Clone());
        }

        private static void Set(this LocationReference locationReference, PostalSuburb postalSuburb)
        {
            locationReference.Set(
                null,
                postalSuburb.Clone(),
                postalSuburb.CountrySubdivision.Clone(),
                postalSuburb.PostalCode.Locality.Clone());
        }

        #endregion

        #region Clone

        public static Country Clone(this Country country)
        {
            if (country == null)
                return null;

            return new Country
            {
                Id = country.Id,
                Name = country.Name,
                IsoCode = country.IsoCode,
                CanResolveLocations = country.CanResolveLocations
            };
        }

        public static NamedLocation Clone(this NamedLocation namedLocation)
        {
            if (namedLocation is Locality)
                return Clone((Locality)namedLocation);
            if (namedLocation is PostalCode)
                return Clone((PostalCode)namedLocation);
            if (namedLocation is Region)
                return Clone((Region)namedLocation);
            if (namedLocation is PostalSuburb)
                return Clone((PostalSuburb)namedLocation);
            if (namedLocation is CountrySubdivision)
                return Clone((CountrySubdivision)namedLocation);
            return null;
        }

        public static GeographicalArea Clone(this GeographicalArea geographicalArea)
        {
            if (geographicalArea is Locality)
                return Clone((Locality)geographicalArea);
            if (geographicalArea is Region)
                return Clone((Region)geographicalArea);
            if (geographicalArea is CountrySubdivision)
                return Clone((CountrySubdivision)geographicalArea);
            return null;
        }

        private static CountrySubdivisionAlias Clone(this CountrySubdivisionAlias countrySubdivisionAlias)
        {
            if (countrySubdivisionAlias == null)
                return null;

            return new CountrySubdivisionAlias
            {
                Id = countrySubdivisionAlias.Id,
                Name = countrySubdivisionAlias.Name,
            };
        }

        public static CountrySubdivision Clone(this CountrySubdivision countrySubdivision)
        {
            if (countrySubdivision == null)
                return null;

            return new CountrySubdivision
            {
                Id = countrySubdivision.Id,
                Name = countrySubdivision.Name,
                ShortName = countrySubdivision.ShortName,
                UrlName = countrySubdivision.UrlName,
                CircleCentreId = countrySubdivision.CircleCentreId,
                CircleRadiusKm = countrySubdivision.CircleRadiusKm,
                Aliases = countrySubdivision.Aliases != null ? (from a in countrySubdivision.Aliases select a.Clone()).ToList() : null,
                Country = countrySubdivision.Country.Clone(),
            };
        }

        public static Region Clone(this Region region)
        {
            if (region == null)
                return null;

            return new Region
            {
                Id = region.Id,
                Name = region.Name,
                UrlName = region.UrlName,
                IsMajorCity = region.IsMajorCity,
                Aliases = region.Aliases != null ? (from a in region.Aliases select a.Clone()).ToList() : null,
                Country = region.Country.Clone(),
            };
        }

        private static RegionAlias Clone(this RegionAlias regionAlias)
        {
            if (regionAlias == null)
                return null;

            return new RegionAlias
            {
                Id = regionAlias.Id,
                Name = regionAlias.Name,
            };
        }

        public static Locality Clone(this Locality locality)
        {
            if (locality == null)
                return null;

            return new Locality
            {
                Id = locality.Id,
                Name = locality.Name,
                Centroid = new GeoCoordinates(locality.Centroid.Latitude, locality.Centroid.Longitude),
                CountrySubdivisions = locality.CountrySubdivisions != null ? (from s in locality.CountrySubdivisions select s.Clone()).ToList() : null,
            };
        }

        public static PostalCode Clone(this PostalCode postalCode)
        {
            if (postalCode == null)
                return null;

            return new PostalCode
            {
                Id = postalCode.Id,
                Name = postalCode.Name,
                Locality = postalCode.Locality.Clone(),
            };
        }

        public static PostalSuburb Clone(this PostalSuburb postalSuburb)
        {
            if (postalSuburb == null)
                return null;

            return new PostalSuburb
            {
                Id = postalSuburb.Id,
                Name = postalSuburb.Name,
                PostalCode = postalSuburb.PostalCode.Clone(),
                CountrySubdivision = postalSuburb.CountrySubdivision.Clone(),
            };
        }

        #endregion

        #region Initialisation

        public static void Initialise(ILocationRepository repository)
        {
            if (_data == null)
            {
                try
                {
                    // Get everything.

                    var locationData = repository.GetLocationData();
                    var countryFinders = new Dictionary<int, CountryFinder>();
                    foreach (var pair in locationData.Countries)
                    {
                        var countryFinder = new CountryFinder(pair.Value);
                        countryFinders[pair.Key] = countryFinder;
                    }

                    // Order countries alphabetically, except that "Other" (ID 0) should appear last.

                    var data = new WorldData
                    {
                        Countries = (from c in locationData.Countries where c.Key != 0 orderby c.Value.Name select c.Value)
                            .Concat(from c in locationData.Countries where c.Key == 0 select c.Value).ToList(),
                        CountryFinders = countryFinders,
                        Finder = new LocationFinder()
                    };

                    // Add all data.

                    AddLocationAbbreviations(data, locationData);
                    AddRelativeLocations(data, locationData);
                    AddSubdivisions(data, locationData);
                    AddRegions(data, locationData);
                    AddLocalities(data, locationData);
                    AddPostalCodes(data, locationData);
                    AddPostalSuburbs(data, locationData);

                    // Let the finders initialise themselves now that they have all the data.

                    foreach (var countryFinder in countryFinders.Values)
                        countryFinder.Initialise();

                    _data = data;
                }
                catch (Exception ex)
                {
                    throw new LocalityInitialisationException(ex);
                }
            }
        }

        public static void Reset(ILocationRepository repository)
        {
            _data = null;
            Initialise(repository);
        }

        private static void AddLocationAbbreviations(WorldData worldData, LocationData locationData)
        {
            foreach (var locationAbbreviations in locationData.LocationAbbreviations)
            {
                foreach (var locationAbbreviation in locationAbbreviations.Value)
                    worldData.CountryFinders[locationAbbreviations.Key].Add(locationAbbreviation);
            }
        }

        private static void AddRelativeLocations(WorldData worldData, LocationData locationData)
        {
            foreach (var relativeLocations in locationData.RelativeLocations)
            {
                foreach (var relativeLocation in relativeLocations.Value)
                    worldData.CountryFinders[relativeLocations.Key].Add(relativeLocation);
            }
        }

        private static void AddSubdivisions(WorldData worldData, LocationData locationData)
        {
            foreach (var pair in locationData.CountrySubdivisions)
            {
                var countrySubdivision = pair.Value;
                worldData.Finder.Add(countrySubdivision, locationData.Localities);
                worldData.CountryFinders[countrySubdivision.Country.Id].Add(countrySubdivision);
            }
        }

        private static void AddLocalities(WorldData worldData, LocationData locationData)
        {
            foreach (var pair in locationData.Localities)
            {
                var locality = pair.Value;
                var postalCodes = locationData.LocalityPostalCodes.ContainsKey(locality.Id)
                    ? (from lc in locationData.LocalityPostalCodes[locality.Id] select locationData.PostalCodes[lc]).ToList()
                    : new List<PostalCode>();
                worldData.Finder.Add(locality, postalCodes);
                worldData.CountryFinders[locality.CountrySubdivisions[0].Country.Id].Add(locality);
            }
        }

        private static void AddPostalCodes(WorldData worldData, LocationData locationData)
        {
            foreach (var pair in locationData.PostalCodes)
            {
                var postalCode = pair.Value;
                var postalSuburbs = locationData.PostalCodeSuburbs.ContainsKey(postalCode.Id)
                    ? (from cs in locationData.PostalCodeSuburbs[postalCode.Id] select locationData.PostalSuburbs[cs]).ToList()
                    : new List<PostalSuburb>();
                worldData.Finder.Add(postalCode, postalSuburbs);
                worldData.CountryFinders[postalCode.Locality.CountrySubdivisions[0].Country.Id].Add(postalCode);
            }
        }

        private static void AddPostalSuburbs(WorldData worldData, LocationData locationData)
        {
            foreach (var pair in locationData.PostalSuburbs)
            {
                var postalSuburb = pair.Value;
                worldData.Finder.Add(postalSuburb);
                worldData.CountryFinders[postalSuburb.CountrySubdivision.Country.Id].Add(postalSuburb);
            }
        }

        private static void AddRegions(WorldData worldData, LocationData locationData)
        {
            foreach (var pair in locationData.Regions)
            {
                var region = pair.Value;
                var localities = locationData.RegionLocalities.ContainsKey(region.Id)
                    ? (from rl in locationData.RegionLocalities[region.Id] select locationData.Localities[rl]).ToList()
                    : new List<Locality>();
                worldData.Finder.Add(region, localities);
                worldData.CountryFinders[region.Country.Id].Add(region);
            }
        }

        #endregion
    }
}
