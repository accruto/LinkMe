using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberUpdates
{
    public abstract class MemberNewsletterTests
        : EmailTests
    {
        public override TemplateEmail GeneratePreview(Community community)
        {
            return null;
        }

        protected static void AssertLinks(string text, IList<MockEmailResource> resources)
        {
            AssertAnchorLinks(text, null);
            AssertImageLinks(text, null, resources);
        }

        protected static void AssertLinks(string text, Guid? verticalId, IList<MockEmailResource> resources)
        {
            AssertAnchorLinks(text, verticalId);
            AssertImageLinks(text, verticalId, resources);
        }

        private static IList<string> GetLinks(string text, string attributeName)
        {
            var links = new List<string>();
            var attributeStart = " " + attributeName + "=\"";
            const string attributeEnd = "\"";

            // Need to do it by hand as loading into a dom etc takes too long.

            var pos = text.IndexOf(attributeStart);
            while (pos != -1)
            {
                var start = pos + attributeStart.Length;
                var end = text.IndexOf(attributeEnd, start);
                if (end == -1)
                    break;

                var link = text.Substring(start, end - start);
                links.Add(link);

                pos = text.IndexOf(attributeStart, end);
            }

            return links;
        }

        private static void AssertAnchorLinks(string text, Guid? verticalId)
        {
            foreach (var link in GetLinks(text, "href"))
                AssertAnchorLink(link, verticalId);
        }

        private static void AssertAnchorLink(string href, Guid? verticalId)
        {
            // Can now put in a mailto.

            if (!href.StartsWith("mailto:"))
            {
                // Should all be part of the LinkMe website.

                var webSiteQuery = Resolve<IWebSiteQuery>();
                var url = webSiteQuery.GetUrl(WebSite.LinkMe, verticalId, false, "~/url/").AbsoluteUri;

                Assert.IsTrue(href.StartsWith(url, StringComparison.InvariantCultureIgnoreCase),
                    "'" + href + "' does not start with '" + url + "'.");

                // Should end with a guid.

                try
                {
                    new Guid(href.Substring(url.Length));
                }
                catch (Exception)
                {
                    Assert.Fail("'" + href + "' is not a tiny url.");
                }
            }
        }

        private static void AssertImageLinks(string text, Guid? verticalId, IEnumerable<MockEmailResource> resources)
        {
            foreach (var link in GetLinks(text, "background"))
                AssertImageLink(link, verticalId, null);

            foreach (var link in GetLinks(text, "src"))
                AssertImageLink(link, verticalId, resources);
        }

        private static void AssertImageLink(string href, Guid? verticalId, IEnumerable<MockEmailResource> resources)
        {
            if (href.StartsWith("cid:"))
            {
                var id = href.Substring("cid:".Length);

                // Look for it in the resources.

                Assert.IsTrue(resources != null && (from r in resources where r.ContentId == id select r).Any(), "Resource '" + id + "' cannot be found.");
            }
            else
            {
                // Might be the tracking pixel.

                var webSiteQuery = Resolve<IWebSiteQuery>();
                var url = webSiteQuery.GetUrl(WebSite.LinkMe, verticalId, false, "~/url/").AbsoluteUri;
                if (href.StartsWith(url))
                {
                    var pos = href.LastIndexOf('.');
                    if (pos == -1)
                        Assert.Fail("'" + href + "' is not a tracking tiny url.");
                    var extension = href.Substring(pos);
                    if (extension != ".aspx")
                        Assert.Fail("'" + href + "' is not a tracking tiny url.");
                    
                    try
                    {
                        new Guid(href.Substring(url.Length, href.Length - url.Length - extension.Length));
                    }
                    catch (Exception)
                    {
                        Assert.Fail("'" + href + "' is not a tracking tiny url.");
                    }
                }
                else
                {
                    // Should all be part of the LinkMe website.

                    url = webSiteQuery.GetUrl(WebSite.LinkMe, verticalId, false, "~/Email/").AbsoluteUri;
                    Assert.IsTrue(href.StartsWith(url, StringComparison.InvariantCultureIgnoreCase), "'" + href + "' does not start with '" + url + "'.");
                }
            }
        }

        protected static string GetSubject()
        {
            return "LinkMe " + DateTime.Now.ToString("MMMM") + " Newsletter";
        }
    }
}