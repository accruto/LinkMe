using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Html;

namespace LinkMe.Web.Views.Shared.MasterPages
{
    public class Site
        : ViewMasterPage
    {
        private static readonly ReadOnlyUrl JavascriptUrl = new ReadOnlyApplicationUrl("~/js/Javascript.aspx");

        protected static ReadOnlyUrl GetJavascriptUrl()
        {
            return JavascriptUrl;
        }

        protected string PageIdentifier
        {
            get
            {
                // The Page type is an ASP.NET generated type which derives from the type defined in the code.

                return Page.GetType().BaseType.FullName;
            }
        }
    }
}
