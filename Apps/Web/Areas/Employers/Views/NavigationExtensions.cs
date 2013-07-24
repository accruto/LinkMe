using System.Web.Mvc;
using System.Web.Routing;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Context;

namespace LinkMe.Web.Areas.Employers.Views
{
    public static class NavigationExtensions
    {
        public static MvcHtmlString BackToLink(this HtmlHelper htmlHelper, CandidatesNavigation navigation, object htmlAttributes)
        {
            if (navigation is SuggestedCandidatesNavigation)
                return htmlHelper.NavigationLink("Back to suggested candidates", navigation, htmlAttributes);

            if (navigation is ManageCandidatesNavigation)
                return htmlHelper.NavigationLink("Back to manage candidates", navigation, htmlAttributes);

            if (navigation is BrowseCandidatesNavigation)
                return htmlHelper.NavigationLink("Back to browse candidates", navigation, htmlAttributes);

            if (navigation is MemberSearchNavigation)
                return htmlHelper.NavigationLink("Back to search results", navigation, htmlAttributes);

            if (navigation is FolderNavigation)
                return htmlHelper.NavigationLink("Back to folder", navigation, htmlAttributes);

            if (navigation is FlagListNavigation)
                return htmlHelper.NavigationLink("Back to folder", navigation, htmlAttributes);

            if (navigation is BlockListNavigation)
                return htmlHelper.NavigationLink("Back to block list", navigation, htmlAttributes);

            return null;
        }

        public static MvcHtmlString NavigationLink(this HtmlHelper helper, string linkText, CandidatesNavigation navigation, object htmlAttributes)
        {
            if (navigation is SuggestedCandidatesNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["jobAdId"] = ((SuggestedCandidatesNavigation)navigation).JobAdId;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink(linkText, JobAdsRoutes.SuggestedCandidates, routeValues, new RouteValueDictionary(htmlAttributes));
            }

            if (navigation is ManageCandidatesNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["jobAdId"] = ((ManageCandidatesNavigation)navigation).JobAdId;
                routeValues["status"] = ((ManageCandidatesNavigation) navigation).ApplicantStatus;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink(linkText, JobAdsRoutes.ManageJobAdCandidates, routeValues, new RouteValueDictionary(htmlAttributes));
            }

            if (navigation is BrowseCandidatesNavigation)
            {
                var browseNavigation = (BrowseCandidatesNavigation)navigation;
                if (navigation.Pagination == null || navigation.Pagination.Page == null)
                    return helper.CandidatesRouteLink(linkText, browseNavigation.Location, browseNavigation.SalaryBand, null, null);

                return helper.CandidatesRouteLink(linkText, browseNavigation.Location, browseNavigation.SalaryBand, navigation.Pagination.Page.Value, null);
            }

            if (navigation is MemberSearchNavigation)
                return helper.RouteRefLink(linkText, SearchRoutes.Results, null, htmlAttributes);

            if (navigation is FolderNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["folderId"] = ((FolderNavigation)navigation).FolderId;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink(linkText, CandidatesRoutes.Folder, routeValues, new RouteValueDictionary(htmlAttributes));
            }

            if (navigation is FlagListNavigation)
            {
                var routeValues = new RouteValueDictionary();
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink(linkText, CandidatesRoutes.FlagList, routeValues, new RouteValueDictionary(htmlAttributes));
            }

            if (navigation is BlockListNavigation)
            {
                var routeValues = new RouteValueDictionary();
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                var blockListType = ((BlockListNavigation)navigation).BlockListType;
                var route = blockListType == BlockListType.Temporary
                    ? CandidatesRoutes.TemporaryBlockList
                    : CandidatesRoutes.PermanentBlockList;
                return helper.RouteRefLink(linkText, route, routeValues, new RouteValueDictionary(htmlAttributes));
            }

            return null;
        }

        public static MvcHtmlString BreadcrumbLink(this HtmlHelper helper, CandidatesNavigation navigation)
        {
            if (navigation is SuggestedCandidatesNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["jobAdId"] = ((SuggestedCandidatesNavigation)navigation).JobAdId;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink("Suggested candidates", JobAdsRoutes.SuggestedCandidates, routeValues);
            }

            if (navigation is ManageCandidatesNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["jobAdId"] = ((ManageCandidatesNavigation)navigation).JobAdId;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink("Manage candidates", JobAdsRoutes.PartialManageJobAdCandidates, routeValues);
            }

            if (navigation is BrowseCandidatesNavigation)
            {
                var browseNavigation = (BrowseCandidatesNavigation)navigation;
                if (navigation.Pagination == null || navigation.Pagination.Page == null)
                    return helper.CandidatesRouteLink("Browse candidates", browseNavigation.Location,
                        browseNavigation.SalaryBand, null, null);

                return helper.CandidatesRouteLink("Browse candidates", browseNavigation.Location,
                    browseNavigation.SalaryBand, navigation.Pagination.Page.Value, null);
            }

            if (navigation is MemberSearchNavigation)
                return helper.RouteRefLink("Search results", SearchRoutes.Results, null);

            if (navigation is FolderNavigation)
            {
                var routeValues = new RouteValueDictionary();
                routeValues["folderId"] = ((FolderNavigation)navigation).FolderId;
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink("Folder", CandidatesRoutes.Folder, routeValues);
            }

            if (navigation is FlagListNavigation)
            {
                var routeValues = new RouteValueDictionary();
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                return helper.RouteRefLink("Flagged candidates", CandidatesRoutes.FlagList, routeValues);
            }

            if (navigation is BlockListNavigation)
            {
                var routeValues = new RouteValueDictionary();
                if (navigation.Pagination != null && navigation.Pagination.Page != null)
                    routeValues["Page"] = navigation.Pagination.Page.Value;
                if (navigation.Pagination != null && navigation.Pagination.Items != null)
                    routeValues["Items"] = navigation.Pagination.Items.Value;

                var blockListType = ((BlockListNavigation)navigation).BlockListType;
                var routeName = blockListType == BlockListType.Temporary
                    ? CandidatesRoutes.TemporaryBlockList
                    : CandidatesRoutes.PermanentBlockList;
                return helper.RouteRefLink("Block list", routeName, routeValues);
            }

            return null;
        }
    }
}
