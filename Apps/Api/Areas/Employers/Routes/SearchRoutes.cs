using System;
using System.Web.Mvc;
using LinkMe.Apps.Api.Areas.Employers.Controllers.Search;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Api.Areas.Employers.Routes
{
    public class SearchRoutes
    {
        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            context.MapAreaRoute<SearchApiController, MemberSearchCriteria, Pagination>(1, "search/candidates", c => c.Search);

            context.MapAreaRoute<SearchesApiController>(1, "employers/searches", c => c.Searches);
            context.MapAreaRoute<SearchesApiController, Guid, string, bool, MemberSearchCriteria, string>(1, "employers/searches/{id}", c => c.EditSearch);
        }
    }
}
