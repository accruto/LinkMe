using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.service
{
    [TestClass, Ignore]
    public class GetFileTests
        : WebTestClass
    {
        [TestMethod]
        public void TestGetFile()
        {
            TestGetFile("2506F820-DECC-4DDE-BA12-001191E11E46");
            TestGetFile("D053DCEE-772F-49DC-83FE-3FCE99D25BDE");
        }

        private static void TestGetFile(string id)
        {
            TestGetFile(HttpStatusCode.Found, "http://www.linkme.com.au/file/" + id + "/logo.jpg");
            TestGetFile(HttpStatusCode.Found, "https://www.linkme.com.au/file/" + id + "/logo.jpg");
            TestGetFile(HttpStatusCode.MovedPermanently, "http://linkme.com.au/file/" + id + "/logo.jpg", "http://www.linkme.com.au/file/" + id + "/default.aspx");
            TestGetFile(HttpStatusCode.Found, "https://linkme.com.au/file/" + id + "/logo.jpg");

            TestGetFile(HttpStatusCode.OK, "http://www.linkme.com.au/file/" + id + "/default.aspx");
            TestGetFile(HttpStatusCode.OK, "https://www.linkme.com.au/file/" + id + "/default.aspx");
            TestGetFile(HttpStatusCode.MovedPermanently, "http://linkme.com.au/file/" + id + "/default.aspx", "http://www.linkme.com.au/file/" + id + "/default.aspx");
            TestGetFile(HttpStatusCode.OK, "https://linkme.com.au/file/" + id + "/default.aspx");

            TestGetFile(HttpStatusCode.Found, "http://newapp1.linkme.net.au/file/" + id + "/logo.jpg");
            TestGetFile(HttpStatusCode.Found, "https://newapp1.linkme.net.au/file/" + id + "/logo.jpg");

            TestGetFile(HttpStatusCode.OK, "http://newapp1.linkme.net.au/file/" + id + "/default.aspx");
            TestGetFile(HttpStatusCode.OK, "https://newapp1.linkme.net.au/file/" + id + "/default.aspx");
        }

        private static void TestGetFile(HttpStatusCode statusCode, string url)
        {
            TestGetFile(statusCode, url, null);
        }

        private static void TestGetFile(HttpStatusCode statusCode, string url, string location)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.Timeout = 10000;
            request.AllowAutoRedirect = false;

            using (var response = (HttpWebResponse) request.GetResponse())
            {
                Assert.AreEqual(statusCode, response.StatusCode);

                if (location != null)
                {
                    Assert.AreEqual(location, response.Headers["Location"]);
                    TestGetFile(HttpStatusCode.OK, location);
                }
            }
        }
    }
}
