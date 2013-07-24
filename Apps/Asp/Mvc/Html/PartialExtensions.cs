using System.Web.Mvc;
using System.Web.Mvc.Html;
using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Roles.Affiliations.Verticals;

namespace LinkMe.Apps.Asp.Mvc.Html
{
    public static class PartialViews
    {
        public const string VerticalIdKey = "VerticalId";
    }

    public static class PartialExtensions
    {
        public static void RenderPartial<T>(this HtmlHelper helper, object model)
        {
            helper.RenderPartial(typeof(T).Name, model);
        }

        public static void RenderPartial(this HtmlHelper helper, Vertical vertical, string partialViewName, object model)
        {
            if (vertical != null)
            {
                // Add in vertical id for this particular rendering.

                var viewData = new ViewDataDictionary(helper.ViewData) { { PartialViews.VerticalIdKey, vertical.Id } };
                helper.RenderPartial(partialViewName, model, viewData);
            }
            else
            {
                helper.RenderPartial(partialViewName, model);
            }
        }

        public static void RenderPartial(this HtmlHelper helper, Vertical vertical, string partialViewName)
        {
            if (vertical != null)
            {
                // Add in vertical id for this particular rendering.

                var viewData = new ViewDataDictionary(helper.ViewData) { { PartialViews.VerticalIdKey, vertical.Id } };
                helper.RenderPartial(partialViewName, viewData);
            }
            else
            {
                helper.RenderPartial(partialViewName);
            }
        }
    }
}
