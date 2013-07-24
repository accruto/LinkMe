using System;
using System.Web.Mvc;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain;
using LinkMe.Domain.Credits;

namespace LinkMe.Web.Domain.Products
{
    public static class ProductsHtmlExtensions
    {
        public static string Quantity(this HtmlHelper htmlHelper, int? quantity)
        {
            return quantity.GetQuantityDisplayText();
        }

        public static string ExpiryDate(this HtmlHelper htmlHelper, DateTime? expiryDate)
        {
            return expiryDate.GetExpiryDateDisplayText();
        }

        public static string Price(this HtmlHelper htmlHelper, decimal price, Currency currency)
        {
            return price.GetPriceDisplayText(currency);
        }

        public static string Price(this HtmlHelper htmlHelper, decimal price, Currency currency, bool rounding)
        {
            return price.GetPriceDisplayText(currency, rounding);
        }

        public static string PricePerCredit(this HtmlHelper htmlHelper, decimal price, int? quantity, Currency currency)
        {
            return price.GetPricePerCreditDisplayText(quantity, currency);
        }

        public static string Status(this HtmlHelper htmlHelper, Allocation allocation)
        {
            if (allocation.IsDeallocated)
                return "Deallocated";

            if (allocation.HasExpired)
                return "Expired";

            if (allocation.IsUnlimited || allocation.RemainingQuantity.Value > 0)
                return "Active";

            return "Inactive";
        }
    }
}
