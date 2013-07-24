using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Query.Search.JobAds;
using LinkMe.Web.Areas.Members.Controllers.Search;
using LinkMe.Web.Areas.Members.Models;

namespace LinkMe.Web.Areas.Members.Routes
{
    public static class SearchRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference PartialSearch { get; private set; }
        public static RouteReference LeftSide { get; private set; }
        public static RouteReference Results { get; private set; }
        public static RouteReference ApiSaveSearch { get; private set; }
        public static RouteReference ApiDeleteSearch { get; private set; }
        public static RouteReference ApiDeleteSearchAlert { get; private set; }
        public static RouteReference RecentSearch { get; private set; }
        public static RouteReference SavedSearch { get; private set; }
        public static RouteReference Searches { get; private set; }
        public static RouteReference RecentSearches { get; private set; }
        public static RouteReference PartialRecentSearches { get; private set; }
        public static RouteReference SavedSearches { get; private set; }
        public static RouteReference PartialSavedSearches { get; private set; }
        public static RouteReference ApiCreateSearchFromCurrent { get; private set; }
        public static RouteReference ApiCreateAlertFromSearch { get; private set; }
        public static RouteReference ApiCreateAlertFromPrevious { get; private set; }
        public static RouteReference ApiRenameSearch { get; private set; }
        public static RouteReference ApiSearch { get; private set; }
        public static RouteReference SaveSearch { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<SearchController, JobAdSearchCriteria, JobAdsPresentationModel>("search/jobs", c => c.Search);
            PartialSearch = context.MapAreaRoute<SearchController, JobAdSearchCriteria, JobAdsPresentationModel>("search/jobs/partial", c => c.PartialSearch);
            ApiSearch = context.MapAreaRoute<SearchApiController, JobAdSearchCriteria, JobAdsPresentationModel>("search/jobs/api", c => c.Search);

            LeftSide = context.MapAreaRoute<SearchController>("search/jobs/leftside", c => c.LeftSide);
            Results = context.MapAreaRoute<SearchController>("search/jobs/results", c => c.Results);

            context.MapAreaRoute<SearchesWebController, Guid>("members/searches/{searchId}/delete", c => c.DeleteSearch);

            Searches = context.MapAreaRoute<SearchesMobileController>("members/searches", c => c.Searches);

            context.MapAreaRoute<SearchesMobileController>("members/searches/recent", c => c.RecentSearches);
            RecentSearches = context.MapAreaRoute<SearchesWebController>("members/searches/recent", c => c.RecentSearches);
            PartialRecentSearches = context.MapAreaRoute<SearchesWebController, Pagination>("members/searches/partial/recent", c => c.PartialRecentSearches);

            context.MapAreaRoute<SearchesMobileController>("members/searches/saved", c => c.SavedSearches);
            SavedSearches = context.MapAreaRoute<SearchesWebController>("members/searches/saved", c => c.SavedSearches);
            PartialSavedSearches = context.MapAreaRoute<SearchesWebController, Pagination>("members/searches/partial/saved", c => c.PartialSavedSearches);

            context.MapRedirectRoute("members/searches", RecentSearches);

            ApiSaveSearch = context.MapAreaRoute<SearchesApiController, ContactDetailsModel>("members/searches/api/save", c => c.SaveSearch);
            ApiDeleteSearch = context.MapAreaRoute<SearchesApiController, Guid>("members/searches/api/delete", c => c.DeleteSearch);
            ApiDeleteSearchAlert = context.MapAreaRoute<SearchesApiController, Guid>("members/alerts/api/delete", c => c.DeleteSearchAlert);
            ApiCreateSearchFromCurrent = context.MapAreaRoute<SearchesApiController, string, bool>("members/searches/api/createFromCurrent", c => c.CreateSearchFromCurrentSearch);
            ApiCreateAlertFromSearch = context.MapAreaRoute<SearchesApiController, Guid>("members/alerts/api/createFromSaved", c => c.CreateAlertFromSavedSearch);
            ApiCreateAlertFromPrevious = context.MapAreaRoute<SearchesApiController, Guid>("members/alerts/api/createFromPrevious", c => c.CreateAlertFromPreviousSearch);
            ApiRenameSearch = context.MapAreaRoute<SearchesApiController, Guid, string>("members/searches/api/rename", c => c.RenameSearch);

            RecentSearch = context.MapAreaRoute<SearchController, Guid>("search/jobs/recent/{recentSearchId}", c => c.Recent);
            SavedSearch = context.MapAreaRoute<SearchController, Guid>("search/jobs/saved/{savedSearchId}", c => c.Saved);

            SaveSearch = context.MapAreaRoute<SearchMobileController>("members/searches/save", c => c.SaveSearch);

            context.MapRedirectRoute("ui/registered/networkers/PreviousJobSearches.aspx", RecentSearches);
            context.MapRedirectRoute("ui/registered/networkers/JobSearchEmailAlerts.aspx", SavedSearches);
            context.MapRedirectRoute("search/jobs/tips", Search);
            context.MapRedirectRoute("search/jobs/Tips.aspx", Search);
            context.MapRedirectRoute("search/jobs/advanced", Search);
            context.MapRedirectRoute("search/jobs/advancedsearch.aspx", Search);
            context.MapRedirectRoute("search/jobs/SimpleSearch.aspx", Search);
            context.MapRedirectRoute("ui/unregistered/JobSearchForm.aspx", Search);
            context.MapRedirectRoute("ui/unregistered/JobSearchAdvancedForm.aspx", Search);
            context.MapRedirectRoute("guests/JobPreviousSearches.aspx", Search);
        }
    }
}
