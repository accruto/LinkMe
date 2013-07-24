using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class JoinRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestJoinRedirects()
        {
            var redirectedUrl = new ReadOnlyApplicationUrl(true, "~/join");

            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/NewNetworkerUserProfileForm.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~/Join.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~/ui/NewNetworkerUserProfileForm.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl(true, "~/join/default.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);
            
            url = new ReadOnlyApplicationUrl("~/ui/unregistered/newnetworkerjoinform.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~//ui/unregistered/NetworkerJoinForm.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/QuickJoin.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/JoinForm.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl); 
        }
    }
}
