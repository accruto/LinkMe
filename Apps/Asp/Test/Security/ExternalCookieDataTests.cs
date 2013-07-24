using LinkMe.Apps.Asp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test.Security
{
    [TestClass]
    public class ExternalCookieDataTests
    {
        [TestMethod]
        public void TestParse()
        {
            const string cookieValue = "test%7Ctest%40.linkme.com.au%7Ctest%2Btest";
            var data = ExternalCookieData.ParseCookieValue(cookieValue);
            Assert.AreEqual("test", data.ExternalId);
            Assert.AreEqual("test@.linkme.com.au", data.EmailAddress);
            Assert.AreEqual("", data.FirstName);
            Assert.AreEqual("test+test", data.LastName);
        }
    }
}
