using LinkMe.Framework.Content;
using LinkMe.Web.Cms.ContentDisplayers;

namespace LinkMe.Web.Cms.ContentDisplayViews
{
    public class ContentItemDisplayView
        : ContentDisplayView
    {
        private ContentItem _item;

        public ContentItem Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
                Display();
            }
        }

        protected override IContentDisplayer GetContentDisplayer()
        {
            return CreateDisplayer(_item);
        }
    }
}
