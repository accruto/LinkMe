using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test.Urls
{
    [TestClass]
    public class UrlTest
    {
        #region Simple ctor tests

        [TestMethod]
        [ExpectedException(typeof(System.UriFormatException))]
        public void TestCtorEmptyString()
        {
            new Url(string.Empty);
        }

        [TestMethod]
        public void TestCtorHost()
        {
            var url = new Url("www.test.com");
            Assert.AreEqual("http://www.test.com/", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual(80, url.Port);
            Assert.AreEqual("/", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHttpHost()
        {
            var url = new Url("http://www.test.com");
            Assert.AreEqual("http://www.test.com/", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual(80, url.Port);
            Assert.AreEqual("/", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHttpHostDefaultPort()
        {
            var url = new Url("http://www.test.com:80");
            Assert.AreEqual("http://www.test.com/", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual(80, url.Port);
            Assert.AreEqual("/", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHttpHostNonDefaultPort()
        {
            var url = new Url("http://www.test.com:27");
            Assert.AreEqual("http://www.test.com:27/", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual(27, url.Port);
            Assert.AreEqual("/", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHttpsHost()
        {
            var url = new Url("https://www.test.com");
            Assert.AreEqual("https://www.test.com/", url.ToString());
            Assert.AreEqual("https", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual(443, url.Port);
            Assert.AreEqual("/", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHttpHostPath()
        {
            var url = new Url("http://www.test.com/page.aspx");
            Assert.AreEqual("http://www.test.com/page.aspx", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorHostPath()
        {
            var url = new Url("www.test.com/AppPath");
            Assert.AreEqual("http://www.test.com/AppPath", url.ToString());
            Assert.AreEqual("http", url.Scheme);
            Assert.AreEqual("www.test.com", url.Host);
            Assert.AreEqual("/AppPath", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtorFromUrl()
        {
            var url1 = new Url("http://www.test.com/page.aspx?key=value1");
            Url url2 = url1.Clone(); //new Url(url1);
            Assert.AreEqual("http://www.test.com/page.aspx?key=value1", url2.ToString());

            url2.QueryString["key"] = "value2";
            Assert.AreEqual("http://www.test.com/page.aspx?key=value1", url1.ToString());
            Assert.AreEqual("http://www.test.com/page.aspx?key=value2", url2.ToString());
        }

        #endregion

        #region QueryString ctor tests

        [TestMethod]
        public void TestCtorEmprtyQueryString()
        {
            var url = new Url("http://www.test.com/page.aspx?");
            Assert.AreEqual("http://www.test.com/page.aspx", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(0, url.QueryString.Count);
        }

        [TestMethod]
        public void TestCtor_OneParameter()
        {
            var url = new Url("http://www.test.com/page.aspx?key=value");
            Assert.AreEqual("http://www.test.com/page.aspx?key=value", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(1, url.QueryString.Count);
            Assert.AreEqual("value", url.QueryString["key"]);
        }

        [TestMethod]
        public void TestCtorTwoParameters()
        {
            var url = new Url("http://www.test.com/page.aspx?key1=value1&key2=value2");
            Assert.AreEqual("http://www.test.com/page.aspx?key1=value1&key2=value2", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(2, url.QueryString.Count);
            Assert.AreEqual("value1", url.QueryString["key1"]);
            Assert.AreEqual("value2", url.QueryString["key2"]);
        }

        [TestMethod]
        public void TestCtor_OneEmptyParameter()
        {
            var url = new Url("http://www.test.com/page.aspx?key=");
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(1, url.QueryString.Count);
            Assert.AreEqual(string.Empty, url.QueryString["key"]);
        }

        [TestMethod]
        public void TestCtorTwoParameterWithOneEmpty()
        {
            var url = new Url("http://www.test.com/page.aspx?key1=&key2=value2");
            Assert.AreEqual("http://www.test.com/page.aspx?key1=&key2=value2", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(2, url.QueryString.Count);
            Assert.AreEqual(string.Empty, url.QueryString["key1"]);
            Assert.AreEqual("value2", url.QueryString["key2"]);
        }

        [TestMethod]
        public void TestCtor_OneParameterWithSpecChar()
        {
            var url = new Url("http://www.test.com/page.aspx?key=prefix%3asuffix");
            Assert.AreEqual("http://www.test.com/page.aspx?key=prefix%3asuffix", url.ToString());
            Assert.AreEqual("/page.aspx", url.Path);
            Assert.AreEqual(1, url.QueryString.Count);
            Assert.AreEqual("prefix:suffix", url.QueryString["key"]);
        }

        [TestMethod]
        public void TestCtorEmptyQueryParams()
        {
            var url = new Url("http://www.test.com/page.aspx?key=");
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
            Assert.AreEqual(1, url.QueryString.Count);
            Assert.AreEqual("", url.QueryString["key"]);
            Assert.IsNull(url.QueryString["nonexistent"]);

            url = new Url("http://www.test.com/page.aspx?key&");
            Assert.AreEqual("http://www.test.com/page.aspx?key&", url.ToString());
            Assert.AreEqual(1, url.QueryString.Count);
            Assert.AreEqual(null, url.QueryString["key"]);
            Assert.AreEqual("key,", url.QueryString[null]); // A bit weird, but this is what System.Uri does!
        }

        #endregion

        #region Add to QueryString tests

        [TestMethod]
        public void TestQueryStringAddOneParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key", "value");
            Assert.AreEqual("http://www.test.com/page.aspx?key=value", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringAddOneParameterWithSpecChar()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key", "prefix:suffix");
            Assert.AreEqual("http://www.test.com/page.aspx?key=prefix%3asuffix", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringAddTwoParameters()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key1", "value1");
            url.QueryString.Add("key2", "value2");
            Assert.AreEqual("http://www.test.com/page.aspx?key1=value1&key2=value2", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringAddNullParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key", null);
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringAddEmptyParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key", string.Empty);
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringAddTwoParametersWithSameKey()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString.Add("key", "value1");
            url.QueryString.Add("key", "value2");
            Assert.AreEqual("http://www.test.com/page.aspx?key=value1&key=value2", url.ToString());
        }

        #endregion

        #region Set QueryString tests

        [TestMethod]
        public void TestQueryStringSetOneParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key"] = "value";
            Assert.AreEqual("http://www.test.com/page.aspx?key=value", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringSetOneParameterWithSpecChar()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key"] = "prefix:suffix";
            Assert.AreEqual("http://www.test.com/page.aspx?key=prefix%3asuffix", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringSetTwoParameters()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key1"] = "value1";
            url.QueryString["key2"] = "value2";
            Assert.AreEqual("http://www.test.com/page.aspx?key1=value1&key2=value2", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringSetNullParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key"] = null;
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringSetEmptyParameter()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key"] = string.Empty;
            Assert.AreEqual("http://www.test.com/page.aspx?key=", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringSetTwoParametersWithSameKey()
        {
            var url = new Url("http://www.test.com/page.aspx");
            url.QueryString["key"] = "value1";
            url.QueryString["key"] = "value2";
            Assert.AreEqual("http://www.test.com/page.aspx?key=value2", url.ToString());
        }

        [TestMethod]
        public void TestQueryStringReplaceParameter()
        {
            var url = new Url("http://www.test.com/page.aspx?key=value1");
            url.QueryString["key"] = "value2";
            Assert.AreEqual("http://www.test.com/page.aspx?key=value2", url.ToString());
        }

        #endregion

        #region Other tests

        [TestMethod]
        public void TestQueryStringRemoveParameter()
        {
            var url = new Url("http://www.test.com/page.aspx?key1=value1&key2=value2");
            url.QueryString.Remove("key1");
            Assert.AreEqual("http://www.test.com/page.aspx?key2=value2", url.ToString());
        }

        [TestMethod]
        public void TestDefaultPortSetHttps()
        {
            var url = new Url("http://www.test.com") {Scheme = "https"};
            Assert.AreEqual("https://www.test.com/", url.ToString());
            Assert.AreEqual(443, url.Port);
        }

        [TestMethod]
        public void TestDefaultPortSetHttp()
        {
            var url = new Url("https://www.test.com") {Scheme = "http"};
            Assert.AreEqual("http://www.test.com/", url.ToString());
            Assert.AreEqual(80, url.Port);
        }

        #endregion
    }
}
