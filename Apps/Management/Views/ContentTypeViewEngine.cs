using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Framework.Utility.Files;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Management.Views
{
    public class ContentTypeViewEngine
        : ContainerViewEngine
    {
        public ContentTypeViewEngine(IUnityContainer container)
            : base(container)
        {
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var contentType = GetContentType(controllerContext);
            if (contentType != null)
            {
                var result = GetViewResult(controllerContext, GetViewName(viewName, contentType), masterName, useCache);
                if (result != null)
                    return GetViewEngineResult(result);
            }

            return GetViewEngineResult(base.FindView(controllerContext, viewName, masterName, useCache));
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var contentType = GetContentType(controllerContext);
            if (contentType != null)
            {
                var result = GetPartialViewResult(controllerContext, GetViewName(partialViewName, contentType), useCache);
                if (result != null)
                    return GetViewEngineResult(result);
            }

            return GetViewEngineResult(base.FindPartialView(controllerContext, partialViewName, useCache));
        }

        private static string GetContentType(ControllerContext controllerContext)
        {
            // Two ways that the content type can be specified, either as a value, more than likely from the query string.

            var contentType = GetContentType(controllerContext.Controller.ValueProvider);
            if (contentType != null)
                return contentType;

            // Or the Accept HTTP header.

            contentType = GetContentType(controllerContext.HttpContext.Request);
            if (contentType != null)
                return contentType;

            return MediaType.Html;
        }

        private static string GetContentType(HttpRequestBase request)
        {
            return request.ContentType;
        }

        private static string GetContentType(IValueProvider valueProvider)
        {
            var result = valueProvider.GetValue("Accept");
            if (result == null || string.IsNullOrEmpty(result.AttemptedValue))
                return null;

            switch (result.AttemptedValue)
            {
                case "html":
                    return MediaType.Html;

                case "text":
                    return MediaType.Text;

                default:
                    return null;
            }
        }

        private ViewEngineResult GetPartialViewResult(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            var result = base.FindPartialView(controllerContext, partialViewName, useCache);
            if (result != null && result.View != null)
                return result;
            return null;
        }

        private ViewEngineResult GetViewResult(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            var result = base.FindView(controllerContext, viewName, masterName, useCache);
            if (result != null && result.View != null)
                return result;
            return null;
        }

        private static string GetViewName(string viewName, string contentType)
        {
            return (contentType == MediaType.Text ? "Text" : "Html") + "/" + viewName;
        }
    }
}
