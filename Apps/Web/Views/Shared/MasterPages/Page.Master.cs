using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Views.Shared.MasterPages
{
    public class Page
        : ViewMasterPage
    {
        protected static readonly ReadOnlyApplicationUrl HomeLogoUrl = new ReadOnlyApplicationUrl("~/ui/images/universal/logo-home.png");
        protected static readonly ReadOnlyApplicationUrl HomeLogoHiLightUrl = new ReadOnlyApplicationUrl("~/ui/images/universal/logo-home-hilight.png");
    }
}
