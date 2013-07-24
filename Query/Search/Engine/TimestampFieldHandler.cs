using System;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine
{
    public enum TimeGranularity
    {
        Day,
        Hour,
        Minute
    }

    internal class TimestampFieldHandler
    {
        private readonly string _fieldName;
        private readonly TimeGranularity _timeGranularity;
        private readonly IBooster _booster;

        protected TimestampFieldHandler(string fieldName, TimeGranularity timeGranularity, IBooster booster)
        {
            _fieldName = fieldName;
            _timeGranularity = timeGranularity;
            _booster = booster;
        }

        protected void AddContent(Document document, DateTime? timestamp)
        {
            var timestampValue = timestamp.HasValue
                ? timestamp.Value.ToFieldValue(_timeGranularity)
                : 0;
            var field = new NumericField(_fieldName).setIntValue(timestampValue);
            _booster.SetBoost(field);
            document.add(field);
        }

        protected LuceneFilter GetFilter(DateTime? minTimestamp)
        {
            return minTimestamp == null
                ? null
                : FieldCacheRangeFilter.newIntRange(_fieldName, new java.lang.Integer(minTimestamp.Value.ToFieldValue(_timeGranularity)), null, true, true);
        }

        protected Sort GetSort(bool reverse)
        {
            return new Sort(new[]
            {
                new SortField(_fieldName, SortField.INT, reverse),
                SortField.FIELD_SCORE
            });
        }
    }
}
