using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace LinkMe.Apps.Asp.Navigation
{
    public class AutoSiteMapProvider
        : SiteMapProvider
    {
        private SiteMapNode _rootNode;
        private readonly object _syncLock = new object();
        private readonly IList<string> _secureUrls = new List<string>();
        private readonly IList<string> _mvcUrls = new List<string>();
        private string _pagePrefix = string.Empty;

        public override void Initialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);

            // Page prefixes.

            string pagePrefix = attributes["pagePrefix"] ?? string.Empty;
            if (!string.IsNullOrEmpty(pagePrefix))
            {
                if (!pagePrefix.EndsWith("."))
                    _pagePrefix = pagePrefix + ".";
                else
                    _pagePrefix = pagePrefix;
            }

            // Secure urls.

            string[] secureUrls = (attributes["secureUrls"] ?? string.Empty).Split(';');
            foreach (string secureUrl in secureUrls)
            {
                if (!string.IsNullOrEmpty(secureUrl))
                    _secureUrls.Add(secureUrl);
            }

            // MVC urls.

            string[] mvcUrls = (attributes["mvcUrls"] ?? string.Empty).Split(';');
            foreach (string mvcUrl in mvcUrls)
            {
                if (!string.IsNullOrEmpty(mvcUrl))
                    _mvcUrls.Add(mvcUrl);
            }
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            return null;
        }

        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            NavigationSiteMapNode siteMapNode;

            lock (_syncLock)
            {
                // Get the url from the page.

                string url = GetUrl(key);

                // Create the attributes for the node.

                var attributes = new NameValueCollection();
                if (IsSecure(url))
                    attributes.Add("security", "Secure");
                
                // Create the node.

                siteMapNode = new NavigationSiteMapNode(this, Guid.NewGuid().ToString(), url, null, null, null, attributes)
                {
                    ParentNode = _rootNode
                };
            }

            return siteMapNode;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            return new SiteMapNodeCollection();
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            if (_rootNode == null)
            {
                lock (_syncLock)
                {
                    if (_rootNode == null)
                        _rootNode = new SiteMapNode(this, "root");
                }
            }

            return _rootNode;
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            return true;
        }

        private string GetUrl(string page)
        {
            // Remove any prefixes.

            if (page.StartsWith(_pagePrefix, StringComparison.InvariantCultureIgnoreCase))
                page = page.Substring(_pagePrefix.Length);

            // Some page names start with "_" so remove it (not sure why, surely there is some other way to deal with whatever the problem was).

            var parts = page.Split('.');
            if (parts.Length > 0 && parts[parts.Length - 1].StartsWith("_"))
                parts[parts.Length - 1] = parts[parts.Length - 1].Substring(1);

            // Add the appropriate file extension.

            var url = "~/" + string.Join("/", parts);
            if (parts.Length > 1 &&
                (string.Compare(parts[parts.Length - 1], "saveresumesearchservice", true) == 0 || 
                    string.Compare(parts[parts.Length - 2], "service", true) == 0
                    && string.Compare(parts[parts.Length - 1], "invitationpopupcontents", true) != 0
                    && string.Compare(parts[parts.Length - 1], "representativepopupcontents", true) != 0
                    )
                )
                return (url + ".ashx").ToLower();
            return GetUrlPath(url);
        }

        private string GetUrlPath(string url)
        {
            foreach (var mvcUrl in _mvcUrls)
            {
                if (url.IndexOf(mvcUrl, StringComparison.InvariantCultureIgnoreCase) != -1)
                    return url.ToLower();
            }

            return (url + ".aspx").ToLower();
        }

        private bool IsSecure(string url)
        {
            foreach (string secureUrl in _secureUrls)
            {
                if (url.IndexOf(secureUrl, StringComparison.InvariantCultureIgnoreCase) != -1)
                    return true;
            }

            return false;
        }
    }
}
