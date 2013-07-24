using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Html;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class NamedField
        : Field, IHaveId
    {
        private string _id;
        private readonly string _name;
        private IList<Tuple<string, string>> _attributes;
        private readonly IList<string> _cssClasses = new List<string>();

        protected NamedField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper)
        {
            _name = name;
        }

        protected string Name
        {
            get { return _name; }
        }

        internal override string Label
        {
            get { return base.Label ?? Name; }
        }

        string IHaveId.Id
        {
            get { return _id; }
            set { _id = value; }
        }

        protected IList<string> CssClasses
        {
            get { return _cssClasses; }
        }

        public override string For
        {
            get { return Name; }
        }

        protected override IDictionary<string, object> GetHtmlAttributes(object htmlAttributes)
        {
            var newHtmlAttributes = base.GetHtmlAttributes(htmlAttributes);

            // Add the id if it is set and if it is not already part of the attributes.

            if (!string.IsNullOrEmpty(_id) && !newHtmlAttributes.ContainsKey("id"))
                newHtmlAttributes["id"] = _id;

            // Add other attributes.

            if (_attributes != null)
            {
                foreach (var attribute in _attributes)
                    newHtmlAttributes[attribute.Item1] = attribute.Item2;
            }

            return newHtmlAttributes;
        }

        protected override void AppendControlCssClass(CssClassBuilder builder)
        {
            base.AppendControlCssClass(builder);

            // Add an error attribute if needed.

            if (string.IsNullOrEmpty(Name) || !Html.ViewData.ModelState.ContainsKey(Name))
                return;

            var modelState = Html.ViewData.ModelState[Name];
            var modelErrors = modelState == null ? null : modelState.Errors;
            if (modelErrors != null && modelErrors.Count > 0)
                builder.Append("error");
        }

        public NamedField WithAttribute(string name, string value)
        {
            if (_attributes == null)
                _attributes = new List<Tuple<string, string>>();
            _attributes.Add(new Tuple<string, string>(name, value));
            return this;
        }

        public NamedField WithCssClass(string value)
        {
            _cssClasses.Add(value);
            return this;
        }
    }
}