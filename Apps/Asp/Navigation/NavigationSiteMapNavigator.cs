using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Xml;
using System.Xml.XPath;

namespace LinkMe.Apps.Asp.Navigation
{
    public class NavigationSiteMapNavigator : XPathNavigator
    {
        private XmlNodeInfo xmlNodeInfo;
        private readonly NameTable nameTable;

        internal NavigationSiteMapNavigator(NavigationSiteMapNode node)
        {
            xmlNodeInfo = new XmlNodeInfo(node);
            nameTable = new NameTable();
            nameTable.Add(String.Empty);
        }

        private NavigationSiteMapNavigator(NavigationSiteMapNavigator navigator)
        {
            xmlNodeInfo = navigator.xmlNodeInfo;
            nameTable = navigator.nameTable;
        }

        public object Object
        {
            get { return xmlNodeInfo.Object; }
        }

        public override XPathNavigator Clone()
        {
            return new NavigationSiteMapNavigator(this);
        }

        #region Node properties

        public override XPathNodeType NodeType
        {
            get { return xmlNodeInfo.Type; }
        }

        public override string LocalName
        {
            get
            {
                string name = string.Empty;

                switch (xmlNodeInfo.Type)
                {
                    case XPathNodeType.Element:
                        name = xmlNodeInfo.SiteMapNode.Key;
                        break;
                    
                    case XPathNodeType.Attribute:
                        name = xmlNodeInfo.SiteMapAttribute.Name;
                        break;
                }

                return nameTable.Add(name);
            }
        }

        public override string Name
        {
            get { return LocalName; }
        }

        public override string Prefix
        {
            get { return string.Empty; }
        }

        public override string Value
        {
            get
            {
                string value = string.Empty;

                switch (xmlNodeInfo.Type)
                {
                    case XPathNodeType.Attribute:
                        {
                            object attributeValue = xmlNodeInfo.SiteMapAttribute.Value;
                            if (attributeValue != null)
                            {
                                if (attributeValue is bool)
                                    value = XmlConvert.ToString((bool) attributeValue);
                                else
                                    value = attributeValue.ToString();
                            }
                        }
                        break;

                    case XPathNodeType.Text:
                        if (xmlNodeInfo.Object != null)
                            value = xmlNodeInfo.Object.ToString();
                        break;
                }

                return value;
            }
        }

        public override string BaseURI
        {
            get { return NamespaceURI; }
        }

        public override string NamespaceURI
        {
            get { return string.Empty; }
        }


        public override bool IsEmptyElement
        {
            get { return false; }
        }

        public override string XmlLang
        {
            get { return String.Empty; }
        }

        public override XmlNameTable NameTable
        {
            get { return nameTable; }
        }

        #endregion

        #region Attributes

        public override bool HasAttributes
        {
            get { return true; }
        }

        public override string GetAttribute(string localName, string namespaceURI)
        {
            Debug.Assert(xmlNodeInfo.Type == XPathNodeType.Element);
            return xmlNodeInfo.SiteMapNode[localName];
        }

        public override bool MoveToAttribute(string localName, string namespaceURI)
        {
            Debug.Assert(xmlNodeInfo.Type == XPathNodeType.Element);



            return false;
        }

        public override bool MoveToFirstAttribute()
        {
            // Check whether it has already been set.

            if (xmlNodeInfo.FirstAttribute != null)
            {
                xmlNodeInfo = xmlNodeInfo.FirstAttribute;
                return true;
            }

            switch (xmlNodeInfo.Type)
            {
                case XPathNodeType.Root:
                case XPathNodeType.Element:
                    {
                        IEnumerator enumerator = xmlNodeInfo.SiteMapNode.GetEnumerator();
                        if (enumerator.MoveNext())
                        {
                            XmlNodeInfo xmlAttributeInfo = new XmlNodeInfo(XPathNodeType.Attribute, xmlNodeInfo.Root, xmlNodeInfo, enumerator, null, null, enumerator.Current);
                            xmlNodeInfo.FirstAttribute = xmlAttributeInfo;
                            xmlNodeInfo = xmlAttributeInfo;
                            return true;
                        }
                    }

                    break;
            }

            return false;
        }

