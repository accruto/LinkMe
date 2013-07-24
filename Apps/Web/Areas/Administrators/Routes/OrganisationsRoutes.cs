using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Organisations;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class OrganisationsRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference New { get; private set; }
        public static RouteReference Employers { get; private set; }
        public static RouteReference NewEmployer { get; private set; }
        public static RouteReference Communications { get; private set; }
        public static RouteReference Reports { get; private set; }
        public static RouteReference Report { get; private set; }
        public static RouteReference RunReport { get; private set; }
        public static RouteReference Credits { get; private set; }
        public static RouteReference Usage { get; private set; }
        public static RouteReference AllocationUsage { get; private set; }
        public static RouteReference ApiDeallocate { get; private set; }
        public static RouteReference ApiPartialMatches { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<OrganisationsController>("administrators/organisations/search", c => c.Search);
            New = context.MapAreaRoute<OrganisationsController>("administrators/organisations/organisation/new", c => c.New);
            Edit = context.MapAreaRoute<OrganisationsController, Guid>("administrators/organisations/{id}", c => c.Edit);
            Employers = context.MapAreaRoute<OrganisationsController, Guid, CheckBoxValue>("administrators/organisations/{id}/employers", c => c.Employers);
            NewEmployer = context.MapAreaRoute<OrganisationsController, Guid>("administrators/organisations/{id}/employers/new", c => c.NewEmployer);

            Communications = context.MapAreaRoute<OrganisationsController, Guid>("administrators/organisations/{id}/communications", c => c.Communications);

            Reports = context.MapAreaRoute<OrganisationReportsController, Guid>("administrators/organisations/{id}/reports", c => c.Index);
            Report = context.MapAreaRoute<OrganisationReportsController, Guid, string>("administrators/organisations/{id}/reports/{type}", c => c.Report);
            RunReport = context.MapAreaRoute<OrganisationReportsController, Guid, string, DateTime?, DateTime?>("administrators/organisations/{id}/reports/{type}/run", c => c.RunReport);

            Credits = context.MapAreaRoute<OrganisationCreditsController, Guid>("administrators/organisations/{id}/credits", c => c.Index);
            Usage = context.MapAreaRoute<OrganisationCreditsController, Guid, DateTime?, DateTime?>("administrators/organisations/{id}/credits/usage", c => c.Usage);
            AllocationUsage = context.MapAreaRoute<OrganisationCreditsController, Guid, Guid>("administrators/organisations/{id}/allocations/{allocationId}/usage", c => c.AllocationUsage);

            ApiDeallocate = context.MapAreaRoute<OrganisationsApiController, Guid, Guid>("administrators/organisations/{id}/allocations/deallocate", c => c.Deallocate);
            ApiPartialMatches = context.MapAreaRoute<OrganisationsApiController, string, int?>("administrators/organisations/api/location/partialmatches", c => c.FindPartialMatchedOrganisations);
        }
    }
}