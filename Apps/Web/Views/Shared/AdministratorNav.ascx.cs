using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Administrators.Routes;

namespace LinkMe.Web.Views.Shared
{
    public class AdministratorNav
        : ViewUserControl
    {
        protected static ReadOnlyUrl SearchMembersUrl { get { return MembersRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchEmployersUrl { get { return EmployersRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchOrganisationsUrl { get { return OrganisationsRoutes.Search.GenerateUrl(); } }
        protected static ReadOnlyUrl SearchEnginesUrl { get { return SearchRoutes.SearchEngines.GenerateUrl(); } }

        protected static ReadOnlyUrl CampaignsUrl { get { return CampaignsRoutes.Index.GenerateUrl(); } }
    }
}