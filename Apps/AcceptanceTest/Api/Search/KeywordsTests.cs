using System.Web.Script.Serialization;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Api.Models.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Search
{
    [TestClass]
    public class KeywordsTests
        : WebTestClass
    {
        [TestMethod]
        public void TestSplitKeywords()
        {
            var model = SplitKeywords("oneWord");
            AssertModel("oneWord", "oneWord", null, null, null, model);

            model = SplitKeywords("two words");
            AssertModel("two words", "two words", null, null, null, model);

            model = SplitKeywords("\"exact phrase\"");
            AssertModel("\"exact phrase\"", null, "exact phrase", null, null, model);

            model = SplitKeywords("any or word");
            AssertModel("any or word", null, null, "any word", null, model);

            model = SplitKeywords("these words and not thisWord");
            AssertModel("these words and not thisWord", "these words", null, null, "thisWord", model);

            model = SplitKeywords("(how or about) all this \"complex stuff\" and not (any or \"of these\")");
            AssertModel("(how or about) all this \"complex stuff\" and not (any or \"of these\")", "all this", "complex stuff", "how about", "any \"of these\"", model);

            // Failure.

            model = SplitKeywords(null);
            AssertModel(null, null, null, null, null, model);

            model = SplitKeywords("(one or two) (three or four)");
            AssertModel("(one or two) (three or four)", "(one or two) (three or four)", null, null, null, model);

            model = SplitKeywords("(one and two) or (three and four)");
            AssertModel("(one and two) or (three and four)", "(one and two) or (three and four)", null, null, null, model);

            model = SplitKeywords("this and not (anded insideNot)");
            AssertModel("this and not (anded insideNot)", "this and not (anded insideNot)", null, null, null, model);
        }

        [TestMethod]
        public void TestCombineKeywords()
        {
            var model = CombineKeywords("oneWord", null, null, null);
            AssertModel("oneWord", "oneWord", null, null, null, model);

            model = CombineKeywords("two words", null, null, null);
            AssertModel("two words", "two words", null, null, null, model);

            model = CombineKeywords(null, "exact phrase", null, null);
            AssertModel("\"exact phrase\"", null, "exact phrase", null, null, model);

            model = CombineKeywords(null, null, "any word", null);
            AssertModel("any OR word", null, null, "any word", null, model);

            model = CombineKeywords("these words", null, null, "thisWord");
            AssertModel("these words AND NOT thisWord", "these words", null, null, "thisWord", model);

            model = CombineKeywords("all this", "complex stuff", "how about", "any \"of these\"");
            AssertModel("all this \"complex stuff\" (how OR about) AND NOT (any OR \"of these\")", "all this", "complex stuff", "how about", "any \"of these\"", model);
        }

        private static void AssertModel(string keywords, string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords, SplitKeywordsModel model)
        {
            Assert.AreEqual(keywords, model.Keywords);
            Assert.AreEqual(allKeywords, model.AllKeywords);
            Assert.AreEqual(exactPhrase, model.ExactPhrase);
            Assert.AreEqual(anyKeywords, model.AnyKeywords);
            Assert.AreEqual(withoutKeywords, model.WithoutKeywords);
        }

        private SplitKeywordsModel SplitKeywords(string keywords)
        {
            var response = Post(new ReadOnlyApplicationUrl("~/api/search/splitkeywords", new ReadOnlyQueryString("keywords", keywords)));
            return new JavaScriptSerializer().Deserialize<SplitKeywordsModel>(response);
        }

        private SplitKeywordsModel CombineKeywords(string allKeywords, string exactPhrase, string anyKeywords, string withoutKeywords)
        {
            var response = Post(new ReadOnlyApplicationUrl("~/api/search/combinekeywords",
                new ReadOnlyQueryString("allKeywords", allKeywords, "exactPhrase", exactPhrase, "anyKeywords", anyKeywords, "withoutKeywords", withoutKeywords)));

            return new JavaScriptSerializer().Deserialize<SplitKeywordsModel>(response);
        }
    }
}
