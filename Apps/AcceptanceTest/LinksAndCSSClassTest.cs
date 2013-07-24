using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Featured;
using LinkMe.Apps.Agents.Featured.Commands;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest
{
    [TestClass]
    public class LinksAndCssClassTest
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IFeaturedCommand _featuredCommand = Resolve<IFeaturedCommand>();

        private Dictionary<string, string> _urls;

        private static readonly string[] ExtensionIgnores = new []
            {
                ".css",
                ".js",
                ".axd",
                ".ashx",
                ".png",
                ".gif",
                ".jpg",
                ".bmp",
                ".pdf",
                ".msi",
                ".txt"
            };

        // Ideally these lists should be empty.  They should be considered placeholders for things
        // that really should get fixed.

        private readonly string[] _ignores = new[]
            {
                // Don't follow the log out form because if the user is logged in
                // it will log them out, which messes up the tests.

                "logout",
                "webresource.axd",

                // This is probably a bug which needs to be sorted out.

                "secure-au.imrworldwide.com"
            };


        private readonly string[] _ignoreLinks = new[]
            {
                // Doesn't contain xml or any links.

                "javascript.aspx",
                "clicktrackerjs.aspx",

                // Developer tools page, which has links for throwing exceptions, etc.

                "dev.aspx"
            };

        private static readonly string[] IgnoreTitles = new[]
            {
                "search/jobs/advanced?Advertiser=LinkMe"
            };

        [TestInitialize]
        public void TestInitialize()
        {
            _urls = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void TestAnonymousLinks()
        {
            // Need to clear out the featured jobads from front page from the cache.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            _featuredCommand.UpdateFeaturedJobAds(new FeaturedItem[0]);
            ClearCache(administrator);

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAds(employer);

            // Assert the root page.

            var rootUrl = new ReadOnlyApplicationUrl("/");
            var cookies = new CookieContainer();
            AssertUrl(new ReadOnlyApplicationUrl("~/").AbsoluteUri, null, true, false, cookies, rootUrl);
        }

        [TestMethod]
        public void TestMemberLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAds(employer);

            // Create a member and login.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            // Assert the root page.

            var rootUrl = new ReadOnlyApplicationUrl(true, "/");

            var url = new ReadOnlyApplicationUrl(true, "~/members/profile").AbsoluteUri;
            var cookies = new CookieContainer();
            cookies.Add(Browser.Cookies.GetCookies(new Uri(url)));

            AssertUrl(url, null, true, false, cookies, rootUrl);
        }

        [TestMethod]
        public void TestEmployerLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAds(employer);

            // Create an employer and login.

            LogIn(employer);

            // Assert the root page.

            var rootUrl = new ReadOnlyApplicationUrl(true, "/");
            var url = new ReadOnlyApplicationUrl(true, "~/employers/settings").AbsoluteUri;
            var cookies = new CookieContainer();
            cookies.Add(Browser.Cookies.GetCookies(new Uri(url)));

            AssertUrl(url, null, true, false, cookies, rootUrl);
        }

        [TestMethod]
        public void TestAdministratorLinks()
        {
            // Create an administrator and login.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            LogIn(administrator);

            // Assert the root page.

            var rootUrl = new ReadOnlyApplicationUrl(true, "/");
            var url = new ReadOnlyApplicationUrl(true, "~/administrators/home").AbsoluteUri;
            var cookies = new CookieContainer();
            cookies.Add(Browser.Cookies.GetCookies(new Uri(url)));

            AssertUrl(url, null, true, false, cookies, rootUrl);
        }

        [TestMethod]
        public void TestCommunityCustodianLinks()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            PostJobAds(employer);

            // Create a community administrator and login.

            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, Guid.NewGuid());
            LogIn(custodian);

            // Assert the root page.

            var rootUrl = new ReadOnlyApplicationUrl(true, "/");
            var url = new ReadOnlyApplicationUrl(true, "~/custodians/home").AbsoluteUri;
            var cookies = new CookieContainer();
            cookies.Add(Browser.Cookies.GetCookies(new Uri(url)));

            AssertUrl(url, null, true, false, cookies, rootUrl);
        }

        private void PostJobAds(IEmployer employer)
        {
            foreach (var industry in Resolve<IIndustriesQuery>().GetIndustries())
            {
                var jobAd = employer.CreateTestJobAd("Manager", "Blah blah blah", industry);
                _jobAdsCommand.PostJobAd(jobAd);
            }
        }

        private void AssertUrl(string url, string referer, bool? absolute, bool newWindow, CookieContainer cookies, ReadOnlyUrl rootUrl)
        {
            // Only check if it hasn't already been processed.

            url = Check(url, referer, absolute, newWindow, rootUrl);
            if (url != null)
            {
                // Do a simple GET and make sure the page is found.

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.AllowAutoRedirect = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
                request.Referer = referer;
                request.CookieContainer = cookies;

                // Get the response and check.

                try
                {
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK:
                            case HttpStatusCode.Found:
                                break;

                            default:
                                var message = "The page at '" + url + "'";
                                if (referer != null)
                                    message += ", referred by '" + referer + "',";
                                message += " returned a status code: " + response.StatusCode + ".";
                                Assert.Fail(message);
                                break;
                        }
                    }
                }
                catch (AssertFailedException)
                {
                    // Already dealt with.

                    throw;
                }
                catch (Exception e)
                {
                    var message = "The page at '" + url + "'";
                    if (referer != null)
                        message += ", referred by '" + referer + "',";
                    message += " could not be loaded: " + e.Message;
                    Assert.Fail(message);
                }

                // Check the page itself.

                AssertUrl(url, referer, cookies, rootUrl);
            }
        }

        private void AssertUrl(string url, string referer, CookieContainer cookies, ReadOnlyUrl rootUrl)
        {
            if (!DescendIntoUrl(url))
                return;

            // Load the page.

            try
            {
                Get(new ReadOnlyApplicationUrl(url));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Cannot get the page '" + url + "' refered by '" + referer + ".", ex);
            }

            var document = GetDocument(url);

            FollowLinks(document.DocumentNode, cookies, rootUrl);
            AssertTitle(url, document.DocumentNode);
        }

        private string Check(string url, string referer, bool? absolute, bool newWindow, ReadOnlyUrl rootUrl)
        {
            // Don't follow if it is to be shown in a new window.

            if (newWindow)
                return null;

            // Remove any fragment.

            var pos = url.IndexOf('#');
            if (pos != -1)
                url = url.Substring(0, pos);

            // Check whether it has already been processed.

            if (_urls.ContainsKey(url))
                return null;
            _urls[url] = referer;

            // Check whether the url is internal or external.

            pos = url.IndexOf(":", StringComparison.Ordinal);
            var scheme = pos == -1 ? string.Empty : url.Substring(0, pos);

            switch (scheme)
            {
                case "http":
                    {
                        // If it is an external web site then ignore it.

                        if (!url.StartsWith(rootUrl.ToString()))
                            return null;

                        // Absolute.

                        if (absolute != null && !absolute.Value)
                        {
                            var message = "The page at '" + url + "'";
                            if (referer != null)
                                message += ", referred by '" + referer + "',";
                            message += " is absolute when it shouldn't be.";
                            Assert.Fail(message);
                        }
                    }
                    break;

                case "https":
                    {
                        if (!url.StartsWith(rootUrl.ToString()))
                            return null;

                        // Nothing should be secured.

                        if (absolute != null && !absolute.Value)
                        {
                            var message = "The page at '" + url + "'";
                            if (referer != null)
                                message += ", referred by '" + referer + "',";
                            message += " is secure when it shouldn't be.";
                            Assert.Fail(message);
                        }
                    }
                    break;

                case "":

                    // Relative.

                    if (absolute != null && absolute.Value)
                    {
                        var message = "The page at '" + url + "'";
                        if (referer != null)
                            message += ", referred by '" + referer + "',";
                        message += " is not absolute when it should be.";
                        Assert.Fail(message);
                    }

                    // Need to make absolute.

                    if (url.StartsWith("#"))
                    {
                        // Ignore all links to within the same page.

                        return null;
                    }
                    if (url.StartsWith("/"))
                    {
                        var baseUrl = new ReadOnlyApplicationUrl(referer);
                        url = baseUrl.Scheme + "://" + baseUrl.Host + url;
                    }
                    else
                    {
                        var uri = new Uri(new Uri(referer, UriKind.Absolute), new Uri(url, UriKind.Relative));
                        url = uri.AbsoluteUri;
                    }
                    break;

                default:

                    // Ignore everything else.

                    return null;
            }

            // Check whether this page should be ignored.

            if (_ignores.Any(ignore => url.IndexOf(ignore, StringComparison.InvariantCultureIgnoreCase) != -1))
                return null;

            // There are some cases where the fragement was not correct.

            pos = url.IndexOf("##", StringComparison.Ordinal);
            if (pos != -1)
            {
                var message = "The page at '" + url + "'";
                if (referer != null)
                    message += ", referred by '" + referer + "',";
                message += " contains a ## for the fragement instead of a single #.";
                Assert.Fail(message);
            }

            return url;
        }

        private static void AssertBase(string baseUrl, string url)
        {
            // Base should not have any query string.

            var pos = baseUrl.IndexOf('?');
            if (pos != -1)
            {
                var message = "The base '" + baseUrl + "' for page '" + url + "'";
                message += " contains a query string.";
                Assert.Fail(message);
            }

            // Base should be the same as the url.

            pos = url.IndexOf('?');
            if (pos != -1)
                url = url.Substring(0, pos);

            if (string.Compare(baseUrl, url, StringComparison.OrdinalIgnoreCase) != 0)
            {
                var message = "The base '" + baseUrl + "' for page '" + url + "'";
                message += " is different when it should be the same.";
                Assert.Fail(message);
            }
        }

        private static void AssertTitle(string url, HtmlNode document)
        {
            // Select the title.

            var node = document.SelectSingleNode("/html/head/title");
            if (node == null)
                Assert.Fail("The page at '" + url + "' does not contain a title.");

            var title = node.InnerText.Trim();
            if (title.Length == 0)
                Assert.Fail("The page '" + url + "' does not have a title.");

            // Check whether it is in the ignore titles list.

            if (IgnoreTitles.Any(url.Contains))
                return;

            // If it has a title then make sure it does not contain the word 'LinkMe'.
            // This is because the pages could be served up as part of a community etc
            // which generally don't like to have LinkMe placed over the site, so make sure the
            // titles are somewhat generic.

            if (title.IndexOf("LinkMe", StringComparison.InvariantCultureIgnoreCase) != -1)
                Assert.Fail("The title '" + title + "' for page '" + url + "' contains the word 'LinkMe'.");

            // Make sure everything has been substituted.

            for (var index = 0; index < 10; ++index)
            {
                if (title.IndexOf("{" + index + "}", StringComparison.Ordinal) != -1)
                    Assert.Fail("The title '" + title + "' for page '" + url + "' has unsubstituted parameters.");
            }
        }

        private void FollowLinks(HtmlNode document, CookieContainer cookies, ReadOnlyUrl rootUrl)
        {
            // The page may have been redirected (302) to another page so keep it for error messages.

            var referer = Browser.CurrentUrl.AbsoluteUri;

            // base

            var nodes = document.SelectNodes("//base");
            if (nodes != null)
            {
                foreach (var node in nodes)
                    AssertBase(node.Attributes["href"].Value, Browser.CurrentUrl.AbsoluteUri);
            }

            // link

            nodes = document.SelectNodes("//link");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var hrefNode = node.Attributes["href"];
                    if (hrefNode != null)
                    {
                        // Don't follow canonical links.

                        var relAttribute = node.Attributes["rel"];
                        if (relAttribute == null || relAttribute.Value != "canonical")
                            AssertUrl(hrefNode.Value, referer, false, false, cookies, rootUrl);
                    }
                }
            }

            // script

            nodes = document.SelectNodes("//script");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node.Attributes["src"] != null)
                        AssertUrl(node.Attributes["src"].Value, referer, false, false, cookies, rootUrl);
                }
            }

            // a

            nodes = document.SelectNodes("//a");
            if (nodes != null)
            {
                foreach (var aNode in nodes)
                {
                    var newWindow = false;
                    var node = aNode.Attributes["target"];
                    if (node != null)
                        newWindow = node.Value == "_blank";

                    node = aNode.Attributes["href"];
                    if (node != null)
                        AssertUrl(node.Value, referer, null, newWindow, cookies, rootUrl);
                }
            }

            // img

            nodes = document.SelectNodes("//img");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node.Attributes["src"] != null)
                        AssertUrl(node.Attributes["src"].Value, referer, false, false, cookies, rootUrl);
                }
            }
        }

        private bool DescendIntoUrl(string url)
        {
            var extension = GetUrlExtension(url);
            if (ExtensionIgnores.Any(extensionIgnore => string.Compare(extensionIgnore, extension, StringComparison.OrdinalIgnoreCase) == 0))
                return false;

            // Check whether this page should be ignored.

            return _ignoreLinks.All(ignoreLink => url.IndexOf(ignoreLink, StringComparison.InvariantCultureIgnoreCase) == -1);
        }

        private static string GetUrlExtension(string url)
        {
            return Path.GetExtension(url.RemoveQueryString());
        }

        private HtmlDocument GetDocument(string url)
        {
            HtmlDocument document = null;
            try
            {
                document = Browser.CurrentHtml;
            }
            catch (Exception)
            {
                var message = "The page at '" + url + "' does not contain xml.";
                Assert.Fail(message);
            }
            return document;
        }
    }
}