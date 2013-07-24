using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class PrivacyTests
        : SupportTests
    {
        private const string PageTitle = "Privacy statement";

        [TestMethod]
        public void TestPrivacy()
        {
            var url = new ReadOnlyApplicationUrl("~/privacy");
            AssertUrl(url, url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/PrivacyStatement.aspx"), url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/ui/unregistered/common/PrivacyStatement.aspx"), url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/ui/unregistered/common/privacystatementform.aspx"), url, PageTitle);
        }
    }
}
