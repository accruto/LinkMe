using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.Search.JobAds;
using org.apache.lucene.analysis;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.lucene.queryParser;
using org.apache.lucene.search;
using org.apache.lucene.search.highlight;
using LuceneEncoder = org.apache.lucene.search.highlight.Encoder;
using LuceneHighlighter = org.apache.lucene.search.highlight.Highlighter;
using LuceneVersion = org.apache.lucene.util.Version;
using LinkMe.Framework.Utility;
using Scorer = org.apache.lucene.search.highlight.Scorer;

namespace LinkMe.Apps.Presentation.Query.Search.JobAds
{
    public class JobSearchHighlighter
    {
        private const string Field = "f"; // dummy

        private const int DigestMaxSize = 150;
        private const int FragmentSize = 70;
        private const int MaxFragments = 3;
        private readonly string _startTag;
        private readonly string _endTag;
        private const string Separator = " ... ";

        private readonly QueryScorer _titleScorer;
        private readonly QueryScorer _contentScorer;
        private readonly QueryScorer _posterScorer;

        public JobSearchHighlighter(JobAdSearchCriteria criteria)
            : this(criteria, "<b>", "</b>")
        { }

        public JobSearchHighlighter(JobAdSearchCriteria criteria, string startTag, string endTag)
        {
            if (criteria == null)
                return; // will be doing pass-through

            _startTag = startTag;
            _endTag = endTag;

            var parser = new QueryParser(LuceneVersion.LUCENE_29, Field, new SimpleAnalyzer());

            // Build title scorer.

            // Both the "title" and "keywords" criteria are matched in the ad title, so highlight them both.
            if (criteria.AdTitleExpression != null || criteria.KeywordsExpression != null)
            {
                var query = new BooleanQuery();

                if (criteria.AdTitleExpression != null)
                    AddExpression(query, criteria.AdTitleExpression, parser);

                if (criteria.KeywordsExpression != null)
                    AddExpression(query, criteria.KeywordsExpression, parser);

                _titleScorer = new QueryScorer(query);
            }

            // Build content scorer.

            if (criteria.KeywordsExpression != null)
            {
                var query = new BooleanQuery();
                AddExpression(query, criteria.KeywordsExpression, parser);

                _contentScorer = new QueryScorer(query);
            }

            // Build job poster scorer.

            if (criteria.KeywordsExpression == null && criteria.AdvertiserNameExpression == null)
                return;

            var jobQuery = new BooleanQuery();

            if (criteria.KeywordsExpression != null)
                AddExpression(jobQuery, criteria.KeywordsExpression, parser);

            if (criteria.AdvertiserNameExpression != null)
                AddExpression(jobQuery, criteria.AdvertiserNameExpression, parser);

            _posterScorer = new QueryScorer(jobQuery);
        }

        public string HighlightTitle(string text)
        {
            return Highlight(_titleScorer, HttpUtility.HtmlDecode(text), false);
        }

        public string HighlightContent(string text, bool isHtml)
        {
            return Highlight(_contentScorer, text, isHtml);
        }

        public string HighlightPoster(string text)
        {
            return Highlight(_posterScorer, text, false);
        }

        public string GetBestContent(string text)
        {
            if (_contentScorer == null)
                return string.Empty;

            // Build the TokenStream on top of the text.

            Analyzer analyzer = new SimpleAnalyzer();
            var tokenStream = analyzer.tokenStream(Field, new java.io.StringReader(text));

            // Build the highlighter.

            var highlighter = new LuceneHighlighter(
                new SimpleHTMLFormatter(_startTag, _endTag),
                new SimpleHTMLEncoder(), _contentScorer);
            highlighter.setTextFragmenter(new SimpleSpanFragmenter(_contentScorer, FragmentSize));

            // Perform highlighting.

            var highlightedText = highlighter.getBestFragments(tokenStream, text,
                MaxFragments, Separator);

            return !string.IsNullOrEmpty(highlightedText)
                ? Separator + highlightedText + Separator
                : string.Empty;
        }

