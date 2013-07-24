using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.solr.common;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.Test.Members
{
    [TestClass]
    public class ExpressionExtensionsTests
    {
        private static Analyzer _queryAnalyzer;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var analyzerFactory = new AnalyzerFactory(new ResourceLoaderImpl(@"Query\Search\Engine\Test\Data"));
            _queryAnalyzer = analyzerFactory.CreateBaseQueryAnalyzer(false);
        }

        [TestMethod]
        public void TermTest()
        {
            var query = CreateQuery("analyst");
            Assert.AreEqual("content:analyst", query.toString());

            query = CreateQuery("analyst programmer");
            Assert.AreEqual("+content:analyst +content:programmer", query.toString());

            query = CreateQuery("\"analyst programmer\"");
            Assert.AreEqual("content:\"analyst programmer\"", query.toString());

            query = CreateQuery("\"analyst OR programmer\"");
            Assert.AreEqual("content:\"analyst_or or_programmer\"", query.toString());
        }

        [TestMethod]
        public void BooleanTest()
        {
            var query = CreateQuery("developer AND programmer");
            Assert.AreEqual("+content:developer +content:programmer", query.toString());

            query = CreateQuery("developer OR programmer");
            Assert.AreEqual("content:developer content:programmer", query.toString());

            query = CreateQuery("developer AND NOT manager");
            Assert.AreEqual("+content:developer -content:manager", query.toString());

            query = CreateQuery("developer AND programmer OR analyst");
            Assert.AreEqual("(+content:developer +content:programmer) content:analyst", query.toString());

            query = CreateQuery("developer OR programmer AND analyst");
            Assert.AreEqual("content:developer (+content:programmer +content:analyst)", query.toString());

            query = CreateQuery("developer AND (programmer OR analyst)");
            Assert.AreEqual("+content:developer +(content:programmer content:analyst)", query.toString());
        }

        [TestMethod]
        public void SpecialCharTest()
        {
            var query = CreateQuery("designer & programmer");
            Assert.AreEqual("+content:designer +content:programmer", query.toString());

            query = CreateQuery("designer ! programmer");
            Assert.AreEqual("+content:designer +content:programmer", query.toString());

            query = CreateQuery("designer + programmer");
            Assert.AreEqual("+content:designer +content:programmer", query.toString());

            query = CreateQuery("designer, programmer");
            Assert.AreEqual("+content:designer +content:programmer", query.toString());
        }

        [TestMethod]
        public void BoostQueryTest()
        {
            var query = CreateBoostQuery("analyst programmer");
            Assert.AreEqual("content:analyst content:programmer", query.toString());

            query = CreateBoostQuery("\"analyst programmer\"");
            Assert.AreEqual("content:analyst content:programmer", query.toString());

            query = CreateBoostQuery("developer AND (programmer OR analyst)");
            Assert.AreEqual("content:developer (content:programmer content:analyst)", query.toString());
        }

        private LuceneQuery CreateQuery(string queryString)
        {
            var expression = Expression.Parse(queryString);
            return expression.GetLuceneQuery("content", _queryAnalyzer);
        }

        private LuceneQuery CreateBoostQuery(string queryString)
        {
            var expression = Expression.Parse(queryString);
            return expression.GetLuceneBoostQuery("content", _queryAnalyzer);
        }
    }
}
