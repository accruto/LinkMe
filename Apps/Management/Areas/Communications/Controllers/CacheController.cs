using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Controllers;

namespace LinkMe.Apps.Management.Areas.Communications.Controllers
{
    public class CacheController
        : ViewController
    {
        public ActionResult Clear()
        {
            var keys = new List<string>();

            var enumerator = HttpContext.Cache.GetEnumerator();
            while (enumerator.MoveNext())
                keys.Add((string) enumerator.Key);

            foreach (var key in keys)
                HttpContext.Cache.Remove(key);

            return View();
        }
    }
}
