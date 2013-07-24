using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Employers;
using LinkMe.Web.Areas.Administrators.Models.Employers;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class EmployersRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference Enable { get; private set; }
        public static RouteReference ChangePassword { get; private set; }
        public static RouteReference Credits { get; private set; }
        public static RouteReference Usage { get; private set; }
        public static RouteReference AllocationUsage { get; private set; }
        public static RouteReference ApiDeallocate { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<MaintainEmployersController>("administrators/employers/search", c => c.Search);
            Edit = context.MapAreaRoute<MaintainEmployersController, Guid>("administrators/employers/{id}", c => c.Edit);
            Enable = context.MapAreaRoute<MaintainEmployersController, Guid>("administrators/employers/{id}/enable", c => c.Enable);
            ChangePassword = context.MapAreaRoute<MaintainEmployersController, Guid, EmployerLoginModel, CheckBoxValue>("administrators/employers/{id}/changepassword", c => c.ChangePassword);

            Credits = context.MapAreaRoute<EmployerCreditsController, Guid>("administrators/employers/{id}/credits", c => c.Index);
            Usage = context.MapAreaRoute<EmployerCreditsController, Guid, DateTime?, DateTime?>("administrators/employers/{id}/credits/usage", c => c.Usage);
            AllocationUsage = context.MapAreaRoute<EmployerCreditsController, Guid, Guid>("administrators/employers/{id}/allocations/{allocationId}/usage", c => c.AllocationUsage);

            ApiDeallocate = context.MapAreaRoute<EmployerCreditsApiController, Guid, Guid>("administrators/employers/{id}/allocations/deallocate", c => c.Deallocate);
        }
    }
}