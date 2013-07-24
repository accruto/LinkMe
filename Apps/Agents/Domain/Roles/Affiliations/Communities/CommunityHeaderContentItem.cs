using LinkMe.Framework.Content;
using LinkMe.Framework.Content.ContentItems;

namespace LinkMe.Apps.Agents.Domain.Roles.Affiliations.Communities
{
    public class CommunityHeaderContentItem
        : ContentItem
    {
        private const string ContentProperty = "Content";

        [Content(ContentProperty)]
        public HtmlContentItem Content
        {
            get { return GetChild<HtmlContentItem>(ContentProperty); }
            set { SetChild(ContentProperty, value); }
        }
    }
}