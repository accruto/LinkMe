using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class BoolPropertyContentEditor
        : ContentEditor
    {
        private readonly object _instance;
        private readonly PropertyInfo _propertyInfo;
        private CheckBox _checkBox;

        internal BoolPropertyContentEditor(object instance, PropertyInfo propertyInfo)
        {
            _instance = instance;
            _propertyInfo = propertyInfo;
        }

        protected override void AddControls(Control container)
        {
            _checkBox = new CheckBox();
            container.Controls.Add(_checkBox);
        }

        protected override void UpdateEditor()
        {
            if (_checkBox != null)
            {
                object value = _propertyInfo.GetValue(_instance, null);
                if (value is bool)
                    _checkBox.Checked = (bool) value;
            }
        }

        protected override void UpdateItem()
        {
            _propertyInfo.SetValue(_instance, _checkBox != null ? _checkBox.Checked : false, null);
        }

        protected override bool IsValid
        {
            get { return true; }
        }
    }

    public class BoolPropertyContentEditorFactory
        : IPropertyContentEditorFactory
    {
        IContentEditor IPropertyContentEditorFactory.CreateEditor(object instance, PropertyInfo propertyInfo)
        {
            if (instance != null && propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
                return new BoolPropertyContentEditor(instance, propertyInfo);
            return null;
        }
    }
}
