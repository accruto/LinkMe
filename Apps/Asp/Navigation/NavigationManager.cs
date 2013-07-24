using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Configuration;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Utilities;

namespace LinkMe.Apps.Asp.Navigation
{
	public sealed class NavigationManager
    {
        private static readonly EventSource EventSource = new EventSource<NavigationManager>();

        #region Member variables

        private static SiteMapProvider _siteMapProvider;
        private static IList<string> _excludedWildcards;
        private static IList<string> _excludedExtensions;

        private static readonly object SyncLock = new object();
        private static volatile bool _initialised ;
        private static readonly string InsecureRootUrl;
        private static readonly string SecureRootUrl;
        private static readonly string ApplicationPath;
        private static ReadOnlyUrl _logOutUrl;
        private static ReadOnlyUrl _logInUrl;
        private static ReadOnlyUrl _homeUrl;

        #endregion

        #region Contructors

        static NavigationManager()
		{
            var applicationUrl = new ReadOnlyApplicationUrl(false, "~/");
            ApplicationPath = applicationUrl.Path;
            InsecureRootUrl = applicationUrl.AbsoluteUri;
            SecureRootUrl = new ReadOnlyApplicationUrl(true, "~/").AbsoluteUri;
        }

        #endregion

        #region Url

        /// <summary>
        /// Returns the page, generally the FullName of the page type, for the given URL.
        /// </summary>
        public static string GetPageForUrl(ReadOnlyUrl url)
        {
            Initialise();

            // The page is the key in the site map.

            NavigationSiteMapNode node = GetNode(url);
            return node != null ? node.Key : null;
		}

        /// <summary>
        /// Return true if the specified URL is from a LinkMe domain.
        /// </summary>
        public static bool IsInternalUrl(ReadOnlyUrl url)
        {
            if (url == null)
                return false;
            return IsInternalUrl(url.Host, url.AbsoluteUri);
        }

        public static bool IsInternalUrl(Uri uri)
        {
            if (uri == null)
                return false;
            return IsInternalUrl(uri.Host, uri.AbsoluteUri);
        }

        /// <summary>
        /// Return true if the specified URL is from a LinkMe domain.
        /// </summary>
        private static bool IsInternalUrl(string host, string absoluteUri)
        {
            CompareInfo compare = CultureInfo.CurrentCulture.CompareInfo;

            // Compare against the current request.

            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                if (compare.Compare(host, context.Request.Url.Host, CompareOptions.IgnoreCase) == 0)
                    return true;
            }

            return (compare.IndexOf(absoluteUri, InsecureRootUrl, CompareOptions.IgnoreCase) == 0)
                || (compare.IndexOf(absoluteUri, SecureRootUrl, CompareOptions.IgnoreCase) == 0)
                || (compare.Compare(host, "localhost", CompareOptions.IgnoreCase) == 0)
                || (compare.Compare(host, Dns.GetHostName(), CompareOptions.IgnoreCase) == 0);
        }

        public static SiteMapNode GetNodeForUrl(ReadOnlyUrl url)
        {
            Initialise();
            return GetNode(url);
        }

        /// <summary>
        /// If the URL is configured to redirect to another page then the URL for that other page
        /// is returned.
        /// </summary>
        public static ReadOnlyUrl GetRedirectUrl(ReadOnlyUrl url)
        {
            Initialise();

            // Redirect only if the url exists and it directly indicates to do so.

            var node = GetNode(url);
            return node == null ? null : GetRedirectUrl(url, node);
        }

        private static ReadOnlyUrl GetRedirectUrl(ReadOnlyUrl url, NavigationSiteMapNode node)
        {
            // Use the node's parent's url as the new url, maintaining the query string and fragment.

            if (!node.Redirect && !node.Rewrite)
                return null;

            var redirectUrl = GetNavigationUrl(node.NavigationParentNode, url.QueryString).AsNonReadOnly();
            redirectUrl.Fragment = url.Fragment;
            return redirectUrl;
        }

