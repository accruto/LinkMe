using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class MobileTests
        : WebTestClass
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Browser.UseMobileUserAgent = true;
        }

        [TestMethod]
        public void TestHome()
        {
            Get(HomeUrl);
            AssertUrl(HomeUrl);

            // Mobile specific HTML.

            AssertPageContains("<div class=\"mylocation\"></div>");
        }

        [TestMethod]
        public void TestMemberHome()
        {
            Assert.AreEqual("http", HomeUrl.Scheme);
            Get(HomeUrl);
            AssertUrl(HomeUrl);

            var url = HomeUrl.AsNonReadOnly();
            url.Scheme = "https";

            Assert.AreEqual("https", url.Scheme);
            Get(url);
            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestEmployerHome()
        {
            Assert.AreEqual("https", EmployerHomeUrl.Scheme);
            Get(EmployerHomeUrl);
            AssertUrl(EmployerHomeUrl);

            var url = EmployerHomeUrl.AsNonReadOnly();
            url.Scheme = "http";

            Assert.AreEqual("http", url.Scheme);
            Get(url);
            AssertUrl(EmployerHomeUrl);
        }
    }
}
