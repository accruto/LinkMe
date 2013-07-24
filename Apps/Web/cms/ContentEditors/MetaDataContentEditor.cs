using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class MetaDataContentEditor
        : ContentEditor
    {
        private readonly ContentItem _item;
        private readonly IList<IContentEditor> _propertyEditors = new List<IContentEditor>();

        public MetaDataContentEditor(ContentItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            _item = item;
        }

        protected override void AddControls(Control container)
        {
            // Create an overall properties control.

            Control properties = AddProperties(container);

            // Add a property for each piece of metadata.

            PropertyInfo propertyInfo = _item.GetType().GetProperty("IsEnabled");
            if (propertyInfo != null)
                AddPropertyEditor(properties, _item, propertyInfo, "Enabled");
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

        private void AddPropertyEditor(Control container, ContentItem item, PropertyInfo propertyInfo, string displayName)
        {
            // Add a label.

            Label label = CreateLabel(displayName);

            // Create an appropriate property editor.

            IContentEditor editor = GetContentEditor(item, propertyInfo);
            _propertyEditors.Add(editor);

            Control panel = CreatePanel();
            editor.AddControls(panel);

            // Add the combination.

            AddProperty(container, label, panel);
        }

        private static IContentEditor GetContentEditor(ContentItem parentItem, PropertyInfo propertyInfo)
        {
            return CreateEditor(propertyInfo.PropertyType, parentItem, propertyInfo);
        }
    }
}