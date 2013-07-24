using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class EmployerLoginLoginTests
        : LoginTests
    {
        private ReadOnlyUrl _employerLoginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _employerLoginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");
        }

        protected override void GetLoginUrl()
        {
            Get(_employerLoginUrl);
        }

        protected override void AssertLoginUrl()
        {
            AssertUrl(_employerLoginUrl);
        }

        protected override void AssertSecureLoginUrl()
        {
            AssertUrl(_employerLoginUrl);
        }
    }
}
