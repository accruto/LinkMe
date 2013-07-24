using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Products;

namespace LinkMe.Apps.Presentation.Domain.Products
{
    public static class ProductsExtensions
    {
        private class CreditAdjustmentComparer
            : IComparer<int?>
        {
            public int Compare(int? x, int? y)
            {
                if (x != null && y == null)
                    return -1;
                if (x == null && y != null)
                    return 1;
                return 0;
            }
        }

        public static string GetQuantityDisplayText(this int? quantity)
        {
            return quantity == null ? "unlimited" : quantity.Value.ToString();
        }

        public static string GetExpiryDateDisplayText(this DateTime? expiryDate)
        {
            return expiryDate == null
                ? "never"
                : expiryDate.Value.ToShortDateString();
        }

        public static string GetPriceDisplayText(this decimal price, Currency currency)
        {
            return GetPriceDisplayText(price, currency, true);
        }

        public static string GetPriceDisplayText(this decimal price, Currency currency, bool rounding)
        {
            price = Math.Round(price, currency.CultureInfo.NumberFormat.CurrencyDecimalDigits);
            return (Math.Round(price) == price && rounding)
                ? price.ToString("C0", currency.CultureInfo)
                : price.ToString("C", currency.CultureInfo);
        }

        public static string GetPricePerCreditDisplayText(this decimal price, int? quantity, Currency currency)
        {
            return quantity == null || quantity.Value == 0
                ? "N/A"
                : GetPriceDisplayText(price / quantity.Value, currency);
        }

        public static ProductCreditAdjustment GetPrimaryCreditAdjustment(this Product product)
        {
            if (product.CreditAdjustments.Count == 1)
                return product.CreditAdjustments[0];

            // Some products are combinations of a limited allocation of one credit plus an unlimited allocation of another.
            // Return the limited credit.

            return product.CreditAdjustments.OrderBy(a => a.Quantity, new CreditAdjustmentComparer()).FirstOrDefault();
        }
    }
}