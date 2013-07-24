using System.Net;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    public abstract class RedirectTests
        : WebTestClass
    {
        protected void AssertRedirect(ReadOnlyUrl url, ReadOnlyUrl redirectUrl, ReadOnlyUrl finalRedirectUrl)
        {
            // Look for MovedPermanently directly.

            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.AllowAutoRedirect = false;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Assert.AreEqual(HttpStatusCode.MovedPermanently, response.StatusCode);

                var expected = redirectUrl.AbsoluteUri;
                var actual = response.Headers["Location"];
                Assert.AreEqual(expected.ToLower(), actual.ToLower());
            }

            // Do a get.

            if (finalRedirectUrl != null)
            {
                Get(url);
                AssertUrl(finalRedirectUrl);
            }
        }

        protected void AssertNoRedirect(ReadOnlyUrl url)
        {
            // Look for MovedPermanently directly.

            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.AllowAutoRedirect = false;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                Assert.AreNotEqual(HttpStatusCode.MovedPermanently, response.StatusCode);
            }

            // Do a get.

            Get(url);
            AssertUrl(url);
        }
    }
}