using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Public.Models.Home;
using LinkMe.Web.Areas.Public.Routes;

namespace LinkMe.Web.Areas.Public.Views.Home
{
    public class ExposeYourself
        : ViewUserControl<ReferenceModel>
    {
        protected static ReadOnlyUrl JoinUrl { get { return JoinRoutes.Join.GenerateUrl(); } }
    }
}
