using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Administrators.Controllers.Custodians;
using LinkMe.Web.Areas.Administrators.Models.Custodians;

namespace LinkMe.Web.Areas.Administrators.Routes
{
    public static class CustodiansRoutes
    {
        public static RouteReference Edit { get; private set; }
        public static RouteReference Enable { get; private set; }
        public static RouteReference ChangePassword { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Edit = context.MapAreaRoute<MaintainCustodiansController, Guid>("administrators/custodians/{id}", c => c.Edit);
            Enable = context.MapAreaRoute<MaintainCustodiansController, Guid>("administrators/custodians/{id}/enable", c => c.Enable);
            ChangePassword = context.MapAreaRoute<MaintainCustodiansController, Guid, CustodianLoginModel>("administrators/custodians/{id}/changepassword", c => c.ChangePassword);
        }
    }
}