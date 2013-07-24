using System.Web.Mvc;
using System.Web.WebPages;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Web.Areas.Public.Controllers.Support
{
    public class BrowserSwitcherController
        : ViewController
    {
        public ActionResult SwitchBrowser(bool mobile, string returnUrl)
        {
            if (Request.Browser.IsMobileDevice == mobile)
                HttpContext.ClearOverriddenBrowser();
            else
                HttpContext.SetOverriddenBrowser(mobile ? BrowserOverride.Mobile : BrowserOverride.Desktop);

            return RedirectToUrl(new ReadOnlyApplicationUrl(returnUrl));
        }
    }
}