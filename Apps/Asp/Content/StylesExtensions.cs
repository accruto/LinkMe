using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Asp.Content
{
    public static class StylesExtensions
    {
        public static IHtmlString RenderStyles(this HtmlHelper html, BundleUrl bundleUrl)
        {
            return Styles.Render(Application.ApplicationPathStartChar + bundleUrl.AppRelativePathAndQuery);
        }
    }
}
