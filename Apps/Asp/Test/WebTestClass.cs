using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Utility.Test;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Asp.Test
{
    [TestClass]
    public abstract class WebTestClass
    {
        private HttpClient _browser;
        protected const string JsonContentType = "application/json";

        [TestInitialize]
        public void AspWebTestClassInitialize()
        {
            _browser = new HttpClient();
        }

        protected HttpClient Browser
        {
            get
            {
                CheckSetUp();
                return _browser;
            }
        }

        private const string PageFilePath = "_LinkMe_Last_Failed_Page.html";

        #region GetPage

        protected string Get(HttpStatusCode expectedStatusCode, ReadOnlyUrl url)
        {
            return Browser.Get(expectedStatusCode, GetUrlPath(url));
        }

        protected string Get(ReadOnlyUrl url)
        {
            return Browser.Get(null, GetUrlPath(url));
        }

        private string GetUrlPath(ReadOnlyUrl url)
        {
            // If the asked for page is relative and the current url is set then just use the path and query.

            if (Browser.CurrentUrl != null)
            {
                var applicationUrl = url as ApplicationUrl;
                if (applicationUrl != null)
                {
                    if (!applicationUrl.IsAbsolute && applicationUrl.IsSecure == null)
                        return url.PathAndQuery;
                }

                var readonlyApplicationUrl = url as ReadOnlyApplicationUrl;
                if (readonlyApplicationUrl != null)
                {
                    if (!readonlyApplicationUrl.IsAbsolute && readonlyApplicationUrl.IsSecure == null)
                        return url.PathAndQuery;
                }
            }

            return url.ToString();
        }

        #endregion

        #region Post

        public string Post(ReadOnlyUrl url, NameValueCollection formVariables, NameValueCollection fileVariables)
        {
            return Browser.Post(null, GetUrlPath(url), formVariables, fileVariables);
        }

        public string Post(HttpStatusCode expectedStatusCode, ReadOnlyUrl url, NameValueCollection formVariables)
        {
            return Browser.Post(expectedStatusCode, GetUrlPath(url), formVariables, null);
        }

        public string Post(ReadOnlyUrl url, NameValueCollection formVariables)
        {
            return Browser.Post(null, GetUrlPath(url), formVariables, null);
        }

        public string Post(HttpStatusCode expectedStatusCode, ReadOnlyUrl url)
        {
            return Browser.Post(expectedStatusCode, GetUrlPath(url));
        }

        public string Post(ReadOnlyUrl url)
        {
            return Browser.Post(null, GetUrlPath(url));
        }

        public string Post(HttpStatusCode expectedStatusCode, ReadOnlyUrl url, string contentType, string content)
        {
            return Browser.Post(expectedStatusCode, GetUrlPath(url), contentType, content);
        }

        public string Post(ReadOnlyUrl url, string contentType, string content)
        {
            return Browser.Post(null, GetUrlPath(url), contentType, content);
        }

        public string Put(HttpStatusCode expectedStatusCode, ReadOnlyUrl url, string contentType, string content)
        {
            return Browser.Put(expectedStatusCode, GetUrlPath(url), contentType, content);
        }

        public string Put(ReadOnlyUrl url, string contentType, string content)
        {
            return Browser.Put(null, GetUrlPath(url), contentType, content);
        }

        public string Put(ReadOnlyUrl url, NameValueCollection formVariables)
        {
            return Browser.Put(null, GetUrlPath(url), formVariables, null);
        }

        public string Put(ReadOnlyUrl url)
        {
            return Browser.Put(null, GetUrlPath(url));
        }

        public string Delete(HttpStatusCode expectedStatusCode, ReadOnlyUrl url)
        {
            return Browser.Delete(expectedStatusCode, GetUrlPath(url));
        }

        public string Delete(ReadOnlyUrl url)
        {
            return Browser.Delete(null, GetUrlPath(url));
        }

        public string Head(ReadOnlyUrl url)
        {
            return Browser.Head(null, GetUrlPath(url));
        }

        #endregion

        protected static string Serialize(JsonRequestModel model, params JavaScriptConverter[] converters)
        {
            return CreateSerializer(converters).Serialize(model);
        }

        protected static TResponseModel Deserialize<TResponseModel>(string value, params JavaScriptConverter[] converters)
        {
            return CreateSerializer(converters).Deserialize<TResponseModel>(value);
        }

        private static JavaScriptSerializer CreateSerializer(ICollection<JavaScriptConverter> converters)
        {
            var serializer = new JavaScriptSerializer();
            if (converters != null && converters.Count > 0)
                serializer.RegisterConverters(converters);
            return serializer;
        }

        protected static void AssertJsonSuccess(JsonResponseModel model)
        {
            Assert.IsTrue(model.Success);
            Assert.IsTrue(model.Errors == null || model.Errors.Count == 0);
        }

        protected static void AssertJsonError(JsonResponseModel model, string key, string code, string message)
        {
            Assert.IsFalse(model.Success);
            Assert.IsNotNull(model.Errors);
            Assert.AreEqual(1, model.Errors.Count);
            Assert.AreEqual(key, model.Errors[0].Key);
            Assert.AreEqual(code, model.Errors[0].Code);
            Assert.AreEqual(message, model.Errors[0].Message);
        }

        protected static void AssertJsonError(JsonResponseModel model, string key, string message)
        {
            AssertJsonError(model, key, "300", message);
        }

        protected void AssertJsonErrors(JsonResponseModel model, params string[] errors)
        {
            Assert.IsFalse(model.Success);
            Assert.IsNotNull(model.Errors);
            Assert.AreEqual(errors.Length / 2, model.Errors.Count);
            for (var index = 0; index < errors.Length / 2; ++index)
            {
                Assert.AreEqual(errors[2 * index], model.Errors[index].Key);
                Assert.AreEqual(errors[2 * index + 1], model.Errors[index].Message);
            }
        }

        #region AssertPageContains

        protected void AssertPageContains(string text)
        {
            AssertPageContainment(text, true);
        }

        protected void AssertPageContains(string text, bool ignoreCase)
        {
            AssertPageContainment(text, true, ignoreCase);
        }

        protected void AssertPageDoesNotContain(string text)
        {
            AssertPageContainment(text, false);
        }

        protected void AssertPageContainment(string text, bool expected)
        {
            AssertPageContainment(text, expected, false);
        }

        private void AssertPageContainment(string text, bool expected, bool ignoreCase)
        {
            var pageText = Browser.CurrentPageText;
            var contained = pageText.IndexOf(text, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) > -1;
            if (contained != expected)
                Assert.Fail(string.Format("'{0}' was {1} in the page. {2}", text, expected ? "expected" : "not expected", SavePageToFile(pageText)));
        }

        #endregion

        #region File

        protected string SaveCurrentPageToFile()
        {
            return SavePageToFile(Browser.CurrentPageText);
        }

        private static string SavePageToFile(string text)
        {
            var file = Path.GetTempFileName() + PageFilePath;
            TestUtils.SaveToFile(file, text);
            return "The page has been saved to a file: '" + file + "'.";
        }

        #endregion

        private void CheckSetUp()
        {
            if (_browser == null)
                throw new InvalidOperationException("A required setup method in WebFormTestCase was not called.  This is probably because you used the [SetUp] attribute in a subclass of WebFormTestCase.  That is not supported.  Override the TestInitialize() method instead.");
        }
    }
}
