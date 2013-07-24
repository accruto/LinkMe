using LinkMe.Framework.Content;
using LinkMe.Web.Cms.ContentEditors;

namespace LinkMe.Web.Cms.ContentEditViews
{
    public class ContentItemMetaDataEditView
        : ContentEditView
    {
        protected override IContentEditor CreateContentEditor(ContentItem item)
        {
            return new MetaDataContentEditor(item);
        }
    }
}
