using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.Search.Engine.JobAds.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.solr.common;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
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
            Assert.AreEqual("content:\"(analyst analyst|programmer) programmer\"", query.toString());

            query = CreateQuery("\"analyst OR programmer\"");
            Assert.AreEqual("content:\"(analyst_or analyst_or|or_programmer) or_programmer\"", query.toString());
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

            query = CreateQuery("designer programmer'");
            Assert.AreEqual("+content:designer +content:programmer", query.toString());
        }

        [TestMethod]
        public void BoostQueryTest()
        {
            var query = CreateBoostQuery("analyst programmer");
            Assert.AreEqual("content:analyst content:programmer", query.toString());

            query = CreateBoostQuery("\"analyst programmer\"");
            Assert.AreEqual("content:\"(analyst analyst|programmer) programmer\"~100", query.toString());

            query = CreateBoostQuery("developer AND (programmer OR analyst)");
            Assert.AreEqual("content:developer (content:programmer content:analyst)", query.toString());
        }

        [TestMethod]
        public void SynonymTest()
        {
            var query = CreateQuery("CEO", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:00001", query.toString());

            query = CreateQuery("C.E.O.", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:00001", query.toString());

            query = CreateQuery("Chief Executive Officer", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:00001", query.toString());

            query = CreateQuery("Executive Officer", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:\"(executive executive|officer) officer\"~100", query.toString());

            query = CreateQuery("Executive", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:executive", query.toString());

            query = CreateQuery("Collections Mgr", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:00002", query.toString());

            query = CreateExactQuery("asst mgr");
            Assert.AreEqual("content:\"(assistant assistant|manager) manager\"", query.toString());

            query = CreateQuery("General Manager HR", ModificationFlags.AllowShingling);
            Assert.AreEqual("content:\"(general general|manager general|manager|human) (manager manager|human manager|human|resources) (human human|resources) resources\"~100", query.toString());

        }

        private org.apache.lucene.search.Query CreateQuery(string queryString)
        {
            return CreateQuery(queryString, ModificationFlags.None);
        }

        private org.apache.lucene.search.Query CreateQuery(string queryString, ModificationFlags flags)
        {
            var expression = Expression.Parse(queryString, flags);
            return expression.GetLuceneQuery("content", _queryAnalyzer);
        }

        private org.apache.lucene.search.Query CreateExactQuery(string queryString)
        {
            var expression = Expression.ParseExactPhrase(queryString);
            return expression.GetLuceneQuery("content", _queryAnalyzer);
        }

        private org.apache.lucene.search.Query CreateBoostQuery(string queryString)
        {
            var expression = Expression.Parse(queryString);
            return expression.GetLuceneBoostQuery("content", _queryAnalyzer);
        }
    }
}
