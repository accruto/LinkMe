using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;
using LinkMe.Framework.Utility;

namespace LinkMe.Web.Html
{
    [Flags]
    public enum FieldSetOptions
    {
        None = 0x0,
        LabelsOnLeft = 0x1
    }

    public static class FieldSetExtensions
    {
        public static FieldSet BeginFieldSet(this HtmlHelper helper)
        {
            return helper.BeginFieldSet(FieldSetOptions.None);
        }

        public static FieldSet BeginFieldSet(this HtmlHelper helper, FieldSetOptions options)
        {
            var cssClass = "forms_v2";
            if (options.IsFlagSet(FieldSetOptions.LabelsOnLeft))
                cssClass += " with-labels-on-left";
            return helper.BeginFieldSet(cssClass);
        }

        public static FieldSet RenderFieldSet(this HtmlHelper helper, FieldSetOptions options)
        {
            return helper.BeginFieldSet(options.IsFlagSet(FieldSetOptions.LabelsOnLeft) ? "with-labels-on-left" : "");
        }
    }
}
