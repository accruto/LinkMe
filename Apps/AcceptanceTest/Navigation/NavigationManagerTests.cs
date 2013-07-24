using System.Net;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    [TestClass]
    public class NavigationManagerTests
        : WebTestClass
    {
        [TestMethod]
        public void TestExpires()
        {
            // This page should not expire.

            var url = new ReadOnlyApplicationUrl("~/guests/Friends.aspx");
            AssertExpires(url, false);

            // Get the RequestNewPassword page which should.

            url = new ReadOnlyApplicationUrl("~/accounts/newpassword");
            AssertExpires(url, true);
        }

        private static void AssertExpires(ReadOnlyUrl url, bool expires)
        {
            var request = (HttpWebRequest)WebRequest.Create(url.ToString());
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                if (expires)
                {
                    Assert.AreEqual("no-cache, no-store, must-revalidate", response.Headers["Cache-Control"]);
                    Assert.IsTrue(response.Headers["Pragma"].Contains("no-cache"));
                    Assert.AreEqual("-1", response.Headers["Expires"]);
                }
                else
                {
                    Assert.AreNotEqual("no-cache", response.Headers["Cache-Control"]);
                    Assert.IsNull(response.Headers["Pragma"]);
                    Assert.IsNull(response.Headers["Expires"]);
                }
            }
        }
    }
}