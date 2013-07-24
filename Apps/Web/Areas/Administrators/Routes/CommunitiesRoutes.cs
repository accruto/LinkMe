using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Communities;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class CommunitiesRoutes
    {
        public static RouteReference Index { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference Custodians { get; private set; }
        public static RouteReference NewCustodian { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Index = context.MapAreaRoute<CommunitiesController>("administrators/communities", c => c.Index);
            Edit = context.MapAreaRoute<CommunitiesController, Guid>("administrators/communities/{id}", c => c.Edit);
            Custodians = context.MapAreaRoute<CommunitiesController, Guid>("administrators/communities/{id}/custodians", c => c.Custodians);
            NewCustodian = context.MapAreaRoute<CommunitiesController, Guid>("administrators/communities/{id}/custodians/new", c => c.NewCustodian);
        }
    }
}