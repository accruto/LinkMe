using System.Web.UI;
using System.Web.UI.WebControls;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;

namespace LinkMe.Web.Cms.ContentEditors
{
    public class TextContentEditor
        : ContentEditor
    {
        private readonly TextContentItem _item;
        private TextBox _textBox;

        internal TextContentEditor(TextContentItem item)
        {
            _item = item;
        }

        protected override void AddControls(Control container)
        {
            if (_item != null)
            {
                var properties = AddProperties(container);

                var label = CreateLabel("Text");
                var panel = CreatePanel();

                _textBox = new TextBox
                {
                    TextMode = TextBoxMode.SingleLine,
                    Width = new Unit("450px"),
                    CssClass = "wide-form-input",
                    ID = "edit_text"
                };
                panel.Controls.Add(_textBox);

                AddProperty(properties, label, panel);
            }
        }

        protected override void UpdateEditor()
        {
            // Reset the text.

            if (_textBox != null)
            {
                if (_item != null && !string.IsNullOrEmpty(_item.Text))
                    _textBox.Text = _item.Text;
                else
                    _textBox.Text = string.Empty;
            }
        }

        protected override void UpdateItem()
        {
            if (_item != null)
                _item.Text = _textBox != null ? _textBox.Text : string.Empty;
        }

        protected override bool IsValid
        {
            get { return true; }
        }
    }

    public class TextContentEditorFactory
        : IItemContentEditorFactory
    {
        IContentEditor IItemContentEditorFactory.CreateEditor(ContentItem item)
        {
            if (item is TextContentItem)
                return new TextContentEditor(item as TextContentItem);
            return null;
        }
    }
}