	    /// <summary>
        /// If the URL is configured to be rewritten to another page then the URL for that other page
        /// is returned.
        /// </summary>
        private static ReadOnlyUrl GetRewriteUrl(string method, SiteMapNode node, ReadOnlyQueryString queryString)
        {
            // If this node is a redirect node then use the parent instead.

            if (string.Equals(method, "POST", StringComparison.InvariantCultureIgnoreCase) && ((NavigationSiteMapNode) node).Redirect)
                node = node.ParentNode;

            // Check whether the node has a child node that indicates to rewrite.

            foreach (NavigationSiteMapNode childNode in node.ChildNodes)
            {
                if (childNode.Rewrite)
                {
                    // Make sure the method is a match as well if supplied.

                    if (!string.IsNullOrEmpty(childNode.Method))
                    {
                        if (string.Equals(method, childNode.Method, StringComparison.InvariantCultureIgnoreCase))
                            return GetNavigationUrl(childNode, queryString);
                    }
                    else
                    {
                        return GetNavigationUrl(childNode, queryString);
                    }
                }
            }

//            var pattern = "^" + _baseUrl.Path + @"([0-9a-z\-]+)/([a-z\-]+)/([^\/]*)/([a-fA-F0-9\-]+)(\?)?(.*)$";


            return null;
        }

        #endregion

        #region GetUrlForPage

        public static ReadOnlyUrl GetUrlForPage<T>(ReadOnlyQueryString queryString)
        {
            return GetUrlForPage(typeof(T).FullName, queryString);
        }

        public static ReadOnlyUrl GetUrlForPage<T>(NameValueCollection queryString)
        {
            return GetUrlForPage(typeof(T).FullName, new ReadOnlyQueryString(queryString));
        }

        public static ReadOnlyUrl GetUrlForPage<T>(params string[] queryString)
        {
            return GetUrlForPage(typeof(T).FullName, new ReadOnlyQueryString(queryString));
        }

        public static ReadOnlyUrl GetUrlForPage<T>()
        {
            return GetUrlForPage(typeof(T).FullName, (ReadOnlyQueryString)null);
        }

        public static ReadOnlyUrl GetUrlForPage(Type type)
        {
            return GetUrlForPage(type.FullName);
        }

        public static ReadOnlyUrl GetUrlForPage(Type type, params string[] queryString)
        {
            return GetUrlForPage(type.FullName, queryString);
        }

        public static ReadOnlyUrl GetUrlForPage(string page, params string[] queryString)
        {
            return GetUrlForPage(page, new ReadOnlyQueryString(queryString));
        }

        private static ReadOnlyUrl GetUrlForPage(string page, ReadOnlyQueryString queryString)
        {
            var node = GetNodeForPage(page);
            return GetNavigationUrl(node, queryString);
        }

	    private static ReadOnlyUrl GetNavigationUrl(NavigationSiteMapNode node, ReadOnlyQueryString queryString)
	    {
            return node.NavigationUrl.Clone(queryString);
	    }

	    #endregion

        #region GetUrlForNode

