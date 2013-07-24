using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.browseengine.bobo.api;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using org.apache.lucene.document;
using org.apache.lucene.search;
using org.apache.lucene.util;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine
{
    internal class LocationFieldHandler
    {
        private readonly string _locationFieldName;
        private readonly string _relocationsFieldName;
        private readonly IBooster _booster;
        private readonly BitWorldIndex _worldIndex;
        private readonly bool[] _homeLocations; // indexed by location ID

        public LocationFieldHandler(string locationField, string relocationsField, IBooster booster, ILocationQuery locationQuery)
        {
            _locationFieldName = locationField;
            _relocationsFieldName = relocationsField;
            _booster = booster;

            // Build the world index.

            _worldIndex = new BitWorldIndex();
            _worldIndex.BuildUp(locationQuery, true);

            // Mark locations in the home country.

            var homeCountry = locationQuery.GetCountry("Australia");
            var homePointSet = _worldIndex.GetPointSet(homeCountry.Id, 0);
            var maxPointSetId = _worldIndex.GetMaxPointSetId();
            _homeLocations = new bool[maxPointSetId + 1];

            foreach (var location in _worldIndex.GetKnownLocations())
            {
                if (location.Value.Overlaps(homePointSet))
                    _homeLocations[location.Key] = true;
            }
        }

        protected void AddContent(Document document, LocationReference location, IList<LocationReference> relocations)
        {
            // Store (location ID + 1) in the Location field.
            // Zero Location field value will represent NULL.

            if (location != null)
            {
                var value = location.GeographicalArea.Id + 1;
                document.add(new NumericField(_locationFieldName).setIntValue(value));
            }

            // Store relocation location IDs in a string as prefix-coded subfields of fixed size.

            if (relocations != null && relocations.Count != 0)
            {
                var values = new StringBuilder(relocations.Count * NumericUtils.BUF_SIZE_INT);
                var buffer = new char[NumericUtils.BUF_SIZE_INT];

                foreach (var relocation in relocations)
                {
                    var value = relocation.GeographicalArea.Id;
                    NumericUtils.intToPrefixCoded(value, 0, buffer);
                    values.Append(buffer);
                }

                var field = new Field(_relocationsFieldName, values.ToString(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }
        }

        protected LuceneFilter GetFilter(LocationReference queryLocation, IEnumerable<LocationReference> queryRelocations, int queryDistance, bool includeRelocating, bool includeInternational)
        {
            if (queryLocation == null)
                return null;

            int queryLocationId;
            int distance = 0;

            if (queryLocation.Locality != null)
            {
                queryLocationId = queryLocation.Locality.Id;
                distance = queryDistance;
            }
            else if (queryLocation.Region != null)
            {
                queryLocationId = queryLocation.Region.Id;
                distance = queryDistance;
            }
            else
            {
                var cs = queryLocation.CountrySubdivision;
                if (cs.IsCountry)
                {
                    queryLocationId = cs.Country.Id;
                }
                else
                {
                    queryLocationId = cs.Id;
                    distance = queryDistance;
                }
            }

            var queryRelocationIds = (queryRelocations == null) ? null :
                queryRelocations.Select(l => l.GeographicalArea.Id);

            return new LocationFilter(_locationFieldName, _relocationsFieldName,
                _worldIndex, _homeLocations, queryLocationId, queryRelocationIds, distance, includeRelocating, includeInternational);
        }

        protected Sort GetSort(LocationReference location, bool reverse)
        {
            var comparatorFactory = new LocationComparatorSource(location.GeographicalArea.Id, _worldIndex, _locationFieldName);

            return new Sort(new[] 
            {
                new BoboCustomSortField(_locationFieldName, reverse, comparatorFactory),
                SortField.FIELD_SCORE
            });
        }
    }
}
