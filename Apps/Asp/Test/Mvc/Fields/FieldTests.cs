using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Test.Mvc.Fields
{
    public class MockHttpContext
        : HttpContextBase
    {
        public override IDictionary Items
        {
            get { return new Dictionary<object, object>(); }
        } 
    }

    public class MockViewDataContainer
        : IViewDataContainer
    {
        private ViewDataDictionary _dictionary = new ViewDataDictionary();

        public ViewDataDictionary ViewData
        {
            get { return _dictionary; }
            set { _dictionary = value; }
        }
    }

    internal abstract class HtmlField
    {
        private readonly string _name;
        private readonly string _value;
        private string _label;
        private string _id;
        private IList<string> _fieldClasses;
        private IList<Tuple<string, string>> _attributes;

        protected HtmlField(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public HtmlField WithLabel(string label)
        {
            _label = label;
            return this;
        }

        public HtmlField WithId(string id)
        {
            _id = id;
            return this;
        }

        public HtmlField WithFieldClasses(params string[] fieldClasses)
        {
            _fieldClasses = fieldClasses;
            return this;
        }

        public HtmlField WithAttribute(string name, string value)
        {
            if (_attributes == null)
                _attributes = new List<Tuple<string, string>>();
            _attributes.Add(new Tuple<string, string>(name, value));
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("<div class=\"").Append(FieldClass);

            if (_fieldClasses != null && _fieldClasses.Count > 0)
            {
                foreach (var @class in _fieldClasses)
                    sb.Append(" ").Append(@class);
            }

            sb.Append(" field\"><label for=\"")
                .Append(_name)
                .Append("\">")
                .Append(_label ?? _name)
                .AppendLine("</label>");

            sb.Append("<div class=\"")
                .Append(ControlClass)
                .Append(" control\"><input class=\"")
                .Append(InputClass)
                .Append("\" id=\"")
                .Append(_id ?? _name)
                .Append("\" name=\"")
                .Append(_name)
                .Append("\"");

            if (_attributes != null && _attributes.Count > 0)
            {
                foreach (var attribute in _attributes)
                    sb.Append(" ").Append(attribute.Item1).Append("=\"").Append(attribute.Item2).Append("\"");
            }

            sb.Append(" type=\"")
                .Append(InputType)
                .Append("\" value=\"")
                .Append(_value)
                .Append("\" /></div></div>");
            return sb.ToString();
        }

        protected abstract string InputType { get; }
        protected abstract string InputClass { get; }
        protected abstract string ControlClass { get; }
        protected abstract string FieldClass { get; }
    }

    public abstract class FieldTests
    {
        protected static HtmlHelper Html
        {
            get
            {
                var viewData = new ViewDataDictionary();
                var viewContext = new ViewContext { ViewData = viewData, HttpContext = new MockHttpContext() };
                var viewContainer = new MockViewDataContainer { ViewData = viewData };
                return new HtmlHelper(viewContext, viewContainer);

            }
        }

    }
}
