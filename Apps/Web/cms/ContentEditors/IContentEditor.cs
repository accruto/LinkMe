using System.Reflection;
using System.Web.UI;
using LinkMe.Framework.Content;

namespace LinkMe.Web.Cms.ContentEditors
{
    public interface IContentEditor
    {
        void AddControls(Control container);
        void UpdateEditor();
        void UpdateItem();
        bool IsValid { get; }
    }

    public interface IItemContentEditorFactory
    {
        IContentEditor CreateEditor(ContentItem item);
    }

    public interface IPropertyContentEditorFactory
    {
        IContentEditor CreateEditor(object instance, PropertyInfo propertyInfo);
    }
}
