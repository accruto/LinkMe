using System.Web.Mvc;
using LinkMe.Apps.Asp.Routing;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class ButtonExtensions
    {
        public static string SubmitButton(this HtmlHelper helper, string name, string value, string cssClass)
        {
            return helper.SubmitButton(null, name, value, cssClass);
        }

        public static string SubmitButton(this HtmlHelper helper, string id, string name, string value, string cssClass)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "submit");

            // Use the name for the id if not explicitly set.

            if (!string.IsNullOrEmpty(id))
                builder.MergeAttribute("id", id);
            else
                builder.MergeAttribute("id", name);

            if (!string.IsNullOrEmpty(name))
                builder.MergeAttribute("name", name);
            if (value != null)
                builder.MergeAttribute("value", value);
            if (!string.IsNullOrEmpty(cssClass))
                builder.MergeAttribute("class", cssClass);
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static string GoButton(this HtmlHelper helper, string id, string name, string value, string cssClass)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "button");

            // Use the name for the id if not explicitly set.

            if (!string.IsNullOrEmpty(id))
                builder.MergeAttribute("id", id);
            else
                builder.MergeAttribute("id", name);

            if (!string.IsNullOrEmpty(name))
                builder.MergeAttribute("name", name);
            if (!string.IsNullOrEmpty(value))
                builder.MergeAttribute("value", value);
            if (!string.IsNullOrEmpty(cssClass))
                builder.MergeAttribute("class", cssClass);
            return builder.ToString(TagRenderMode.SelfClosing);
        }

        public static string BackButton(this HtmlHelper helper, string value, string cssClass)
        {
            return Button(cssClass, value, "javascript:history.go(-1)");
        }

        public static string ClearButton(this HtmlHelper helper, string value, string cssClass, string fieldName)
        {
            return Button(cssClass, value,
                "$(\'input[name=" + fieldName + "][type=radio]\').each(function(){if(this.checked)this.checked=false;});" +
                "$(\'input[name=" + fieldName + "][type=text]\').each(function(){this.value=\'\';});");
        }

        public static string PreviewButton(this HtmlHelper helper, RouteReference route, object routeValues, string value, string cssClass)
        {
            // Get the url.

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.RouteUrl(route.Name, routeValues);

            return Button(cssClass, value, "window.open('" + url + "')");
        }

        public static string LinkButton(this HtmlHelper helper, RouteReference route, object routeValues, string value, string cssClass)
        {
            // Get the url.

            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.RouteUrl(route.Name, routeValues);

            return Button(cssClass, value, "location.href='" + url + "'");
        }

        private static string Button(string cssClass, string value, string onClick)
        {
            var builder = new TagBuilder("input");
            builder.MergeAttribute("type", "button");
            builder.MergeAttribute("value", value);
            builder.MergeAttribute("class", cssClass);
            builder.MergeAttribute("OnClick", onClick);
            return builder.ToString(TagRenderMode.SelfClosing);
        }
    }
}
