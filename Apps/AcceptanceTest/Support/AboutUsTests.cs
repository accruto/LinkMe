using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Support
{
    [TestClass]
    public class CareersTests
        : SupportTests
    {
        private const string PageTitle = "Careers at LinkMe";

        [TestMethod]
        public void TestCareers()
        {
            var url = new ReadOnlyApplicationUrl("~/careers");
            AssertUrl(url, url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/CareersAtLinkMe.aspx"), url, PageTitle);
            AssertUrl(new ReadOnlyApplicationUrl("~/ui/unregistered/common/CareersAtLinkMe.aspx"), url, PageTitle);
        }
    }
}
