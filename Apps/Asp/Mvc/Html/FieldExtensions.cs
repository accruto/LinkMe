using System;
using System.Web;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public class FieldSet
        : IDisposable
    {
        private bool _disposed;
        private readonly HttpResponseBase _httpResponse;

        public FieldSet(HttpResponseBase httpResponse)
        {
            if (httpResponse == null)
                throw new ArgumentNullException("httpResponse");
            _httpResponse = httpResponse;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                _httpResponse.Write("</fieldset>");
            }
        }

        public void EndFieldSet()
        {
            Dispose(true);
        }
    }

    public static class FieldExtensions
    {
        public static FieldSet RenderFieldSet(this HtmlHelper helper)
        {
            var builder = new TagBuilder("fieldSet");
            helper.ViewContext.HttpContext.Response.Write(builder.ToString(TagRenderMode.StartTag));
            return new FieldSet(helper.ViewContext.HttpContext.Response);
        }

        public static FieldSet BeginFieldSet(this HtmlHelper helper, string cssClass)
        {
            var builder = new TagBuilder("fieldSet");
            if (!string.IsNullOrEmpty(cssClass))
                builder.MergeAttribute("class", cssClass);
            helper.ViewContext.HttpContext.Response.Write(builder.ToString(TagRenderMode.StartTag));
            return new FieldSet(helper.ViewContext.HttpContext.Response);
        }
    }
}
