using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;

namespace LinkMe.Apps.Api.Areas.Dev.Controllers
{
    public class DevApiController
        : ApiController
    {
        public ActionResult Error()
        {
            throw new ApplicationException("Test error requested by user.");
        }
    }
}