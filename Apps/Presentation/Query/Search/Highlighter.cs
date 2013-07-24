using System;
using org.apache.lucene.analysis;
using org.apache.lucene.search.highlight;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneHighlighter = org.apache.lucene.search.highlight.Highlighter;

namespace LinkMe.Apps.Presentation.Query.Search
{
    public class HighlighterConfiguration
    {
        public HighlighterConfiguration()
        {
            FragmentSize = 70;
            MaxFragments = 3;
            StartTag = "<span class=\"highlighted-word\">";
            EndTag = "</span>";
            Separator = " ... ";
        }

        public int FragmentSize { get; set; }
        public int MaxFragments { get; set; }
        public string StartTag { get; set; }
        public string EndTag { get; set; }
        public string Separator { get; set; }
    }

    public class Highlighter
    {
        private readonly Analyzer _contentAnalyzer;
        protected readonly HighlighterConfiguration _configuration;

        protected Highlighter(Analyzer contentAnalyzer, HighlighterConfiguration configuration)
        {
            _contentAnalyzer = contentAnalyzer;
            _configuration = configuration;
        }

        protected string Summarize(LuceneQuery query, string text, bool htmlEncodeOutput)
        {
            if (query == null || string.IsNullOrEmpty(text))
                return null;

            try
            {
                // Build the highlighter.

                var formatter = new SimpleHTMLFormatter(_configuration.StartTag, _configuration.EndTag);
                var scorer = new QueryScorer(query);

                Encoder encoder;

                if (htmlEncodeOutput)
                    encoder = new SimpleHTMLEncoder();
                else
                    encoder = new DefaultEncoder();

                var highlighter = new LuceneHighlighter(formatter, encoder, scorer);
                highlighter.setTextFragmenter(new SimpleSpanFragmenter(scorer, _configuration.FragmentSize));

                // Perform highlighting.

                var tokenStream = _contentAnalyzer.tokenStream(string.Empty, new java.io.StringReader(text));
                return highlighter.getBestFragments(tokenStream, text, _configuration.MaxFragments, _configuration.Separator);
            }
            catch (Exception)
            {
                // on error just return the original string
                return text;
            }
        }

        protected string Highlight(LuceneQuery query, string text, bool htmlEncodeOutput)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            try
            {
                Encoder encoder;

                if (htmlEncodeOutput)
                    encoder = new SimpleHTMLEncoder();
                else
                    encoder = new DefaultEncoder();

                if (query == null)
                    return encoder.encodeText(text); // nothing to highlight

                // Build the highlighter.

                var formatter = new SimpleHTMLFormatter(_configuration.StartTag, _configuration.EndTag);
                var highlighter = new LuceneHighlighter(formatter, encoder, new QueryScorer(query));
                highlighter.setTextFragmenter(new NullFragmenter());

                // Perform highlighting.

                var highlightedHtml = highlighter.getBestFragment(_contentAnalyzer, string.Empty, text);
                return highlightedHtml ?? encoder.encodeText(text);
            }
            catch (Exception)
            {
                // on error just return the original string
                return text;
            }
        }
    }
}
