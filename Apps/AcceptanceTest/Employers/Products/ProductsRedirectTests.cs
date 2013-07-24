using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Products
{
    [TestClass]
    public class ProductsRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestPricingPlan()
        {
            var url = new ReadOnlyApplicationUrl("~/employers/PricingPlan.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/employers/products/neworder");
            var finalRedirectUrl = new ReadOnlyApplicationUrl("~/employers/products/choose");
            AssertRedirect(url, redirectUrl, null);

            Get(url);
            AssertUrlWithoutQuery(finalRedirectUrl);
        }
    }
}
