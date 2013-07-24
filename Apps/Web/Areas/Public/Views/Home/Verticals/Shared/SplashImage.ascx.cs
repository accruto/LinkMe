using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class SplashImage
        : ViewUserControl
    {
        protected static ReadOnlyUrl JoinUrl { get { return Routes.JoinRoutes.Join.GenerateUrl(); } }
        protected static ReadOnlyUrl EmployerUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        protected static readonly ReadOnlyUrl Step1ImageUrl = new ReadOnlyApplicationUrl("~/ui/images/home/banner_step_1.png");
        protected static readonly ReadOnlyUrl StepArrowImageUrl = new ReadOnlyApplicationUrl("~/ui/images/home/banner_step_arrow.png");
        protected static readonly ReadOnlyUrl Step2ImageUrl = new ReadOnlyApplicationUrl("~/ui/images/home/banner_step_2.png");
    }
}
