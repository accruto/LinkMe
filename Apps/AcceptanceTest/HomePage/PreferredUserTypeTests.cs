using System;
using System.Collections.Specialized;
using System.Net;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class PreferredUserTypeTests
        : WebTestClass
    {
        private ReadOnlyUrl _preferredUserTypeUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _preferredUserTypeUrl = new ReadOnlyApplicationUrl("~/accounts/api/preferredusertype");
        }

        [TestMethod]
        public void TestPreferredMember()
        {
            TestPreferred(UserType.Member, HomeUrl, EmployerHomeUrl);
        }

        [TestMethod]
        public void TestPreferredEmployer()
        {
            TestPreferred(UserType.Employer, EmployerHomeUrl, HomeUrl);
        }

        private void TestPreferred(UserType userType, ReadOnlyUrl userHomeUrl, ReadOnlyUrl otherHomeUrl)
        {
            // Not set yet so all allowed through, test for popup (?) ...

            Get(userHomeUrl);
            AssertUrl(userHomeUrl);

            Get(otherHomeUrl);
            AssertUrl(otherHomeUrl);

            Get(GetIgnorePreferredUrl(userHomeUrl, false));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, false));

            Get(GetIgnorePreferredUrl(otherHomeUrl, false));
            AssertUrl(GetIgnorePreferredUrl(otherHomeUrl, false));

            Get(GetIgnorePreferredUrl(userHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, true));

            Get(GetIgnorePreferredUrl(otherHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(otherHomeUrl, true));

            // Set.

            AssertJsonSuccess(PreferredUserType(userType));

            // Redirect occurs.

            Get(userHomeUrl);
            AssertUrl(userHomeUrl);

            Get(otherHomeUrl);
            AssertUrl(userHomeUrl);

            Get(GetIgnorePreferredUrl(userHomeUrl, false));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, false));

            Get(GetIgnorePreferredUrl(otherHomeUrl, false));
            AssertUrl(userHomeUrl);

            // An explicit ignore will be allowed through with no redirect.

            Get(GetIgnorePreferredUrl(userHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, true));

            Get(GetIgnorePreferredUrl(otherHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(otherHomeUrl, true));

            // Remove the cookie which will revert things.

            RemoveAnonymousCookie();

            // Go again.

            Get(userHomeUrl);
            AssertUrl(userHomeUrl);

            Get(otherHomeUrl);
            AssertUrl(otherHomeUrl);

            Get(GetIgnorePreferredUrl(userHomeUrl, false));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, false));

            Get(GetIgnorePreferredUrl(otherHomeUrl, false));
            AssertUrl(GetIgnorePreferredUrl(otherHomeUrl, false));

            Get(GetIgnorePreferredUrl(userHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(userHomeUrl, true));

            Get(GetIgnorePreferredUrl(otherHomeUrl, true));
            AssertUrl(GetIgnorePreferredUrl(otherHomeUrl, true));
        }

        private static ReadOnlyUrl GetIgnorePreferredUrl(ReadOnlyUrl url, bool ignorePreferred)
        {
            var ignorePreferredUrl = url.AsNonReadOnly();
            ignorePreferredUrl.QueryString["ignorePreferred"] = ignorePreferred.ToString();
            return ignorePreferredUrl;
        }

        private void RemoveAnonymousCookie()
        {
            var cookies = Browser.Cookies;
            Browser.Cookies = new CookieContainer();

            var found = false;
            foreach (Cookie cookie in cookies.GetCookies(new Uri(HomeUrl.AbsoluteUri)))
            {
                if (cookie.Name != "LinkMeAnon")
                    Browser.Cookies.Add(cookie);
                else
                    found = true;
            }

            Assert.IsTrue(found);
        }

        private JsonResponseModel PreferredUserType(UserType userType)
        {
            return Deserialize<JsonResponseModel>(Post(_preferredUserTypeUrl, new NameValueCollection {{"userType", userType.ToString()}}));
        }
    }
}
