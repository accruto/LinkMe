using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LinkMe.Apps.Asp.Content;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public static class StyleSheetExtensions
    {
        private const string Key = "StyleSheetPaths";

        public static MvcHtmlString StyleSheet(this HtmlHelper helper, StyleSheetReference stylesheet)
        {
            // Check it has not already been added.

            return ShouldOutput(helper, stylesheet)
                ? MvcHtmlString.Create(stylesheet.ToString())
                : MvcHtmlString.Empty;
        }

        private static bool ShouldOutput(HtmlHelper helper, StyleSheetReference stylesheet)
        {
            var paths = helper.ViewData[Key] as IDictionary<string, StyleSheetReference>;
            if (paths == null)
            {
                paths = new Dictionary<string, StyleSheetReference>();
                helper.ViewData[Key] = paths;
                paths[stylesheet.Path] = stylesheet;
                return true;
            }

            var shouldOutput = !paths.ContainsKey(stylesheet.Path);
            paths[stylesheet.Path] = stylesheet;
            return shouldOutput;
        }
    }

    internal class StyleSheetTextWriter
        : HtmlTextWriter
    {
        public StyleSheetTextWriter(TextWriter writer)
            : base(writer)
        {
        }
    }

    internal static class RegisteredStyleSheets
    {
        private const string Key = "RegisteredStyleSheets";

        public static void Add(RegisterStyleSheets registerStyleSheets)
        {
            Controls.Add(registerStyleSheets);
        }

        public static IList<RegisterStyleSheets> Controls
        {
            get
            {
                var controls = HttpContext.Current.Items[Key] as List<RegisterStyleSheets>;
                if (controls == null)
                {
                    controls = new List<RegisterStyleSheets>();
                    HttpContext.Current.Items[Key] = controls;
                }

                return controls;
            }
        }
    }

    public class RegisterStyleSheets
        : TemplateControl
    {
        public RegisterStyleSheets()
        {
            RegisteredStyleSheets.Add(this);
        }

        internal LiteralControl GetContent()
        {
            using (var writer = new StringWriter())
            {
                using (var htmlWriter = new StyleSheetTextWriter(writer))
                {
                    base.Render(htmlWriter);
                    return new LiteralControl(writer.ToString().Replace("/>", ">"));
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (writer is StyleSheetTextWriter)
                base.Render(writer);
        }
    }

    [ParseChildren(false)]
    [PersistChildren(false)]
    public class DisplayStyleSheets
        : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (var control in RegisteredStyleSheets.Controls)
                Controls.Add(control.GetContent());

            base.Render(writer);
        }
    }
}