using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class EmployerHomeLoginTests
        : LoginTests
    {
        protected override void GetLoginUrl()
        {
            Get(EmployerHomeUrl);
        }

        protected override void AssertLoginUrl()
        {
            AssertPath(EmployerHomeUrl);
        }

        protected override void AssertSecureLoginUrl()
        {
            var url = EmployerHomeUrl.AsNonReadOnly();
            url.Scheme = Uri.UriSchemeHttps;
            AssertUrl(url);
        }
    }
}
