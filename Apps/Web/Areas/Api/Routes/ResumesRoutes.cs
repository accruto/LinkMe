using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Web.Areas.Api.Controllers;

namespace LinkMe.Web.Areas.Api.Routes
{
    public class ResumesRoutes
    {
        public static RouteReference Upload { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            Upload = context.MapAreaRoute<ResumesApiController, HttpPostedFileBase>("api/resumes/upload", c => c.Upload);
            context.MapAreaRoute<ResumesApiController, Guid>("api/resumes/parse", c => c.Parse);
        }
    }
}
