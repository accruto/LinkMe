using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Errors.Controllers;
using LinkMe.Web.Areas.Errors.Models.Errors;

namespace LinkMe.Web.Areas.Errors.Routes
{
    public static class ErrorsRoutes
    {
        public static RouteReference ObjectNotFound { get; private set; }
        public static RouteReference ServerError { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            ObjectNotFound = context.MapAreaRoute<ErrorsController, string, string, string>("errors/objectnotfound", c => c.ObjectNotFound);
            context.MapAreaRoute<ErrorsController, string, string>("errors/notfound", c => c.NotFound);
            ServerError = context.MapAreaRoute<ErrorsController, ErrorReport, bool?>("errors/servererror", c => c.ServerError);
        }
    }
}