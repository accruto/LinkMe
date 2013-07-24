using System;
using com.browseengine.bobo.sort;
using org.apache.lucene.index;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine
{
    internal class ZeroLastIntComparatorSource
        : DocComparatorSource
    {
        private readonly FieldCache.IntParser _parser;
        private readonly string _fieldName;
        private readonly bool _descending;

        public ZeroLastIntComparatorSource(FieldCache.IntParser parser, string fieldName, bool descending)
        {
            _parser = parser;
            _fieldName = fieldName;
            _descending = descending;
        }

        public override DocComparator getComparator(IndexReader reader, int docBase)
        {
            var docValues = FieldCache.__Fields.DEFAULT.getInts(reader, _fieldName, _parser);
            return new ZeroLastIntComparator(_descending, docValues);
        }
    }

    internal class ZeroLastIntComparator
        : DocComparator
    {
        private readonly int[] _docValues;
        private readonly bool _descending;

        public ZeroLastIntComparator(bool descending, int[] docValues)
        {
            _descending = descending;
            _docValues = docValues;
        }

        #region Overrides of DocComparator

        public override int compare(ScoreDoc scoreDoc1, ScoreDoc scoreDoc2)
        {
            int sortValue1 = GetSortValue(scoreDoc1.doc);
            int sortValue2 = GetSortValue(scoreDoc2.doc);
            return sortValue1.CompareTo(sortValue2);
        }

        public override IComparable value(ScoreDoc scoreDoc)
        {
            int sortValue = GetSortValue(scoreDoc.doc);
            return sortValue;
        }

        #endregion

        private int GetSortValue(int doc)
        {
            var docValue = _docValues[doc];

            if (_descending)
                return docValue;

            if (docValue == 0)
                return int.MaxValue; // zeroes should go to the bottom

            return docValue;
        }
    }
}
