using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Apps.Asp.Mvc.Html;

namespace LinkMe.Web.Html
{
    public class FieldHelpText
        : FieldHelp
    {
        private readonly string _helpText;

        public FieldHelpText(string helpText)
        {
            _helpText = helpText;
        }

        public override string ToString(HtmlHelper htmlHelper)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("helptext");
            tagBuilder.InnerHtml = _helpText;
            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }

    public class FieldExampleText
        : FieldHelp
    {
        private readonly string _helpText;

        public FieldExampleText(string helpText)
        {
            _helpText = helpText;
        }

        public override string ToString(HtmlHelper htmlHelper)
        {
            var tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("example_helptext");
            tagBuilder.AddCssClass("helptext");
            tagBuilder.InnerHtml = _helpText;
            return tagBuilder.ToString(TagRenderMode.Normal);
        }
    }

    public class FieldHelpArea
        : FieldHelp
    {
        private readonly string _helpFor;
        private readonly string _helpText;

        public FieldHelpArea(string helpFor, string helpText)
        {
            _helpFor = helpFor;
            _helpText = helpText;
        }

        public override string ToString(HtmlHelper htmlHelper)
        {
            if (string.IsNullOrEmpty(_helpFor) || string.IsNullOrEmpty(_helpText))
                return null;

            var builder = new TagBuilderTree("div");
            builder.AddCssClass("help-area");

            var modelState = htmlHelper.ViewData.ModelState[_helpFor];
            var modelErrors = modelState == null ? null : modelState.Errors;
            if (modelErrors != null && modelErrors.Count > 0)
                builder.AddCssClass("error");

            builder.MergeAttribute("helpfor", _helpFor);

            var triangle = new TagBuilder("div");
            triangle.AddCssClass("triangle");
            builder.AddTag(triangle);

            var helpText = new TagBuilderTree("div");
            helpText.AddCssClass("help-text");

            helpText.AddTag(new TagBuilder("span") { InnerHtml = _helpText });
            builder.AddTag(helpText);

            return builder.ToString(TagRenderMode.Normal);
        }
    }

    public static class HelpExtensions
    {
        public static TField WithHelpText<TField>(this TField field, string helpText)
            where TField : Field
        {
            field.WithHelp(new FieldHelpText(helpText));
            return field;
        }

        public static TField WithExampleText<TField>(this TField field, string exampleText)
            where TField : Field
        {
            field.WithHelp(new FieldExampleText(exampleText));
            return field;
        }

        public static TField WithHelpArea<TField>(this TField field, string helpText)
            where TField : Field
        {
            field.WithHelp(new FieldHelpArea(field.For, helpText));
            return field;
        }

        public static TField WithHelpArea<TField>(this TField field, string helpFor, string helpText)
            where TField : Field
        {
            field.WithHelp(new FieldHelpArea(helpFor, helpText));
            return field;
        }
    }
}
