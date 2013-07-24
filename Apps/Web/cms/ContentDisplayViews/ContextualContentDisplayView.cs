using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Domain.Roles.Affiliations.Communities;
using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;
using LinkMe.Web.Cms.ContentDisplayers;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.Cms.ContentDisplayViews
{
    public class ContextualContentDisplayView<T>
        : ContentDisplayView
        where T : ContentItem
    {
        private readonly IContentEngine _contentEngine = Container.Current.Resolve<IContentEngine>();

        private string _itemName;
        private string _templateUrl;
        private ContentItem _item;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        public string TemplateUrl
        {
            get { return _templateUrl; }
            set { _templateUrl = value; }
        }

        public ContentItem Item
        {
            get { return _item; }
        }

        protected override IContentDisplayer GetContentDisplayer()
        {
            // Get the current vertical.

            var id = ActivityContext.Current.Vertical.Id;

            // Get the content appropiate for the item type and vertical.

            _item = _contentEngine.GetContentItem<T>(_itemName, id);
            return CreateDisplayer(_item, _templateUrl);
        }
    }

    public class HtmlContextualContentDisplayView
        : ContextualContentDisplayView<HtmlContentItem>
    {
    }

    public class SectionContextualContentDisplayView
        : ContextualContentDisplayView<SectionContentItem>
    {
    }

    public class CommunityHeaderContextualContentDisplayView
        : ContextualContentDisplayView<CommunityHeaderContentItem>
    {
    }

    public class CommunityFooterContextualContentDisplayView
        : ContextualContentDisplayView<CommunityFooterContentItem>
    {
    }
}
