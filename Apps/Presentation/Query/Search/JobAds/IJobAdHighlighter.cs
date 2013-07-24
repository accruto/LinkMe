using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public interface IJobAdHighlighter
    {
        string SummarizeContent(JobAd jobAd);

        string HighlightTitle(string text);
        string HighlightAdvertiser(string text);
        string HighlightContent(string text);
    }
}