using LinkMe.Query.Search.Engine;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.JobAds;
using org.apache.lucene.search;
using org.apache.solr.common;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public class JobAdHighlighterFactory
        : HighlighterFactory, IJobAdHighlighterFactory
    {
        public JobAdHighlighterFactory(ResourceLoader resourceLoader)
            : base(new AnalyzerFactory(resourceLoader), false)
        {
        }

        IJobAdHighlighter IJobAdHighlighterFactory.Create(JobAdHighlighterKind kind, JobAdSearchCriteria criteria, HighlighterConfiguration configuration)
        {
            if (kind == JobAdHighlighterKind.Null || criteria == null)
                return new NullResumeHighlighter();

            var query = criteria.GetSearchQuery(null);
            var queryBuilder = new QueryBuilder(query.IncludeSynonyms ? _defaultQueryAnalyzer : _exactQueryAnalyzer, new JobAdSearchBooster());

            var contentQuery = queryBuilder.GetContentQuery(query);
            var titleQuery = new[] { queryBuilder.GetTitleQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);
            var advertiserQuery = new[] { queryBuilder.GetAdvertiserQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);

            var contentAnalyzer = query.IncludeSynonyms ? _defaultContentAnalyzer : _exactContentAnalyzer;
            return new JobAdHighlighter(contentQuery, titleQuery, advertiserQuery, contentAnalyzer, configuration);
        }
    }
}
