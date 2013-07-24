using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Framework.Utility.Xml;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using LinkMe.Web.Applications.Ajax;
using LinkMe.Web.Areas.Accounts.Models;
using LinkMe.Web.Manager.Errors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest
{
    [TestClass]
	public abstract class WebTestClass
        : Apps.Asp.Test.WebTestClass
	{
	    private ReadOnlyUrl _homeUrl;
        private ReadOnlyUrl _employerHomeUrl;
        private ReadOnlyUrl _loggedInMemberHomeUrl;
        private ReadOnlyUrl _loggedInMobileMemberHomeUrl;
        private ReadOnlyUrl _loggedInEmployerHomeUrl;
        private ReadOnlyUrl _loggedInAdministratorHomeUrl;
        private ReadOnlyUrl _loggedInCustodianHomeUrl;

        private ReadOnlyUrl _clearCacheUrl;
        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _employerLoginUrl;
        private ReadOnlyUrl _logOutUrl;
        private ReadOnlyUrl _apiLoginUrl;
        private ReadOnlyUrl _apiAnonymousIdUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlCheckBoxTester _rememberMeCheckBox;
        private string _loginFormId;

        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }

        protected static T Resolve<T>(string name)
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>(name);
        }

        protected ReadOnlyUrl HomeUrl
        {
            get { return _homeUrl ?? (_homeUrl = new ReadOnlyApplicationUrl("~/")); }
        }

        protected ReadOnlyUrl EmployerHomeUrl
        {
            get { return _employerHomeUrl ?? (_employerHomeUrl = new ReadOnlyApplicationUrl(true, "~/employers")); }
        }

	    protected ReadOnlyUrl LoggedInMemberHomeUrl
	    {
            get { return _loggedInMemberHomeUrl ?? (_loggedInMemberHomeUrl = new ReadOnlyApplicationUrl(true, "~/members/profile")); }
	    }

        protected ReadOnlyUrl LoggedInMobileMemberHomeUrl
        {
            get { return _loggedInMobileMemberHomeUrl ?? (_loggedInMobileMemberHomeUrl = new ReadOnlyApplicationUrl(true, "~/members/home")); }
        }

        protected ReadOnlyUrl LoggedInEmployerHomeUrl
	    {
	        get { return _loggedInEmployerHomeUrl ?? (_loggedInEmployerHomeUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates")); }
	    }

	    protected ReadOnlyUrl LoggedInAdministratorHomeUrl
	    {
	        get { return _loggedInAdministratorHomeUrl ?? (_loggedInAdministratorHomeUrl = new ReadOnlyApplicationUrl(true, "~/administrators/home")); }
	    }

	    protected ReadOnlyUrl LoggedInCustodianHomeUrl
	    {
	        get { return _loggedInCustodianHomeUrl ?? (_loggedInCustodianHomeUrl = new ReadOnlyApplicationUrl(true, "~/custodians/home")); }
	    }

        protected ReadOnlyUrl GetLoginUrl(ReadOnlyUrl returnUrl)
        {
            return GetLoginUrl(LogInUrl, returnUrl);
        }

        protected ReadOnlyUrl LogInUrl
        {
            get { return _loginUrl ?? (_loginUrl = new ReadOnlyApplicationUrl(true, "~/login")); }
        }

	    private ReadOnlyUrl ApiLoginUrl
	    {
            get { return _apiLoginUrl ?? (_apiLoginUrl = new ReadOnlyApplicationUrl(true, "~/accounts/api/login")); }
	    }

        protected ReadOnlyUrl GetEmployerLoginUrl(ReadOnlyUrl returnUrl)
        {
            return GetLoginUrl(EmployerLogInUrl, returnUrl);
        }

        protected ReadOnlyUrl EmployerLogInUrl
        {
            get { return _employerLoginUrl ?? (_employerLoginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login")); }
        }

        protected ReadOnlyUrl LogOutUrl
        {
            get { return _logOutUrl ?? (_logOutUrl = new ReadOnlyApplicationUrl("~/logout")); }
        }

        private static ReadOnlyUrl GetLoginUrl(ReadOnlyUrl loginUrl, ReadOnlyUrl returnUrl)
        {
            var url = loginUrl.AsNonReadOnly();
            url.QueryString["returnUrl"] = returnUrl.PathAndQuery;
            return url;
        }

        private ReadOnlyUrl ClearCacheUrl
	    {
	        get { return _clearCacheUrl ?? (_clearCacheUrl = new ReadOnlyApplicationUrl("~/api/cache/clear")); }
	    }

        private ReadOnlyUrl AnonymousIdUrl
        {
            get { return _apiAnonymousIdUrl ?? (_apiAnonymousIdUrl = new ReadOnlyApplicationUrl("~/api/dev/anonymousid")); }
        }

        protected ReadOnlyUrl GetEmailUrl(string definition, ReadOnlyUrl url)
        {
            var expectedUrl = url.AsNonReadOnly();
            expectedUrl.QueryString["utm_source"] = "linkme";
            expectedUrl.QueryString["utm_medium"] = "email";
            expectedUrl.QueryString["utm_campaign"] = definition.ToLower();
            return expectedUrl;
        }

        protected void GetPage<T>()
        {
            Get(NavigationManager.GetUrlForPage<T>());
        }

        protected void GetPage<T>(params string[] queryString)
        {
            Get(NavigationManager.GetUrlForPage<T>(queryString));
        }

        #region Login

        protected void LogIn(IUser user)
        {
            LogIn(user.GetLoginId(), user.GetPassword(), false);
        }

        protected void LogIn(IUser user, string password)
        {
            LogIn(user.GetLoginId(), password, false);
        }

        protected void LogIn(IUser user, bool rememberMe)
        {
            LogIn(user.GetLoginId(), user.GetPassword(), rememberMe);
        }

        protected void LogIn(string loginId, string password)
        {
            LogIn(loginId, password, false);
        }

        protected void LogIn(string loginId, string password, bool rememberMe)
        {
            LogOut();
            if (Browser.UseMobileUserAgent)
            {
                ApiLogIn(loginId, password, rememberMe);
                Get(LoggedInMobileMemberHomeUrl);
            }
            else
            {
                SubmitLogIn(loginId, password, rememberMe);
            }
        }

        protected void SubmitLogIn(IUser user)
        {
            SubmitLogIn(user.GetLoginId(), user.GetPassword(), false);
        }

        protected void SubmitLogIn(IUser user, string password)
        {
            SubmitLogIn(user.GetLoginId(), password, false);
        }

        protected void SubmitLogIn(IUser user, bool rememberMe)
        {
            SubmitLogIn(user.GetLoginId(), user.GetPassword(), rememberMe);
        }

        protected void SubmitLogIn(string loginId, string password)
        {
            SubmitLogIn(loginId, password, false);
        }

        protected void SubmitLogIn(string loginId, string password, bool rememberMe)
	    {
            _loginIdTextBox.Text = loginId;
            _passwordTextBox.Text = password;
            if (rememberMe)
                _rememberMeCheckBox.IsChecked = true;

            // Make sure it is the login form submitted and not the join.

            Browser.SetFormVariable(_loginFormId, "login", string.Empty, false);
            Browser.Submit(_loginFormId);
	    }

        protected JsonResponseModel ApiLogIn(IUser user)
        {
            return ApiLogIn(user.GetLoginId(), user.GetPassword(), false);
        }

        protected JsonResponseModel ApiLogIn(string loginId, string password, bool rememberMe)
        {
            var loginModel = new LoginModel { LoginId = loginId, Password = password, RememberMe = rememberMe };
            return Deserialize<JsonResponseModel>(Post(ApiLoginUrl, JsonContentType, Serialize(loginModel)));
        }

        #endregion

        #region Cache

        protected void ClearCache(Administrator administrator)
        {
            LogIn(administrator);
            Post(ClearCacheUrl);
            LogOut();
        }

        #endregion

        #region Page

        protected void AssertPage<T>()
        {
            var page = new HtmlTextBoxTester(Browser, "txtPageIdentifier").Text.ToLower();
            var expectedPage = typeof(T).FullName;

            // Check.

            if (string.Compare(page, expectedPage, true) == 0)
                return;

            // Look for the error page.

            if (string.Compare(page, "ServerError", true) == 0)
                ThrowFromServerError(Browser.CurrentPageText);

            Assert.Fail(string.Format("The current page ID is '{0}', but '{1}' was expected. The full URL is {2} {3}", page, expectedPage, Browser.CurrentUrl, SaveCurrentPageToFile()));
        }

        protected void AssertUrl(ReadOnlyUrl url)
        {
            if (!string.Equals(url.AbsoluteUri, Browser.CurrentUrl.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
                Assert.Fail(string.Format("The current page ID is '{0}', but '{1}' was expected. {2}", Browser.CurrentUrl.AbsoluteUri, url.AbsoluteUri, SaveCurrentPageToFile()));
            
            AssertPageDoesNotContain("Server error");
        }

        protected void AssertUrlWithoutQuery(ReadOnlyUrl url)
        {
            var urlWithNoQuery = url.AsNonReadOnly();
            urlWithNoQuery.QueryString.Clear();

            var currentUrlWithNoQuery = new Url(Browser.CurrentUrl);
            currentUrlWithNoQuery.QueryString.Clear();

            if (!string.Equals(urlWithNoQuery.AbsoluteUri, currentUrlWithNoQuery.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
                Assert.Fail(string.Format("The current page ID is '{0}', but '{1}' was expected. {2}", Browser.CurrentUrl.AbsoluteUri, url.AbsoluteUri, SaveCurrentPageToFile()));
        }

        protected void AssertLink(string xpath, ReadOnlyUrl expectedUrl, string expectedText)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(xpath);
            Assert.IsNotNull(node);
            Assert.IsTrue(string.Equals(expectedUrl.PathAndQuery, node.Attributes["href"].Value, StringComparison.CurrentCultureIgnoreCase));
            Assert.AreEqual(expectedText, node.InnerText);
        }

        private void ThrowFromServerError(string serverErrorText)
        {
            const string stackTraceStart = "<b>Stack Trace:</b>";
            const string compilerErrorStart = "<b> Compiler Error Message: </b>";
            const string start = "<code><pre>";
            const string end = "</pre></code>";

            // Look for a compilation error first.

            var startIndex = serverErrorText.IndexOf(compilerErrorStart);
            if (startIndex != -1)
            {
                startIndex += compilerErrorStart.Length;
            }
            else
            {
                // No compile error, look for a server-side exception.

                startIndex = serverErrorText.IndexOf(stackTraceStart);
                if (startIndex == -1)
                    throw new ApplicationException("Failed to find the stack trace or compiler error in the ServerError page. " + SaveCurrentPageToFile());

                startIndex = serverErrorText.IndexOf(start, startIndex);
                if (startIndex == -1)
                    throw new ApplicationException("Failed to find the exception start in the ServerError page. " + SaveCurrentPageToFile());

                startIndex += start.Length;
            }

            var endIndex = serverErrorText.IndexOf(end, startIndex);
            if (endIndex == -1)
                throw new ApplicationException("Failed to find the exception end in the ServerError page. " + SaveCurrentPageToFile());

            var exception = serverErrorText.Substring(startIndex, endIndex - startIndex);
            throw new ApplicationException("\r\nThe following exception occurred on the server:\r\n\r\n" + exception.Trim('\r', '\n'));
        }
		
        #endregion

        protected Guid GetAnonymousId()
        {
            var anonymousId = Post(AnonymousIdUrl);
            if (string.IsNullOrEmpty(anonymousId))
                throw new ApplicationException("Cannot get the anonymous id.");
            return new Guid(anonymousId.Substring(1, anonymousId.Length - 2));
        }

        protected void AssertErrorMessage(string message)
        {
            AssertErrorMessages(message);
        }

        protected void AssertNoErrorMessage(string message)
        {
            var errorMessages = GetErrorMessages();
            Assert.IsFalse(errorMessages.Contains(message));
        }

        protected void AssertNoErrorMessages()
        {
            Assert.AreEqual(0, GetErrorMessages().Count);
        }

        protected void AssertErrorMessages(params string[] expectedMessages)
        {
            var messages = GetErrorMessages();
            Assert.AreEqual(expectedMessages.Length, messages.Count);
            foreach (var expectedMessage in expectedMessages)
            {
                if (!messages.Contains(expectedMessage))
                    Assert.Fail(string.Format("Expected error message '{0}'. {1}", expectedMessage, SaveCurrentPageToFile()));
            }
        }

	    protected List<string> GetErrorMessages()
	    {
            var messages = new List<string>();

            // At the moment there are 2 places error messages might be dependeing upon which view is used to render them.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='error-message']/ul/li");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (!string.IsNullOrEmpty(node.InnerHtml))
                        messages.Add(node.InnerHtml);
                }
            }

            nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='validation-error-msg']//li");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (!string.IsNullOrEmpty(node.InnerHtml))
                        messages.Add(node.InnerHtml);
                }
            }

            return messages;
	    }

        protected void AssertConfirmationMessage(string expectedMessage)
        {
            AssertConfirmationMessages(expectedMessage);
        }

        protected void AssertConfirmationMessages(params string[] expectedMessages)
        {
            var messages = GetConfirmationMessages();
            Assert.AreEqual(expectedMessages.Length, messages.Count);
            foreach (var expectedMessage in expectedMessages)
            {
                if (!messages.Contains(expectedMessage))
                    Assert.Fail(string.Format("Expected confirmation message '{0}'. {1}", expectedMessage, SaveCurrentPageToFile()));
            }
        }

        protected void AssertNoConfirmationMessages()
        {
            Assert.AreEqual(0, GetConfirmationMessages().Count);
        }

        private List<string> GetConfirmationMessages()
        {
            var messages = new List<string>();

            // At the moment there are 2 places error messages might be dependeing upon which view is used to render them.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='confirm-message']/ul/li");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (!string.IsNullOrEmpty(node.InnerHtml))
                        messages.Add(node.InnerHtml);
                }
            }

            return messages;
        }

        protected void AssertInformationMessage(string expectedMessage)
        {
            AssertInformationMessages(expectedMessage);
        }

        protected void AssertInformationMessages(params string[] expectedMessages)
        {
            var messages = GetInformationMessages();
            Assert.AreEqual(expectedMessages.Length, messages.Count);
            foreach (var expectedMessage in expectedMessages)
            {
                if (!messages.Contains(expectedMessage))
                    Assert.Fail(string.Format("Expected information message '{0}'. {1}", expectedMessage, SaveCurrentPageToFile()));
            }
        }

        protected void AssertNoInformationMessages()
        {
            Assert.AreEqual(0, GetInformationMessages().Count);
        }

        private List<string> GetInformationMessages()
        {
            var messages = new List<string>();

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@id='info-message']/ul/li");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (!string.IsNullOrEmpty(node.InnerHtml))
                        messages.Add(node.InnerHtml);
                }
            }

            return messages;
        }

        protected void AssertNoMessages()
        {
            Assert.AreEqual(0, GetErrorMessages().Count + GetConfirmationMessages().Count + GetInformationMessages().Count);
        }

        private static readonly Regex UnescapeAjaxPro = new Regex(@"\\(\\|\"")", RegexOptions.Compiled);

        #region Nested types

        private class XmlStringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return XmlExtensions.DefaultEncoding; }
            }
        }

        #endregion
        
        private static readonly Encoding DefaultHttpEncoding = Encoding.GetEncoding(28591); // Latin-1

        protected IMockEmailServer _emailServer;

        private const string ReturnEmailAddress = "do_not_reply@test.linkme.net.au";
	    private const string ReturnDisplayName = "LinkMe";
        private const string MemberServicesInboxEmailAddress = "msinbox@test.linkme.net.au";
        private const string MemberServicesInboxDisplayName = "LinkMe";
        private const string ClientServicesInboxEmailAddress = "csinbox@test.linkme.net.au";
        private const string ClientServicesInboxDisplayName = "LinkMe";
        private const string SystemEmailAddress = "system@test.linkme.net.au";
        private const string SystemDisplayName = "LinkMe";
        private const string AllStaffEmailAddress = "allstaff@test.linkme.net.au";
        private const string AllStaffDisplayName = "LinkMe Staff";
        protected readonly static EmailRecipient Return = new EmailRecipient(ReturnEmailAddress, ReturnDisplayName);
        protected readonly static EmailRecipient MemberServicesInbox = new EmailRecipient(MemberServicesInboxEmailAddress, MemberServicesInboxDisplayName);
        protected readonly static EmailRecipient ClientServicesInbox = new EmailRecipient(ClientServicesInboxEmailAddress, ClientServicesInboxDisplayName);
        protected readonly static EmailRecipient System = new EmailRecipient(SystemEmailAddress, SystemDisplayName);
        protected readonly static EmailRecipient AllStaff = new EmailRecipient(AllStaffEmailAddress, AllStaffDisplayName);

        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IMemberSearchService _memberSearchService = Resolve<IMemberSearchService>();
        protected readonly IJobAdSearchService _jobAdSearchService = Resolve<IJobAdSearchService>();

        protected Country Australia
        {
            get { return _locationQuery.GetCountry("Australia"); }
        }

        #region Login

        protected void SubmitSidebarLogInForm(string userId, string password)
        {
            var txtUserId = new HtmlTextBoxTester(Browser, AddBodyPrefix("ucSidebarContainer_ctl01") + "_ucLogin_txtUserId");
            var txtPassword = new HtmlTextBoxTester(Browser, AddBodyPrefix("ucSidebarContainer_ctl01") + "_ucLogin_txtPassword");
            var btnLogin = new HtmlButtonTester(Browser, AddBodyPrefix("ucSidebarContainer_ctl01") + "_ucLogin_btnLogin");

            txtUserId.Text = userId;
            txtPassword.Text = password;

            btnLogin.Click();
        }

        protected void LogOut()
        {
            Get(LogOutUrl);
        }

        protected void AssertNotLoggedIn()
        {
            // Check whether can get to not-logged in home page.

            Get(HomeUrl);

            // Only check the path as could be http or https.

            AssertPath(HomeUrl);
            AssertPageDoesNotContain("Logged in as ");
        }

        #endregion

        #region Contains

        protected static void AssertStringContains(string containing, string contained)
        {
            AssertStringContains(containing, contained, true);
        }

        protected static void AssertStringContains(string containing, string contained, bool shouldContain)
        {
            Assert.IsTrue((containing.IndexOf(contained) != -1) == shouldContain, "The string does not contain '" + contained + "'");
        }

		protected static void AssertContains(string[] strings, string stringToAssert)
		{
			var found = false;
			foreach (var s in strings)
			{
				if (s.ToLower().IndexOf(stringToAssert.ToLower()) > -1)
				{
					found = true;
					break;
				}
			}
			Assert.IsTrue(found);
        }

        #endregion

        #region Cookies

        protected void ClearCookies(ReadOnlyUrl url)
        {
            var cookies = Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri));
            foreach (Cookie cookie in cookies)
                cookie.Value = "";
        }

        #endregion

        #region Errors

        /// <summary>
        /// Throws an exception if the current page contains a server error or assertion failure.
        /// 
        /// You should normally call AssertPage(), which also does these checks. This method is useful only
        /// when ANY page is acceptable as long as there's no error (which is unusual).
        /// </summary>
        protected void CheckForServerError()
        {
            ThrowFromAssertionFailure(Browser.CurrentPageText);

            if (string.Compare(GetPageIdentity(), "ServerError", true) == 0)
            {
                // On the error page - get the error, unless it's expected.
                ThrowFromServerErrorHtml(Browser.CurrentPageText);
            }
        }

        protected void ThrowIfContainsServerError(string responseHtml)
        {
            if (responseHtml != null && responseHtml.Contains("value=\"ServerError\""))
            {
                ThrowFromServerErrorHtml(responseHtml);
            }
        }

	    private void ThrowFromServerErrorHtml(string serverErrorHtml)
		{
			const string stackTraceStart = "<b>Stack Trace:</b>";
            const string compileErrorStart = "<b> Compiler Error Message: </b>";
			const string startSentinel = "<code><pre>";
			const string endSentinel = "</pre></code>";

            // Look for a compilation error first.

            var startIndex = serverErrorHtml.IndexOf(compileErrorStart);
            if (startIndex != -1)
            {
                startIndex += compileErrorStart.Length;
            }
            else
		    {
                // No compile error, look for a server-side exception.

                startIndex = serverErrorHtml.IndexOf(stackTraceStart);
                if (startIndex == -1)
                {
                    throw new ApplicationException("Failed to find the stack trace or compiler error in the" + " ServerError page. " + SaveCurrentPageToFile());
                }

                startIndex = serverErrorHtml.IndexOf(startSentinel, startIndex);
                if (startIndex == -1)
                {
                    throw new ApplicationException("Failed to find the exception start in the ServerError page. " + SaveCurrentPageToFile());
                }

                startIndex += startSentinel.Length;
            }

            var endIndex = serverErrorHtml.IndexOf(endSentinel, startIndex);
            if (endIndex == -1)
            {
                throw new ApplicationException("Failed to find the exception end in the ServerError page. " + SaveCurrentPageToFile());
            }

			var exception = serverErrorHtml.Substring(startIndex, endIndex - startIndex);

			throw new ApplicationException("\r\nThe following exception occurred on the server:\r\n\r\n"
				+ exception.Trim('\r', '\n'));
		}
		
		private static void ThrowFromAssertionFailure(string html)
		{
			var startIndex = html.IndexOf(WriteToResponseListener.SHORT_MESSAGE_START);
			if (startIndex == -1)
				return;

			startIndex += WriteToResponseListener.SHORT_MESSAGE_START.Length;

			var endIndex = html.IndexOf(WriteToResponseListener.SHORT_MESSAGE_END, startIndex);
			if (endIndex == -1)
			{
				throw new ApplicationException("\r\nAn assertion failed on the server, but the message"
					+ " couldn't be extracted from the response HTML.");
			}

			var shortMessage = html.Substring(startIndex, endIndex - startIndex);

			throw new ApplicationException("\r\nAn assertion failed on the server. Short message:\r\n"
				+ shortMessage);
        }

        #endregion

        #region Pages

        protected void AssertSecureUrl(ReadOnlyUrl url, ReadOnlyUrl loginUrl)
        {
            Get(url);
            AssertUrlWithoutQuery(loginUrl);
        }

        protected void AssertPath(ReadOnlyUrl url)
        {
            Assert.AreEqual(url.Path.ToLower(), Browser.CurrentUrl.AbsolutePath.ToLower());
        }

	    protected string GetPageIdentity()
        {
            // Get the page ID.

            try
            {
                var tester = new HtmlTextBoxTester(Browser, "txtPageIdentifier");
                return tester.Text.ToLower();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the page identifier for URL '"
                    + Browser.CurrentUrl.AbsoluteUri + "'.", ex);
            }
        }

        #endregion

        #region Web service helper methods

        protected string PostAjaxProRequest(Type service, string methodName, params object[] parameters)
        {
            return PostAjaxProRequest(Browser, service, methodName, parameters);
        }

        public static string PostAjaxProRequest(HttpClient browser, Type service, string methodName, params object[] parameters)
        {
            if (string.IsNullOrEmpty(methodName))
                throw new ArgumentException("The method name must be specified.", "methodName");

            var serviceUrl = new ApplicationUrl(true, "~/ajaxpro/" + service.FullName + "," + service.Assembly.GetName().Name + ".ashx");
            var requestString = GetAjaxProRequestString(parameters);

            var request = CreatePostRequest(browser, serviceUrl.ToString(), "text/plain; charset=utf-8");
            request.Headers.Add("X-AjaxPro-Method", methodName);
            WriteRequestString(request, requestString);

            return GetResponseAsString(request);
        }

        protected static string GetAjaxResultMessage(string response)
        {
            const string messageStart = "\"Message\":\"";

            var startIndex = response.IndexOf(messageStart);
            if (startIndex == -1)
                return null;

            startIndex += messageStart.Length;

            var endSearchStart = startIndex;
            var endIndex = -1;

            while (endSearchStart < response.Length)
            {
                endIndex = response.IndexOf('\"', endSearchStart);
                if (endIndex == -1)
                    break;

                // Check that it's not an escaped quote in the body of the message.

                var slashCount = 0;
                while (response[endIndex - slashCount - 1] == '\\')
                {
                    slashCount++;
                }

                if (slashCount % 2 == 0)
                    break; // No \ or an even number of them, so it's the \ that's escaped, not the "

                endSearchStart = endIndex + 1;
            }

            if (endIndex == -1)
                throw new ApplicationException("Failed to find the end of the AJAX result message.");

            return UnescapeAjaxProString(response.Substring(startIndex, endIndex - startIndex));
        }

        protected static void AssertAjaxFailure(string actual, string expectedMessage)
        {
            AssertAjaxCodeAndMessage(actual, AjaxResultCode.FAILURE, expectedMessage);
        }

        protected static void AssertAjaxSuccess(string actual, string expectedMessage, string[] expectedElementNames,
            string[] expectedElementValues)
        {
            AssertAjaxCodeAndMessage(actual, AjaxResultCode.SUCCESS, expectedMessage);

            if (expectedElementNames != null && expectedElementValues != null)
            {
                var expectedUserData = BuildAjaxUserData(expectedElementNames, expectedElementValues);
                Assert.IsTrue(actual.IndexOf(expectedUserData) != -1, "The response doesn't contain the expected user data: " + expectedUserData + "\r\nResponse: " + actual);
            }
        }

        protected string CallGetService<T>(params string[] queryString)
        {
            return CallGetService(NavigationManager.GetUrlForPage<T>(queryString));
        }

        protected string CallGetService(ReadOnlyUrl url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            var request = CreateGetRequest(url.ToString());
            return GetResponseAsString(request);
        }

        protected static string CallIntegrationPostService<T>(string username, string password, string requestXml)
        {
            return CallIntegrationPostService(NavigationManager.GetUrlForPage<T>(), username, password, requestXml);
        }

        protected static StringWriter GetXmlStringWriter()
        {
            // This ridiculous hack is needed, because XmlTextWriter uses the Encoding property of the textwriter to write
            // the "encoding" attribute of the XML declaration. A normal System.IO.StringWriter returns Unicode (UTF-16) encoding,
            // which is not what we actually use, so the XmlReader on the other end fails to parse it.
            return new XmlStringWriter();
        }

        protected static string CallIntegrationPostService(ReadOnlyUrl url, string username, string password, string requestXml)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            var request = CreateIntegrationServiceRequest(url.ToString(), username, password);
            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentType = "text/xml";

            using (var writer = new StreamWriter(request.GetRequestStream(), XmlExtensions.DefaultEncoding))
            {
                writer.Write(requestXml);
            }

            return GetResponseAsString(request);
        }

        protected static string CallIntegrationGetService<T>(string username, string password,
            params string[] queryString)
        {
            return CallIntegrationGetService(NavigationManager.GetUrlForPage<T>(queryString), username, password);
        }

        protected static string CallIntegrationGetService(ReadOnlyUrl url, string username, string password)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            var request = CreateIntegrationServiceRequest(url.ToString(), username, password);
            return GetResponseAsString(request);
        }

        private static HttpWebRequest CreateIntegrationServiceRequest(string url, string username, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            SetCommonProperties(request);
            AddIntegratorCredentials(request, username, password);

            return request;
        }

        private HttpWebRequest CreateGetRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";

            SetCommonProperties(request);
            AddCookies(Browser, request);

            return request;
        }

        private static HttpWebRequest CreatePostRequest(HttpClient browser, string url, string contentType)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            request.ContentType = contentType;

            SetCommonProperties(request);
            AddCookies(browser, request);

            return request;
        }

        private static void SetCommonProperties(HttpWebRequest request)
        {
            request.Expect = "";
            request.Timeout = (Debugger.IsAttached ? -1 : 300000);
        }

        private static void AddCookies(HttpClient browser, HttpWebRequest request)
        {
            if (browser.CurrentUrl == null)
                return;

            var cCol = browser.Cookies.GetCookies(browser.CurrentUrl);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(cCol);
        }

        private static void AddIntegratorCredentials(WebRequest request, string username, string password)
        {
            if (!string.IsNullOrEmpty(username))
            {
                request.Headers.Add("X-LinkMeUsername", username);
            }
            if (!string.IsNullOrEmpty(username))
            {
                request.Headers.Add("X-LinkMePassword", password);
            }
        }

        private static string GetResponseAsString(WebRequest request)
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                return ReadWebResponseAsString(response);
            }
        }

        private static string ReadWebResponseAsString(HttpWebResponse response)
        {
            var encoding = (string.IsNullOrEmpty(response.ContentEncoding) ?
                DefaultHttpEncoding : Encoding.GetEncoding(response.ContentEncoding));

            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                return reader.ReadToEnd();
            }
        }

        private static void WriteRequestString(WebRequest request, string postData)
        {
            using (var requestStream = request.GetRequestStream())
            {
                var requestBuffer = Encoding.UTF8.GetBytes(postData);
                requestStream.Write(requestBuffer, 0, requestBuffer.Length);
            }
        }

        private static string GetAjaxProRequestString(IList<object> parameters)
        {
            if (parameters.Count % 2 != 0)
                throw new ArgumentException("The number of parameters must be even - they're name-value pairs.", "parameters");

            if (parameters.Count == 0)
                return "";

            var sb = new StringBuilder("{");
            AppendAjaxProPair(sb, parameters[0], parameters[1]);

            for (var i = 2; i < parameters.Count; i += 2)
            {
                sb.Append(",");
                AppendAjaxProPair(sb, parameters[i], parameters[i + 1]);
            }

            sb.Append("}");

            return sb.ToString();
        }

        private static void AppendAjaxProPair(StringBuilder sb, object key, object value)
        {
            sb.Append("\"");
            sb.Append((string)key);
            sb.Append("\":");

            if (value == null)
            {
                sb.Append("null");
            }
            else if (value is string)
            {
                sb.Append("\"");
                sb.Append(EscapeAjaxProString((string)value));
                sb.Append("\"");
            }
            else if (value is bool)
            {
                sb.Append((bool)value ? "true" : "false");
            }
            else if (value is string[])
            {
                sb.Append("[");
                var array = (string[]) value;
                for (var index = 0; index < array.Length; ++index)
                {
                    if (index != 0)
                        sb.Append(",");
                    sb.Append("\"");
                    sb.Append(EscapeAjaxProString(array[index]));
                    sb.Append("\"");
                }
                sb.Append("]");
            }
            else
            {
                sb.Append(value);
            }
        }

        private static string BuildAjaxUserData(string[] elementNames, string[] elementValues)
        {
            var sb = new StringBuilder();

            if (elementNames.Length != elementValues.Length)
                throw new ArgumentException("The element names and values must have the same length.");

            sb.Append("\"UserData\":{\"ElementNames\":[\"");
            sb.Append(string.Join("\",\"", elementNames));
            sb.Append("\"],\"ElementValues\":[\"");
            sb.Append(string.Join("\",\"", elementValues));
            sb.Append("\"]}}");

            return sb.ToString();
        }

        private static string EscapeAjaxProString(string text)
        {
            return text.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private static string UnescapeAjaxProString(string text)
        {
            return UnescapeAjaxPro.Replace(text, "$1");
        }

        private static void AssertAjaxCodeAndMessage(string actual, AjaxResultCode expectedCode, string expectedMessage)
        {
            Assert.IsTrue(actual.IndexOf("{\"ResultCode\":" + (int)expectedCode) != -1, "The response doesn't contain the expected result code: " + expectedCode + "\r\nResponse: " + actual);

            if (expectedMessage != null)
            {
                Assert.IsTrue(actual.IndexOf("\"Message\":\"" + EscapeAjaxProString(expectedMessage) + "\"") != -1, "The response doesn't contain the expected message: " + expectedMessage + "\r\nResponse: " + actual);
            }
        }

        #endregion

        [TestInitialize]
        public void WebTestClassInitialize()
		{
            // use ADO.NET to wipe the database
			Resolve<IDbConnectionFactory>().DeleteAllTestData();

            ClearSearchIndexes();

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _rememberMeCheckBox = new HtmlCheckBoxTester(Browser, "RememberMe");
            _loginFormId = "LoginForm";

            _emailServer = EmailHost.Start();
        }

        /// <summary>
        /// This call prepends the body id based on the current page. This is 
        /// neccesary, as different pages have different body ids. This should 
        /// only be called by form containers
        /// </summary>
        /// <param name="controlName">Name of control inside body</param>
        /// <returns></returns>
        protected string AddBodyPrefix(string controlName)
        {
            return "ctl00_ctl00_ctl00_Body_" + controlName;
        }

        protected void ClearSearchIndexes()
        {
            _memberSearchService.Clear();
            _jobAdSearchService.Clear();
        }
	}
}
