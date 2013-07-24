using System.Net;
using System.Text;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.EmailStatus
{
    [TestClass]
    public class WebTests
        : WebTestClass
    {
        private ReadOnlyUrl _baseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _baseUrl = new ReadOnlyApplicationUrl("~/EmailStatus/EmailStatusService.svc/");
        }

        [TestMethod]
        public void HealthTest()
        {
            var request = (HttpWebRequest) WebRequest.Create(_baseUrl.ToString());
            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentType = "text/plain";
            var requestBytes = Encoding.UTF8.GetBytes("Content-Type: test");

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestBytes, 0, requestBytes.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }
    }
}