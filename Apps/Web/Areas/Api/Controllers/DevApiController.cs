using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class DevApiController
        : ApiController
    {
        public ActionResult Error()
        {
            throw new ApplicationException("Test error requested by user.");
        }

        [HttpPost]
        public ActionResult AnonymousId()
        {
            return Json(Request.AnonymousID);
        }
    }
}
