using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class RadioButtonsField<TModel>
        : MultiPartField<TModel, bool>
    {
        private readonly string _name;
        private bool _withVertical;

        public RadioButtonsField(HtmlHelper htmlHelper, TModel model, string name)
            : base(htmlHelper, model)
        {
            _name = name;
            base.AddCssPrefix("radiobuttons");
            if (!_withVertical)
                base.AddCssPrefix("horizontal_radiobuttons");
        }

        public string Name
        {
            get { return _name; }
        }

        internal override string Label
        {
            get { return base.Label ?? Name; }
        }

        public RadioButtonsField<TModel> WithVertical()
        {
            _withVertical = true;
            return this;
        }

        protected override void AppendPart(StringBuilder sb, Part part)
        {
            if (part is ValuePart<bool>)
            {
                var valuePart = (ValuePart<bool>)part;

                sb.AppendLine("<div class=\"radio_control control\">");

                var htmlAttributes = (!IsReadOnly && !((ValuePart)part).Disabled) ?
                    (object)new { id = valuePart.Id, @class = "radio" } :
                    new { id = valuePart.Id, @class = "radio", disabled = "disabled" };

                sb.Append(RadioButton(Name, valuePart.Name, valuePart.Value, htmlAttributes));
                BuildLabelHtml(sb, valuePart.Name, valuePart.Label);

                if (!string.IsNullOrEmpty(valuePart.PostHtml))
                    sb.Append(valuePart.PostHtml);

                sb.AppendLine("</div>");
            }
            else
            {
                base.AppendPart(sb, part);
            }
        }
    }
    public class RadioButtonsField : MultiPartField<bool>
    {
        private readonly string _name;

        protected RadioButtonsField(HtmlHelper htmlHelper, string name)
            : base(htmlHelper)
        {
            _name = name;
            base.AddCssPrefix("radiobuttons");
        }

        private string Name
        {
            get { return _name; }
        }

        internal override string Label
        {
            get { return base.Label ?? Name; }
        }

        public override MvcHtmlString InnerHtml
        {
            get
            {
                var sb = new StringBuilder();
                var orderedParts = (from p in Parts where p is ValuePart orderby ((ValuePart)p).Order select p);
                foreach (var part in orderedParts)
                    AppendPart(sb, part);

                return MvcHtmlString.Create(sb.ToString());
            }
        }

        protected override void AppendPart(StringBuilder sb, Part part)
        {
            if (part is ValuePart<bool>)
            {
                var valuePart = (ValuePart<bool>)part;

                sb.AppendLine("<div class=\"radio_control control" + (((ValuePart)part).Disabled ? " disabled" : "") + "\">");

                var htmlAttributes = ((ValuePart)part).Disabled ?
                    new { id = valuePart.Id, @class = "radio", disabled = "disabled" } : 
                    (object)new { id = valuePart.Id, @class = "radio" };

                sb.Append(RadioButton(Name, valuePart.Name, valuePart.Value, htmlAttributes));
                BuildLabelHtml(sb, valuePart.Name, valuePart.Label);

                if (!string.IsNullOrEmpty(valuePart.PostHtml))
                    sb.Append(valuePart.PostHtml);

                sb.AppendLine("</div>");
            }
            else
            {
                base.AppendPart(sb, part);
            }
        }
    }

    public class EnumRadioButtonsField<TModel, TEnum>
        : RadioButtonsField<TModel>
        where TEnum : struct, IConvertible
    {
        public EnumRadioButtonsField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TEnum>> getValue)
            : base(htmlHelper, model, GetMemberName(getValue))
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            Add(getValue.Compile()(model));
        }

        public EnumRadioButtonsField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TEnum> getValue)
            : base(htmlHelper, model, name)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            Add(getValue(model));
        }

        public EnumRadioButtonsField<TModel, TEnum> With(IEnumerable<TEnum> values)
        {
            // Remove all that are not included.

            var allValues = (TEnum[])Enum.GetValues(typeof(TEnum));
            foreach (var value in allValues.Except(values))
                Remove(Enum.GetName(typeof(TEnum), value));
            return this;
        }

        public EnumRadioButtonsField<TModel, TEnum> Without(TEnum value)
        {
            Remove(Enum.GetName(typeof(TEnum), value));
            return this;
        }

        public EnumRadioButtonsField<TModel, TEnum> WithLabel(TEnum value, string label)
        {
            SetLabel(Enum.GetName(typeof(TEnum), value), label);
            return this;
        }

        public EnumRadioButtonsField<TModel, TEnum> WithId(TEnum value, string id)
        {
            SetId(Enum.GetName(typeof(TEnum), value), id);
            return this;
        }

        public EnumRadioButtonsField<TModel, TEnum> WithDisabled(TEnum value, bool disabled)
        {
            SetDisabled(Enum.GetName(typeof(TEnum), value), disabled);
            return this;
        }

        public EnumRadioButtonsField<TModel, TEnum> WithOrder(TEnum value, int order)
        {
            SetOrder(Enum.GetName(typeof(TEnum), value), order);
            return this;
        }

        private void Add(TEnum value)
        {
            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));

            for (var index = 0; index < names.Length; ++index)
                Add(names[index], Equals(value, values[index]));
        }
    }

    public class EnumRadioButtonsField<TEnum> : RadioButtonsField where TEnum : struct, IConvertible
    {
        public EnumRadioButtonsField(HtmlHelper htmlHelper, string name, TEnum defaultValue)
            : base(htmlHelper, name)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            // Add one for each enum value.
            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[]) Enum.GetValues(typeof(TEnum));
            for (var index = 0; index < names.Length; ++index)
                Add(new ValuePart<bool>(names[index], null, Equals(values[index], defaultValue)));
        }

        public EnumRadioButtonsField<TEnum> WithLabel(TEnum value, string label)
        {
            SetLabel(Enum.GetName(typeof(TEnum), value), label);
            return this;
        }

        public EnumRadioButtonsField<TEnum> WithDisabled(TEnum value, bool disabled)
        {
            SetDisabled(Enum.GetName(typeof(TEnum), value), disabled);
            return this;
        }

        public EnumRadioButtonsField<TEnum> WithOrder(TEnum value, int order)
        {
            SetOrder(Enum.GetName(typeof(TEnum), value), order);
            return this;
        }
    }

    public class OptionalEnumRadioButtonsField<TModel, TEnum>
        : RadioButtonsField<TModel>
        where TEnum : struct, IConvertible
    {
        public OptionalEnumRadioButtonsField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TEnum?>> getValue)
            : base(htmlHelper, model, GetMemberName(getValue))
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            Add(getValue.Compile()(model));
        }

        public OptionalEnumRadioButtonsField(HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TEnum?> getValue)
            : base(htmlHelper, model, name)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            Add(getValue(model));
        }

        public OptionalEnumRadioButtonsField<TModel, TEnum> Without(TEnum value)
        {
            Remove(Enum.GetName(typeof(TEnum), value));
            return this;
        }

        public OptionalEnumRadioButtonsField<TModel, TEnum> WithLabel(TEnum value, string label)
        {
            SetLabel(Enum.GetName(typeof(TEnum), value), label);
            return this;
        }

        public OptionalEnumRadioButtonsField<TModel, TEnum> WithId(TEnum value, string id)
        {
            SetId(Enum.GetName(typeof(TEnum), value), id);
            return this;
        }

        private void Add(TEnum? value)
        {
            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));

            for (var index = 0; index < names.Length; ++index)
                Add(names[index], value != null && Equals(value.Value, values[index]));
        }
    }

    public static class RadioButtonsFieldExtensions
    {
        public static EnumRadioButtonsField<TModel, TEnum> RadioButtonsField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TEnum>> getValue)
            where TEnum : struct, IConvertible
        {
            return new EnumRadioButtonsField<TModel, TEnum>(htmlHelper, model, getValue);
        }

        public static EnumRadioButtonsField<TModel, TEnum> RadioButtonsField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TEnum> getValue)
            where TEnum : struct, IConvertible
        {
            return new EnumRadioButtonsField<TModel, TEnum>(htmlHelper, model, name, getValue);
        }

        public static EnumRadioButtonsField<TEnum> RadioButtonsField<TEnum>(this HtmlHelper htmlHelper, string name, TEnum defaultValue) where TEnum : struct, IConvertible
        {
            return new EnumRadioButtonsField<TEnum>(htmlHelper, name, defaultValue);
        }

        public static OptionalEnumRadioButtonsField<TModel, TEnum> RadioButtonsField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, TEnum?>> getValue)
            where TEnum : struct, IConvertible
        {
            return new OptionalEnumRadioButtonsField<TModel, TEnum>(htmlHelper, model, getValue);
        }

        public static OptionalEnumRadioButtonsField<TModel, TEnum> RadioButtonsField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, string name, Func<TModel, TEnum?> getValue)
            where TEnum : struct, IConvertible
        {
            return new OptionalEnumRadioButtonsField<TModel, TEnum>(htmlHelper, model, name, getValue);
        }
    }
}
