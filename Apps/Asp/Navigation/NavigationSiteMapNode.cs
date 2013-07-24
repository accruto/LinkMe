using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Navigation
{
    public static class NavigationSiteMap
    {
        public static NavigationSiteMapNode RootNode
        {
            get { return (NavigationSiteMapNode)SiteMap.RootNode; }
        }
        
        public static NavigationSiteMapNode CurrentNode
        {
            get { return (NavigationSiteMapNode)SiteMap.CurrentNode; }
        }
    }

    public class NavigationSiteMapAttribute
    {
        private readonly string _name;
        private readonly string _value;

        internal NavigationSiteMapAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }
    }

    internal class NavigationSiteMapAttributeEnumerator
        : IEnumerator<NavigationSiteMapAttribute>
    {
        private readonly NameValueCollection _attributes;
        private readonly int _count;
        private int _index = -1;

        public NavigationSiteMapAttributeEnumerator(NameValueCollection attributes)
        {
            _attributes = attributes;
            _count = (attributes == null ? 0 : attributes.Count);
        }

        NavigationSiteMapAttribute IEnumerator<NavigationSiteMapAttribute>.Current
        {
            get
            {
                if (_attributes == null)
                    return null;
                if (_index < 0 || _index >= _count)
                    throw new InvalidOperationException("The enumerator state is invalid - index = " + _index);

                string key = _attributes.GetKey(_index);
                string value = _attributes.Get(_index);
                return new NavigationSiteMapAttribute(key, value);
            }
        }

        object IEnumerator.Current
        {
            get { return ((IEnumerator<NavigationSiteMapAttribute>) this).Current; }
        }

        bool IEnumerator.MoveNext()
        {
            return _attributes != null ? (++_index < _count) : false;
        }

        void IEnumerator.Reset()
        {
            _index = -1;
        }

        void IDisposable.Dispose()
        {
        }
    }

    public enum NavigationSecurity
    {
        Secure,
        Insecure,
        Indifferent
    }

    public sealed class NavigationSiteMapNode
    : SiteMapNode, IEnumerable<NavigationSiteMapAttribute>, IXPathNavigable
    {
        private string _keywords;
        private string _description;
        private NavigationSecurity _security;
        private bool _securityIsSet;
        private bool _rewrite;
        private string _method;
        private bool _redirect;
        private bool _crawlable;
        private bool _noIndex;
        private bool _crawlableIsSet;
        private string _text;
        private ReadOnlyUrl _navigationUrl;
        private static readonly IDictionary<UserType, string> UserTypes = new Dictionary<UserType, string>();

        static NavigationSiteMapNode()
        {
            // The ToString method of UserRoles was coming up in performance traces so cache it.

            foreach (UserType role in Enum.GetValues(typeof(UserType)))
                UserTypes[role] = role.ToString();
        }

        public NavigationSiteMapNode(SiteMapProvider provider, string key, string url, string title, string description, IList roles, NameValueCollection attributes)
            : base(provider, key, url, title, description, roles, attributes, null, null)
        {
            SetAttributes();
        }

        public override string this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
                SetAttributes();
            }
        }

        public ReadOnlyUrl NavigationUrl
        {
            get
            {
                if (_navigationUrl == null)
                    _navigationUrl = new ReadOnlyApplicationUrl(IsSecure(), Url);
                return _navigationUrl;
            }
        }

        public string Keywords
        {
            get
            {
                if (_keywords == null)
                {
                    if (Rewrite)
                        _keywords = NavigationParentNode.Keywords;

                    if (_keywords == null)
                        _keywords = string.Empty;
                }

                return _keywords;
            }
        }

        public override string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = base.Description;

                    // If it is still not written and it is a rewrite then use the parent.

                    if (string.IsNullOrEmpty(_description) && Rewrite)
                        _description = ParentNode.Description;

                    if (_description == null)
                        _description = string.Empty;
                }

                return _description;
            }
            set
            {
                base.Description = value;
                _description = string.IsNullOrEmpty(value) ? null : value;
            }
        }

        public NavigationSecurity Security
        {
            get
            {
                // Check the explicit setting.

                if (_securityIsSet)
                    return _security;

                // If there is no parent then indifferent.

                var parentNode = GetParentNode();
                if (parentNode == null)
                    return NavigationSecurity.Indifferent;

                // Use the parent's setting.

                return parentNode.Security;
            }
        }

        public bool Rewrite
        {
            get { return _rewrite; }
        }

        public string Method
        {
            get { return _method; }
        }

        public bool Redirect
        {
            get { return _redirect; }
        }

        public bool Crawlable
        {
            get
            {
                // Check the explicit setting.

                if (_crawlableIsSet)
                    return _crawlable;

                // If there is no parent then crawlable.

                var parentNode = GetParentNode();
                if (parentNode == null)
                    return true;

                // Use the parent's children setting.

                return parentNode.Crawlable;
            }
        }

        public bool NoIndex
        {
            get
            {
                var parentNode = GetParentNode();

                // Use this _noIndex if set or parent node if it exists
                return _noIndex || parentNode == null ? _noIndex : parentNode.NoIndex;
            }
        }

        public string Text
        {
            get { return _text; }
        }

        public NavigationSiteMapNode NavigationParentNode
        {
            get { return GetParentNode(); }
        }

        public override SiteMapNode Clone()
        {
            return new NavigationSiteMapNode(Provider, Key, Url, Title, Description, new ArrayList(Roles), new NameValueCollection(Attributes));
        }

        public SiteMapNode GetEffectiveNode(string key)
        {
            // Check whether this node explicitly sets the value.

            string value = this[key];
            if (value != null)
                return this;

            // Pass onto the parent.

            var parentNode = GetParentNode();
            return parentNode == null ? null : parentNode.GetEffectiveNode(key);
        }

        public string GetEffectiveValue(string key)
        {
            // Check whether this node explicitly sets the value.

            string value = this[key];
            if (value != null)
                return value;

            // Pass onto the parent.

            var parentNode = GetParentNode();
            return parentNode == null ? null : parentNode.GetEffectiveValue(key);
        }

        public bool GetEffectiveBoolValue(string key, bool defaultValue)
        {
            // Check whether this node explicitly sets the value.

            string value = this[key];
            if (value != null)
                return XmlConvert.ToBoolean(value);

            // Pass onto the parent.

            var parentNode = GetParentNode();
            return parentNode == null ? defaultValue : parentNode.GetEffectiveBoolValue(key, defaultValue);
        }

        public string GetActiveValue(string key, UserType role)
        {
            // Use the user's role to decide.

            return GetActiveValue(key, UserTypes[role], this);
        }

        public string GetActiveValue(string key, string filterKey, string filterValue)
        {
            // Use the user's role to decide.

            return GetActiveValue(key, filterKey, filterValue, this);
        }

        public string GetActiveTitle(UserType role)
        {
            // Use the user's role to decide.

            return GetActiveTitle(UserTypes[role], this);
        }

        private bool? IsSecure()
        {
            switch (Security)
            {
                case NavigationSecurity.Secure:
                    return true;

                case NavigationSecurity.Insecure:
                    return false;

                default:
                    return null;
            }
        }

        private static string GetActiveValue(string key, string role, NavigationSiteMapNode node)
        {
            // Check whether a value has been specified for the user's role.

            string value = GetValue(key, role, node);
            if (!string.IsNullOrEmpty(value))
                return value;

            // Check whether the node itself specifies the value.

            value = node[key];
            if (!string.IsNullOrEmpty(value))
                return value;

            // Pass onto the parent.

            var parentNode = node.GetParentNode();
            return parentNode == null ? null : GetActiveValue(key, role, parentNode);
        }

        private static string GetActiveValue(string key, string filterKey, string filterValue, NavigationSiteMapNode node)
        {
            // Check whether a value has been specified for the user's role.

            string value = GetValue(key, filterKey, filterValue, node);
            if (!string.IsNullOrEmpty(value))
                return value;

            // Check whether the node itself specifies the value.

            value = node[key];
            if (!string.IsNullOrEmpty(value))
                return value;

            // Pass onto the parent.

            var parentNode = node.GetParentNode();
            return parentNode == null ? null : GetActiveValue(key, filterKey, filterValue, parentNode);
        }

        private static string GetActiveTitle(string role, NavigationSiteMapNode node)
        {
            // Check whether a title has been specified for the user's role.

            string title = GetTitle(role, node);
            if (!string.IsNullOrEmpty(title))
                return title;

            // Check whether the node itself specifies the title.

            title = node.Title;
            if (!string.IsNullOrEmpty(title))
                return title;

            // Pass onto the parent.

            var parentNode = node.GetParentNode();
            return parentNode == null ? null : GetActiveTitle(role, parentNode);
        }

        private static string GetValue(string key, string role, SiteMapNode node)
        {
            // Look for a child node that has the specified role set.

            foreach (SiteMapNode childNode in node.ChildNodes)
            {
                if (childNode.Roles != null)
                {
                    foreach (string childRole in childNode.Roles)
                    {
                        if (childRole == "*" || string.Compare(childRole, role, true) == 0)
                            return childNode[key];
                    }
                }
            }

            return null;
        }

        private static string GetValue(string key, string filterKey, string filterValue, SiteMapNode node)
        {
            // Look for a child node that has the specified filter key set.

            foreach (SiteMapNode childNode in node.ChildNodes)
            {
                if (string.Compare(childNode[filterKey], filterValue, true) == 0)
                    return childNode[key];
            }

            return null;
        }

        private static string GetTitle(string role, SiteMapNode node)
        {
            // Look for a child node that has the specified role set.

            foreach (SiteMapNode childNode in node.ChildNodes)
            {
                if (childNode.Roles != null)
                {
                    foreach (string childRole in childNode.Roles)
                    {
                        if (childRole == "*" || string.Compare(childRole, role, true) == 0)
                            return childNode.Title;
                    }
                }
            }

            return null;
        }

        public SiteMapNode SelectSingleNode(string xpath)
        {
            var navigator = new NavigationSiteMapNavigator(this);
            XPathNodeIterator iterator = navigator.Select(xpath);
            return iterator.MoveNext() ? ((NavigationSiteMapNavigator)iterator.Current).Object as SiteMapNode : null;
        }

        public IEnumerable<SiteMapNode> SelectNodes(string xpath)
        {
            var navigator = new NavigationSiteMapNavigator(this);
            XPathNodeIterator iterator = navigator.Select(xpath);
            return new NavigationSiteMapNodeSet(iterator);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<NavigationSiteMapAttribute> GetEnumerator()
        {
            return new NavigationSiteMapAttributeEnumerator(Attributes);
        }

        XPathNavigator IXPathNavigable.CreateNavigator()
        {
            return new NavigationSiteMapNavigator(this);
        }

        private static bool IsTrue(SiteMapNode node, string key, bool defaultValue)
        {
            string nodeValue = node[key];

            if (nodeValue == null)
                return defaultValue;
            return bool.Parse(nodeValue);
        }

        private static void CheckIsSet(SiteMapNode node, string key, ref bool value, ref bool isSet)
        {
            string nodeValue = node[key];

            if (nodeValue != null)
            {
                isSet = true;
                value = bool.Parse(nodeValue);
            }
            else
            {
                isSet = false;
            }
        }

        private static void CheckIsSet<T>(SiteMapNode node, string key, ref T value, ref bool isSet)
        {
            string nodeValue = node[key];

            if (nodeValue != null)
            {
                isSet = true;
                value = (T) Enum.Parse(typeof(T), nodeValue, true);
            }
            else
            {
                isSet = false;
            }
        }

        private void SetAttributes()
        {
            _keywords = this["keywords"];
            CheckIsSet(this, "security", ref _security, ref _securityIsSet);
            CheckIsSet(this, "crawlable", ref _crawlable, ref _crawlableIsSet);
            _rewrite = IsTrue(this, "rewrite", false);
            _method = this["method"];
            _redirect = IsTrue(this, "redirect", false);

            if (!_crawlableIsSet && (_rewrite || _redirect))
            {
                _crawlable = false;
                _crawlableIsSet = true;
            }

            _text = this["text"];
            _noIndex = IsTrue(this, "noindex", false);
        }

        private NavigationSiteMapNode GetParentNode()
        {
            return ParentNode as NavigationSiteMapNode;
        }
    }
}
