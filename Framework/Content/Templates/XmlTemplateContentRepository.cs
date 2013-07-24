using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Linq;

namespace LinkMe.Framework.Content.Templates
{
    /// <summary>
    /// This class could be made more generic to deal with any content item in an XML file,
    /// tweaks would be needed to deal with non-TemplateContentItem types and the Xml format would need
    /// some revision.
    /// </summary>
    public class XmlTemplateContentRepository
        : IContentRepository
    {
        private readonly string _templateFolder;
        private readonly string _templateSearchPattern;
        private IList<ContentItem> _contentItems;

        public XmlTemplateContentRepository(string templateFolder, string templateSearchPattern)
        {
            _templateFolder = FileSystem.GetSourcePath(templateFolder);
            _templateSearchPattern = templateSearchPattern;
        }

        void IContentRepository.CreateContentItem(ContentItem item)
        {
            throw new NotImplementedException();
        }

        void IContentRepository.UpdateContentItem(ContentItem item)
        {
            throw new NotImplementedException();
        }

        void IContentRepository.DeleteContentItem(Guid id)
        {
            throw new NotImplementedException();
        }

        void IContentRepository.EnableContentItem(Guid id)
        {
            throw new NotImplementedException();
        }

        void IContentRepository.DisableContentItem(Guid id)
        {
            throw new NotImplementedException();
        }

        ContentItem IContentRepository.GetContentItem(string type, string name, Guid? verticalId, bool includeDisabled)
        {
            throw new NotImplementedException();
        }

        IList<ContentItem> IContentRepository.GetContentItems(Guid? verticalId)
        {
            throw new NotImplementedException();
        }

        IList<ContentItem> IContentRepository.GetContentItems(string type)
        {
            Initialise();
            return (from i in _contentItems
                    where i.GetType().Name == type
                    select i).ToList();
        }

        private void Initialise()
        {
            if (_contentItems != null)
                return;

            var contentItems = new List<ContentItem>();
            string lastFile = null;

            try
            {
                foreach (var file in Directory.GetFiles(_templateFolder, _templateSearchPattern))
                {
                    lastFile = file;
                    if (!File.Exists(file))
                        throw new FileNotFoundException("The email templates XML file, '" + file + "', does not exist.", file);

                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(file);

                    // Load the masters first.

                    foreach (XmlNode xmlNode in xmlDocument.SelectNodes("//MasterTemplateContentItem"))
                        contentItems.AddRange(CreateTemplateContentItems<MasterTemplateContentItem>(xmlNode));

                    // Load the definitions themselves.

                    foreach (XmlNode xmlNode in xmlDocument.SelectNodes("//TemplateContentItem"))
                        contentItems.AddRange(CreateTemplateContentItems<TemplateContentItem>(xmlNode));
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to read email templates from XML file '" + lastFile + "'.", ex);
            }

            _contentItems = contentItems;
        }

        #region Static methods

        private static IEnumerable<T> CreateTemplateContentItems<T>(XmlNode xmlNode)
            where T : TemplateBaseContentItem, new()
        {
            var name = GetAttribute(xmlNode, "Name");
            var master = GetAttribute(xmlNode, "Master");

            var list = new List<T>();
            foreach (XmlNode xmlVerticalNode in xmlNode.SelectNodes("VerticalContentItem"))
            {
                Guid? verticalId = null;
                var key = GetAttribute(xmlVerticalNode, "VerticalId");
                if (!string.IsNullOrEmpty(key))
                    verticalId = new Guid(key);

                var templateContentItem = new T
                {
                    Name = name,
                    VerticalId = verticalId,
                    Subject = GetElement(xmlVerticalNode, "Subject"),
                    Views = CreateViewContentItems(xmlVerticalNode)
                };
                if (templateContentItem is TemplateContentItem)
                    (templateContentItem as TemplateContentItem).Master = master;
                list.Add(templateContentItem);
            }

            return list;
        }

        private static IList<ViewContentItem> CreateViewContentItems(XmlNode xmlNode)
        {
            return (from XmlNode viewNode in xmlNode.SelectNodes("ViewContentItem") select CreateViewContentItem(viewNode)).ToList();
        }

        private static ViewContentItem CreateViewContentItem(XmlNode xmlNode)
        {
            var mimeType = GetAttribute(xmlNode, "MimeType");
            var contentItem = new ViewContentItem { MimeType = mimeType };

            // Look for parts.

            var parts = new List<ViewPartContentItem>();
            var partNodes = xmlNode.SelectNodes("ViewPartContentItem");
            if (partNodes.Count == 0)
            {
                // The body is the content of the element.

                parts.Add(new ViewPartContentItem { Text = xmlNode.InnerText });
            }
            else
            {
                foreach (XmlNode partNode in partNodes)
                {
                    var name = GetAttribute(partNode, "Name");
                    parts.Add(new ViewPartContentItem { Name = name, Text = partNode.InnerText });
                }
            }

            contentItem.Parts = parts;
            return contentItem;
        }

        private static string GetAttribute(XmlNode xmlNode, string name)
        {
            var attribute = xmlNode.Attributes[name];
            return attribute == null ? null : attribute.Value;
        }

        private static string GetElement(XmlNode xmlNode, string name)
        {
            var element = xmlNode.SelectSingleNode(name);
            return element == null ? null : element.InnerText;
        }

        #endregion
    }
}