using LinkMe.Query.JobAds;
using org.apache.lucene.analysis;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    public class QueryBuilder
    {
        private readonly Analyzer _queryAnalyzer;
        private readonly IJobAdSearchBooster _booster;

        public QueryBuilder(Analyzer queryAnalyzer, IJobAdSearchBooster booster)
        {
            _queryAnalyzer = queryAnalyzer;
            _booster = booster;
        }

        public LuceneQuery GetContentQuery(JobAdSearchQuery query)
        {
            return query.Keywords.GetLuceneQuery(query.IncludeSynonyms ? FieldName.Content : FieldName.ContentExact, _queryAnalyzer);
        }

        public LuceneQuery GetTitleQuery(JobAdSearchQuery query)
        {
            return query.AdTitle.GetLuceneQuery(query.IncludeSynonyms ? FieldName.Title : FieldName.TitleExact, _queryAnalyzer);
        }

        public LuceneQuery GetAdvertiserQuery(JobAdSearchQuery query)
        {
            return query.AdvertiserName.GetLuceneQuery(query.IncludeSynonyms ? FieldName.AdvertiserName : FieldName.AdvertiserNameExact, _queryAnalyzer);
        }

        public LuceneQuery GetQuery(JobAdSearchQuery searchQuery)
        {
            var fieldQueries = CreateFieldQueries(searchQuery);
            var query = fieldQueries.CombineQueries(BooleanClause.Occur.MUST) ?? new MatchAllDocsQuery();

            return searchQuery.SortOrder == JobAdSortOrder.Relevance
                ? _booster.GetRecencyBoostingQuery(query)
                : query;
        }

        private LuceneQuery[] CreateFieldQueries(JobAdSearchQuery query)
        {
            return new[]
            {
                GetContentQuery(query),
                GetTitleQuery(query),
                GetAdvertiserQuery(query)
            };
        }
    }
}
