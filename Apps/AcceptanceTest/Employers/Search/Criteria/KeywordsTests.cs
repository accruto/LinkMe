using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class KeywordsTests
        : CriteriaTests
    {
        private const string AllKeywords = "one two";
        private const string ExactPhrase = "three four";
        private const string AnyKeywords = "five six seven";
        private const string WithoutKeywords = "eight none";

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(AllKeywords, null, null, null);
            TestDisplay(criteria);
            criteria.SetKeywords(null, ExactPhrase, null, null);
            TestDisplay(criteria);
            criteria.SetKeywords(null, null, AnyKeywords, null);
            TestDisplay(criteria);
            criteria.SetKeywords(AllKeywords, null, null, WithoutKeywords);
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestKeywords()
        {
            SearchKeywords("architect", "", "", "", "", "", "");
            AssertKeywords("architect", "", "", "", "", "", "");

            SearchKeywords("it architect", "", "", "", "", "", "");
            AssertKeywords("it architect", "", "", "", "", "", "");

            SearchKeywords("\"it architect\"", "", "", "", "", "", "");
            AssertKeywords("", "", "it architect", "", "", "", "");

            SearchKeywords("\"office management\" \"customer service\"", "", "", "", "", "", "");
            AssertKeywords("\"office management\" \"customer service\"", "", "", "", "", "", "");
        }

        [TestMethod]
        public void TestAllKeywords()
        {
            SearchKeywords("", "architect", "", "", "", "", "");
            AssertKeywords("architect", "", "", "", "", "", "");

            SearchKeywords("", "it architect", "", "", "", "", "");
            AssertKeywords("it architect", "", "", "", "", "", "");

            SearchKeywords("", "\"it architect\"", "", "", "", "", "");
            AssertKeywords("", "", "it architect", "", "", "", "");

            SearchKeywords("", "\"office management\" \"customer service\"", "", "", "", "", "");
            AssertKeywords("\"office management\" \"customer service\"", "", "", "", "", "", "");
        }

        [TestMethod]
        public void TestExactPhrase()
        {
            SearchKeywords("", "", "architect", "", "", "", "");
            AssertKeywords("architect", "", "", "", "", "", "");

            SearchKeywords("", "", "it architect", "", "", "", "");
            AssertKeywords("", "", "it architect", "", "", "", "");

            SearchKeywords("", "", "\"it architect\"", "", "", "", "");
            AssertKeywords("", "", "it architect", "", "", "", "");
        }

        [TestMethod]
        public void TestAnyKeywords()
        {
            SearchKeywords("", "", "", "architect", "", "", "");
            AssertKeywords("architect", "", "", "", "", "", "");

            SearchKeywords("", "", "", "architect", "it", "", "");
            AssertKeywords("", "", "", "architect", "it", "", "");

            SearchKeywords("", "", "", "architect", "", "it", "");
            AssertKeywords("", "", "", "architect", "it", "", "");

            SearchKeywords("", "", "", "architect", "it", ".NET", "");
            AssertKeywords("", "", "", "architect", "it", ".NET", "");

            SearchKeywords("", "", "", "architect java", "it", ".NET", "");
            AssertKeywords("", "", "", "architect", "java", "it .NET", "");

            SearchKeywords("", "", "", "\"a b\"", "\"c d\"", "\"e f\"", "");
            AssertKeywords("", "", "", "\"a b\"", "\"c d\"", "\"e f\"", "");
        }

        [TestMethod]
        public void TestCombination()
        {
            SearchKeywords("", "\"office management\" \"customer service\"", "", "data", "entry", "diary management", "");
            AssertKeywords("", "\"office management\"", "customer service", "data", "entry", "diary management", "");
        }

        [TestMethod]
        public void TestErrorAnd()
        {
            SearchKeywords("AND", null, null, null, null, null, null);
            AssertUrl(GetSearchUrl());
            AssertErrorMessage("The entire query (or a sub-query inside it) is an operator: \"AND\".");
        }

        [TestMethod]
        public void TestErrorOrAnd()
        {
            SearchKeywords("something OR AND else", null, null, null, null, null, null);
            AssertUrl(GetSearchUrl());
            AssertErrorMessage("The query (or a sub-query inside it) contains two operators next to each other: \"OR AND\".");
        }

        [TestMethod]
        public void TestErrorNot()
        {
            SearchKeywords("NOT (Human OR Resources)", null, null, null, null, null, null);
            AssertUrl(GetSearchUrl());
            AssertErrorMessage("The entire expression is a \"NOT\" expression, which is not supported.");
        }

        private void SearchKeywords(string keywords, string allKeywords, string exactPhrase, string anyKeywords1, string anyKeywords2, string anyKeywords3, string withoutKeywords)
        {
            Get(GetSearchUrl());
            _keywordsTextBox.Text = keywords;
            _allKeywordsTextBox.Text = allKeywords;
            _exactPhraseTextBox.Text = exactPhrase;
            _anyKeywordsTester.Texts = new[] { anyKeywords1, anyKeywords2, anyKeywords3 };
            _withoutKeywordsTextBox.Text = withoutKeywords;
            _searchButton.Click();
        }

        private void AssertKeywords(string keywords, string allKeywords, string exactPhrase, string anyKeywords1, string anyKeywords2, string anyKeywords3, string withoutKeywords)
        {
            Assert.AreEqual(keywords, _keywordsTextBox.Text);
            Assert.AreEqual(allKeywords, _allKeywordsTextBox.Text);
            Assert.AreEqual(exactPhrase, _exactPhraseTextBox.Text);
            var texts = _anyKeywordsTester.Texts;
            Assert.AreEqual(anyKeywords1, texts[0]);
            Assert.AreEqual(anyKeywords2, texts[1]);
            Assert.AreEqual(anyKeywords3, texts[2]);
            Assert.AreEqual(withoutKeywords, _withoutKeywordsTextBox.Text);
        }
    }
}