using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class HomeLoginTests
        : LoginTests
    {
        protected override void GetLoginUrl()
        {
            Get(HomeUrl);
        }

        protected override void AssertLoginUrl()
        {
            AssertPath(HomeUrl);
        }

        protected override void AssertSecureLoginUrl()
        {
            var url = HomeUrl.AsNonReadOnly();
            url.Scheme = Uri.UriSchemeHttps;
            AssertUrl(url);
        }
    }
}