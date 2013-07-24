using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public static class CouponsRoutes
    {
        public static RouteReference Coupon { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Coupon = context.MapAreaRoute<CouponsApiController, string, Guid[]>("api/coupons", c => c.Coupon);
        }
    }
}
