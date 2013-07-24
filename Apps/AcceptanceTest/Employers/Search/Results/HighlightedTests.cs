using System.Linq;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass]
    public class HighlightedTests
        : SearchTests
    {
        [TestMethod]
        public void TestHighlightedWords()
        {
            CreateMember(0);

            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("mentor test harness");

            // Expanded.

            Get(GetPartialSearchUrl(criteria, DetailLevel.Expanded));
            AssertHighlightedWords("Mentor", "Test", "Harness", "testing", "tests");
        }

        private void AssertHighlightedWords(params string[] words)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//span[@class='highlighted-word']");
            Assert.IsTrue(words.CollectionEqual((from n in nodes select n.InnerText).Distinct()));
        }
    }
}
