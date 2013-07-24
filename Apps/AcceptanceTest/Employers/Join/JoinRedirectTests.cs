using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Join
{
    [TestClass]
    public class JoinRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestJoinRedirect()
        {
            var url = new ReadOnlyApplicationUrl("~/employers/Join.aspx");
            var redirectedUrl = new ReadOnlyApplicationUrl(true, "~/employers/join");
            AssertRedirect(url, redirectedUrl, redirectedUrl);
        }
    }
}
