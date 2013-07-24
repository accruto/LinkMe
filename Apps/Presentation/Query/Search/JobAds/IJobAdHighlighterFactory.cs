using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public enum JobAdHighlighterKind
    {
        Null,
        Snippet,
        Full
    }

    public interface IJobAdHighlighterFactory
    {
        IJobAdHighlighter Create(JobAdHighlighterKind kind, JobAdSearchCriteria criteria, HighlighterConfiguration configuration);
    }
}
