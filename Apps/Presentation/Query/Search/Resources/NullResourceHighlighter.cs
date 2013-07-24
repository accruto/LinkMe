using System.Web;

namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    public class NullResourceHighlighter
        : IResourceHighlighter
    {
        string IResourceHighlighter.SummarizeContent(string text, bool htmlEncodeOutput)
        {
            return htmlEncodeOutput ? HttpUtility.HtmlEncode(text) : text;
        }

        string IResourceHighlighter.HighlightTitle(string text, bool htmlEncodeOutput)
        {
            return htmlEncodeOutput ? HttpUtility.HtmlEncode(text) : text;
        }

        string IResourceHighlighter.HighlightContent(string text, bool htmlEncodeOutput)
        {
            return htmlEncodeOutput ? HttpUtility.HtmlEncode(text) : text;
        }
    }
}
