using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Presentation.Domain.Roles.Orders;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders;

namespace LinkMe.Web.Domain.Roles.Orders
{
    public static class OrdersHtmlExtensions
    {
        public static string OrderTime(this HtmlHelper htmlHelper, DateTime time)
        {
            return time.GetOrderTimeDisplayText();
        }

        public static string AdjustmentLabelCssClass(this HtmlHelper htmlHelper, OrderAdjustment adjustment)
        {
            return adjustment.GetAdjustmentTypeString() + "_order-adjustment order-adjustment";
        }

        public static string AdjustmentLabelText(this HtmlHelper htmlHelper, Order order, OrderAdjustment adjustment, IEnumerable<Product> products)
        {
            return order.GetAdjustmentLabelText(adjustment, products);
        }
    }
}
