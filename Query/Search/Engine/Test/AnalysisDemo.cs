using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using java.util;
using org.apache.lucene.analysis;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.solr.analysis;
using org.apache.solr.common;
using org.apache.solr.util.plugin;

namespace LinkMe.Query.Search.Engine.Test
{
    [TestClass]
    public class AnalysisDemo
    {
        private static TokenizerFactory _tokenizerFactory;
        private static TokenFilterFactory _lowerCaseFactory;
        private static TokenFilterFactory _synonymFactory;
        private static TokenFilterFactory _stopFactory;
        private static TokenFilterFactory _wordDelimiterFactory;
        private static TokenFilterFactory _stemmerFactory;

        //private const string text1 = "M.D. of XY&Z Corporation (xyz@example.com) expressed some concerns about user experience.";
        private const string Text1 = "business activities in the center";

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var resourceLoader = new ResourceLoaderImpl(@"Apps\Config");

            _tokenizerFactory = new WhitespaceTokenizerFactory();
            _lowerCaseFactory = new LowerCaseFilterFactory();

            _synonymFactory = new SynonymFilterFactory();
            var args = new HashMap();
            args.put("ignoreCase", "true");
            args.put("expand", "false");
            args.put("synonyms", "synonyms.txt");
            _synonymFactory.init(args);
            ((ResourceLoaderAware)_synonymFactory).inform(resourceLoader);

            _stopFactory = new StopFilterFactory();
            args = new HashMap();
            args.put("ignoreCase", "true");
            args.put("enablePositionIncrements", "true");
            args.put("words", "stopwords.txt");
            _stopFactory.init(args);
            ((ResourceLoaderAware)_stopFactory).inform(resourceLoader);

            _wordDelimiterFactory = new WordDelimiterFilterFactory();
            args = new HashMap();
            args.put("catenateWords", "1");
            args.put("catenateNumbers", "1");
            args.put("protected", "protwords.txt");
            _wordDelimiterFactory.init(args);
            ((ResourceLoaderAware)_wordDelimiterFactory).inform(resourceLoader);

            _stemmerFactory = new KStemFilterFactory();
            args = new HashMap();
            args.put("protected", "protwords.txt");
            _stemmerFactory.init(args);
            ((ResourceLoaderAware)_stemmerFactory).inform(resourceLoader);
        }

        [TestMethod]
        public void TokenizerDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory, new TokenFilterFactory[0]);
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void SynonymDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory, new[] { _synonymFactory });
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void StopDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory, 
                new[] { _synonymFactory, _stopFactory });
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void WordDelimiterDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory, 
                new[] { _synonymFactory, _stopFactory, _wordDelimiterFactory });
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void LowerCaseDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory,
                new[] { _synonymFactory, _stopFactory, _wordDelimiterFactory, _lowerCaseFactory });
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void StemmerDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory,
                new[] { _synonymFactory, _stopFactory, _wordDelimiterFactory, _lowerCaseFactory, _stemmerFactory });
            DisplayTokens(analyzer, Text1);
        }

        [TestMethod]
        public void ExactDemo()
        {
            Analyzer analyzer = new TokenizerChain(_tokenizerFactory,
                new[] { _stopFactory, _wordDelimiterFactory, _lowerCaseFactory });
            DisplayTokens(analyzer, Text1);
        }

        private static void DisplayTokens(Analyzer analyzer, string text)
        {
            var stream = analyzer.tokenStream(string.Empty, new java.io.StringReader(text));
            var term = (TermAttribute)stream.addAttribute(typeof(TermAttribute));
            var posIncr = (PositionIncrementAttribute)stream.addAttribute(typeof(PositionIncrementAttribute));
            var offset = (OffsetAttribute)stream.addAttribute(typeof(OffsetAttribute));
            var type = (TypeAttribute)stream.addAttribute(typeof(TypeAttribute));

            int position = 0;
            while (stream.incrementToken())
            {
                int increment = posIncr.getPositionIncrement();
                if (increment > 0)
                {
                    position = position + increment;
                    Console.WriteLine();
                    Console.Write(position + ": ");
                }

                Console.Write("[" +
                                 term.term() + ":" +
                                 offset.startOffset() + "->" +
                                 offset.endOffset() + ":" +
                                 type.type() + "] ");
            }

            Console.WriteLine();
        }
    }
}
