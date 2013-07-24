using System;
using org.apache.lucene.index;
using org.apache.lucene.search;
using org.apache.lucene.search.function;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine
{
    internal class RecencyBoostingQuery
        : CustomScoreQuery
    {
        private readonly string _fieldName;
        private readonly TimeGranularity _granularity;
        private readonly double _alpha;
        private readonly double _halfDecay;

        public RecencyBoostingQuery(LuceneQuery subQuery, string fieldName, TimeGranularity granularity, double alpha, double halfDecay)
            : base(subQuery)
        {
            _fieldName = fieldName;
            _granularity = granularity;
            _alpha = alpha;
            _halfDecay = halfDecay;
        }

        protected override CustomScoreProvider getCustomScoreProvider(IndexReader indexReader)
        {
            return new RecencyScoreProvider(indexReader, _fieldName, _granularity, _alpha, _halfDecay);
        }
    }

    internal class RecencyScoreProvider
        : CustomScoreProvider
    {
        private readonly double _alpha;
        private readonly double _halfDecay;
        private readonly int[] _lastUpdatedDay;
        private readonly int _today;

        public RecencyScoreProvider(IndexReader indexReader, string fieldName, TimeGranularity granularity, double alpha, double halfDecay) 
            : base(indexReader)
        {
            _alpha = alpha;
            _halfDecay = halfDecay;
            _lastUpdatedDay = FieldCache.__Fields.DEFAULT.getInts(indexReader, fieldName);
            _today = DateTime.Now.ToFieldValue(granularity);
        }

        public override float customScore(int doc, float subQueryScore, float valSrcScore)
        {
            var lastUpdatedDay = _lastUpdatedDay[doc];
            var age = (lastUpdatedDay != 0) ? _today - lastUpdatedDay : _halfDecay;
            var value = (float) (subQueryScore * (0.1 + 0.9 * Sigmoid(age)));
            return value;
        }

        private double Sigmoid(double age)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, _alpha * (age - _halfDecay)));
        }
    }
}
