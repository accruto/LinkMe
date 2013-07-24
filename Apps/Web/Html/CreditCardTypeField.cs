using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Fields;
using LinkMe.Domain.Products;
using LinkMe.Web.Content;

namespace LinkMe.Web.Html
{
    public class CreditCardTypeField<TModel>
        : EnumDropDownListField<TModel, CreditCardType>
    {
        public CreditCardTypeField(HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, CreditCardType>> getValue)
            : base(htmlHelper, model, getValue)
        {
            base.AddCssPrefix("credit-card-type");
            SetText(GetText);
        }

        private static string GetText(CreditCardType type)
        {
            switch (type)
            {
                case CreditCardType.Amex:
                    return "American Express";

                default:
                    return type.ToString();
            }
        }

        protected override void BuildPostInnerHtml(StringBuilder sb)
        {
            base.BuildPostInnerHtml(sb);

            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", Images.Block.PaymentOptions.ToString());
            builder.MergeAttribute("alt", "Pay with VISA, American Express or Mastercard");
            sb.AppendLine(builder.ToString(TagRenderMode.SelfClosing));
        }
    }

    public static class CreditCardTypeFieldExtensions
    {
        public static CreditCardTypeField<TModel> CreditCardTypeField<TModel>(this HtmlHelper htmlHelper, TModel model, Expression<Func<TModel, CreditCardType>> getValue)
        {
            return new CreditCardTypeField<TModel>(htmlHelper, model, getValue);
        }
    }
}
