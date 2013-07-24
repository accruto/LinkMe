using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class PrivacyDetailTests
        : SupportTests
    {
        private const string PageTitle = "Privacy statement";

        [TestMethod]
        public void TestPrivacy()
        {
            var url = new ReadOnlyApplicationUrl("~/privacydetail");
            AssertUrl(url, url, PageTitle);
        }

        protected override string PageTitleCssClass
        {
            get { return "page-title terms-detail"; }
        }
    }
}
