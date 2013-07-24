using org.apache.lucene.analysis;
using org.apache.lucene.analysis.ngram;
using org.apache.solr.analysis;
using org.apache.solr.common;
using org.apache.solr.util.plugin;

namespace LinkMe.Query.Search.Engine.Resources
{
    public class AnalyzerFactory: IAnalyzerFactory
    {
        private readonly TokenizerFactory _tokenizerFactory;
        private readonly TokenFilterFactory _lowerCaseFactory;
        private readonly TokenFilterFactory _synonymFactory;
        private readonly TokenFilterFactory _wordDelimiterFactory;
        private readonly TokenFilterFactory _stemmerFactory;
        private readonly TokenFilterFactory _commonGramsFactory;
        private readonly TokenFilterFactory _commonGramsQueryFactory;
        private readonly TokenFilterFactory _edgeNGramFactory;

        public AnalyzerFactory(ResourceLoader resourceLoader)
        {
            _tokenizerFactory = new WhitespaceTokenizerFactory();
            _lowerCaseFactory = new LowerCaseFilterFactory();

            _synonymFactory = new SynonymFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                args.put("expand", "false");
                args.put("synonyms", "synonyms.txt");
                _synonymFactory.init(args);
                ((ResourceLoaderAware)_synonymFactory).inform(resourceLoader);
            }

            _commonGramsFactory = new CommonGramsFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                _commonGramsFactory.init(args);
                ((ResourceLoaderAware)_commonGramsFactory).inform(resourceLoader);
            }

            _commonGramsQueryFactory = new CommonGramsQueryFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                _commonGramsQueryFactory.init(args);
                ((ResourceLoaderAware)_commonGramsQueryFactory).inform(resourceLoader);
            }

            _wordDelimiterFactory = new WordDelimiterFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("catenateWords", "1");
                args.put("catenateNumbers", "1");
                args.put("protected", "protwords.txt");
                _wordDelimiterFactory.init(args);
                ((ResourceLoaderAware)_wordDelimiterFactory).inform(resourceLoader);
            }

            _stemmerFactory = new KStemFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("protected", "protwords.txt");
                _stemmerFactory.init(args);
                ((ResourceLoaderAware)_stemmerFactory).inform(resourceLoader);
            }

            _edgeNGramFactory = new EdgeNGramTokenFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("side", "FRONT");
                args.put("minGramSize", 2);
                _edgeNGramFactory.init(args);
                ((ResourceLoaderAware)_edgeNGramFactory).inform(resourceLoader);
            }
        }

        #region Content Analyzer

        public Analyzer CreateContentAnalyzer()
        {
            var analyzer = new PerFieldAnalyzerWrapper(CreateBaseContentAnalyzer(false));

            return analyzer;
        }

        public Analyzer CreateHighlighterContentAnalyzer(bool exact)
        {
            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsFactory,
                        _edgeNGramFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsFactory,
                        _edgeNGramFactory,
                    });
        }

        public Analyzer CreateBaseContentAnalyzer(bool exact)
        {
            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsFactory,
                        _edgeNGramFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsFactory,
                        _edgeNGramFactory,
                    });
        }

        #endregion

        #region Query Analyzer

        public Analyzer CreateQueryAnalyzer()
        {
            var analyzer = new PerFieldAnalyzerWrapper(CreateBaseQueryAnalyzer(false));

            return analyzer;
        }

        public Analyzer CreateHighlighterQueryAnalyzer(bool exact)
        {
            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsQueryFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsQueryFactory,
                    });
        }

        public Analyzer CreateBaseQueryAnalyzer(bool exact)
        {
            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsQueryFactory,
                        _edgeNGramFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsQueryFactory,
                        _edgeNGramFactory,
                    });
        }

        #endregion

        public Analyzer CreateSpellingAnalyzer()
        {
            return new TokenizerChain(_tokenizerFactory, new[] 
                {
                    _wordDelimiterFactory, 
                    _lowerCaseFactory, 
                });
        }
    }
}
