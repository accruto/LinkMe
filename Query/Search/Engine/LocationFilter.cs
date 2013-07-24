using System.Collections.Generic;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.util;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine
{

    internal class LocationFilter
        : LuceneFilter
    {
        private readonly string _locationFieldName;
        private readonly string _relocationsFieldName;
        private readonly bool _includeRelocating;
        private readonly bool _includeInternational;
        private readonly bool[] _homeLocations;
        private readonly bool[] _matchingLocations;
        private readonly bool[] _matchingRelocations;

        public LocationFilter(string locationFieldName, string relocationsFieldName, BitWorldIndex worldIndex, 
            bool[] homeLocations, int queryLocationId, IEnumerable<int> queryRelocationIds, int distance, bool includeRelocating, bool includeInternational)
        {
            _locationFieldName = locationFieldName;
            _relocationsFieldName = relocationsFieldName;

            _homeLocations = homeLocations;
            _includeInternational = includeInternational;

            var maxPointSetId = worldIndex.GetMaxPointSetId();
            _matchingLocations = new bool[maxPointSetId + 1];

            _includeRelocating = includeRelocating;
            if (includeRelocating)
                _matchingRelocations = new bool[maxPointSetId + 1];

            var queryPointSet = worldIndex.GetPointSet(queryLocationId, distance);

            if (queryRelocationIds != null)
            {
                foreach (var queryRelocationId in queryRelocationIds)
                    queryPointSet.UnionWith(worldIndex.GetPointSet(queryRelocationId, 0));
            }

            foreach (var location in worldIndex.GetKnownLocations())
            {
                if (MatchLocation(location.Value, queryPointSet))
                    _matchingLocations[location.Key] = true;

                if (includeRelocating && MatchRelocation(location.Value, queryPointSet))
                    _matchingRelocations[location.Key] = true;
            }
        }

        public override DocIdSet getDocIdSet(IndexReader indexReader)
        {
            var locationFields = FieldCache.__Fields.DEFAULT.getInts(indexReader, _locationFieldName, FieldCache.__Fields.NUMERIC_UTILS_INT_PARSER);

            string[] relocationFields = null;
            if (_includeRelocating)
                relocationFields = FieldCache.__Fields.DEFAULT.getStrings(indexReader, _relocationsFieldName);

            int numDocs = indexReader.maxDoc();
            var docIdSet = new OpenBitSet(numDocs);

            for (int i = 0; i < numDocs; i++)
            {
                // Check for exact location match.

                int location = locationFields[i] - 1;
                if (location >= 0 && _matchingLocations[location])
                {
                    docIdSet.fastSet(i);
                    continue;
                }

                // Filter out international candidates.

                if (!_includeInternational)
                {
                    if (location < 0 || !_homeLocations[location])
                        continue;
                }

                // Check relocation preferences.

                if (_includeRelocating)
                {
                    var field = relocationFields[i];
                    if (field != null)
                    {
                        for (int pos = 0; pos < field.Length; pos += NumericUtils.BUF_SIZE_INT)
                        {
                            string prefixCoded = field.Substring(pos, NumericUtils.BUF_SIZE_INT);
                            int relocation = NumericUtils.prefixCodedToInt(prefixCoded);

                            if (_matchingRelocations[relocation])
                            {
                                docIdSet.fastSet(i);
                                break;
                            }
                        }
                    }
                }
            }

            return docIdSet;
        }

        private static bool MatchLocation(BitPointSet locationPointSet, BitPointSet queryPointSet)
        {
            if (!locationPointSet.IsEmpty && locationPointSet.IsSubsetOf(queryPointSet))
                return true;

            return false;
        }

        private static bool MatchRelocation(BitPointSet relocationPointSet, BitPointSet queryPointSet)
        {
            return relocationPointSet.Overlaps(queryPointSet);
        }
    }
}
