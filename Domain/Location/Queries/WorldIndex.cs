using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinkMe.Domain.Location.Queries
{
    public class WorldIndex
    {
        private int _localityCount;
        private IEnumerable<int> _points; // all point indexes
        private short[][] _distances;
        private Dictionary<int, HashSet<int>> _subsets; // Id -> {set of point indexes}
        private int _maxPointSetId;

        #region Construction

        public void BuildUp(ILocationQuery locationQuery, bool includeDistances)
        {
            Clear();

            // Create points in our metric space.
            // The points are Localities and empty Countries.
            // The distance between an empty countries and any other entity is presumed to be infinite.

            var localities = new List<Locality>();
            var emptyCountries = new List<Country>();

            foreach (var country in locationQuery.GetCountries())
            {
                var countryLocalities = locationQuery.GetLocalities(country);
                if (countryLocalities.Count > 0)
                    localities.AddRange(countryLocalities);
                else
                    emptyCountries.Add(country);
            }

            _localityCount = localities.Count;
            var pointCount = localities.Count + emptyCountries.Count;
            _points = Enumerable.Range(0, pointCount);

            if (includeDistances)
            {
                // Calculate distances between localities.

                _distances = new short[_localityCount][];

                for (var i = 0; i < _localityCount; i++)
                {
                    _distances[i] = new short[i];

                    for (var j = 0; j < i; j++)
                    {
                        _distances[i][j] = (short) Math.Round(EarthDistance(
                                                                  localities[i].Centroid.Latitude,
                                                                  localities[i].Centroid.Longitude,
                                                                  localities[j].Centroid.Latitude,
                                                                  localities[j].Centroid.Longitude));
                    }
                }
            }

            // Add Localities.

            _subsets = new Dictionary<int, HashSet<int>>();

            for (var i = 0; i < _localityCount; i++)
            {
                var subset = new HashSet<int> { i };
                _subsets.Add(localities[i].Id, subset);
            }

            // Add empty Countries.

            for (var i = 0; i < emptyCountries.Count; i++)
            {
                var subset = new HashSet<int> { _localityCount + i };
                _subsets.Add(emptyCountries[i].Id, subset);
            }

            // Add CountrySubdivisions.

            foreach (var locality in localities)
            {
                foreach (var countrySubdivision in locality.CountrySubdivisions)
                {
                    HashSet<int> subset;
                    if (!_subsets.TryGetValue(countrySubdivision.Id, out subset))
                    {
                        subset = new HashSet<int>();
                        _subsets.Add(countrySubdivision.Id, subset);
                    }

                    subset.Add(GetLocalityPoint(locality.Id));
                }
            }

#if DEBUG
            // Check that there are no empty CountrySubdivisions.

            foreach (var country in locationQuery.GetCountries())
            {
                foreach (var countrySubdivision in locationQuery.GetCountrySubdivisions(country))
                {
                    if (countrySubdivision.Id != 1)
                        Debug.Assert(_subsets[countrySubdivision.Id].Count > 0);
                }
            }
#endif

            // Add Countries.

            foreach (var country in locationQuery.GetCountries())
            {
                var subset = new HashSet<int>();

                foreach (var locality in locationQuery.GetLocalities(country))
                    subset.Add(GetLocalityPoint(locality.Id));

                if (subset.Count > 0)
                    _subsets.Add(country.Id, subset);
            }

            // Add Regions.

            foreach (var country in locationQuery.GetCountries())
            {
                foreach (var region in locationQuery.GetRegions(country))
                {
                    var subset = new HashSet<int>();

                    foreach (Locality locality in locationQuery.GetLocalities(region))
                        subset.Add(GetLocalityPoint(locality.Id));

                    Debug.Assert(subset.Count > 0);
                    _subsets.Add(region.Id, subset);
                }
            }

            _maxPointSetId = _subsets.Max(kvp => kvp.Key);
        }

        #endregion

        #region High Level API

        public HashSet<int> GetPointSet(LocationReference location)
        {
            return GetPointSet(location, 0);
        }

        public HashSet<int> GetPointSet(LocationReference location, int radius)
        {
            return GetPointSet(location.GeographicalArea.Id, radius);
        }

        public HashSet<int> GetPointSet(GeographicalArea area)
        {
            return GetPointSet(area, 0);
        }

        public HashSet<int> GetPointSet(GeographicalArea area, int radius)
        {
            return GetPointSet(area.Id, radius);
        }

        public IEnumerable<KeyValuePair<int, HashSet<int>>> GetKnownLocations()
        {
            return _subsets;
        }

        public int GetMaxPointSetId()
        {
            return _maxPointSetId;
        }

        public short Distance(Locality p, Locality q)
        {
            return Distance(GetLocalityPoint(p.Id), GetLocalityPoint(q.Id));
        }

        #endregion

        #region Low Level API

        public HashSet<int> GetPointSet(int id, int radius)
        {
            if (!_subsets.ContainsKey(id))
                return new HashSet<int>();

            var subset = _subsets[id];

            if (radius == 0)
                return subset;

            if (_distances == null)
                throw new InvalidOperationException("This instance of WorldIndex does not contain distances between points.");

            var expression = _points.Where(p => subset.Any(q => (Distance(p, q) <= radius)));

            return new HashSet<int>(expression);
        }

        public static double EarthDistance(double latDeg1, double lngDeg1, double latDeg2, double lngDeg2)
        {
            const double avgEarthRadius = 6372.795;

            // Convert to radians
            var lat1 = latDeg1 * Math.PI / 180;
            var lat2 = latDeg2 * Math.PI / 180;
            var dLng = (lngDeg2 - lngDeg1) * Math.PI / 180;

            var sinLat = Math.Sin((lat1 - lat2) / 2);
            var sinLng = Math.Sin(dLng / 2);
            var cosLat1 = Math.Cos(lat1);
            var cosLat2 = Math.Cos(lat2);

            // Use the haversine formula (see http://en.wikipedia.org/wiki/Great-circle_distance)
            return avgEarthRadius * 2 * Math.Asin(Math.Sqrt(sinLat * sinLat + cosLat1 * cosLat2 * sinLng * sinLng));
        }

        #endregion

        private void Clear()
        {
            _localityCount = 0;
            _points = null;
            _distances = null;
            _subsets = null;
        }

        private int GetLocalityPoint(int id)
        {
            return _subsets[id].Single();
        }

        private short Distance(int p, int q)
        {
            if (p == q)
                return 0;

            if (p >= _localityCount || q >= _localityCount)
                return short.MaxValue;

            if (q < p)
                return _distances[p][q];

            return _distances[q][p];
        }
    }
}