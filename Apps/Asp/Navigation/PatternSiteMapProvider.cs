using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Web;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Navigation
{
    public class PatternSiteMapProvider
        : DynamicSiteMapProvider
    {
        private SiteMapNode _rootNode;
        private Regex _regex;
        private string _pattern;
        private string _result;
        private string _security;

        protected override void DoInitialize(string name, NameValueCollection attributes)
        {
            base.DoInitialize(name, attributes);

            // Grab the pattern and result.

            _pattern = attributes["pattern"];
            if (string.IsNullOrEmpty(_pattern))
                throw new ApplicationException("The 'pattern' attribute is not provided.");
            _result = attributes["result"];
            _security = attributes["security"];

            // Make sure the pattern is rooted.

            if (_pattern.StartsWith("~/"))
                _pattern = "^" + new ApplicationUrl("~/").Path + _pattern.Substring(2);

            // Create the regular expression.

            _regex = new Regex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            // Check the url against the regular expression.

            Match match = _regex.Match(rawUrl);
            if (!match.Success)
                return null;

            // Create the node.

            SiteMapNode node;
            if (_security == null)
            {
                node = CreateSiteMapNode(new ReadOnlyApplicationUrl(rawUrl));
            }
            else
            {
                NameValueCollection attributes = new NameValueCollection();
                attributes["security"] = _security;
                node = CreateSiteMapNode(new ReadOnlyApplicationUrl(rawUrl), attributes);
            }

            // Create the rewrite node.

            if (!string.IsNullOrEmpty(_result))
            {
                string rewriteResult = match.Result(_result);
                CreateRewriteNode(node, new ReadOnlyApplicationUrl(rewriteResult));
            }

            node.ParentNode = _rootNode;
            return node;
        }

        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            return null;
        }

        protected override SiteMapNode CreateRootNode()
        {
            return _rootNode = CreateSiteMapNode(_pattern);
        }

        protected override void BuildStaticSiteMap(SiteMapNode rootNode)
        {
        }
    }
}
