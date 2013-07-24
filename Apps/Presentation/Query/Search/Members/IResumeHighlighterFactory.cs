using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Query.Search.Members
{
    public enum ResumeHighlighterKind
    {
        Null,
        Snippet,
        Full
    }

    public interface IResumeHighlighterFactory
    {
        IResumeHighlighter Create(ResumeHighlighterKind kind, MemberSearchCriteria criteria, HighlighterConfiguration configuration);
    }
}