        public static ReadOnlyUrl GetUrlForNode(NavigationSiteMapNode node, ReadOnlyQueryString queryString)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            return GetNavigationUrl(node, queryString);
        }

        #endregion

        #region IsCurrentPage

        public static bool IsPageForUrl<T>(ReadOnlyUrl url)
        {
            return IsPageForPath(typeof(T).FullName, url.Path);
        }

        public static bool IsCurrentUrl(ReadOnlyUrl url)
        {
            return string.Equals(url.AbsoluteUri, HttpContext.Current.Request.Url.AbsoluteUri, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCurrentPage(ReadOnlyUrl url)
        {
            return string.Equals(url.Path, HttpContext.Current.Request.Path, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsCurrentPage<T>()
        {
            return IsCurrentPage(typeof(T).FullName);
        }

        public static bool IsCurrentPage(string page)
        {
            if (string.IsNullOrEmpty(page))
                throw new ArgumentException("The page must be specified.", "page");
            return IsPageForPath(page, HttpContext.Current.Request.Path);
        }

        private static bool IsPageForPath(string page, string path)
        {
            Initialise();

            // Look up the node for the page.

            var node = FindNode(Normalise(page));
            if (node == null)
                return false;

            // Check the url and any rewrites.

            if (string.Equals(path, node.Url, StringComparison.OrdinalIgnoreCase))
                return true;

            // If this node is a rewrite check the parent.

            if (node.Rewrite)
            {
                var parentNode = node.ParentNode;
                if (string.Equals(path, parentNode.Url, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            // Check any child nodes that may be rewrites.

            foreach (NavigationSiteMapNode childNode in node.ChildNodes)
            {
                if (childNode.Rewrite)
                {
                    if (string.Equals(path, childNode.Url, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }
            
            return false;
        }

        #endregion

        public static ReadOnlyUrl GetLogOutUrl()
        {
            return _logOutUrl;
        }

        public static ReadOnlyUrl GetLogInUrl()
        {
            return _logInUrl;
        }

        public static ReadOnlyUrl GetHomeUrl()
        {
            return _homeUrl;
        }

        #region Redirect

        public static void Redirect<T>(params string[] queryString)
        {
            Redirect(typeof(T).FullName, queryString);
        }

        public static void Redirect(string page, params string[] queryString)
        {
            var url = GetUrlForPage(page, queryString);
            Redirect(url);
        }

        public static void Redirect(ReadOnlyUrl url)
        {
            HttpContext.Current.Response.Redirect(url.ToString());
        }

        public static void Redirect(ReadOnlyUrl url, params string[] queryString)
        {
            url = url.Clone(new QueryString(queryString));
            HttpContext.Current.Response.Redirect(url.ToString());
        }

        public static void Redirect(ReadOnlyUrl url, bool endResponse)
        {
            HttpContext.Current.Response.Redirect(url.ToString(), endResponse);
        }
        
        public static void RedirectPermanently<T>()
        {
            var url = GetUrlForPage<T>();
            RedirectPermanently(url);
        }

        public static void RedirectPermanently(ReadOnlyUrl url)
        {
            // Redirect with 301 instead of 302.  Use an absolute URI.

            HttpResponse response = HttpContext.Current.Response;
            response.StatusCode = 301;
            response.AddHeader("Location", url.AbsoluteUri);
            response.End();
        }

        #endregion

        #region Private

		public static bool IsExcluded(string path)
		{
            if (string.IsNullOrEmpty(path))
                return false;

            // Hardcode an exemption for a Server Error page, so that NavigationManager errors don't cause
            // a redirection loop.  Make sure to do this before attempting to initialise.

            if (path.EndsWith("/servererror", StringComparison.OrdinalIgnoreCase))
                return true;

            Initialise();

            // Check extensions.

            if (IsExcludedExtension(path))
                return true;

            return StringUtils.IsWildcardMatchAny(path, _excludedWildcards, StringComparison.OrdinalIgnoreCase);
		}

        private static bool IsExcludedExtension(string path)
        {
            string extension = Path.GetExtension(path);
            if (extension != null)
                extension = extension.Trim('.');

            foreach (var excludedExtension in _excludedExtensions)
            {
                if (excludedExtension == extension)
                    return true;
            }

            return false;
        }

        private static bool IsTrue(SiteMapNode siteMapNode, string key)
        {
            return siteMapNode[key] != null && siteMapNode[key] == "true" ? true : false;
        }

        private static void AddExcluded(SiteMapNode siteMapNode)
        {
            string expression = siteMapNode["expression"];
            if (string.IsNullOrEmpty(expression))
                throw new ApplicationException("The 'expression' value for site map node '" + siteMapNode.Key + "' must not be empty.");

            if (expression[0] == Application.ApplicationPathStartChar)
                expression = expression.Substring(1);
            _excludedWildcards.Add(Application.Current.ApplicationPath.AddUrlSegments(expression));
        }

        private static void AddExcludedExtensions(SiteMapNode siteMapNode)
        {
            string extensions = siteMapNode["extensions"];
            if (string.IsNullOrEmpty(extensions))
                throw new ApplicationException("The 'extensions' value for site map node '" + siteMapNode.Key + "' must not be empty.");

            foreach (string extension in extensions.Split(';'))
            {
                if (!string.IsNullOrEmpty(extension))
                    _excludedExtensions.Add(extension);
            }
        }

        private static NavigationSiteMapNode GetNodeForPage(string page)
        {
            Initialise();
            NavigationSiteMapNode node = FindNode(Normalise(page));
            if (node == null)
                throw new ArgumentException("No page named '" + page + "' was found in the navigation paths.", "page");

            // If the node is a rewrite then return the parent.

            if (node.Rewrite)
                return node.ParentNode as NavigationSiteMapNode;
            return node;
        }

        private static NavigationSiteMapNode FindNode(string page)
        {
            return _siteMapProvider.FindSiteMapNodeFromKey(page) as NavigationSiteMapNode;
        }

        private static string Normalise(string page)
        {
            // A few classes are named eg Default etc to avoid name clashes.  Remove the leading character.

            int index = page.LastIndexOf("._");
            if (index != -1)
            {
                page = page.Remove(index + 1, 1);
            }

            return page;
        }

        private static NavigationSiteMapNode GetNode(ReadOnlyUrl url)
        {
            // Try with the query first and then just the path.

            var node = _siteMapProvider.FindSiteMapNode(url.PathAndQuery) as NavigationSiteMapNode;
            return node ?? _siteMapProvider.FindSiteMapNode(url.Path) as NavigationSiteMapNode;
        }

        #endregion

        #region Initialise

		private static void Initialise()
		{
            if (!_initialised)
			{
                lock (SyncLock)
                {
                    if (!_initialised)
                    {
                        try
                        {
                            // Create the collections.

                            _excludedWildcards = new List<string>();
                            _excludedExtensions = new List<string>();

                            // Iterate over all nodes.

                            _siteMapProvider = SiteMap.Provider;
                            var siteMapNode = _siteMapProvider.RootNode as NavigationSiteMapNode;
                            if (siteMapNode != null)
                                Initialise(siteMapNode);
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException("Failed to load navigation manager configuration from the site map.", ex);
                        }

                        _initialised = true;
                    }
                }
			}
		}

        private static void Initialise(SiteMapNode node)
        {
            // Come back to these.

            if (IsTrue(node, "excluded"))
            {
                AddExcluded(node);
            }
            else if (IsTrue(node, "excludedExtensions"))
            {
                AddExcludedExtensions(node);
            }
            else if (IsTrue(node, "homeUrl"))
            {
                _homeUrl = GetUrlForNode(node as NavigationSiteMapNode, null);
            }
            else if (IsTrue(node, "logInUrl"))
            {
                _logInUrl = GetUrlForNode(node as NavigationSiteMapNode, null);
            }
            else if (IsTrue(node, "logOutUrl"))
            {
                _logOutUrl = GetUrlForNode(node as NavigationSiteMapNode, null);
            }
            else
            {
                // Iterate over children.

                foreach (SiteMapNode childSiteMapNode in node.ChildNodes)
                    Initialise(childSiteMapNode);
            }
        }

        #endregion

        /// <summary>
        /// Returns the roles required to access the specified URL or UserRoles.Anonymous if anonymous
        /// access is allowed. The returned value may be a combination of multiple roles, membership of any
        /// of which would allow access.
        /// </summary>
        public static UserType GetUserTypesRequiredForUrl(Url url)
        {
            if (url == null)
                return UserType.Anonymous;

            var requiredRoles = UserType.Anonymous;

            try
            {
                var config = WebConfigurationManager.OpenWebConfiguration(url.Path);
                var systemWeb = (SystemWebSectionGroup)config.GetSectionGroup("system.web");
                if (systemWeb == null)
                    return UserType.Anonymous;

                var rules = systemWeb.Authorization.Rules;
                if (rules == null || rules.Count == 0)
                    return UserType.Anonymous;

                foreach (AuthorizationRule rule in rules)
                {
                    if (rule.Action == AuthorizationRuleAction.Allow)
                    {
                        foreach (string role in rule.Roles)
                        {
                            try
                            {
                                // Recruiter is no longer its own role.

                                if (string.Compare(role, "Recruiter", StringComparison.InvariantCultureIgnoreCase) == 0)
                                    requiredRoles |= UserType.Employer;
                                else
                                    requiredRoles |= (UserType)Enum.Parse(typeof(UserType), role);
                            }
                            catch (Exception ex)
                            {
                                throw new ApplicationException("Failed to parse user role '" + role + "'.", ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get the required user roles for '" + url + "'.", ex);
            }

            return requiredRoles;
        }

	    public static void CheckRequest(ReadOnlyUrl url, string httpMethod)
	    {
	        const string method = "CheckRequest";

            Initialise();

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Checking request.", Event.Arg("url", url.AbsoluteUri), Event.Arg("httpMethod", httpMethod));

            // Check for a redirects based on just the url, but not for a POST because some clients won't be able to handle it.

            if (httpMethod != "POST")
            {
                if (!CheckRedirect(url))
                    return;
            }

            // Get the node for this client url.

            var node = GetNode(url);

            if (EventSource.IsEnabled(Event.Trace))
            {
                if (node == null)
                    EventSource.Raise(Event.Trace, method, "No node found for url.", Event.Arg("url", url.AbsoluteUri));
                else
                    EventSource.Raise(Event.Trace, method, "Node found for url.", Event.Arg("url", url.AbsoluteUri), Event.Arg("node", node.ToString()));
            }

	        // Check that the appropriate security is specified.

	        if (!CheckSecurity(url, node))
	            return;

            // Check for a redirect based on the node, but not for a POST because some clients won't be able to handle it.

            if (httpMethod != "POST")
            {
                if (!CheckRedirect(url, node))
                    return;
            }

	        // Check for a rewrite.

            CheckRewrite(url, node, httpMethod);
	    }

	    private static bool CheckSecurity(ReadOnlyUrl url, NavigationSiteMapNode node)
        {
            const string method = "CheckSecurity";

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Checking security.", Event.Arg("url", url.AbsoluteUri));

            if (node != null)
            {
                // Check the security requirements of the node.

                switch (node.Security)
                {
                    case NavigationSecurity.Secure:
                        if (url.Scheme != Url.SecureScheme)
                        {
                            var secureUrl = url.AsNonReadOnly();
                            secureUrl.Scheme = Url.SecureScheme;

                            if (EventSource.IsEnabled(Event.Trace))
                                EventSource.Raise(Event.Trace, method, "Needs to be secure but request is not secure.", Event.Arg("url", url.AbsoluteUri), Event.Arg("secureUrl", secureUrl.AbsoluteUri));

                            Redirect(secureUrl);
                            return false;
                        }
                        break;

                    case NavigationSecurity.Insecure:
                        if (url.Scheme != Url.InsecureScheme)
                        {
                            var insecureUrl = url.AsNonReadOnly();
                            insecureUrl.Scheme = Url.InsecureScheme;

                            if (EventSource.IsEnabled(Event.Trace))
                                EventSource.Raise(Event.Trace, method, "Needs to be insecure but request is secure.", Event.Arg("url", url.AbsoluteUri), Event.Arg("insecureUrl", insecureUrl.AbsoluteUri));

                            Redirect(insecureUrl);
                            return false;
                        }
                        break;
                }
            }

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "No security redirect required.", Event.Arg("url", url.AbsoluteUri));
            return true;
        }

        private static bool CheckRedirect(ReadOnlyUrl url)
        {
            const string method = "CheckRedirect";

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Checking redirect.", Event.Arg("url", url.AbsoluteUri));

            // Special case - standardise on e.g. "~/groups" instead of "~/groups/".

            if (!url.Path.Equals(ApplicationPath, StringComparison.InvariantCultureIgnoreCase) && url.Path.EndsWith("/"))
            {
                var newUrl = url.AsNonReadOnly();
                newUrl.Path = url.Path.Substring(0, url.Path.Length - 1);

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Redirecting.", Event.Arg("url", url.AbsoluteUri), Event.Arg("newUrl", newUrl.AbsoluteUri));

                RedirectPermanently(newUrl);
                return false;
            }

            if (url.Path.EndsWith("/default.aspx", StringComparison.InvariantCultureIgnoreCase))
            {
                var newUrl = url.AsNonReadOnly();
                newUrl.Path = url.Path.Substring(0, url.Path.Length - "/default.aspx".Length);
                if ((newUrl.Path + "/").Equals(ApplicationPath, StringComparison.InvariantCultureIgnoreCase))
                    newUrl.Path += "/";

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Redirecting.", Event.Arg("url", url.AbsoluteUri), Event.Arg("newUrl", newUrl.AbsoluteUri));

                RedirectPermanently(newUrl);
                return false;
            }

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "No redirect.", Event.Arg("url", url.AbsoluteUri));
            return true;
        }

        private static bool CheckRedirect(ReadOnlyUrl url, NavigationSiteMapNode node)
        {
            const string method = "CheckRedirect";

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Checking redirect.", Event.Arg("url", url.AbsoluteUri));

            if (node != null)
            {
                // Look for the redirect, and if there is one then do it.

                var redirectUrl = GetRedirectUrl(url, node);
                if (redirectUrl != null)
                {
                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Redirect needed.", Event.Arg("url", url.AbsoluteUri), Event.Arg("redirectUrl", redirectUrl.AbsoluteUri));

                    RedirectPermanently(redirectUrl);
                    return false;
                }
            }

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "No redirect.", Event.Arg("url", url.AbsoluteUri));
            return true;
        }

        private static void CheckRewrite(ReadOnlyUrl url, SiteMapNode node, string httpMethod)
        {
            const string method = "CheckRewrite";

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Checking rewrite.", Event.Arg("url", url.AbsoluteUri));

            if (node == null)
                return;

            // Look for a rewrite, and if there then do it.

            var rewriteUrl = GetRewriteUrl(httpMethod, node, url.QueryString);
            if (rewriteUrl != null)
            {
                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Rewrite needed.", Event.Arg("url", url.AbsoluteUri), Event.Arg("rewriteUrl", rewriteUrl.AbsoluteUri));

                // Make sure the request query string is added to whatever query string may be present in the rewrite.

                HttpContext.Current.RewritePath(rewriteUrl.Path, string.Empty, rewriteUrl.QueryString.ToString(), false);
            }
        }
    }
}
