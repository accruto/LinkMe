using System;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.analysis.tokenattributes;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.queryParser;
using org.apache.lucene.search;
using org.apache.lucene.store;
using org.apache.solr.common;
using LuceneVersion = org.apache.lucene.util.Version;

namespace LinkMe.Query.Search.Engine.Test
{
    [TestClass]
    public class AnalyzerTests
    {
        private const string ContentField = "content";
        private static Analyzer _contentAnalyzer;
        private static QueryParser _queryParser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var analyzerFactory = new AnalyzerFactory(new ResourceLoaderImpl(@"Query\Search\Engine\Test\Data"));

            _contentAnalyzer = analyzerFactory.CreateBaseContentAnalyzer(false);

            var queryAnalyzer = analyzerFactory.CreateBaseQueryAnalyzer(false);
            _queryParser = new QueryParser(LuceneVersion.LUCENE_29, ContentField, queryAnalyzer);
            _queryParser.setDefaultOperator(QueryParser.Operator.AND);
        }

        [TestMethod]
        public void SynonymTest()
        {
            var indexWriter = new IndexWriter(new RAMDirectory(), _contentAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocument(indexWriter, "This device is HDTV compatible.");
            AddDocument(indexWriter, "Purchasing officer");
            indexWriter.commit();
            var indexSearcher = new IndexSearcher(indexWriter.getReader());

            var query = _queryParser.parse("hdtv");
            var hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("high definition television");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("television");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("\"high definition television\"");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            // Case 11017 "Purchasing Officer" returned while searching for "Technical coordinator".

            query = _queryParser.parse("Technical coordinator");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(0, hits.totalHits);
        }

        [TestMethod]
        public void StemmerTest()
        {
            var indexWriter = new IndexWriter(new RAMDirectory(), _contentAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocument(indexWriter, "All our accountants and lawyers are busy at the moment");
            indexWriter.commit();
            var indexSearcher = new IndexSearcher(indexWriter.getReader());

            var query = _queryParser.parse("Accountant");
            var hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("account");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(0, hits.totalHits);

            query = _queryParser.parse("Lawyers");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("Lawyer");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);
        }

        [TestMethod]
        public void PunctuationTest()
        {
            var indexWriter = new IndexWriter(new RAMDirectory(), _contentAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocument(indexWriter, "One, two.Three");
            indexWriter.commit();
            var indexSearcher = new IndexSearcher(indexWriter.getReader());

            var query = _queryParser.parse("one");
            var hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("two");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);

            query = _queryParser.parse("three");
            hits = indexSearcher.search(query, 10);
            Assert.AreEqual(1, hits.totalHits);
        }

        [TestMethod]
        public void StopWordTest()
        {
            var indexWriter = new IndexWriter(new RAMDirectory(), _contentAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocument(indexWriter, "Senior IT architect Awesome Software");
            AddDocument(indexWriter, "Senior architect  Awesome Homes");
            AddDocument(indexWriter, "the rain in spain falls mainly");
            indexWriter.commit();
            var indexSearcher = new IndexSearcher(indexWriter.getReader());

            var hits = Search(indexSearcher, "architect Awesome");
            Assert.AreEqual(2, hits.totalHits);

            hits = Search(indexSearcher, "\"architect Awesome\"");
            Assert.AreEqual(2, hits.totalHits);

            hits = Search(indexSearcher, "\"IT architect\"");
            Assert.AreEqual(1, hits.totalHits);

            hits = Search(indexSearcher, "\"senior architect\"");
            Assert.AreEqual(1, hits.totalHits);

            hits = Search(indexSearcher, "\"senior IT architect\"");
            Assert.AreEqual(1, hits.totalHits);

            hits = Search(indexSearcher, "\"senior IT\"");
            Assert.AreEqual(1, hits.totalHits);
        }

        private static void AddDocument(IndexWriter indexWriter, string content)
        {
            var document = new Document();
            document.add(new Field(ContentField, content, Field.Store.NO, Field.Index.ANALYZED));
            indexWriter.addDocument(document);
            DisplayTokens(indexWriter.getAnalyzer(), content);
        }

        private TopDocs Search(IndexSearcher indexSearcher, string queryString)
        {
            var query = _queryParser.parse(queryString);
            Console.WriteLine();
            Console.WriteLine(queryString);
            Console.WriteLine(query.toString());
            var hits = indexSearcher.search(query, 10);
            return hits;
        }

        private static void DisplayTokens(Analyzer analyzer, string text)
        {
            var stream = analyzer.tokenStream(ContentField, new java.io.StringReader(text));
            var term = (TermAttribute)stream.addAttribute(typeof(TermAttribute));
            var posIncr = (PositionIncrementAttribute)stream.addAttribute(typeof(PositionIncrementAttribute));
            var offset = (OffsetAttribute)stream.addAttribute(typeof(OffsetAttribute));
            var type = (TypeAttribute)stream.addAttribute(typeof(TypeAttribute));

            Console.WriteLine();
            Console.WriteLine(text);
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
