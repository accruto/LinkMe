using System;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Applications.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;

namespace LinkMe.Apps.Management.Areas.Communications
{
    public static class UrlExtensions
    {
        private const string DefaultPhotoFileName = "photo-default.png";
        private static readonly IWebSiteQuery _webSiteQuery = Container.Current.Resolve<IWebSiteQuery>();

        public static string ImageUrl(this HtmlHelper htmlHelper, string fileName)
        {
            return _webSiteQuery.GetUrl(WebSite.LinkMe, null, false, "~/email/images/" + fileName).AbsoluteUri;
        }

        public static string HomeUrl(this HtmlHelper htmlHelper)
        {
            return htmlHelper.TinyUrl(false, "~/");
        }

        public static string TrackingPixelUrl(this HtmlHelper htmlHelper, Guid contextId)
        {
            var applicationPath = "~/url/" + contextId.ToString("n") + ".aspx";
            return _webSiteQuery.GetUrl(WebSite.LinkMe, null, false, applicationPath).AbsoluteUri;
        }

        public static string PhotoUrl(this HtmlHelper htmlHelper, IMember member, bool isPreview)
        {
            if (member == null)
                return htmlHelper.ImageUrl(DefaultPhotoFileName);

            // Determine whether the current member has a photo.

            if (member.PhotoId == null)
                return htmlHelper.ImageUrl(DefaultPhotoFileName);

            return PhotoUrl(member.PhotoId.Value, isPreview);
        }

        public static string TinyUrl(this HtmlHelper htmlHelper, bool secure, string applicationPath, params string[] queryString)
        {
            return ((TinyUrlMappings)htmlHelper.ViewContext.TempData["TinyUrlMappings"]).Register(secure, applicationPath, queryString);
        }

        public static string TinyLoginUrl(this HtmlHelper htmlHelper, string loginApplicationPath, string applicationPath, params string[] queryString)
        {
            return ((TinyUrlMappings)htmlHelper.ViewContext.TempData["TinyUrlMappings"]).RegisterLogin(loginApplicationPath, applicationPath, queryString);
        }

        private static string PhotoUrl(Guid photoId, bool isPreview)
        {
            return isPreview
                ? new ReadOnlyApplicationUrl("~/communications/photo/" + photoId.ToString("n")).PathAndQuery
                : "cid:" + photoId.ToString("n");
        }
    }
}
