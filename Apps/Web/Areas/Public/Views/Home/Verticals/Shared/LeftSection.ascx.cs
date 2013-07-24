using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Public.Models.Home;

namespace LinkMe.Web.Areas.Public.Views.Home.Verticals.Shared
{
    public class LeftSection
        : ViewUserControl<ReferenceModel>
    {
        protected static ReadOnlyUrl EmployerUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
    }
}
