using LinkMe.Framework.Utility;
using org.apache.lucene.analysis;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Apps.Presentation.Query.Search.Resources
{
    internal class ResourceHighlighter
        : Highlighter, IResourceHighlighter
    {
        private readonly LuceneQuery _contentQuery;

        public ResourceHighlighter(LuceneQuery contentQuery, Analyzer contentAnalyzer, HighlighterConfiguration configuration)
            : base(contentAnalyzer, configuration)
        {
            _contentQuery = contentQuery;
        }

        string IResourceHighlighter.SummarizeContent(string text, bool htmlEncodeOutput)
        {
            if (string.IsNullOrEmpty(text) || _contentQuery == null)
                return string.Empty;

            var highlightedString = Summarize(_contentQuery, text, htmlEncodeOutput);
            if (!string.IsNullOrEmpty(highlightedString) && highlightedString.Contains(_configuration.StartTag))
            {
                //some highlighting occured - return this string
                return HtmlUtil.CloseHtmlTags(highlightedString);
            }

            return string.Empty;
        }

        string IResourceHighlighter.HighlightTitle(string text, bool htmlEncodeOutput)
        {
            return Highlight(_contentQuery, text, htmlEncodeOutput);
        }

        string IResourceHighlighter.HighlightContent(string text, bool htmlEncodeOutput)
        {
            return Highlight(_contentQuery, text, htmlEncodeOutput);
        }
    }
}
