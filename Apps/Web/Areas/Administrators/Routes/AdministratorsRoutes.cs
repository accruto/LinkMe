using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Administrators;
using LinkMe.Web.Areas.Administrators.Models.Administrators;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class AdministratorsRoutes
    {
        public static RouteReference Index { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference New { get; private set; }
        public static RouteReference Enable { get; private set; }
        public static RouteReference ChangePassword { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Index = context.MapAreaRoute<MaintainAdministratorsController>("administrators/administrators", c => c.Index);
            New = context.MapAreaRoute<MaintainAdministratorsController>("administrators/administrators/new", c => c.New);
            Edit = context.MapAreaRoute<MaintainAdministratorsController, Guid>("administrators/administrators/{id}", c => c.Edit);
            Enable = context.MapAreaRoute<MaintainAdministratorsController, Guid>("administrators/administrators/{id}/enable", c => c.Enable);
            ChangePassword = context.MapAreaRoute<MaintainAdministratorsController, Guid, AdministratorLoginModel>("administrators/administrators/{id}/changepassword", c => c.ChangePassword);
        }
    }
}