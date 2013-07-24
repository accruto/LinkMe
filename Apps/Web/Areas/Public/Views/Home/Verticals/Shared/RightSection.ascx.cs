using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class RightSection
        : ViewUserControl
    {
        protected static readonly ReadOnlyUrl Image1Url = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/candidates/susi_sml.png");
        protected static readonly ReadOnlyUrl Image2Url = new ReadOnlyApplicationUrl("~/themes/communities/linkme/img/candidates/emily_sml.png");
    }
}