        public override bool MoveToNextAttribute()
        {
            if (xmlNodeInfo.NextSibling != null)
            {
                xmlNodeInfo = xmlNodeInfo.NextSibling;
                return true;
            }

            if (xmlNodeInfo.Type == XPathNodeType.Attribute && xmlNodeInfo.Siblings.MoveNext())
            {
                XmlNodeInfo xmlNextNode = new XmlNodeInfo(XPathNodeType.Attribute, xmlNodeInfo.Root, xmlNodeInfo.Parent, xmlNodeInfo.Siblings, xmlNodeInfo.FirstSibling, xmlNodeInfo, xmlNodeInfo.Siblings.Current);
                xmlNodeInfo.NextSibling = xmlNextNode;
                xmlNodeInfo = xmlNextNode;
                return true;
            }

            return false;
        }

        #endregion

        #region Namespaces

        public override string GetNamespace(string name)
        {
            return string.Empty;
        }

        public override bool MoveToNamespace(string name)
        {
            return false;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
        {
            return false;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
        {
            return false;
        }

        #endregion

        #region Navigation

        public override bool MoveToNext()
        {
            if (xmlNodeInfo.NextSibling != null)
            {
                xmlNodeInfo = xmlNodeInfo.NextSibling;
                return true;
            }

            if (xmlNodeInfo.Type == XPathNodeType.Element && xmlNodeInfo.Siblings.MoveNext())
            {
                XmlNodeInfo xmlNextNode = new XmlNodeInfo(XPathNodeType.Element, xmlNodeInfo.Root, xmlNodeInfo.Parent, xmlNodeInfo.Siblings, xmlNodeInfo.FirstSibling, xmlNodeInfo, xmlNodeInfo.Siblings.Current);
                xmlNodeInfo.NextSibling = xmlNextNode;
                xmlNodeInfo = xmlNextNode;
                return true;
            }

            return false;
        }

        public override bool MoveToPrevious()
        {
            if (xmlNodeInfo.PreviousSibling == null)
            {
                return false;
            }
            else
            {
                xmlNodeInfo = xmlNodeInfo.PreviousSibling;
                return true;
            }
        }

        public override bool MoveToFirst()
        {
            Debug.Assert(xmlNodeInfo.FirstSibling != null);
            xmlNodeInfo = xmlNodeInfo.FirstSibling;
            return true;
        }

        public override bool MoveToFirstChild()
        {
            // Check whether it has already been set.

            if (xmlNodeInfo.FirstChild != null)
            {
                xmlNodeInfo = xmlNodeInfo.FirstChild;
                return true;
            }

            switch (xmlNodeInfo.Type)
            {
                case XPathNodeType.Root:
                case XPathNodeType.Element:
                    {
                        IEnumerator enumerator = xmlNodeInfo.SiteMapNode.ChildNodes.GetEnumerator();
                        if (enumerator.MoveNext())
                        {
                            XmlNodeInfo xmlChildInfo = new XmlNodeInfo(XPathNodeType.Element, xmlNodeInfo.Root, xmlNodeInfo, enumerator, null, null, enumerator.Current);
                            xmlNodeInfo.FirstChild = xmlChildInfo;
                            xmlNodeInfo = xmlChildInfo;
                            return true;
                        }
                    }

                    break;
            }

            return false;
        }

        public override bool MoveToParent()
        {
            if (xmlNodeInfo == xmlNodeInfo.Root)
            {
                return false;
            }
            else
            {
                xmlNodeInfo = xmlNodeInfo.Parent;
                return true;
            }
        }

        public override void MoveToRoot()
        {
            xmlNodeInfo = xmlNodeInfo.Root;
        }

        public override bool MoveTo(XPathNavigator other)
        {
            NavigationSiteMapNavigator navigator = other as NavigationSiteMapNavigator;
            if (navigator != null)
            {
                xmlNodeInfo = navigator.xmlNodeInfo;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool MoveToId(string id)
        {
            return false;
        }

        public override bool IsSamePosition(XPathNavigator other)
        {
            NavigationSiteMapNavigator navigator = other as NavigationSiteMapNavigator;
            if (navigator != null)
                return xmlNodeInfo == navigator.xmlNodeInfo;
            else
                return false;
        }

        public override bool HasChildren
        {
            get
            {
                if (xmlNodeInfo.FirstChild != null)
                    return true;

                if (xmlNodeInfo.Type == XPathNodeType.Element)
                {
                    IEnumerator children = xmlNodeInfo.SiteMapNode.ChildNodes.GetEnumerator();
                    if (children.MoveNext())
                        return true;
                }

                return false;
            }
        }

        #endregion

        private class XmlNodeInfo
        {
            private readonly XmlNodeInfo root;
            private readonly XmlNodeInfo parent;

            // Children.

            private XmlNodeInfo firstChild;

            // Siblings.

            private readonly IEnumerator siblings;
            private readonly XmlNodeInfo firstSibling;
            private readonly XmlNodeInfo previousSibling;
            private XmlNodeInfo nextSibling;

            // Attributes.

            private XmlNodeInfo firstAttribute;

            private readonly object nodeObject; // Node - NavigationSiteMapNode, Attribute - NavigationSiteMapAttribute, Value - object
            private readonly XPathNodeType type;

            public XmlNodeInfo(XPathNodeType type, XmlNodeInfo root, XmlNodeInfo parent, IEnumerator siblings, XmlNodeInfo firstSibling, XmlNodeInfo previousSibling, object nodeObject)
            {
                this.type = type;
                this.root = root;
                this.parent = parent;
                this.siblings = siblings;
                this.firstSibling = firstSibling ?? this;
                this.previousSibling = previousSibling;
                this.nodeObject = nodeObject;
                Debug.Assert(this.nodeObject != null);
                if (this.nodeObject is SiteMapNode)
                    Debug.Assert(this.nodeObject is NavigationSiteMapNode);
            }

            public XmlNodeInfo(object nodeObject)
            {
                root = this;
                firstSibling = this;
                type = XPathNodeType.Element;
                this.nodeObject = nodeObject;
                if (this.nodeObject is SiteMapNode)
                    Debug.Assert(this.nodeObject is NavigationSiteMapNode);
            }

            public XmlNodeInfo Root
            {
                get { return root; }
            }

            public XmlNodeInfo Parent
            {
                get { return parent; }
            }

            public XmlNodeInfo FirstChild
            {
                get { return firstChild; }
                set { firstChild = value; }
            }

            public IEnumerator Siblings
            {
                get { return siblings; }
            }

            public XmlNodeInfo NextSibling
            {
                get { return nextSibling; }
                set { nextSibling = value; }
            }

            public XmlNodeInfo PreviousSibling
            {
                get { return previousSibling; }
            }

            public XmlNodeInfo FirstSibling
            {
                get { return firstSibling; }
            }

            public XmlNodeInfo FirstAttribute
            {
                get { return firstAttribute; }
                set { firstAttribute = value; }
            }

            public object Object
            {
                get { return nodeObject; }
            }

            public NavigationSiteMapNode SiteMapNode
            {
                get { return nodeObject as NavigationSiteMapNode; }
            }

            public NavigationSiteMapAttribute SiteMapAttribute
            {
                get { return nodeObject as NavigationSiteMapAttribute; }
            }

            public XPathNodeType Type
            {
                get { return type; }
            }
        }
    }

    internal class NavigationSiteMapNodeSet : IEnumerable<SiteMapNode>
    {
        private readonly XPathNodeIterator iterator;

        internal NavigationSiteMapNodeSet(XPathNodeIterator iterator)
        {
            this.iterator = iterator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(iterator);
        }

        public IEnumerator<SiteMapNode> GetEnumerator()
        {
            return new Enumerator(iterator);
        }

        public int Count
        {
            get { return iterator.Count; }
        }

        private class Enumerator : IEnumerator<SiteMapNode>
        {
            private readonly XPathNodeIterator iterator;
            private bool isCurrentValid = false;

            public Enumerator(XPathNodeIterator iterator)
            {
                this.iterator = iterator;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public SiteMapNode Current
            {
                get
                {
                    if (isCurrentValid)
                        return ((NavigationSiteMapNavigator)iterator.Current).Object as SiteMapNode;
                    else
                        throw new InvalidOperationException();
                }
            }

            bool IEnumerator.MoveNext()
            {
                isCurrentValid = iterator.MoveNext();
                return isCurrentValid;
            }

            void IEnumerator.Reset()
            {
                throw new NotImplementedException();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }
}
