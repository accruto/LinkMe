using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using org.apache.lucene.util;

namespace LinkMe.Query.Search.Engine
{
    internal class BitPointSet
    {
        private int _cardinality;
        private readonly OpenBitSet _points;

        public BitPointSet(int maxPoints)
        {
            _points = new OpenBitSet(maxPoints);
        }

        public bool IsEmpty
        {
            get { return (_cardinality == 0); }
        }

        public void Add(int point)
        {
            _points.fastSet(point);
            _cardinality++;
        }

        public void UnionWith(BitPointSet other)
        {
            _points.union(other._points);
            _cardinality = (int) _points.cardinality();
        }

        public int GetNextPoint(int startPoint)
        {
            return _points.nextSetBit(startPoint);
        }

        public bool Overlaps(BitPointSet other)
        {
            return _points.intersects(other._points);
        }

        public bool IsSubsetOf(BitPointSet other)
        {
            return (_cardinality == OpenBitSet.intersectionCount(_points, other._points));
        }
    }

    internal class BitWorldIndex
    {
        private int _localityCount;
        private int _pointCount;
        private short[][] _distances;
        private Dictionary<int, BitPointSet> _subsets; // Id -> {set of point indexes}
        private int _maxPointSetId;
        private Dictionary<int, int> _subsetToIndex;
        private short[][] _subsetDistances; // indexed using _subsetToIndex

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
            _pointCount = localities.Count + emptyCountries.Count;

            if (includeDistances)
            {
                // Calculate distances between localities.

                _distances = new short[_localityCount][];

                for (var i = 0; i < _localityCount; i++)
                {
                    _distances[i] = new short[i];

                    for (var j = 0; j < i; j++)
                    {
                        _distances[i][j] = (short)Math.Round(localities[i].Centroid.Distance(localities[j].Centroid));
                    }
                }
            }

            // Add Localities.

            _subsets = new Dictionary<int, BitPointSet>();

            for (var i = 0; i < _localityCount; i++)
            {
                var subset = CreateSubset();
                subset.Add(i);
                _subsets.Add(localities[i].Id, subset);
            }

            // Add empty Countries.

            for (var i = 0; i < emptyCountries.Count; i++)
            {
                var subset = CreateSubset(); 
                subset.Add(_localityCount + i);
                _subsets.Add(emptyCountries[i].Id, subset);
            }

            // Add CountrySubdivisions.

            foreach (var locality in localities)
            {
                foreach (var countrySubdivision in locality.CountrySubdivisions)
                {
                    BitPointSet subset;
                    if (!_subsets.TryGetValue(countrySubdivision.Id, out subset))
                    {
                        subset = CreateSubset();
                        _subsets.Add(countrySubdivision.Id, subset);
                    }

                    subset.Add(GetLocalityPoint(locality.Id));
                }
            }

#if DEBUG
            // Check that there are no empty CountrySubdivisions.

            foreach (var countrySubdivision in locationQuery.GetCountries().SelectMany(country => locationQuery.GetCountrySubdivisions(country).Where(countrySubdivision => countrySubdivision.Id != 1)))
                Debug.Assert(!_subsets[countrySubdivision.Id].IsEmpty);
#endif

            // Add Countries.

            foreach (var country in locationQuery.GetCountries())
            {
                var subset = CreateSubset();

                foreach (var locality in locationQuery.GetLocalities(country))
                    subset.Add(GetLocalityPoint(locality.Id));

                if (!subset.IsEmpty)
                    _subsets.Add(country.Id, subset);
            }

            // Add Regions.

            foreach (var country in locationQuery.GetCountries())
            {
                foreach (var region in locationQuery.GetRegions(country))
                {
                    var subset = CreateSubset();

                    foreach (Locality locality in locationQuery.GetLocalities(region))
                        subset.Add(GetLocalityPoint(locality.Id));

                    Debug.Assert(!subset.IsEmpty);
                    _subsets.Add(region.Id, subset);
                }
            }

            if (_subsets.Count != 0)
                _maxPointSetId = _subsets.Max(kvp => kvp.Key);

            if (includeDistances)
                CalculateSubsetDistances();
        }

        #endregion

        public IEnumerable<KeyValuePair<int, BitPointSet>> GetKnownLocations()
        {
            return _subsets;
        }

        public int GetMaxPointSetId()
        {
            return _maxPointSetId;
        }

        public BitPointSet GetPointSet(int id, int radius)
        {
            if (!_subsets.ContainsKey(id))
                return CreateSubset();

            var querySubset = _subsets[id];

            if (radius == 0)
                return querySubset;

            if (_distances == null)
                throw new InvalidOperationException("This instance of WorldIndex does not contain distances between points.");

            var subset = CreateSubset();

            for (var p = 0; p < _pointCount; p++)
            {
                for (var q = querySubset.GetNextPoint(0); q != -1; q = querySubset.GetNextPoint(q + 1))
                {
                    if (Distance(p, q) > radius) continue;
                    
                    subset.Add(p);
                    break;
                }
            }

            return subset;
        }

        public short PointSetDistance(int pSet, int qSet)
        {
            var p = _subsetToIndex[pSet];
            var q = _subsetToIndex[qSet];

            if (p == q)
                return 0;

            return q < p ? _subsetDistances[p][q] : _subsetDistances[q][p];
        }

        private void Clear()
        {
            _localityCount = 0;
            _pointCount = 0;
            _distances = null;
            _subsets = null;
            _subsetToIndex = null;
            _subsetDistances = null;
        }

        private int GetLocalityPoint(int id)
        {
            return _subsets[id].GetNextPoint(0);
        }

        private short Distance(int p, int q)
        {
            if (p == q)
                return 0;

            if (p >= _localityCount || q >= _localityCount)
                return short.MaxValue; // empty countries reside in infinity

            return q < p ? _distances[p][q] : _distances[q][p];
        }

        private BitPointSet CreateSubset()
        {
            return new BitPointSet(_pointCount);
        }

        private void CalculateSubsetDistances()
        {
            _subsetToIndex = new Dictionary<int, int>(_subsets.Count);
            var indexToSubset = new int[_subsets.Count];
            _subsetDistances = new short[_subsets.Count][];

            var index = 0;
            foreach (var key in _subsets.Keys)
            {
                _subsetToIndex.Add(key, index);
                indexToSubset[index] = key;
                index++;
            }

            for (var i = 0; i < _subsets.Count; i++)
            {
                _subsetDistances[i] = new short[i];
                var iSubset = _subsets[indexToSubset[i]];

                for (var j = 0; j < i; j++)
                {
                    var jSubset = _subsets[indexToSubset[j]];
                    _subsetDistances[i][j] = Distance(iSubset, jSubset);
                }
            }
        }

        private short Distance(BitPointSet pSet, BitPointSet qSet)
        {
            if (pSet.Overlaps(qSet)) // it's faster than comparing distances
                return 0;

            var minDistance = short.MaxValue;

            for (var p = pSet.GetNextPoint(0); p != -1; p = pSet.GetNextPoint(p + 1))
                for (var q = qSet.GetNextPoint(0); q != -1; q = qSet.GetNextPoint(q + 1))
                {
                    var distance = Distance(p, q);
                    if (distance < minDistance)
                        minDistance = distance;
                }

            return minDistance;
        }
    }
}
