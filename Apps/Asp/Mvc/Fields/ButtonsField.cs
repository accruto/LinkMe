using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class Button
    {
        public abstract string GetHtml(HtmlHelper html);
    }

    public class SubmitButton
        : Button, IHaveId
    {
        private string _id;
        private readonly string _name;
        private readonly string _value;
        private readonly string _cssClass;

        public SubmitButton(string name, string value, string cssClass)
        {
            _name = name;
            _value = value;
            _cssClass = cssClass;
        }

        public SubmitButton(string name, string value)
            : this(name, value, null)
        {
        }

        public SubmitButton(string name)
            : this(name, name)
        {
        }

        public override string GetHtml(HtmlHelper html)
        {
            return html.SubmitButton(_id, _name, _value, _cssClass);
        }

        string IHaveId.Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }

    public class ButtonsField
        : Field
    {
        private readonly IList<Button> _buttons = new List<Button>();

        public ButtonsField(HtmlHelper htmlHelper, params Button[] buttons)
            : base(htmlHelper)
        {
            base.AddCssPrefix("buttons");
            foreach (var button in buttons)
                _buttons.Add(button);
        }

        public ButtonsField(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            // For backwards compatability use "button".

            base.AddCssPrefix("button");
        }

        public ButtonsField Add(Button button)
        {
            _buttons.Add(button);
            return this;
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var button in _buttons)
                    sb.AppendLine(button.GetHtml(Html));
                return MvcHtmlString.Create(sb.ToString());
            }
        }
    }

    public static class ButtonsFieldExtensions
    {
        public static ButtonsField ButtonsField(this HtmlHelper helper)
        {
            return new ButtonsField(helper);
        }

        public static ButtonsField ButtonsField(this HtmlHelper helper, params Button[] buttons)
        {
            return new ButtonsField(helper, buttons);
        }
    }
}
