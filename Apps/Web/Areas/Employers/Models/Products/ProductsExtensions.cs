using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Presentation.Domain.Products;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;

namespace LinkMe.Web.Areas.Employers.Models.Products
{
    public static class ProductsExtensions
    {
        public static Order PrepareOrder(this IEmployerOrdersCommand employerOrdersCommand, IProductsQuery productsQuery, ICreditsQuery creditsQuery, IEnumerable<Guid> productIds, Coupon coupon, Discount discount, CreditCardType creditCardType)
        {
            var order = employerOrdersCommand.PrepareOrder(productIds, coupon, discount, creditCardType);
            SortItems(creditsQuery, order, productsQuery.GetProducts());
            return order;
        }

        private static void SortItems(ICreditsQuery creditsQuery, Order order, IEnumerable<Product> products)
        {
            // Reorder the items by primary credit description just to introduce some certainty.

            order.Items = (from i in order.Items
                           join p in products on i.ProductId equals p.Id
                           let a = p.GetPrimaryCreditAdjustment()
                           let c = a == null ? null : creditsQuery.GetCredit(a.CreditId)
                           orderby c == null ? "" : c.Description
                           select i).ToList();

        }

        public static OrderSummaryModel GetOrderSummary(this IEmployerOrdersQuery employerOrdersQuery, ICreditsQuery creditsQuery, IOrdersQuery ordersQuery, Order order, IEnumerable<Product> products, IRegisteredUser purchaser)
        {
            // Reorder the items by product name just to introduce some certainty.

            SortItems(creditsQuery, order, products);
            var receipt = ordersQuery.GetPurchaseReceipt(order.Id) as CreditCardReceipt;
            return new OrderSummaryModel { OrderDetails = employerOrdersQuery.GetOrderDetails(creditsQuery, order, products), Receipt = receipt, Purchaser = purchaser };
        }

        public static OrderDetailsModel GetOrderDetails(this IEmployerOrdersQuery employerOrdersQuery, ICreditsQuery creditsQuery, Order order, IEnumerable<Product> products)
        {
            return new OrderDetailsModel
            {
                Order = order,
                Products = products.ToList(),
                OrderProducts = GetOrderProducts(order, products),
                Credits = creditsQuery.GetCredits().ToDictionary(c => c.Id, c => c),
            };
        }

        private static List<Product> GetOrderProducts(Order order, IEnumerable<Product> products)
        {
            // Grab the list of products corresponding to the order items.

            return (from i in order.Items
            join p in products on i.ProductId equals p.Id
            select p).ToList();
        }
    }
}