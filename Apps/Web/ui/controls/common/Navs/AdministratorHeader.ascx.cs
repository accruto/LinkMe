using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Administrators.Routes;
using LinkMe.Web.Areas.Public.Routes;
using HomeRoutes=LinkMe.Web.Areas.Administrators.Routes.HomeRoutes;

namespace LinkMe.Web.UI.Controls.Common.Navs
{
    public partial class AdministratorHeader
        : LinkMeUserControl
    {
        protected static ReadOnlyUrl HomeUrl { get { return HomeRoutes.Home.GenerateUrl(); } }
        protected static ReadOnlyUrl LogOutUrl { get { return LoginsRoutes.LogOut.GenerateUrl(); } }

        protected static ReadOnlyUrl SearchMembersUrl { get { return MembersRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchEmployersUrl { get { return EmployersRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchOrganisationsUrl { get { return OrganisationsRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchEnginesUrl { get { return SearchRoutes.SearchEngines.GenerateUrl(); } }

        protected static ReadOnlyUrl CampaignsUrl { get { return CampaignsRoutes.Index.GenerateUrl(); } }
    }
}