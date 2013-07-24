using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class AccountsRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestRequestNewPassword()
        {
            var url = new ReadOnlyApplicationUrl("~/guests/RequestNewPassword.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/accounts/newpassword");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/ui/unregistered/common/RequestNewPassword.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestChangeEmail()
        {
            var url = new ReadOnlyApplicationUrl("~/ui/registered/common/ChangeEmailForm.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl(true, "~/accounts/changeemail");
            var loginUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", redirectUrl.PathAndQuery));
            AssertRedirect(url, redirectUrl, loginUrl);
        }

        [TestMethod]
        public void TestActivation()
        {
            const string activationCode = "11812220967150626583";
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/accountactivationform.aspx", new ReadOnlyQueryString("activationcode", activationCode));
            var redirectUrl = new ReadOnlyApplicationUrl("~/accounts/activation", new ReadOnlyQueryString("activationcode", activationCode));
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/ui/registered/common/ActivationEmailSentForm.aspx");
            redirectUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
            var loginUrl = new ReadOnlyApplicationUrl(true, "~/login", new ReadOnlyQueryString("returnUrl", redirectUrl.PathAndQuery));
            AssertRedirect(url, redirectUrl, loginUrl);
        }
    }
}
