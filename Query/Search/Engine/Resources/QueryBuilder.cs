using LinkMe.Query.Resources;
using org.apache.lucene.analysis;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.Resources
{
    public class QueryBuilder
    {
        private readonly Analyzer _queryAnalyzer;

        public QueryBuilder(Analyzer queryAnalyzer)
        {
            _queryAnalyzer = queryAnalyzer;
        }

        public LuceneQuery GetContentQuery(ResourceSearchQuery query)
        {
            return query.Keywords.GetLuceneQuery(FieldName.Content, _queryAnalyzer);
        }

        public LuceneQuery GetQuery(ResourceSearchQuery searchQuery)
        {
            var fieldQueries = CreateFieldQueries(searchQuery);
            var query = fieldQueries.CombineQueries(BooleanClause.Occur.MUST);
            return query ?? new MatchAllDocsQuery();
        }

        private LuceneQuery[] CreateFieldQueries(ResourceSearchQuery searchQuery)
        {
            return new[]
            {
                GetContentQuery(searchQuery)
            };
        }
    }
}
