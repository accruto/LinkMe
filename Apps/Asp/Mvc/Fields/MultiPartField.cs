using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public abstract class Part
    {
    }

    internal class Parts
        : IEnumerable<Part>
    {
        private readonly IList<Part> _parts = new List<Part>();

        public void Add(Part part)
        {
            _parts.Add(part);
        }

        public void Remove(Part part)
        {
            _parts.Remove(part);
        }

        IEnumerator<Part> IEnumerable<Part>.GetEnumerator()
        {
            return _parts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _parts.GetEnumerator();
        }
    }

    public abstract class ValuePart
        : Part
    {
        private readonly string _name;
        private string _label;
        private string _id;

        protected ValuePart(string name, string label)
        {
            _name = name;
            _label = label;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Label
        {
            get { return string.IsNullOrEmpty(_label) ? Name : _label; }
            internal set { _label = value; }
        }

        public string Id
        {
            get { return string.IsNullOrEmpty(_id) ? Name : _id; }
            internal set { _id = value; }
        }

        public int Order { get; internal set; }

        public bool Disabled { get; internal set; }

        public string PostHtml { get; internal set; }
    }

    public class ValuePart<TValue>
        : ValuePart
    {
        private readonly TValue _value;

        public ValuePart(string name, string label, TValue value)
            : base(name, label)
        {
            _value = value;
        }

        public TValue Value
        {
            get { return _value; }
        }
    }

    public class HtmlPart
        : Part
    {
        private readonly string _html;

        public HtmlPart(string html)
        {
            _html = html;
        }

        public string Html
        {
            get { return _html; }
        }
    }

    public abstract class MultiPartField
        : Field
    {
        private readonly Parts _parts = new Parts();

        protected MultiPartField(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        protected void Add(Part part)
        {
            _parts.Add(part);
        }

        protected void Remove(string name)
        {
            var value = (from p in _parts
                         where p is ValuePart
                         && ((ValuePart)p).Name == name
                         select p).SingleOrDefault();
            if (value != null)
                _parts.Remove(value);
        }

        protected void SetLabel(string name, string label)
        {
            var value = (from p in _parts
                         where p is ValuePart
                         && ((ValuePart)p).Name == name
                         select p).SingleOrDefault();
            if (value != null)
                ((ValuePart)value).Label = label;
        }

        protected void SetId(string name, string id)
        {
            var value = (from p in _parts
                         where p is ValuePart
                         && ((ValuePart)p).Name == name
                         select p).SingleOrDefault();
            if (value != null)
                ((ValuePart)value).Id = id;
        }

        protected void SetDisabled(string name, bool disabled)
        {
            var value = (from p in _parts
                where p is ValuePart
                    && ((ValuePart) p).Name == name
                select p).SingleOrDefault();
            if (value != null)
                ((ValuePart) value).Disabled = disabled;
        }

        protected void SetOrder(string name, int order)
        {
            var value = (from p in _parts
                where p is ValuePart
                    && ((ValuePart) p).Name == name
                select p).SingleOrDefault();
            if (value != null)
                ((ValuePart) value).Order = order;
        }

        protected IEnumerable<Part> Parts
        {
            get { return _parts; }
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var part in Parts)
                {
                    if (part is HtmlPart)
                        sb.AppendLine(((HtmlPart)part).Html);
                    else
                        AppendPart(sb, part);
                }

                return MvcHtmlString.Create(sb.ToString());
            }
        }

        protected virtual void AppendPart(StringBuilder sb, Part part)
        {
        }
    }

    public abstract class MultiPartField<TValue>
        : MultiPartField
    {
        protected MultiPartField(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
        }

        protected void AddValue(string name, TValue value)
        {
            Add(new ValuePart<TValue>(name, null, value));
        }
    }

    public abstract class MultiPartField<TModel, TValue>
        : MultiPartField
    {
        private readonly TModel _model;

        protected MultiPartField(HtmlHelper htmlHelper, TModel model)
            : base(htmlHelper)
        {
            _model = model;
        }

        public MultiPartField<TModel, TValue> Add(Expression<Func<TModel, TValue>> getValue)
        {
            Add(new ValuePart<TValue>(GetMemberName(getValue), null, getValue.Compile()(_model)));
            return this;
        }

        public MultiPartField<TModel, TValue> Add(string label, Expression<Func<TModel, TValue>> getValue)
        {
            Add(new ValuePart<TValue>(GetMemberName(getValue), label, getValue.Compile()(_model)));
            return this;
        }

        public MultiPartField<TModel, TValue> Add(string label, Expression<Func<TModel, TValue>> getValue, string postHtml)
        {
            Add(new ValuePart<TValue>(GetMemberName(getValue), label, getValue.Compile()(_model)) {PostHtml = postHtml});
            return this;
        }

        protected void Add(string name, TValue value)
        {
            Add(new ValuePart<TValue>(name, null, value));
        }
    }
}