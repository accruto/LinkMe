using System;
using com.browseengine.bobo.sort;
using org.apache.lucene.index;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine
{
    internal class LocationComparatorSource
        : DocComparatorSource
    {
        private static readonly FieldCache.IntParser Parser = FieldCache.__Fields.NUMERIC_UTILS_INT_PARSER;

        private readonly int _queryLocationId;
        private readonly BitWorldIndex _worldIndex;
        private readonly string _fieldName;

        public LocationComparatorSource(int queryLocationId, BitWorldIndex worldIndex, string fieldName)
        {
            _queryLocationId = queryLocationId;
            _worldIndex = worldIndex;
            _fieldName = fieldName;
        }

        public override DocComparator getComparator(IndexReader reader, int docBase)
        {
            var docValues = FieldCache.__Fields.DEFAULT.getInts(reader, _fieldName, Parser);
            return new LocationComparator(_queryLocationId, _worldIndex, docValues);
        }
    }

    internal class LocationComparator
        : DocComparator
    {
        private readonly int[] _docValues; // locationID + 1
        private readonly int _queryLocationId;
        private readonly BitWorldIndex _worldIndex;

        public LocationComparator(int queryLocationId, BitWorldIndex worldIndex, int[] docValues)
        {
            _queryLocationId = queryLocationId;
            _worldIndex = worldIndex;
            _docValues = docValues;
        }

        #region Overrides of DocComparator

        public override int compare(ScoreDoc scoreDoc1, ScoreDoc scoreDoc2)
        {
            var sortValue1 = GetSortValue(scoreDoc1.doc);
            var sortValue2 = GetSortValue(scoreDoc2.doc);
            return sortValue1.CompareTo(sortValue2);
        }

        public override IComparable value(ScoreDoc scoreDoc)
        {
            var sortValue = GetSortValue(scoreDoc.doc);
            return sortValue;
        }

        #endregion

        private short GetSortValue(int doc)
        {
            var locationId = _docValues[doc] - 1;
            if (locationId < 0)
                return short.MaxValue; // location unknown

            return _worldIndex.PointSetDistance(_queryLocationId, locationId);
        }
    }
}
