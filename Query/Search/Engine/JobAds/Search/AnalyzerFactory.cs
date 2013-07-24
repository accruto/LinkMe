using org.apache.lucene.analysis;
using org.apache.lucene.analysis.shingle;
using org.apache.solr.analysis;
using org.apache.solr.common;
using org.apache.solr.util.plugin;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    public class AnalyzerFactory: IAnalyzerFactory
    {
        private static readonly string[] FieldNamesExact = 
        {
             FieldName.ContentExact,                                                   
             FieldName.TitleExact,                                                   
             FieldName.BulletPointsExact,                                                   
             FieldName.AdvertiserNameExact,                                                   
        };

        private readonly TokenizerFactory _tokenizerFactory;
        private readonly TokenFilterFactory _lowerCaseFactory;
        private readonly TokenFilterFactory _synonymFactory;
        private readonly TokenFilterFactory _titleNormaliserFactory;
        private readonly TokenFilterFactory _titleNormaliserQueryFactory;
        private readonly TokenFilterFactory _stopFactory;
        private readonly TokenFilterFactory _wordDelimiterFactory;
        private readonly TokenFilterFactory _stemmerFactory;
        private readonly TokenFilterFactory _commonGramsFactory;
        private readonly TokenFilterFactory _commonGramsQueryFactory;
        private readonly TokenFilterFactory _shingleFilterFactory;

        public AnalyzerFactory(ResourceLoader resourceLoader)
        {
            _tokenizerFactory = new WhitespaceTokenizerFactory();
            _lowerCaseFactory = new LowerCaseFilterFactory();
            
            _shingleFilterFactory = new ShingleFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("maxShingleSize", 3);
                _shingleFilterFactory.init(args);
            }

            _synonymFactory = new SynonymFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                args.put("expand", "false");
                args.put("synonyms", "synonyms.txt");
                _synonymFactory.init(args);
                ((ResourceLoaderAware)_synonymFactory).inform(resourceLoader);
            }

            _titleNormaliserFactory = new SynonymFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                args.put("expand", "true");
                args.put("synonyms", "titles.txt");
                _titleNormaliserFactory.init(args);
                ((ResourceLoaderAware)_titleNormaliserFactory).inform(resourceLoader);
            }

            _titleNormaliserQueryFactory = new SynonymFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                args.put("expand", "false");
                args.put("synonyms", "titles.txt");
                _titleNormaliserQueryFactory.init(args);
                ((ResourceLoaderAware)_titleNormaliserQueryFactory).inform(resourceLoader);
            }

            _stopFactory = new StopFilterFactory();
            {
                var args = new java.util.HashMap();
                args.put("ignoreCase", "true");
                args.put("enablePositionIncrements", "true");
                args.put("words", "stopwords.txt");
                _stopFactory.init(args);
                ((ResourceLoaderAware)_stopFactory).inform(resourceLoader);
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
                ((ResourceLoaderAware) _stemmerFactory).inform(resourceLoader);
            }
        }

        #region Content Analyzer

        public Analyzer CreateContentAnalyzer()
        {
            var analyzer = new PerFieldAnalyzerWrapper(CreateBaseContentAnalyzer(false));

            // Special handling of exact fields.

            var exactAnalyzer = CreateBaseContentAnalyzer(true);

            foreach (var fieldName in FieldNamesExact)
                analyzer.addAnalyzer(fieldName, exactAnalyzer);

            return analyzer;
        }

        public Analyzer CreateHighlighterContentAnalyzer(bool exact)
        {
            // Tokenizer Filters required for highlighting.
            // Excludes ShingleFilterFactory 

            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsFactory,
                        _stopFactory, 
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _titleNormaliserQueryFactory,
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsFactory,
                        _stopFactory, 
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
                        _stopFactory, 
                        _shingleFilterFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _titleNormaliserFactory,
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsFactory,
                        _stopFactory, 
                        _shingleFilterFactory,
                    });
        }

        #endregion

        #region Query Analyzer

        public Analyzer CreateQueryAnalyzer()
        {
            var analyzer = new PerFieldAnalyzerWrapper(CreateBaseQueryAnalyzer(false));

            // Special handling of exact fields.

            var exactAnalyzer = CreateBaseQueryAnalyzer(true);

            foreach (var fieldName in FieldNamesExact)
                analyzer.addAnalyzer(fieldName, exactAnalyzer);

            return analyzer;
        }

        public Analyzer CreateHighlighterQueryAnalyzer(bool exact)
        {
            // Tokenizer Filters required for highlighting.
            // Excludes ShingleFilterFactory 

            return exact
                ? new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _wordDelimiterFactory, 
                        _lowerCaseFactory,
                        _commonGramsQueryFactory,
                        _stopFactory, 
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _titleNormaliserQueryFactory,
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsQueryFactory,
                        _stopFactory, 
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
                        _stopFactory, 
                        _shingleFilterFactory,
                    })
                : new TokenizerChain(_tokenizerFactory, new[] 
                    {
                        _synonymFactory, 
                        _titleNormaliserQueryFactory,
                        _wordDelimiterFactory, 
                        _lowerCaseFactory, 
                        _stemmerFactory,
                        _commonGramsQueryFactory,
                        _stopFactory, 
                        _shingleFilterFactory,
                    });
        }

        #endregion

        public Analyzer CreateSpellingAnalyzer()
        {
            return new TokenizerChain(_tokenizerFactory, new[] 
                {
                    _stopFactory, 
                    _wordDelimiterFactory, 
                    _lowerCaseFactory, 
                });
        }
    }
}
