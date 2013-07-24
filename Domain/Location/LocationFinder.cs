using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Location
{
    public class PartialMatch
    {
        private readonly bool _isSynonym;
        private readonly string _key;
        private readonly NamedLocation _namedLocation;

        public PartialMatch(string key, NamedLocation namedLocation)
            : this(false, key, namedLocation)
        {
        }

        public PartialMatch(bool isSynonym, string key, NamedLocation namedLocation)
        {
            _isSynonym = isSynonym;
            _key = key;
            _namedLocation = namedLocation.Clone();
        }

        public bool IsSynonym
        {
            get { return _isSynonym; }
        }

        public string Key
        {
            get { return _key; }
        }

        public NamedLocation NamedLocation
        {
            get { return _namedLocation; }
        }
    }

    internal class LocationFinder
    {
        private readonly Dictionary<int, NamedLocation> _namedLocationsById = new Dictionary<int, NamedLocation>();
        private readonly IDictionary<int, IList<Locality>> _regionLocalities = new Dictionary<int, IList<Locality>>();
        private readonly Dictionary<GeoCoordinates, Locality> _localitiesByCoordinates = new Dictionary<GeoCoordinates, Locality>();
        private readonly Dictionary<GeoCoordinates, CountrySubdivision> _subdivisionsByCoordinates = new Dictionary<GeoCoordinates, CountrySubdivision>();
        private readonly Dictionary<int, IList<PostalCode>> _localityPostalCodes = new Dictionary<int, IList<PostalCode>>();
        private readonly Dictionary<int, IList<PostalSuburb>> _postalCodeSuburbs = new Dictionary<int, IList<PostalSuburb>>();

        private static readonly IList<PartialMatch> EmptyMatches = new PartialMatch[0];

        #region Get

        public NamedLocation GetNamedLocation(int id, bool throwIfNotFound)
        {
            NamedLocation location;
            if (_namedLocationsById.TryGetValue(id, out location))
                return location;

            if (location == null && throwIfNotFound)
                throw new ApplicationException("There is no location with id " + id + ".");

            return location;
        }

        public CountrySubdivision GetCountrySubdivision(GeoCoordinates coordinates)
        {
            CountrySubdivision subdivision;
            _subdivisionsByCoordinates.TryGetValue(coordinates, out subdivision);
            return subdivision;
        }

        public Locality GetLocality(GeoCoordinates coordinates)
        {
            Locality locality;
            _localitiesByCoordinates.TryGetValue(coordinates, out locality);
            return locality;
        }

        public IList<Locality> GetLocalities(Region region)
        {
            IList<Locality> localities;
            _regionLocalities.TryGetValue(region.Id, out localities);
            return localities ?? new List<Locality>();
        }

        public IList<PostalCode> GetPostalCodes(Locality locality)
        {
            IList<PostalCode> postalCodes;
            _localityPostalCodes.TryGetValue(locality.Id, out postalCodes);
            return postalCodes ?? new List<PostalCode>();
        }

        public IList<PostalSuburb> GetPostalSuburbs(PostalCode postalCode)
        {
            IList<PostalSuburb> postalSuburbs;
            _postalCodeSuburbs.TryGetValue(postalCode.Id, out postalSuburbs);
            return postalSuburbs ?? new List<PostalSuburb>();
        }

        #endregion

        #region Partial Matches

        public static IList<PartialMatch> FindPartialMatchedLocations(Country country, string location, int maximum)
        {
            if (location == null)
                return EmptyMatches;

            if (location.Trim().Length == 0)
                return EmptyMatches;

            // Trim leading whitespace but retain a single space for any trailing whitespace.

            location = location.TrimStart();
            var locationNoWhitespace = location.TrimEnd();
            if (location != locationNoWhitespace)
                location = locationNoWhitespace + ' ';

            var matches = new List<PartialMatch>();

            // Populate subdivisions and regions.

            var countryFinder = World.GetCountryFinder(country.Id);
            countryFinder.FindPartialMatchedSubdivisions(matches, location, maximum);
            if (maximum > 0 && matches.Count == maximum)
                return matches;

            countryFinder.FindPartialMatchedRegions(matches, location, maximum);
            if (maximum > 0 && matches.Count == maximum)
                return matches;

            // Move onto postal suburbs.

            countryFinder.FindPartialMatchedPostalSuburbs(matches, location, maximum);
            return matches;
        }

        /// <summary>
        /// Returns a list of postal suburbs that partially match 'location'.  Only upto 'maximum' will
        /// be returned. 
        /// </summary>
        public static IList<PartialMatch> FindPartialMatchedPostalSuburbs(Country country, string location, int maximum)
        {
            if (location == null)
                return EmptyMatches;

            if (location.Trim().Length == 0)
                return EmptyMatches;

            // Trim leading whitespace but retain a single space for any trailing whitespace.

            location = location.TrimStart();
            var locationNoWhitespace = location.TrimEnd();
            if (location != locationNoWhitespace)
                location = locationNoWhitespace + ' ';

            var matches = new List<PartialMatch>();
            World.GetCountryFinder(country.Id).FindPartialMatchedPostalSuburbs(matches, location, maximum);
            return matches;
        }

        #endregion

        #region Initialise

        public void Add(CountrySubdivision subdivision, IDictionary<int, Locality> localities)
        {
            _namedLocationsById[subdivision.Id] = subdivision;
            if (subdivision.CircleCentreId != null)
            {
                Locality locality;
                localities.TryGetValue(subdivision.CircleCentreId.Value, out locality);
                if (locality != null)
                    _subdivisionsByCoordinates[locality.Centroid] = subdivision;
            }
        }

        public void Add(Region region, IList<Locality> localities)
        {
            _namedLocationsById[region.Id] = region;
            _regionLocalities[region.Id] = localities;
        }

        public void Add(Locality locality, IList<PostalCode> postalCodes)
        {
            _namedLocationsById[locality.Id] = locality;
            _localitiesByCoordinates[locality.Centroid] = locality;
            _localityPostalCodes[locality.Id] = postalCodes;
        }

        public void Add(PostalCode postalCode, IList<PostalSuburb> postalSuburbs)
        {
            _namedLocationsById[postalCode.Id] = postalCode;
            _postalCodeSuburbs[postalCode.Id] = postalSuburbs;
        }

        public void Add(PostalSuburb postalSuburb)
        {
            _namedLocationsById[postalSuburb.Id] = postalSuburb;
        }

        #endregion
    }
}
