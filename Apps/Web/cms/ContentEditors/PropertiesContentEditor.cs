using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class PropertiesContentEditor
        : ContentEditor
    {
        private readonly ContentItem _parentItem;
        private readonly IList<IContentEditor> _propertyEditors = new List<IContentEditor>();

        public PropertiesContentEditor(ContentItem parentItem)
        {
            if (parentItem == null)
                throw new ArgumentNullException("parentItem");

            _parentItem = parentItem;
        }

        protected override void AddControls(Control container)
        {
            // Create an overall properties control.

            Control properties = AddProperties(container);

            // Iterate through all the properties of the item.

            foreach (PropertyInfo propertyInfo in _parentItem.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                // Include it if it is derived from CmsContentItem or has a Content derived attribute.

                if (propertyInfo.PropertyType == typeof(ContentItem) || propertyInfo.PropertyType.IsSubclassOf(typeof(ContentItem)))
                    AddPropertyEditor(properties, _parentItem, propertyInfo);
                else if (IsContent(propertyInfo))
                    AddPropertyEditor(properties, _parentItem, propertyInfo);
            }
        }

        private static bool IsContent(ICustomAttributeProvider propertyInfo)
        {
            // The base class just defines a display name so look for a derived class.

            var attributes = (ContentAttribute[])propertyInfo.GetCustomAttributes(typeof(ContentAttribute), true);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].GetType() != typeof(ContentAttribute);
            return false;
        }

        protected override void UpdateEditor()
        {
            foreach (IContentEditor editor in _propertyEditors)
                editor.UpdateEditor();
        }

        protected override void UpdateItem()
        {
            foreach (IContentEditor editor in _propertyEditors)
                editor.UpdateItem();
        }

        protected override bool IsValid
        {
            get
            {
                foreach (IContentEditor editor in _propertyEditors)
                {
                    if (!editor.IsValid)
                        return false;
                }

                return true;
            }
        }

        private void AddPropertyEditor(Control container, ContentItem item, PropertyInfo propertyInfo)
        {
            // Add a label.

            string displayName = propertyInfo.Name;

            var attributes = (ContentAttribute[])propertyInfo.GetCustomAttributes(typeof(ContentAttribute), true);
            if (attributes != null && attributes.Length > 0)
                displayName = attributes[0].DisplayName;

            Label label = CreateLabel(displayName);

            // Create an appropriate property editor.

            IContentEditor editor = GetContentEditor(item, propertyInfo.Name);
            _propertyEditors.Add(editor);

            Control panel = CreatePanel();
            editor.AddControls(panel);
            
            // Add teh combination.

            AddProperty(container, label, panel);
        }

        private static IContentEditor GetContentEditor(ContentItem parentItem, string childName)
        {
            // Create an editor based on the type of the property.

            PropertyInfo propertyInfo = GetPropertyInfo(parentItem, childName);
            if (propertyInfo == null)
                return null;

            // If it is content item then create the editor for it.

            if (propertyInfo.PropertyType == typeof(ContentItem) || propertyInfo.PropertyType.IsSubclassOf(typeof(ContentItem)))
                return CreateEditor(propertyInfo.GetValue(parentItem, null) as ContentItem);

            // If it is marked as content then get an editor for it.

            var attributes = (ContentAttribute[])propertyInfo.GetCustomAttributes(typeof(ContentAttribute), true);
            if (attributes != null && attributes.Length > 0)
                return CreateEditor(attributes[0], parentItem, propertyInfo);

            return null;
        }
    }
}