using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Controllers;

namespace LinkMe.Web.Areas.Api.Controllers
{
    [EnsureHttps, EnsureAuthorized(UserType.Administrator)]
    public class CacheApiController
        : ApiController
    {
        [HttpPost]
        public ActionResult Clear()
        {
            var keys = new List<string>();

            var enumerator = HttpContext.Cache.GetEnumerator();
            while (enumerator.MoveNext())
                keys.Add((string)enumerator.Key);

            foreach (var key in keys)
                HttpContext.Cache.Remove(key);

            return Json((object)null);
        }
    }
}
