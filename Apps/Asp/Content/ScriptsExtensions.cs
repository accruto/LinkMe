using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public static class ScriptsExtensions
    {
        public static IHtmlString RenderScripts(this HtmlHelper html, BundleUrl bundleUrl)
        {
            return Scripts.Render(Application.ApplicationPathStartChar + bundleUrl.AppRelativePathAndQuery);
        }
    }
}
