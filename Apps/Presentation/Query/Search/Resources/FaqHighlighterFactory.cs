using LinkMe.Query.Search.Engine.Resources;
using LinkMe.Query.Search.Resources;
using org.apache.solr.common;

namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    public class FaqHighlighterFactory
        : HighlighterFactory, IFaqHighlighterFactory
    {
        public FaqHighlighterFactory(ResourceLoader resourceLoader)
            : base(new AnalyzerFactory(resourceLoader), true)
        {
        }

        IResourceHighlighter IFaqHighlighterFactory.Create(ResourceHighlighterKind kind, FaqSearchCriteria criteria, HighlighterConfiguration configuration)
        {
            if (kind == ResourceHighlighterKind.Null || criteria == null)
                return new NullResourceHighlighter();

            var searchQuery = criteria.GetSearchQuery(null);
            var contentQuery = new QueryBuilder(_defaultQueryAnalyzer).GetContentQuery(searchQuery);
            return new FaqHighlighter(contentQuery, _defaultContentAnalyzer, configuration);
        }
    }
}
