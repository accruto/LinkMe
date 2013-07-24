using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class ControlsField
        : Field
    {
        private readonly IList<Field> _innerFields = new List<Field>();

        public ControlsField(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var field in _innerFields)
                {
                    sb.AppendLine(field.InnerHtml.ToString());
                    if (!string.IsNullOrEmpty(field.Label))
                        BuildLabelHtml(sb, field.For, field.Label);
                }
                return MvcHtmlString.Create(sb.ToString());
            }
        }

        public ControlsField Add(Field field)
        {
            _innerFields.Add(field);
            return this;
        }
    }

    public static class ControlsFieldExtensions
    {
        public static ControlsField ControlsField(this HtmlHelper htmlHelper)
        {
            return new ControlsField(htmlHelper);
        }
    }
}