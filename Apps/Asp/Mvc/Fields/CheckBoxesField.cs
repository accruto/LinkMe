using System;
using System.Text;
using System.Web.Mvc;
using LinkMe.Framework.Utility;

namespace LinkMe.Apps.Asp.Mvc.Fields
{
    public class CheckBoxesField<TModel>
        : MultiPartField<TModel, bool>
    {
        private bool _withVertical;

        public CheckBoxesField(HtmlHelper htmlHelper, TModel model)
            : base(htmlHelper, model)
        {
            base.AddCssPrefix("checkboxes");
        }

        protected override void AppendPart(StringBuilder sb, Part part)
        {
            if (part is ValuePart<bool>)
            {
                var valuePart = (ValuePart<bool>)part;

                if (_withVertical)
                    sb.AppendLine("<div class=\"checkbox_control control\">");

               var htmlAttributes = IsReadOnly
                   ? (object)new { @class = "checkbox", disabled = "disabled" }
                   : new { @class = "checkbox" };
                sb.AppendLine(CheckBox(valuePart.Name, valuePart.Value, htmlAttributes).ToString());
                BuildLabelHtml(sb, valuePart.Name, valuePart.Label);

                if (!string.IsNullOrEmpty(valuePart.PostHtml))
                    sb.Append(valuePart.PostHtml);

                if (_withVertical)
                    sb.AppendLine("</div>");
            }
            else
            {
                base.AppendPart(sb, part);
            }
        }

        public CheckBoxesField<TModel> WithVertical()
        {
            _withVertical = true;
            return this;
        }
    }

    public class EnumCheckBoxesField<TModel, TEnum>
        : CheckBoxesField<TModel>
        where TEnum : struct, IConvertible
    {
        public EnumCheckBoxesField(HtmlHelper htmlHelper, TModel model, Func<TModel, TEnum> getValue)
            : base(htmlHelper, model)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            var value = getValue(model);

            // Add one for each enum value.

            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[]) Enum.GetValues(typeof(TEnum));

            for (var index = 0; index < names.Length; ++index)
                Add(names[index], value.IsFlagSet(values[index]));
        }

        public EnumCheckBoxesField(HtmlHelper htmlHelper, TModel model, Func<TModel, TEnum?> getValue)
            : base(htmlHelper, model)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ApplicationException("Cannot use a non-enum type.");

            var value = getValue(model);

            // Add one for each enum value.

            var names = Enum.GetNames(typeof(TEnum));
            var values = (TEnum[])Enum.GetValues(typeof(TEnum));

            for (var index = 0; index < names.Length; ++index)
                Add(names[index], value != null && value.Value.IsFlagSet(values[index]));
        }

        public EnumCheckBoxesField<TModel, TEnum> Without(TEnum value)
        {
            Remove(Enum.GetName(typeof(TEnum), value));
            return this;
        }

        public EnumCheckBoxesField<TModel, TEnum> WithLabel(TEnum value, string label)
        {
            SetLabel(Enum.GetName(typeof(TEnum), value), label);
            return this;
        }
    }

    public static class CheckBoxesFieldExtensions
    {
        public static CheckBoxesField<TModel> CheckBoxesField<TModel>(this HtmlHelper htmlHelper, TModel model)
        {
            return new CheckBoxesField<TModel>(htmlHelper, model);
        }

        public static EnumCheckBoxesField<TModel, TEnum> CheckBoxesField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, Func<TModel, TEnum> getValue)
            where TEnum : struct, IConvertible
        {
            return new EnumCheckBoxesField<TModel, TEnum>(htmlHelper, model, getValue);
        }

        public static EnumCheckBoxesField<TModel, TEnum> CheckBoxesField<TModel, TEnum>(this HtmlHelper htmlHelper, TModel model, Func<TModel, TEnum?> getValue)
            where TEnum : struct, IConvertible
        {
            return new EnumCheckBoxesField<TModel, TEnum>(htmlHelper, model, getValue);
        }
    }
}