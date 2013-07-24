using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class LoginLoginTests
        : LoginTests
    {
        private ReadOnlyUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        protected override void GetLoginUrl()
        {
            Get(_loginUrl);
        }

        protected override void AssertLoginUrl()
        {
            AssertUrl(_loginUrl);
        }

        protected override void AssertSecureLoginUrl()
        {
            AssertUrl(_loginUrl);
        }
    }
}
