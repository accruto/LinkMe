using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Query.Search.Members;
using LinkMe.Web.Areas.Employers.Controllers.Search;
using LinkMe.Web.Areas.Employers.Models;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class SearchRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference PartialSearch { get; private set; }
        public static RouteReference Tips { get; private set; }
        public static RouteReference Results { get; private set; }
        public static RouteReference Saved { get; private set; }

        public static RouteReference PartialSearches { get; private set; }
        public static RouteReference ApiSaveSearch { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<SearchController, MemberSearchCriteria, CandidatesPresentationModel, bool?>("search/candidates", c => c.Search);
            PartialSearch = context.MapAreaRoute<SearchController, MemberSearchCriteria, CandidatesPresentationModel>("search/candidates/partial", c => c.PartialSearch);
            Tips = context.MapAreaRoute<SearchController>("search/candidates/tips", c => c.Tips);
            Results = context.MapAreaRoute<SearchController, bool?>("search/candidates/results", c => c.Results);
            Saved = context.MapAreaRoute<SearchController, Guid>("search/candidates/saved/{savedSearchId}", c => c.Saved);

            context.MapAreaRoute<SearchesController>("employers/searches", c => c.Searches);
            PartialSearches = context.MapAreaRoute<SearchesController, Pagination, int>("employers/searches/partial", c => c.PartialSearches);

            ApiSaveSearch = context.MapAreaRoute<SearchesApiController, string, bool>("employers/searches/api/save", c => c.SaveSearch);

            context.MapRedirectRoute("search/resumes/SimpleSearch.aspx", Search);
            context.MapRedirectRoute("search/resumes/AdvancedSearch.aspx", Search);
            context.MapRedirectRoute("search/candidates/current", Results);
            context.MapRedirectRoute("search/resumes/Tips.aspx", Tips);
        }
    }
}
