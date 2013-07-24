using System;
using System.IO;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Errors
{
    [TestClass]
    public class ErrorsTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        private const string AcceptJson = "application/json, text/javascript, */*; q=0.01";

        [TestMethod]
        public void TestNotFound()
        {
            var id = Guid.NewGuid();
            var url = new ReadOnlyApplicationUrl(true, "~/v1/employers/candidates/" + id);
            var response = Deserialize<JsonResponseModel>(TestNotFound(url, "GET", AcceptJson));

            Assert.IsFalse(response.Success);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual("400", response.Errors[0].Code);
            Assert.AreEqual(null, response.Errors[0].Key);
            Assert.AreEqual("The candidate cannot be found.", response.Errors[0].Message);
        }

        [TestMethod]
        public void TestUrlNotFound()
        {
            var url = new ReadOnlyApplicationUrl("~/v1/badurl");
            var response = Deserialize<JsonResponseModel>(TestUrlNotFound(url, AcceptJson));

            Assert.IsFalse(response.Success);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual("400", response.Errors[0].Code);
            Assert.AreEqual(null, response.Errors[0].Key);
            Assert.AreEqual("The url '" + url.AbsoluteUri + "' cannot be found.", response.Errors[0].Message);
        }

        [TestMethod]
        public void TestServerError()
        {
            var url = new ReadOnlyApplicationUrl("~/v1/dev/error");
            var response = Deserialize<JsonResponseModel>(TestServerError(url, AcceptJson));

            Assert.IsFalse(response.Success);
            Assert.AreEqual(1, response.Errors.Count);
            Assert.AreEqual("500", response.Errors[0].Code);
            Assert.AreEqual(null, response.Errors[0].Key);
            Assert.AreEqual("An error has occurred in the LinkMe website. We apologise for the inconvenience.", response.Errors[0].Message);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private string TestNotFound(ReadOnlyUrl url, string method, string accept)
        {
            LogIn(CreateEmployer());
            var cookies = new CookieContainer();
            cookies.Add(Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri)));

            return TestNotFound(url, method, accept, cookies);
        }

        private static string TestNotFound(ReadOnlyUrl url, string method, string accept, CookieContainer cookies)
        {
            return GetResponse(Test(url, method, accept, cookies, HttpStatusCode.NotFound));
        }

        private static string TestUrlNotFound(ReadOnlyUrl url, string accept)
        {
            return GetResponse(Test(url, "GET", accept, null, HttpStatusCode.NotFound));
        }

        private static string TestServerError(ReadOnlyUrl url, string accept)
        {
            return GetResponse(Test(url, "GET", accept, null, HttpStatusCode.InternalServerError));
        }

        private static string GetResponse(WebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        private static HttpWebResponse Test(ReadOnlyUrl url, string method, string accept, CookieContainer cookies, HttpStatusCode expectedStatusCode)
        {
            // Do a simple GET and make sure the page is found.

            var request = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            request.Method = method;
            if (method == "POST")
                request.ContentLength = 0;
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.CookieContainer = cookies ?? new CookieContainer();
            request.Accept = accept;

            // Get the response and check.

            try 
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return AssertResponse(url, expectedStatusCode, GetExpectedContentType(accept), response);
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                    throw new ApplicationException("Failed to get HTTP response from " + request.Address.AbsoluteUri, e);

                return AssertResponse(url, expectedStatusCode, GetExpectedContentType(accept), (HttpWebResponse)e.Response);
            }
        }

        private static string GetExpectedContentType(string accept)
        {
            return accept == AcceptJson
                ? "application/json; charset=utf-8"
                : "text/html; charset=utf-8";
        }

        private static HttpWebResponse AssertResponse(ReadOnlyUrl url, HttpStatusCode expectedStatusCode, string expectedContentType, HttpWebResponse response)
        {
            string message = null;
            if (response.StatusCode != expectedStatusCode)
            {
                message = "The page at\r\n'" + url + "'" + " was expected to return a status code of " + expectedStatusCode + " but instead returned " + response.StatusCode;
                if (response.StatusCode == HttpStatusCode.Found)
                    message += ". Redirected to '" + response.Headers["Location"] + "'.";
            }
            else if (response.ContentType != expectedContentType)
            {
                message = "The page at\r\n'" + url + "'" + " was expected to return a content type of '" + expectedContentType + "' but instead returned '" + response.ContentType + "'";
            }

            if (message == null)
                return response;

            Assert.Fail(message);
            return null;
        }
    }
}