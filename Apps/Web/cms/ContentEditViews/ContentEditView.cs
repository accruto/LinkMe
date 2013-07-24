using LinkMe.Framework.Content;
using LinkMe.Web.Cms.ContentEditors;

namespace LinkMe.Web.Cms.ContentEditViews
{
    public abstract class ContentEditView
        : ContentView
    {
        private ContentItem _item;
        private IContentEditor _editor;

        public ContentItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                CreateEditor();
            }
        }

        protected abstract IContentEditor CreateContentEditor(ContentItem item);

        private void CreateEditor()
        {
            _editor = CreateContentEditor(_item);
            if (_editor != null)
            {
                Controls.Clear();
                _editor.AddControls(this);
            }
        }

        public void UpdateItem()
        {
            if (_editor != null)
                _editor.UpdateItem();
        }

        public void UpdateEditor()
        {
            if (_editor != null)
                _editor.UpdateEditor();
        }

        public bool IsValid
        {
            get { return _editor != null ? _editor.IsValid : true; }
        }
    }
}
