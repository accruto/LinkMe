using System;
using System.Collections.Generic;
using System.Web;
using HtmlAgilityPack;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public abstract class NavTests
        : WebTestClass
    {
        protected class Nav
        {
            public ReadOnlyApplicationUrl Url { get; set; }
            public string Text { get; set; }
            public IList<Nav> SubNavs { get; set; }
        }

        private static ReadOnlyApplicationUrl _switchBrowserUrl;

        protected const string SwitchBrowserText = "Mobile site";

        private static ReadOnlyApplicationUrl _aboutUrl;
        protected const string AboutText = "About";

        private static ReadOnlyApplicationUrl _privacyUrl;
        protected const string PrivacyText = "Privacy";

        private static ReadOnlyApplicationUrl _contactUrl;
        protected const string ContactText = "Contact Us";

        private static ReadOnlyApplicationUrl _logoutUrl;

        private static ReadOnlyApplicationUrl _termsUrl;
        protected const string TermsText = "Terms of use";

        private const string HomeXPath = "//div[@id='header']/div[@id='header-links-container']/div[@id='header-links']/div/div[@id='logo']";
        private const string NavXPath = "//div[@id='header']/div[@id='header-links-container']/div[@id='header-links']/div/div[@id='nav']";
        private const string HeaderRhsLinksXPath = "//div[@id='header']/div[@id='header-links-container']/div[@id='header-links']/div[@class='right-section']/div[@id='action-links']/div[@class='rhs-links']";

        private ReadOnlyApplicationUrl _homeUrl;
        private IList<Nav> _navs;

        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        protected readonly IVerticalsQuery _verticalsQuery = Resolve<IVerticalsQuery>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        protected readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestInitialize]
        public void NavTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _switchBrowserUrl = new ReadOnlyApplicationUrl("~/browser/switch");
            _aboutUrl = new ReadOnlyApplicationUrl("~/aboutus");
            _privacyUrl = new ReadOnlyApplicationUrl("~/privacy");
            _contactUrl = new ReadOnlyApplicationUrl("~/contactus");
            _logoutUrl = new ReadOnlyApplicationUrl(true, "~/logout");
            _termsUrl = new ReadOnlyApplicationUrl("~/terms");
        }

        protected void SetNavs(ReadOnlyApplicationUrl homeUrl, IList<Nav> navs)
        {
            _homeUrl = homeUrl;
            _navs = navs;
        }

        protected void TestNavs(ReadOnlyApplicationUrl url)
        {
            // Get everything securely.

            if (url.IsAbsolute || url.IsSecure != null)
                GetPage(url.Scheme == Url.SecureScheme, url);
            else
                Get(url);

            AssertHomeNav(_homeUrl);
            AssertNavs(_navs);
            AssertHeaderNavs();
            AssertFooterNavs();
        }

        protected void TestNavs(IList<Nav> navs)
        {
            foreach (var nav in navs)
            {
                TestNavs(nav.Url);
                if (nav.SubNavs != null)
                    TestNavs(nav.SubNavs);
            }
        }

        protected void SwitchBrowser(bool mobile)
        {
            Get(GetSwitchBrowserUrl(mobile, HomeUrl));
        }

        protected ReadOnlyUrl GetSwitchBrowserUrl(bool mobile, ReadOnlyUrl returnUrl)
        {
            var url = _switchBrowserUrl.AsNonReadOnly();
            url.QueryString["mobile"] = mobile.ToString();
            url.QueryString["returnUrl"] = returnUrl.PathAndQuery;
            return url;
        }

        private void AssertHomeNav(ReadOnlyApplicationUrl url)
        {
            Assert.IsTrue(string.Compare(GetDivUrl(HomeXPath), GetNavUrl(url), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        private void AssertNavs(IList<Nav> navs)
        {
            var xmlNavs = Browser.CurrentHtml.DocumentNode.SelectNodes(NavXPath + "/div[@class='nav-menu']/div");
            Assert.AreEqual(navs.Count, xmlNavs == null ? 0 : xmlNavs.Count, "Expected to find " + navs.Count + " primary navs but found " + (xmlNavs == null ? 0 : xmlNavs.Count) + " instead.");
            if (xmlNavs != null)
            {
                for (var index = 0; index < navs.Count; ++index)
                    AssertNav(navs[index], xmlNavs[index]);
            }
        }

        private void AssertNav(Nav nav, HtmlNode navNode)
        {
            // Select the node.

            var a = navNode.SelectSingleNode("a");

            Assert.AreEqual(GetNavUrl(nav.Url).ToLower(), a.Attributes["href"].Value.ToLower(), "Current url: " + Browser.CurrentUrl.AbsoluteUri);
            Assert.AreEqual(nav.Text, HttpUtility.HtmlDecode(a.InnerText.Trim()));

            var uls = navNode.SelectNodes("span/span/ul/li[@class='nav-item']");
            if (nav.SubNavs == null || nav.SubNavs.Count == 0)
            {
                Assert.AreEqual(0, uls == null ? 0 : uls.Count);
            }
            else
            {
                Assert.AreEqual(nav.SubNavs.Count, uls == null ? 0 : uls.Count);
                if (uls != null)
                {
                    for (var index = 0; index < nav.SubNavs.Count; ++index)
                        AssertNav(nav.SubNavs[index], uls[index]);
                }
            }
        }

        protected void GetPage(bool secure, ReadOnlyUrl url)
        {
            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = secure ? Url.SecureScheme : Url.InsecureScheme;
            Get(secureUrl);
        }

        protected void AssertMemberSwitchNav(ReadOnlyApplicationUrl url)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(HeaderRhsLinksXPath + "/div[@class='member switch-link']");
            Assert.IsNotNull(xmlNode, "Cannot find switch nav.");
            Assert.AreEqual(GetNavUrl(url).ToLower(), GetLoadPageUrl(xmlNode).ToLower());
        }

        protected void AssertEmployerSwitchNav(ReadOnlyApplicationUrl url)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(HeaderRhsLinksXPath + "/div[@class='employer switch-link']");
            Assert.IsNotNull(xmlNode, "Cannot find switch nav.");
            Assert.AreEqual(GetNavUrl(url).ToLower(), GetLoadPageUrl(xmlNode).ToLower());
        }

        protected void AssertNoMemberSwitchNav()
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(HeaderRhsLinksXPath + "/div[@class='member switch-link']");
            Assert.IsNull(xmlNode, "A switch nav was found when none was expected.");
        }

        protected void AssertNoEmployerSwitchNav()
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(HeaderRhsLinksXPath + "/div[@class='employer switch-link']");
            Assert.IsNull(xmlNode, "A switch nav was found when none was expected.");
        }

        private string GetLoginUrl()
        {
            return GetDivUrl(HeaderRhsLinksXPath + "/div[@class='login-join switch-link']");
        }

        protected void AssertLoginNav(ReadOnlyApplicationUrl url)
        {
            Assert.IsTrue(string.Compare(GetNavLoginUrl(url), GetLoginUrl(), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        protected void AssertNoLoginNav()
        {
            Assert.IsNull(GetLoginUrl(), "A login nav was found when none was expected.");
        }

        private string GetAccountUrl()
        {
            return GetDivUrl(HeaderRhsLinksXPath + "/div[@class='account action-link']");
        }

        private string GetSettingsUrl()
        {
            return GetDivUrl(HeaderRhsLinksXPath + "/div[@class='settings action-link']");
        }

        protected void AssertAccountNav(ReadOnlyApplicationUrl url)
        {
            Assert.IsTrue(string.Compare(GetNavUrl(url), GetAccountUrl(), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        protected void AssertNoAccountNav()
        {
            Assert.IsNull(GetAccountUrl());
        }

        protected void AssertSettingsNav(ReadOnlyApplicationUrl url)
        {
            Assert.IsTrue(string.Compare(GetNavUrl(url), GetSettingsUrl(), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        protected void AssertNoSettingsNav()
        {
            Assert.IsNull(GetSettingsUrl());
        }

        private string GetLogoutUrl()
        {
            return GetDivUrl(HeaderRhsLinksXPath + "/div[@class='logout action-link']");
        }

        protected void AssertLogoutNav()
        {
            Assert.IsTrue(string.Compare(Browser.CurrentUrl.Scheme == Url.SecureScheme ? _logoutUrl.PathAndQuery : _logoutUrl.AbsoluteUri, GetLogoutUrl(), StringComparison.InvariantCultureIgnoreCase) == 0, "Current url is '" + Browser.CurrentUrl + "'.");
        }

        protected void AssertNoLogoutNav()
        {
            Assert.IsNull(GetLogoutUrl());
        }

        private string GetAUrl(string path)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode(path);
            return node == null ? null : HttpUtility.HtmlDecode(node.Attributes["href"].Value);
        }

        private string GetDivUrl(string path)
        {
            var xmlNode = Browser.CurrentHtml.DocumentNode.SelectSingleNode(path);
            if (xmlNode == null)
                return null;

            var attribute = xmlNode.Attributes["onclick"];
            Assert.IsTrue(attribute.Value.StartsWith("javascript:loadPage('") && attribute.Value.EndsWith("');"));
            return attribute.Value.Substring("javascript:loadPage('".Length, attribute.Value.Length - "javascript:loadPage('".Length - "');".Length);
        }

        protected abstract void AssertHeaderNavs();

        protected virtual void AssertFooterNavs()
        {
            if (Browser.UseMobileUserAgent)
                AssertMainFooterNav(GetSwitchBrowserUrl(true, new ReadOnlyUrl(Browser.CurrentUrl)), SwitchBrowserText);
            else
                AssertNoMainFooterNav(SwitchBrowserText);

            AssertMainFooterNav(_aboutUrl, AboutText);
            AssertMainFooterNav(_contactUrl, ContactText);
            AssertSubFooterNav(_termsUrl, TermsText);
            AssertSubFooterNav(_privacyUrl, PrivacyText);
        }

        private string GetMainFooterUrl(string text)
        {
            return GetAUrl("//div[@id='footer']//div[@class='right-section']//div[@class='footer-link']/a[.='" + text + "']");
        }

        private string GetSubFooterUrl(string text)
        {
            return GetAUrl("//div[@id='footer']//div[@class='sub-links']/div[@class='footer-link']/a[.='" + text + "']");
        }

        private void AssertMainFooterNav(ReadOnlyUrl url, string text)
        {
            Assert.IsTrue(string.Compare(url.PathAndQuery, GetMainFooterUrl(text), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        private void AssertSubFooterNav(ReadOnlyUrl url, string text)
        {
            Assert.IsTrue(string.Compare(url.PathAndQuery, GetSubFooterUrl(text), StringComparison.InvariantCultureIgnoreCase) == 0);
        }

        protected void AssertNoMainFooterNav(string text)
        {
            Assert.IsNull(GetMainFooterUrl(text));
        }

        protected void AssertNoSubFooterNav(string text)
        {
            Assert.IsNull(GetSubFooterUrl(text));
        }

        private string GetNavLoginUrl(ReadOnlyUrl url)
        {
            var navUrl = (ApplicationUrl)url.AsNonReadOnly();
            navUrl.QueryString["returnUrl"] = new ReadOnlyUrl(Browser.CurrentUrl).PathAndQuery;
            if (navUrl.IsAbsolute || navUrl.IsSecure != null)
                return Browser.CurrentUrl.Scheme == navUrl.Scheme ? navUrl.PathAndQuery : navUrl.AbsoluteUri;
            return navUrl.PathAndQuery;
        }

        private string GetNavUrl(ReadOnlyApplicationUrl url)
        {
            if (url.IsAbsolute || url.IsSecure != null)
                return Browser.CurrentUrl.Scheme == url.Scheme ? url.PathAndQuery : url.AbsoluteUri;
            return url.PathAndQuery;
        }

        private static string GetLoadPageUrl(HtmlNode node)
        {
            var value = node.Attributes["onclick"].Value;
            if (value.StartsWith("javascript:loadPage('") && value.EndsWith("');"))
                return value.Substring("javascript:loadPage('".Length, value.Length - ("javascript:loadPage('".Length + "');".Length));
            Assert.Fail("The node does not contain a loadPage call.");
            return null;
        }
    }
}