using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Members;
using LinkMe.Web.Areas.Administrators.Models.Members;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class MembersRoutes
    {
        public static RouteReference Search { get; private set; }
        public static RouteReference Edit { get; private set; }
        public static RouteReference Enable { get; private set; }
        public static RouteReference ChangePassword { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Search = context.MapAreaRoute<MaintainMembersController>("administrators/members/search", c => c.Search);
            Edit = context.MapAreaRoute<MaintainMembersController, Guid>("administrators/members/{id}", c => c.Edit);
            Enable = context.MapAreaRoute<MaintainMembersController, Guid>("administrators/members/{id}/enable", c => c.Enable);
            ChangePassword = context.MapAreaRoute<MaintainMembersController, Guid, MemberLoginModel, CheckBoxValue>("administrators/members/{id}/changepassword", c => c.ChangePassword);
        }
    }
}