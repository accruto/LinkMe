using LinkMe.Query.Search.Engine;
using org.apache.lucene.analysis;

namespace LinkMe.Apps.Presentation.Query.Search
{
    public class HighlighterFactory
    {
        protected Analyzer _defaultContentAnalyzer;
        protected Analyzer _exactContentAnalyzer;
        protected Analyzer _defaultQueryAnalyzer;
        protected Analyzer _exactQueryAnalyzer;

        protected HighlighterFactory(IAnalyzerFactory analyzerFactory, bool useExactForDefault)
        {
            _defaultContentAnalyzer = analyzerFactory.CreateHighlighterContentAnalyzer(useExactForDefault);
            _exactContentAnalyzer = analyzerFactory.CreateHighlighterContentAnalyzer(true);

            _defaultQueryAnalyzer = analyzerFactory.CreateHighlighterQueryAnalyzer(useExactForDefault);
            _exactQueryAnalyzer = analyzerFactory.CreateHighlighterQueryAnalyzer(true);
        }
    }
}
