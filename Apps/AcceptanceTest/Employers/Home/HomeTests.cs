using LinkMe.Apps.Asp.Test.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Home
{
    [TestClass]
    public class HomeTests
        : WebTestClass
    {
        private const string HomeContent = "Start posting your own job ads";
        private const string PhoneHomeContent = "Continue to LinkMe.com.au";

        private HtmlLinkTester _continueLink;

        [TestInitialize]
        public void TestInitialize()
        {
            _continueLink = new HtmlLinkTester(Browser, "Continue");
        }

        [TestMethod]
        public void TestHome()
        {
            Browser.UseMobileUserAgent = false;
            Get(EmployerHomeUrl);

            AssertUrl(EmployerHomeUrl);
            AssertPageContains(HomeContent);
            AssertPageDoesNotContain(PhoneHomeContent);
        }

        [TestMethod]
        public void TestIPhoneHome()
        {
            Browser.UseMobileUserAgent = true;
            Get(EmployerHomeUrl);

            AssertUrl(EmployerHomeUrl);
            AssertPageDoesNotContain(HomeContent);
            AssertPageContains(PhoneHomeContent);

            // Click the link to get to the home page.

            _continueLink.Click();
            AssertUrlWithoutQuery(EmployerHomeUrl);
            AssertPageContains(HomeContent);
            AssertPageDoesNotContain(PhoneHomeContent);

            // Going back to the home page should not result in the iPhone prompt for the rest of the session.

            Get(EmployerHomeUrl);
            AssertUrl(EmployerHomeUrl);
            AssertPageContains(HomeContent);
            AssertPageDoesNotContain(PhoneHomeContent);
        }
    }
}
