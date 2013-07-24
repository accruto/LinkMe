using LinkMe.Query.Search.Engine.Resources;
using LinkMe.Query.Search.Resources;
using org.apache.solr.common;

namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    public class ResourceHighlighterFactory
        : HighlighterFactory, IResourceHighlighterFactory
    {
        public ResourceHighlighterFactory(ResourceLoader resourceLoader)
            : base(new AnalyzerFactory(resourceLoader), false)
        {
        }

        IResourceHighlighter IResourceHighlighterFactory.Create(ResourceHighlighterKind kind, ResourceSearchCriteria criteria, HighlighterConfiguration configuration)
        {
            if (kind == ResourceHighlighterKind.Null || criteria == null)
                return new NullResourceHighlighter();

            var searchQuery = criteria.GetSearchQuery(null);
            var contentQuery = new QueryBuilder(_defaultQueryAnalyzer).GetContentQuery(searchQuery);
            return new ResourceHighlighter(contentQuery, _defaultContentAnalyzer, configuration);
        }
    }
}
