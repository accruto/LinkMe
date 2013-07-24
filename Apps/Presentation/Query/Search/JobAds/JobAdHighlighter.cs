using LinkMe.Domain.Roles.JobAds;
using org.apache.lucene.analysis;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    internal class JobAdHighlighter
        : Highlighter, IJobAdHighlighter
    {
        private readonly LuceneQuery _contentQuery;
        private readonly LuceneQuery _advertiserQuery;
        private readonly LuceneQuery _titleQuery;

        public JobAdHighlighter(LuceneQuery contentQuery, LuceneQuery titleQuery, LuceneQuery advertiserQuery, Analyzer contentAnalyzer, HighlighterConfiguration configuration)
            : base(contentAnalyzer, configuration)
        {
            _contentQuery = contentQuery;
            _titleQuery = titleQuery;
            _advertiserQuery = advertiserQuery;
        }

        string IJobAdHighlighter.SummarizeContent(JobAd jobAd)
        {
            if (jobAd == null || _contentQuery == null)
                return string.Empty;

            if (jobAd.Description.BulletPoints.Count == 0)
            {
                //add two more fragments in place of the "missing" bullet points
                _configuration.MaxFragments += 2;
            }

            var highlightedString = Summarize(_contentQuery, jobAd.Description.Summary, true);
            if (!string.IsNullOrEmpty(highlightedString) && highlightedString.Contains(_configuration.StartTag))
            {
                //some highlighting occured - return this string
                return highlightedString;
            }
            
            highlightedString = Summarize(_contentQuery, jobAd.Description.Content, true);
            if (!string.IsNullOrEmpty(highlightedString) && highlightedString.Contains(_configuration.StartTag))
            {
                //some highlighting occured - return this string
                return highlightedString;
            }
            
            return string.Empty;
        }

        string IJobAdHighlighter.HighlightTitle(string text)
        {
            return Highlight(_titleQuery, text, true);
        }

        string IJobAdHighlighter.HighlightAdvertiser(string text)
        {
            return Highlight(_advertiserQuery, text, true);
        }

        string IJobAdHighlighter.HighlightContent(string text)
        {
            return Highlight(_contentQuery, text, true);
        }

    }
}
