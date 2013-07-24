using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Navigation
{
    public abstract class DynamicSiteMapProvider
        : SiteMapProvider
    {
        private class SiteMapProviderState
        {
            private SiteMapNode _rootNode;

            private readonly IDictionary<SiteMapNode, SiteMapNode> _parentNodes = new Dictionary<SiteMapNode, SiteMapNode>();
            private readonly IDictionary<SiteMapNode, SiteMapNodeCollection> _childNodeCollections = new Dictionary<SiteMapNode, SiteMapNodeCollection>();
            private readonly IDictionary<string, SiteMapNode> _nodesFromUrl = new Dictionary<string, SiteMapNode>(StringComparer.InvariantCultureIgnoreCase);
            private readonly IDictionary<string, SiteMapNode> _nodesFromKey = new Dictionary<string, SiteMapNode>(StringComparer.InvariantCultureIgnoreCase);
            private readonly IDictionary<string, SiteMapProvider> _childProviders = new Dictionary<string, SiteMapProvider>();
            private IDictionary<string, SiteMapNode> _allNodesFromKey;

            public SiteMapNode RootNode
            {
                get { return _rootNode; }
                set { _rootNode = value; }
            }

            public IDictionary<SiteMapNode, SiteMapNode> ParentNodes
            {
                get { return _parentNodes; }
            }

            public IDictionary<SiteMapNode, SiteMapNodeCollection> ChildNodeCollections
            {
                get { return _childNodeCollections; }
            }

            public IDictionary<string, SiteMapNode> NodesFromUrl
            {
                get { return _nodesFromUrl; }
            }

            public IDictionary<string, SiteMapNode> NodesFromKey
            {
                get { return _nodesFromKey; }
            }

            public IDictionary<string, SiteMapProvider> ChildProviders
            {
                get { return _childProviders; }
            }

            public IDictionary<string, SiteMapNode> AllNodesFromKey
            {
                get { return _allNodesFromKey; }
                set { _allNodesFromKey = value; }
            }
        }

        private string _pagePrefix;

        // This state represents consistent read-only colelctions that can be used to satisfy queries.

        private SiteMapProviderState _currentState = new SiteMapProviderState();

        // This state presents a new state that is being constructed to replace the current state.
        // The lock specifically ensures that only one thread is generating a new state at a time.
        // When constructed the new state will replace the current state.

        private readonly object _syncLock = new object();
        private SiteMapProviderState _newState;

        public sealed override void Initialize(string name, NameValueCollection attributes)
        {
            try
            {
                DoInitialize(name, attributes);
            }
            catch (Exception ex)
            {
                // Unfortunately ProvidersHelper.InstantiateProvider(), which calls this method, kindly
                // "repackages" any exception thrown so that the full details are lost, UNLESS it's a
                // ConfigurationException. So to allow the Server Error page to show the proper exception
                // make sure anything thrown out of here is a ConfigurationException.

                if (ex is ConfigurationException)
                    throw;
                else
                    throw new ConfigurationErrorsException("Failed to initialise " + GetType().Name + ".", ex);
            }
        }

        protected virtual void DoInitialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);

            // Grab the page prefix.

            string pagePrefix = attributes["pagePrefix"];
            if (!string.IsNullOrEmpty(pagePrefix))
                _pagePrefix = pagePrefix.ToLower();
        }

        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            if (rawUrl == null)
                throw new ArgumentNullException("rawUrl");
            CheckStaticSiteMap();

            // Look up the table.

            SiteMapNode node;
            if (_currentState.NodesFromUrl.TryGetValue(rawUrl, out node))
                return node;

            // Check with child providers.

            foreach (SiteMapProvider provider in _currentState.ChildProviders.Values)
            {
                node = provider.FindSiteMapNode(rawUrl);
                if (node != null)
                    return node;
            }

            return null;
        }

        public override SiteMapNode FindSiteMapNodeFromKey(string key)
        {
            CheckStaticSiteMap();

            // Look up the short cut table.

            if (_currentState.AllNodesFromKey == null)
            {
                var allNodesFromKey = new Dictionary<string, SiteMapNode>(StringComparer.InvariantCultureIgnoreCase);
                CacheStaticSiteMap(allNodesFromKey, _currentState.RootNode);
                _currentState.AllNodesFromKey = allNodesFromKey;
            }

            SiteMapNode node;
            if (_currentState.AllNodesFromKey.TryGetValue(key, out node))
                return node;

            // Check with child providers.

            foreach (SiteMapProvider provider in _currentState.ChildProviders.Values)
            {
                node = provider.FindSiteMapNodeFromKey(key);
                if (node != null)
                    return node;
            }

            return null;
        }

        public override SiteMapNode GetParentNode(SiteMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            CheckStaticSiteMap();

            // Look up the table.

            SiteMapNode parentNode;
            if (_currentState.ParentNodes.TryGetValue(node, out parentNode))
                return parentNode;

            // Ask the parent provider.

            if (parentNode == null && ParentProvider != null)
                return ParentProvider.GetParentNode(node);
            else
                return null;
        }

        public override SiteMapNodeCollection GetChildNodes(SiteMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            CheckStaticSiteMap();

            // Get the collection of child nodes.

            SiteMapNodeCollection childNodeCollection;
            if (!_currentState.ChildNodeCollections.TryGetValue(node, out childNodeCollection))
                childNodeCollection = new SiteMapNodeCollection();
            return childNodeCollection;
        }

        public void Reset()
        {
            lock (_syncLock)
            {
                // Keep the root node.

                var rootNode = _currentState.RootNode;

                try
                {
                    // Build again.

                    _newState = new SiteMapProviderState();
                    AddNode(rootNode);
                    BuildStaticSiteMap(rootNode);
                    _newState.RootNode = rootNode;

                    // New state has been successfully generated so use it.

                    _currentState = _newState;
                }
                finally
                {
                    // Don't leave in a partial state.

                    _newState = null;
                }
            }
        }

        protected override void AddNode(SiteMapNode node, SiteMapNode parentNode)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            // This should only be called when the static map is being created
            // as the child providers collection is not protected otherwise.

            if (_newState == null)
                throw new InvalidOperationException("AddNode should only be called in response to building the static site map.");

            // Url.

            string url = node.Url;
            if (!string.IsNullOrEmpty(url))
                _newState.NodesFromUrl[url] = node;

            // Key.

            _newState.NodesFromKey[node.Key] = node;

            // Relationship.

            if (parentNode != null)
            {
                _newState.ParentNodes[node] = parentNode;
                SiteMapNodeCollection childNodeCollection;
                if (!_newState.ChildNodeCollections.TryGetValue(parentNode, out childNodeCollection))
                {
                    childNodeCollection = new SiteMapNodeCollection();
                    _newState.ChildNodeCollections[parentNode] = childNodeCollection;
                }
                childNodeCollection.Add(node);
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            CheckStaticSiteMap();
            return _currentState.RootNode;
        }

        private void CheckStaticSiteMap()
        {
            if (_currentState.RootNode == null)
            {
                lock (_syncLock)
                {
                    if (_currentState.RootNode == null)
                        BuildStaticSiteMap();
                }
            }
        }

        private void BuildStaticSiteMap()
        {
            // Build.

            try
            {
                _newState = new SiteMapProviderState();
                _newState.RootNode = CreateRootNode();
                BuildStaticSiteMap(_newState.RootNode);
                
                // New state has been successfully generated so use it.
                
                _currentState = _newState;
            }
            finally
            {
                // Don't leave in a partial state.

                _newState = null;
            }
        }

        private static void CacheStaticSiteMap(IDictionary<string, SiteMapNode> allNodesFromKey, SiteMapNode node)
        {
            allNodesFromKey[node.Key] = node;

            // Iterate through all descendents to populate the collection.

            foreach (SiteMapNode childNode in node.ChildNodes)
                CacheStaticSiteMap(allNodesFromKey, childNode);
        }

        protected abstract SiteMapNode CreateRootNode();
        protected abstract void BuildStaticSiteMap(SiteMapNode rootNode);

        protected string PagePrefix
        {
            get { return _pagePrefix; }
        }

        protected SiteMapNode CreateSiteMapNode(ReadOnlyApplicationUrl url, string title, string description, IList roles, NameValueCollection attributes)
        {
            return new NavigationSiteMapNode(this, GetKey(url, attributes), url == null ? null : url.PathAndQuery, title, description, roles, attributes);
        }

        protected SiteMapNode CreateSiteMapNode(ReadOnlyApplicationUrl url, NameValueCollection attributes)
        {
            return CreateSiteMapNode(url, null, null, null, attributes);
        }

        protected SiteMapNode CreateSiteMapNode(ReadOnlyApplicationUrl url)
        {
            return CreateSiteMapNode(url, null, null, null, null);
        }

        protected SiteMapNode CreateSiteMapNode(string key)
        {
            return new NavigationSiteMapNode(this, key, null, null, null, null, null);
        }

        protected void AddRedirectNode(SiteMapNode parentNode, ReadOnlyApplicationUrl url)
        {
            SiteMapNode node = CreateRedirectNode(url);
            AddNode(node, parentNode);
        }

        private SiteMapNode CreateRedirectNode(ReadOnlyApplicationUrl url)
        {
            // Create the attributes.

            NameValueCollection attributes = new NameValueCollection();
            attributes.Add("redirect", XmlConvert.ToString(true));

            // Create the node.

            return CreateSiteMapNode(url, null, null, null, attributes);
        }

        protected void AddRewriteNode(SiteMapNode parentNode, ReadOnlyApplicationUrl url)
        {
            AddRewriteNode(parentNode, url, null);
        }

        protected void AddRewriteNode(SiteMapNode parentNode, ReadOnlyApplicationUrl url, string method)
        {
            SiteMapNode node = CreateRewriteNode(url, method);
            AddNode(node, parentNode);
        }

        protected void CreateRewriteNode(SiteMapNode parentNode, ReadOnlyApplicationUrl url)
        {
            CreateRewriteNode(parentNode, url, null);
        }

        protected void CreateRewriteNode(SiteMapNode parentNode, ReadOnlyApplicationUrl url, string method)
        {
            SiteMapNode rewriteNode = CreateRewriteNode(url, method);
            parentNode.ChildNodes = new SiteMapNodeCollection();
            parentNode.ChildNodes.Add(rewriteNode);
        }

        private SiteMapNode CreateRewriteNode(ReadOnlyApplicationUrl url, string method)
        {
            // Create the attributes.

            NameValueCollection attributes = new NameValueCollection();
            attributes.Add("rewrite", XmlConvert.ToString(true));
            if (!string.IsNullOrEmpty(method))
                attributes.Add("method", method);
            attributes.Add("crawlable", XmlConvert.ToString(false));

            // Create the node.

            return CreateSiteMapNode(url, null, null, null, attributes);
        }

        protected void AddChildProvider(SiteMapProvider provider)
        {
            // This should only be called when the static map is being created
            // as the child providers collection is not protected otherwise.

            if (_newState == null)
                throw new InvalidOperationException("AddChildProvider should only be called in response to building the static site map.");

            string name = provider.Name;
            if (!string.IsNullOrEmpty(name))
            {
                // This should only be called when the static 

                if (!_newState.ChildProviders.ContainsKey(name))
                    _newState.ChildProviders[name] = provider;
            }
        }

        protected SiteMapNode GetSiteMapNodeFromPattern(string pattern, string result, NavigationSecurity security)
        {
            return GetSiteMapNodeFromPattern(pattern, result, security.ToString().ToLower());
        }

        protected SiteMapNode GetSiteMapNodeFromPattern(string pattern, string result, string security)
        {
            // Create the provider instance.

            var provider = new PatternSiteMapProvider();

            // Initialise it with the pattern and result.

            var attributes = GetChildAttributes();
            attributes["pattern"] = pattern;
            attributes["result"] = result;
            if (security != null)
                attributes["security"] = security;
            provider.Initialize(pattern, attributes);

            provider.ParentProvider = this;
            AddChildProvider(provider);
            return provider.RootNode;
        }

        protected NameValueCollection GetChildAttributes()
        {
            var attributes = new NameValueCollection();
            attributes["pagePrefix"] = _pagePrefix;
            return attributes;
        }

        private string GetKey(ReadOnlyApplicationUrl url, NameValueCollection attributes)
        {
            // If the original key is not the url then just return it.

            if (url == null)
                return Guid.NewGuid().ToString();

            // If the url has a query string then use the entire url as the key.

            if (url.QueryString.Count > 0)
                return url.AppRelativePathAndQuery.ToLower();

            // If the node represents a redirect then use the url.

            if (IsTrue(attributes, "redirect", false))
                return url.AppRelativePathAndQuery.ToLower();

            // Convert to the class name that implements the url page.

            return GetClassKey(url.AppRelativePath);
        }

        private string GetClassKey(string path)
        {
            // Remove any end seperators.

            if (path.StartsWith("/"))
                path = path.Substring(1);
            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);

            if (path == string.Empty)
            {
                // No suffix to add.

                return _pagePrefix == null ? string.Empty : _pagePrefix.ToLower();
            }
            else
            {
                // Remove the extension.

                string[] parts = path.Split('/');
                if (parts.Length > 0)
                    parts[parts.Length - 1] = Path.GetFileNameWithoutExtension(parts[parts.Length - 1]);

                // Translate the url into a full name by attaching a prefix and replacing path separators.

                return (_pagePrefix + '.' + string.Join(".", parts)).ToLower();
            }
        }

        private static bool IsTrue(NameValueCollection attributes, string key, bool defaultValue)
        {
            if (attributes == null)
                return defaultValue;
            string value = attributes[key];
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            else
                return bool.Parse(value);
        }
    }
}
