using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using LinkMe.Apps.Asp.Content;

namespace LinkMe.Apps.Asp.Mvc.Views
{
    public static class JavaScriptExtensions
    {
        private const string Key = "JavaScriptPaths";

        public static MvcHtmlString JavaScript(this HtmlHelper helper, JavaScriptReference javascript)
        {
            // Check it has not already been added.

            return ShouldOutput(helper, javascript)
                ? MvcHtmlString.Create(javascript.ToString())
                : MvcHtmlString.Empty;
        }

        private static bool ShouldOutput(HtmlHelper helper, JavaScriptReference javascript)
        {
            var paths = helper.ViewData[Key] as IDictionary<string, JavaScriptReference>;
            if (paths == null)
            {
                paths = new Dictionary<string, JavaScriptReference>();
                helper.ViewData[Key] = paths;
                paths[javascript.Path] = javascript;
                return true;
            }

            var shouldOutput = !paths.ContainsKey(javascript.Path);
            paths[javascript.Path] = javascript;
            return shouldOutput;
        }
    }

    internal class JavaScriptTextWriter
        : HtmlTextWriter
    {
        public JavaScriptTextWriter(TextWriter writer)
            : base(writer)
        {
        }
    }

    internal static class RegisteredJavaScripts
    {
        private const string Key = "RegisteredJavaScripts";

        public static void Add(RegisterJavaScripts registerJavaScripts)
        {
            Controls.Add(registerJavaScripts);
        }

        public static IList<RegisterJavaScripts> Controls
        {
            get
            {
                var controls = HttpContext.Current.Items[Key] as List<RegisterJavaScripts>;
                if (controls == null)
                {
                    controls = new List<RegisterJavaScripts>();
                    HttpContext.Current.Items[Key] = controls;
                }

                return controls;
            }
        }
    }

    public class RegisterJavaScripts
        : TemplateControl
    {
        public RegisterJavaScripts()
        {
            RegisteredJavaScripts.Add(this);
        }

        internal LiteralControl GetContent()
        {
            using (var writer = new StringWriter())
            {
                using (var htmlWriter = new JavaScriptTextWriter(writer))
                {
                    base.Render(htmlWriter);
                    return new LiteralControl(writer.ToString());
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (writer is JavaScriptTextWriter)
                base.Render(writer);
        }
    }

    [ParseChildren(false)]
    [PersistChildren(false)]
    public class DisplayJavaScripts
        : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (var control in RegisteredJavaScripts.Controls)
                Controls.Add(control.GetContent());

            base.Render(writer);
        }
    }
}
