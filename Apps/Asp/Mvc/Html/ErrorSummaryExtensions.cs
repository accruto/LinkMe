using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class ErrorSummaryExtensions
    {
        public static MvcHtmlString ErrorSummary(this HtmlHelper helper)
        {
            return helper.ErrorSummary(null);
        }

        public static MvcHtmlString ErrorSummary(this HtmlHelper helper, string message)
        {
            var div = new TagBuilderTree("div");
            div.AddCssClass("validation-error-msg");
            if (helper.ViewData.ModelState.IsValid)
                div.MergeAttribute("style", "display: none;");

            var topBar = new TagBuilder("div");
            topBar.AddCssClass("top-bar");
            div.AddTag(topBar);

            var body = new TagBuilderTree("div");
            body.AddCssClass("message-body");

            var icon = new TagBuilder("div");
            icon.AddCssClass("message-icon");
            body.AddTag(icon);

            var summary = new TagBuilder("span");
            summary.AddCssClass("error-summary");
            if (!helper.ViewData.ModelState.IsValid && !string.IsNullOrEmpty(message))
                summary.SetInnerText(message);
            body.AddTag(summary);

            var ul = new TagBuilderTree("ul");

            if (!helper.ViewData.ModelState.IsValid)
            {
                foreach (var modelState in helper.ViewData.ModelState)
                {
                    if (modelState.Key == null || !modelState.Key.EndsWith("ErrorCode"))
                    {
                        foreach (var modelError in modelState.Value.Errors)
                        {
                            var errorMessage = modelError.ErrorMessage;
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                var li = new TagBuilder("li") { InnerHtml = errorMessage };
                                ul.AddTag(li);
                            }
                        }
                    }
                }
            }

            body.AddTag(ul);

            div.AddTag(body);

            var bottomBar = new TagBuilder("div");
            bottomBar.AddCssClass("bottom-bar");
            div.AddTag(bottomBar);

            return MvcHtmlString.Create(div.ToString(TagRenderMode.Normal));
        }
    }
}
