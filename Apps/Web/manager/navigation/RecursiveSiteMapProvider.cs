using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Manager.Navigation
{
    public class RecursiveSiteMapProvider
        : DynamicSiteMapProvider
    {
        private ReadOnlyApplicationUrl _baseUrl;
        private bool _crawlable;

        protected override void DoInitialize(string name, NameValueCollection attributes)
        {
            base.DoInitialize(name, attributes);

            // Set the base url.

            _baseUrl = new ReadOnlyApplicationUrl(attributes["url"] ?? string.Empty);
            _crawlable = bool.Parse(attributes["crawlable"] ?? bool.FalseString);
        }

        protected override void BuildStaticSiteMap(SiteMapNode rootNode)
        {
            // Add child nodes.

            AddChildNodes(HttpContext.Current, rootNode);
        }

        protected override SiteMapNode CreateRootNode()
        {
            // The root node itself is not crawlable.

            var attributes = new NameValueCollection();
            attributes["crawlable"] = false.ToString();

            var node = CreateSiteMapNode(_baseUrl, null, null, null, attributes);
            AddNode(node, null);
            return node;
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            // Check if it matches the baseUrl.

            if (!rawUrl.StartsWith(_baseUrl.Path, StringComparison.InvariantCultureIgnoreCase))
                return null;

            // If there is a query string then it won't match.

            if (rawUrl.HasQueryString())
                return null;

            // Check whether the file exists.

            var filePath = HttpContext.Current.Server.MapPath(rawUrl);
            if (Path.GetExtension(filePath) != ".aspx")
                return null;
            if (!File.Exists(filePath))
                return null;

            // Create a node.

            var attributes = new NameValueCollection();
            attributes["crawlable"] = _crawlable.ToString();
            attributes["recursed"] = true.ToString();
            var fileNode = CreateSiteMapNode(new ReadOnlyApplicationUrl(rawUrl), null, null, null, attributes);
            fileNode.ParentNode = GetRootNodeCore();
            
            return fileNode;
        }

        private void AddChildNodes(HttpContext context, SiteMapNode parentNode)
        {
            var folderPath = context.Server.MapPath(parentNode.Url);
            var parentInfo = new DirectoryInfo(folderPath);
            if (parentInfo.Exists)
            {
                AddChildFolders(context, parentNode, parentInfo);
                AddChildPages(parentNode, parentInfo);
            }
        }

        private void AddChildFolders(HttpContext context, SiteMapNode parentNode, DirectoryInfo parentInfo)
        {
            foreach (var childInfo in parentInfo.GetDirectories())
            {
                var childUrl = new ReadOnlyApplicationUrl(new ApplicationUrl(parentNode.Url), childInfo.Name + "/");

                // Folder nodes are not crawlable.

                var attributes = new NameValueCollection();
                attributes["crawlable"] = false.ToString();

                var childNode = CreateSiteMapNode(childUrl, null, null, null, attributes);
                AddNode(childNode, parentNode);

                // Recurse.

                AddChildNodes(context, childNode);
            }
        }

        private void AddChildPages(SiteMapNode parentNode, DirectoryInfo parentInfo)
        {
            foreach (var fileInfo in parentInfo.GetFiles("*.aspx"))
            {
                var fileUrl = new ReadOnlyApplicationUrl(new ApplicationUrl(parentNode.Url), fileInfo.Name);

                var attributes = new NameValueCollection();
                attributes["crawlable"] = _crawlable.ToString();
                attributes["recursed"] = true.ToString();

                var fileNode = CreateSiteMapNode(fileUrl, null, null, null, attributes);
                AddNode(fileNode, parentNode);
            }
        }
    }
}
