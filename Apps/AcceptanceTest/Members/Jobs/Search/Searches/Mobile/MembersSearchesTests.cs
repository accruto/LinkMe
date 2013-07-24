using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Mobile
{
    [TestClass]
    public class MembersSearchesTests
        : JobsTests
    {
        private ReadOnlyApplicationUrl _searchesUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
            _searchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches");
        }

        [TestMethod]
        public void TestLinks()
        {
            var member = CreateMember(0);
            LogIn(member);

            Get(_searchesUrl);
            AssertUrl(_searchesUrl);

            AssertLink("//a[@class='button newsearch']", new ReadOnlyApplicationUrl("~/members/home"), "New job search");
            AssertLink("//a[@class='button recentsearches']", new ReadOnlyApplicationUrl("~/members/searches/recent"), "Recent searches");
            AssertLink("//a[@class='button myfavouritesearches']", new ReadOnlyApplicationUrl("~/members/searches/saved"), "My favourite searches");
        }
    }
}
