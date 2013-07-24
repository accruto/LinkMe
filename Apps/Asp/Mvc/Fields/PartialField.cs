using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class PartialField
        : Field
    {
        private readonly string _partialViewName;
        private readonly object _model;

        internal PartialField(HtmlHelper htmlHelper, string partialViewName, object model)
            : base(htmlHelper)
        {
            _partialViewName = partialViewName;
            _model = model;
        }

        internal PartialField(HtmlHelper htmlHelper, string partialViewName)
            : base(htmlHelper)
        {
            _partialViewName = partialViewName;
        }

        public void Render()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Label))
                BuildLabelHtml(sb, For, Label);
            var labelHtml = sb.ToString();

            // Deep inside RenderPartial the WebForm ViewEngine assumes the Output stream is in place
            // and accesses this property so it seems OK to do so here.

            var writer = Html.ViewContext.HttpContext.Response.Output;

            WriteStartDivHtml(writer, GetFieldCssClass());

            // Label.

            if (!string.IsNullOrEmpty(labelHtml))
                writer.Write(labelHtml);

            // Control.

            WriteStartDivHtml(writer, GetControlCssClass());

            // Render the partial view.

            Html.RenderPartial(_partialViewName, _model);

            WriteEndDivHtml(writer);
            WriteEndDivHtml(writer);
        }

        private static void WriteStartDivHtml(TextWriter writer, string cssClass)
        {
            writer.Write("<div");
            if (!string.IsNullOrEmpty(cssClass))
            {
                writer.Write(" class=\"");
                writer.Write(cssClass);
                writer.Write("\"");
            }
            writer.Write(">");
            writer.Write(Environment.NewLine);
        }

        private static void WriteEndDivHtml(TextWriter writer)
        {
            writer.Write("</div>");
            writer.Write(Environment.NewLine);
        }

        public override MvcHtmlString InnerHtml
        {
            get { throw new NotImplementedException("Should be calling Render() instead."); }
        }
    }

    public static class PartialFieldExtensions
    {
        public static PartialField PartialField(this HtmlHelper htmlHelper, string partialView, object model)
        {
            return new PartialField(htmlHelper, partialView, model);
        }

        public static PartialField PartialField(this HtmlHelper htmlHelper, string partialView)
        {
            return new PartialField(htmlHelper, partialView);
        }
    }
}