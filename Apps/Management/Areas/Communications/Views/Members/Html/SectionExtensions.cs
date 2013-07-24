using System.Web.Mvc;
using System.Web.Mvc.Html;
using LinkMe.Apps.Management.Areas.Communications.Models;

namespace LinkMe.Apps.Management.Areas.Communications.Views.Members.Html
{
    public static class SectionExtensions
    {
        public static void RenderLeftSection(this HtmlHelper htmlHelper, string view, object model, string heading)
        {
            htmlHelper.RenderLeftSection(view, model, heading, null, null);
        }

        public static void RenderLeftSection(this HtmlHelper htmlHelper, string view, object model, string heading, string headingLink, string headingLinkText)
        {
            htmlHelper.RenderPartial("LeftSection", new SectionModel
            {
                View = view,
                ViewModel = model,
                Heading = heading,
                HeadingLink = headingLink,
                HeadingLinkText = headingLinkText
            });
        }

        public static void RenderRightSection(this HtmlHelper htmlHelper, string view, object model, string heading)
        {
            htmlHelper.RenderRightSection(view, model, heading, null, null);
        }

        public static void RenderRightSection(this HtmlHelper htmlHelper, string view, object model, string heading, string headingLink, string headingLinkText)
        {
            htmlHelper.RenderPartial("RightSection", new SectionModel
            {
                View = view,
                ViewModel = model,
                Heading = heading,
                HeadingLink = headingLink,
                HeadingLinkText = headingLinkText
            });
        }
    }
}