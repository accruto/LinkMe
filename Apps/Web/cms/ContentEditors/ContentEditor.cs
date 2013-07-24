using System;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Web.UI;

namespace LinkMe.Web.Cms.ContentEditors
{
    public abstract class ContentEditor
        : IContentEditor
    {
        private Control _container;

        public static IContentEditor CreateEditor(ContentItem item)
        {
            if (item == null)
                return null;

            // If the item has a editor then use that.

            IContentEditor editor = ContentManager.GetEditor(item);
            if (editor != null)
                return editor;

            // Create an editor for all of the editable properties of the item's type.

            return new PropertiesContentEditor(item);
        }

        protected static IContentEditor CreateEditor(ContentAttribute attribute, object instance, PropertyInfo propertyInfo)
        {
            if (attribute == null)
                return null;

            // If the item has a editor then use that.

            IContentEditor editor = ContentManager.GetEditor(attribute, instance, propertyInfo);
            if (editor != null)
                return editor;

            // Cannot display.

            return null;
        }

        protected static IContentEditor CreateEditor(Type type, object instance, PropertyInfo propertyInfo)
        {
            if (type == null)
                return null;

            // If the item has a editor then use that.

            IContentEditor editor = ContentManager.GetEditor(type, instance, propertyInfo);
            if (editor != null)
                return editor;

            // Cannot display.

            return null;
        }

        void IContentEditor.AddControls(Control container)
        {
            _container = container;
            AddControls(container);
        }

        void IContentEditor.UpdateEditor()
        {
            UpdateEditor();
        }

        void IContentEditor.UpdateItem()
        {
            UpdateItem();
        }

        bool IContentEditor.IsValid
        {
            get { return IsValid; }
        }

        protected abstract void AddControls(Control container);
        protected abstract void UpdateEditor();
        protected abstract void UpdateItem();
        protected abstract bool IsValid { get; }

        protected void AddError(string message)
        {
            if (_container != null)
            {
                if (_container.Page is LinkMePage)
                    ((LinkMePage)_container.Page).AddError(message);
            }
        }

        protected static Label CreateLabel(string text)
        {
            var label = new Label {Text = text, CssClass = "small-form-label"};
            return label;
        }

        protected static Control CreatePanel()
        {
            return new HtmlGenericControl("div");
        }

        protected static void AddLabel(Control container, string text)
        {
            var label = new Label {ID = container.ID + "_label", Text = text, CssClass = "small-form-label"};
            container.Controls.Add(label);
        }

        protected static Control AddProperties(Control container)
        {
            var table = new HtmlGenericControl("table");
            table.Attributes["width"] = "100%";
            container.Controls.Add(table);

            var tbody = new HtmlGenericControl("tbody");
            table.Controls.Add(tbody);

            return tbody;
        }

        protected static void AddProperty(Control properties, Label label, Control property)
        {
            var tr = new HtmlGenericControl("tr");
            properties.Controls.Add(tr);

            var td = new HtmlGenericControl("td");
            td.Attributes["width"] = "15%";
            td.Attributes["valign"] = "top";
            td.Attributes["align"] = "right";
            td.Attributes["style"] = "padding-top: 4pt;";
            tr.Controls.Add(td);

            td.Controls.Add(label);
            
            td = new HtmlGenericControl("td");
            tr.Controls.Add(td);

            td.Controls.Add(property);
        }

        protected static PropertyInfo GetPropertyInfo(ContentItem parentItem, string name)
        {
            if (parentItem == null || name == null)
                return null;
            return parentItem.GetType().GetProperty(name);
        }
    }
}
