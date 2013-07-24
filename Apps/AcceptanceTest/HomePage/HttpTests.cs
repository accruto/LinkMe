using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class HttpTests
        : WebTestClass
    {
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

        [TestMethod]
        public void TestHead()
        {
            Head(HomeUrl);
            AssertUrl(HomeUrl);
            Assert.AreEqual("", Browser.CurrentPageText);
        }
    }
}
