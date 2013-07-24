using System.Net;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.ui
{
	[TestClass]
    public class SecurityTest
        : WebTestClass
	{
		[TestMethod]
        public void TestIsDangerousRequest()
        {
            var url = new ReadOnlyApplicationUrl("~/search/candidates", new ReadOnlyQueryString("performSearch=True&jobTitle=</XSS/*-*/STYLE=xss:e/**/xpression(alert(document.cookie))>"));
            Get(HttpStatusCode.InternalServerError, url);
            AssertPageDoesNotContain("</XSS/*-*/");
        }
        
        [TestMethod]
		public void TestRequestInvalidPage()
		{
            AssertSecureUrl(NavigationManager.GetUrlForPage<ViewFriends>(), LogInUrl);
		}
	}
}
