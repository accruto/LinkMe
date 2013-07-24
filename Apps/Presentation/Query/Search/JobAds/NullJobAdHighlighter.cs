using System.Web;
using LinkMe.Domain.Roles.JobAds;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    internal class NullResumeHighlighter : IJobAdHighlighter
    {
        #region IResumeHighlighter Members

        string IJobAdHighlighter.SummarizeContent(JobAd jobAd)
        {
            return null;
        }

        string IJobAdHighlighter.HighlightTitle(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        string IJobAdHighlighter.HighlightAdvertiser(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        string IJobAdHighlighter.HighlightContent(string text)
        {
            return HttpUtility.HtmlEncode(text);
        }

        #endregion
    }
}
