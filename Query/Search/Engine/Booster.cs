using System;
using System.Collections.Generic;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine
{
    public abstract class Booster
    {
        private IDictionary<string, Func<float?>> _fieldBoosts;

        protected void SetFieldBoosts(IDictionary<string, Func<float?>> fieldBoosts)
        {
            _fieldBoosts = fieldBoosts;
        }

        protected void SetBoost(AbstractField field)
        {
            if (field == null)
                return;

            Func<float?> getBoost;
            if (!_fieldBoosts.TryGetValue(field.name(), out getBoost))
                return;

            var boost = getBoost();
            if (boost != null)
                field.setBoost(boost.Value);
        }

        protected void SetBoost(Document document, float? boost)
        {
            if (boost != null)
            {
                var currentBoost = document.getBoost();
                document.setBoost(currentBoost * boost.Value);
            }
        }

        protected LuceneQuery GetBoostingQuery(LuceneQuery query, LuceneQuery contextQuery, float? boost)
        {
            if (contextQuery == null || boost == null)
                return query;
            return new BoostingQuery(query, contextQuery, boost.Value);
        }

        protected LuceneQuery GetRecencyBoostingQuery(LuceneQuery query, string fieldName, TimeGranularity granularity, double? alpha, double? halfDecay)
        {
            return (alpha == null || halfDecay == null)
                ? query
                : new RecencyBoostingQuery(query, fieldName, granularity, alpha.Value, halfDecay.Value);
        }
    }
}
