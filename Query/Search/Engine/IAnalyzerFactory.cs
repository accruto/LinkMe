using org.apache.lucene.analysis;

namespace LinkMe.Query.Search.Engine
{
    public interface IAnalyzerFactory
    {
        #region Content Analyzer
        
        Analyzer CreateContentAnalyzer();
        Analyzer CreateHighlighterContentAnalyzer(bool exact);
        Analyzer CreateBaseContentAnalyzer(bool exact);
        
        #endregion

        #region Query Analyzer

        Analyzer CreateQueryAnalyzer();
        Analyzer CreateHighlighterQueryAnalyzer(bool exact);
        Analyzer CreateBaseQueryAnalyzer(bool exact);

        #endregion
    }
}
