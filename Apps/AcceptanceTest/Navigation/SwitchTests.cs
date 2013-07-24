using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    public abstract class SwitchTests
        : WebTestClass
    {
        private ReadOnlyUrl _switchBrowserUrl;
        
        private const string WebHtml = "<div class=\"nav-menu\">";
        private const string MobileHtml = "<div class=\"wrapper logo\">";

        [TestInitialize]
        public void SwitchTestInitialize()
        {
            _switchBrowserUrl = new ReadOnlyApplicationUrl("~/browser/switch");
        }

        protected void AssertSwitch(bool mobile, ReadOnlyUrl url, ReadOnlyUrl expectedUrl)
        {
            Get(GetSwitchBrowserUrl(mobile, url));
            AssertUrl(expectedUrl);
        }

        protected void AssertMobile()
        {
            AssertPageContains(MobileHtml);
            AssertPageDoesNotContain(WebHtml);
        }

        protected void AssertWeb()
        {
            AssertPageDoesNotContain(MobileHtml);
            AssertPageContains(WebHtml);
        }

        private ReadOnlyUrl GetSwitchBrowserUrl(bool mobile, ReadOnlyUrl returnUrl)
        {
            var url = _switchBrowserUrl.AsNonReadOnly();
            url.QueryString["mobile"] = mobile.ToString();
            url.QueryString["returnUrl"] = returnUrl.PathAndQuery;
            return url;
        }
    }
}