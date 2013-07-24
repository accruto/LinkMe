using LinkMe.Query.Search.Engine;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Query.Search.Members;
using org.apache.lucene.search;
using org.apache.solr.common;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public class ResumeHighlighterFactory
        : HighlighterFactory, IResumeHighlighterFactory
    {
        public ResumeHighlighterFactory(ResourceLoader resourceLoader)
            : base(new AnalyzerFactory(resourceLoader), false)
        {
        }

        IResumeHighlighter IResumeHighlighterFactory.Create(ResumeHighlighterKind kind, MemberSearchCriteria criteria, HighlighterConfiguration configuration)
        {
            if (kind == ResumeHighlighterKind.Null || criteria == null)
                return new NullResumeHighlighter();

            var query = criteria.GetSearchQuery(null);
            var queryBuilder = new QueryBuilder(query.IncludeSynonyms ? _defaultQueryAnalyzer : _exactQueryAnalyzer, new MemberSearchBooster());

            var contentQuery = queryBuilder.GetContentQuery(query);
            var jobTitleQuery = new[] { queryBuilder.GetJobTitleQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);
            var desiredJobTitleQuery = new[] { queryBuilder.GetDesiredJobTitleQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);
            var employerQuery = new[] { queryBuilder.GetEmployerQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);
            var educationQuery = new[] { queryBuilder.GetEducationQuery(query), contentQuery }.CombineQueries(BooleanClause.Occur.SHOULD);

            var contentAnalyzer = query.IncludeSynonyms ? _defaultContentAnalyzer : _exactContentAnalyzer;
            return new ResumeHighlighter(contentQuery, jobTitleQuery, desiredJobTitleQuery, employerQuery, educationQuery, contentAnalyzer, configuration);
        }
    }
}
