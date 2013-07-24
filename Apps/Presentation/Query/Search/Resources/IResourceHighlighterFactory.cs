using LinkMe.Query.Search.Resources;

namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    public enum ResourceHighlighterKind
    {
        Null,
        Snippet,
        Full
    }

    public interface IResourceHighlighterFactory
    {
        IResourceHighlighter Create(ResourceHighlighterKind kind, ResourceSearchCriteria criteria, HighlighterConfiguration configuration);
    }

    public interface IFaqHighlighterFactory
    {
        IResourceHighlighter Create(ResourceHighlighterKind kind, FaqSearchCriteria criteria, HighlighterConfiguration configuration);
    }
}
