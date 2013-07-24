using System;
using org.apache.lucene.search.function;
using LuceneQuery = org.apache.lucene.search.Query;
using org.apache.lucene.index;

namespace LinkMe.Query.Search.Engine
{
    internal class SeniorityIndexQuery
        : CustomScoreQuery
    {
        private readonly int _seniorityIndex;
        private readonly float _sigma;

        internal SeniorityIndexQuery(LuceneQuery subQuery, ValueSourceQuery valueSourceQuery, int seniorityIndex, float sigma)
            : base(subQuery, valueSourceQuery)
        {
            _seniorityIndex = seniorityIndex;
            _sigma = sigma;
        }

        protected override CustomScoreProvider getCustomScoreProvider(IndexReader indexReader)
        {
            return new SeniorityIndexScoreProvider(indexReader, _sigma, _seniorityIndex);
        }
    }

    internal class SeniorityIndexScoreProvider
        : CustomScoreProvider
    {
        private readonly float _sigma;
        private readonly int _seniorityIndex;

        public SeniorityIndexScoreProvider(IndexReader indexReader, float sigma, int seniorityIndex) 
            : base(indexReader)
        {
            _sigma = sigma;
            _seniorityIndex = seniorityIndex;
        }

        public override float customScore(int doc, float subQueryScore, float valSrcScore)
        {
            // valSrcScore is a value of seniorityIndex field, represented as a float
            // convert seniority value to int
            var seniorityScore = (float)Math.Exp(-Math.Pow(valSrcScore - _seniorityIndex, 2) / (2 * _sigma * _sigma));

            var finalScore = seniorityScore * subQueryScore;

            return finalScore;
        }
    }
}
