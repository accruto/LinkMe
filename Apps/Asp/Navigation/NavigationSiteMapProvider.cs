using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Navigation
{
    public class NavigationSiteMapProvider
        : DynamicSiteMapProvider
    {
        private static readonly char[] Separators = new[] { ';', ',' };
        private ApplicationUrl _siteMapUrl;

        protected override SiteMapNode CreateRootNode()
        {
            XmlDocument document = GetDocument();
            var xmlNsMgr = new XmlNamespaceManager(document.NameTable);
            xmlNsMgr.AddNamespace("sm", "http://schemas.microsoft.com/AspNet/SiteMap-File-1.0");
            
            XmlNode xmlNode = document.SelectSingleNode("sm:siteMap/sm:siteMapNode", xmlNsMgr);
            if (xmlNode != null)
                return BuildSiteMapNode(xmlNode, xmlNsMgr, null);
            return null;
        }

        protected override void BuildStaticSiteMap(SiteMapNode rootNode)
        {
        }

        private SiteMapNode BuildSiteMapNode(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr, SiteMapNode parentNode)
        {
            SiteMapNode node;

            // Look for provider.

            string provider = GetAttribute(xmlNode, "provider");
            if (provider != null)
            {
                node = GetSiteMapNodeFromProvider(provider);
                node.ParentNode = parentNode;
            }
            else
            {
                // Look for siteMapFile.

                string siteMapFile = GetAttribute(xmlNode, "siteMapFile");
                if (siteMapFile != null)
                {
                    node = GetSiteMapNodeFromFile(siteMapFile);
                }
                else
                {
                    // Look for a pattern.

                    string pattern = GetAttribute(xmlNode, "pattern");
                    node = pattern != null ? GetSiteMapNodeFromPattern(xmlNode, xmlNsMgr, pattern) : GetSiteMapNode(xmlNode);
                }
            }

            AddNode(node, parentNode);

            // Iterate.

            foreach (XmlNode xmlChildNode in xmlNode.SelectNodes("sm:siteMapNode", xmlNsMgr))
                BuildSiteMapNode(xmlChildNode, xmlNsMgr, node);

            // Deal with urls with no extensions.

            if (HasNoExtension(node.Url))
            {
                // A '/' at the end of a client url can be optional for those urls where the file name does not have an extension.
                // Turn something like /search/jobs into /search/jobs/ and add a redirect node.

                var normalisedUrl = node.Url + "/";
                AddRedirectNode(node, new ReadOnlyApplicationUrl(normalisedUrl));
            }

            return node;
        }

        private static string GetAttribute(XmlNode xmlNode, string name)
        {
            XmlAttribute xmlAttribute = xmlNode.Attributes[name];
            if (xmlAttribute == null)
                return null;
            if (string.IsNullOrEmpty(xmlAttribute.Value))
                return null;
            return xmlAttribute.Value;
        }

        private static string GetAttribute(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr, string xpath)
        {
            var xmlAttribute = xmlNode.SelectSingleNode(xpath, xmlNsMgr) as XmlAttribute;
            if (xmlAttribute == null)
                return null;
            if (string.IsNullOrEmpty(xmlAttribute.Value))
                return null;
            return xmlAttribute.Value;
        }

        public override SiteMapNode FindSiteMapNode(HttpContext context)
        {
            // The base class uses the rawUrl which we don't want it to.
            // Use what the client actually sees.

            var clientUrl = context.GetClientUrl();
            SiteMapNode node = null;
            if (clientUrl.QueryString != null && clientUrl.QueryString.Count > 0)
                node = FindSiteMapNode(clientUrl.Path + Url.QueryStringSeparatorChar + clientUrl.QueryString);

            return node ?? FindSiteMapNode(clientUrl.Path);
        }
        
        private XmlDocument GetDocument()
        {
            var document = new XmlDocument();
            if (_siteMapUrl == null)
              _siteMapUrl = new ApplicationUrl("~/Web.sitemap");
            string path = HttpContext.Current.Request.MapPath(_siteMapUrl.Path);
            document.Load(path);
            return document;
        }
        
        private SiteMapNode GetSiteMapNodeFromProvider(string name)
        {
            // Look up the provider.

            SiteMapProvider provider = SiteMap.Providers[name];
            if (provider == null)
                throw new ApplicationException("Cannot find the '" + name + "' provider.");
            AddChildProvider(provider);

            // Return its root node.

            return provider.RootNode;
        }

        private SiteMapNode GetSiteMapNodeFromFile(string siteMapFile)
        {
            // Create a new provider.

            var provider = new NavigationSiteMapProvider();

            // Initialise it with the relative path to its file.

            var providerSiteMapUrl = new ApplicationUrl(_siteMapUrl, siteMapFile);
            provider.Initialize(providerSiteMapUrl, GetChildAttributes());

            provider.ParentProvider = this;
            AddChildProvider(provider);
            return provider.RootNode;
        }

        private SiteMapNode GetSiteMapNodeFromPattern(XmlNode xmlNode, XmlNamespaceManager xmlNsMgr, string pattern)
        {
            // Extract the other attributes from the node.

            string result = GetAttribute(xmlNode, xmlNsMgr, "sm:siteMapNode[@rewrite='true']/@result");
            string security = GetAttribute(xmlNode, xmlNsMgr, "@security");
            return GetSiteMapNodeFromPattern(pattern, result, security);
        }

        private SiteMapNode GetSiteMapNode(XmlNode xmlNode)
        {
            string url = GetAttribute(xmlNode, "url");
            string title = GetAttribute(xmlNode, "title");
            string description = GetAttribute(xmlNode, "description");

            // Convert roles.

            ArrayList roles = GetRoles(GetAttribute(xmlNode, "roles"));

            // Copy over all attributes from the node.

            var attributes = new NameValueCollection();
            foreach (XmlAttribute attribute in xmlNode.Attributes)
                attributes[attribute.Name] = attribute.Value;

            return CreateSiteMapNode(url == null ? null : new ReadOnlyApplicationUrl(url), title, description, roles, attributes);
        }

        private static ArrayList GetRoles(string value)
        {
            var roles = new ArrayList();

            if (value != null)
            {
                foreach (string role in value.Split(Separators))
                {
                    if (role.Trim().Length > 0)
                        roles.Add(role.Trim());
                }
            }

            return roles;
        }

        protected override void DoInitialize(string name, NameValueCollection attributes)
        {
            base.DoInitialize(name, attributes);

            // Look for the siteMapFile.

            string siteMapFile = attributes["siteMapFile"];
            if (!string.IsNullOrEmpty(siteMapFile))
                _siteMapUrl = new ApplicationUrl(siteMapFile);
        }

        private void Initialize(ApplicationUrl siteMapUrl, NameValueCollection attributes)
        {
            string siteMapFile = "~" + siteMapUrl.AppRelativePath;
            attributes.Add("siteMapFile", siteMapFile);
            Initialize(siteMapFile, attributes);
        }

        private static bool HasNoExtension(string url)
        {
            // A '/' at the end of a client url can be optional for those urls where the file name does not have an extension.
            // Turn something like /search/jobs into /search/jobs/ and add a redirect node.

            if (!string.IsNullOrEmpty(url) && !url.EndsWith("/"))
            {
                var pos = url.LastIndexOf('/');
                if (pos != -1)
                {
                    pos = url.IndexOf('.', pos + 1);
                    if (pos == -1)
                        return true;
                }
            }

            return false;
        }
    }
}
