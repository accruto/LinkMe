using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile
{
    [TestClass]
    public class ProfileRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestProfileRedirect()
        {
            var profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            var redirectedUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", profileUrl.PathAndQuery));

            var url = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/NetworkerMyResumeForm.aspx");
            AssertRedirect(url, profileUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/AboutMe.aspx");
            AssertRedirect(url, profileUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/employmentdetails.aspx");
            AssertRedirect(url, profileUrl, redirectedUrl);
        }
    }
}
