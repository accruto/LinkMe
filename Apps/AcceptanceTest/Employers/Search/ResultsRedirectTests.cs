using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class ResultsRedirectTests
        : SearchTests
    {
        [TestMethod]
        public void TestResultsUrl()
        {
            Get(GetResultsUrl());
            AssertUrl(GetResultsUrl());

            AssertPageContains("window.location = \"" + GetSearchUrl() + "\";", true);
        }
    }
}
