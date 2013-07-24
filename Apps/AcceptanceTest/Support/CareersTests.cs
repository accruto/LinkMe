using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class AboutUsTests
        : SupportTests
    {
        private const string PageTitle = "About LinkMe";

        [TestMethod]
        public void TestAboutUs()
        {
            var url = new ReadOnlyApplicationUrl("~/aboutus");
            AssertUrl(url, url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/AboutUs.aspx"), url, PageTitle);
        }
    }
}