        public void Summarize(string summaryText, IList<string> bulletPointsText, string contentHtml,
            bool includeContent, out string digestText, out string bodyText)
        {
            var digestBuilder = new StringBuilder(DigestMaxSize);

            // Add summary.

            digestBuilder.Append(summaryText);

            // Add bullet points.

            if (bulletPointsText != null)
            {
                foreach (var bulletPoint in bulletPointsText)
                {
                    if (!string.IsNullOrEmpty(bulletPoint))
                        digestBuilder.AppendFormat(" \x2022\x00a0{0}", bulletPoint.Trim()); // &bull;&nbsp;text
                }
            }

            // Check whether we need to go through the content.

            // TODO: consider using MS Search HTML IFilter instead
            var contentText = HttpUtility.HtmlDecode(
                HtmlUtil.StripHtmlTags(
                    HtmlUtil.AposToHtml(contentHtml)));

            contentText = contentText.Replace('\x0095', '\x2022'); // replace Windows Western bullet with Unicode bullet

            if (!includeContent && digestBuilder.Length > 0)
            {
                digestText = digestBuilder.ToString();
                bodyText = contentText;
                return;
            }

            // Add the content extract (up to DigestMaxSize in total).

            Analyzer analyzer = new SimpleAnalyzer();
            var contentStream = analyzer.tokenStream(Field, new java.io.StringReader(contentText));
            var offset = (OffsetAttribute)contentStream.addAttribute(typeof(OffsetAttribute));
            Fragmenter fragmenter = new SimpleFragmenter(DigestMaxSize - digestBuilder.Length);
            fragmenter.start(contentText, contentStream);

            var endOffset = 0;
            while (contentStream.incrementToken())
            {
                endOffset = offset.endOffset();
                if (fragmenter.isNewFragment())
                    break;
            }

            if (digestBuilder.Length > 0)
                digestBuilder.Append(' ');

            digestBuilder.Append(contentText, 0, endOffset);
            digestBuilder.Append(" ...");
            digestText = digestBuilder.ToString();
            bodyText = contentText.Substring(endOffset);
        }

        private static void AddExpression(BooleanQuery query, IExpression expr, QueryParser queryParser)
        {
            foreach (var literal in expr.GetUniqueLiterals(false))
            {
                // Ensure there are no escape chars in the literal before adding
                var phraseQuery = queryParser.parse("\"" + literal.Replace("\\", string.Empty) + "\"");
                query.add(phraseQuery, BooleanClause.Occur.SHOULD);
            }
        }

        private string Highlight(Scorer scorer, string text, bool isHtml)
        {
            var encoder = isHtml ? (LuceneEncoder)new DefaultEncoder() : new SimpleHTMLEncoder();

            if (scorer == null)
                return encoder.encodeText(text); // nothing to highlight

            // Build the TokenStream on top of the text. 

            var analyzer = isHtml ? (Analyzer)new SimpleHtmlAnalyzer() : new SimpleAnalyzer();
            var tokenStream = analyzer.tokenStream(Field, new java.io.StringReader(text));

            // Build the highlighter.

            var highlighter = new LuceneHighlighter(
                new SimpleHTMLFormatter(_startTag, _endTag),
                encoder, scorer);
            highlighter.setTextFragmenter(new NullFragmenter());

            // Perform highlighting.

            var highlightedText = highlighter.getBestFragment(tokenStream, text);
            return highlightedText ?? encoder.encodeText(text);
        }

        #region Nested Classes

        private class HtmlTokenizer : CharTokenizer
        {
            private bool _inTag;

            public HtmlTokenizer(java.io.Reader input)
                : base(input)
            {
            }

            protected override bool isTokenChar(char c)
            {
                if (c == '<' && _inTag == false)
                {
                    _inTag = true;
                    return false;
                }
                if (c == '>' && _inTag)
                {
                    _inTag = false;
                    return false;
                }

                return !_inTag && !Char.IsWhiteSpace(c);
            }
        }

        /// <summary>
        /// Custom Lucene Analyzer that strips HTML markup (case-insensitive).
        /// </summary>
        private class SimpleHtmlAnalyzer : Analyzer
        {
            public override TokenStream tokenStream(string fieldName, java.io.Reader reader)
            {
                TokenStream result = new HtmlTokenizer(reader);
                result = new LowerCaseFilter(result);
                return result;
            }
        }

        #endregion
    }
}