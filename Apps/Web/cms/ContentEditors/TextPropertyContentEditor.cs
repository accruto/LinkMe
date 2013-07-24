using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class TextPropertyContentEditor
        : ContentEditor
    {
        private readonly object _instance;
        private readonly PropertyInfo _propertyInfo;
        private TextBox _textBox;

        internal TextPropertyContentEditor(object instance, PropertyInfo propertyInfo)
        {
            _instance = instance;
            _propertyInfo = propertyInfo;
        }

        protected override void AddControls(Control container)
        {
            _textBox = new TextBox {CssClass = "generic-form-input"};
            container.Controls.Add(_textBox);
        }

        protected override void UpdateEditor()
        {
            // Reset the text.

            if (_textBox != null)
                _textBox.Text = _propertyInfo.GetValue(_instance, null) as string;
        }

        protected override void UpdateItem()
        {
            _propertyInfo.SetValue(_instance, _textBox != null ? _textBox.Text : string.Empty, null);
        }

        protected override bool IsValid
        {
            get { return true; }
        }
    }

    public class TextPropertyContentEditorFactory
        : IPropertyContentEditorFactory
    {
        IContentEditor IPropertyContentEditorFactory.CreateEditor(object instance, PropertyInfo propertyInfo)
        {
            if (instance != null && propertyInfo != null && propertyInfo.PropertyType == typeof(string))
                return new TextPropertyContentEditor(instance, propertyInfo);
            return null;
        }
    }
}
