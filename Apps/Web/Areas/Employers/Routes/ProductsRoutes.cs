using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Domain.Products;
using LinkMe.Web.Areas.Employers.Controllers.Products;

namespace LinkMe.Web.Areas.Employers.Routes
{
    public static class ProductsRoutes
    {
        public static RouteReference NewOrder { get; private set; }
        public static RouteReference Choose { get; private set; }
        public static RouteReference Account { get; private set; }
        public static RouteReference Payment { get; private set; }
        public static RouteReference Receipt { get; private set; }
        public static RouteReference Print { get; private set; }
        public static RouteReference Credits { get; private set; }
        public static RouteReference Order { get; private set; }
        public static RouteReference Orders { get; private set; }
        public static RouteReference PrepareOrder { get; private set; }
        public static RouteReference PrepareCompactOrder { get; private set; }

        public static void RegisterRoutes(AreaRegistrationContext context)
        {
            NewOrder = context.MapAreaRoute<NewOrderController>("employers/products/neworder", c => c.NewOrder);
            Choose = context.MapAreaRoute<NewOrderController, string>("employers/products/choose", c => c.Choose);
            Account = context.MapAreaRoute<NewOrderController>("employers/products/account", c => c.Account);
            Payment = context.MapAreaRoute<NewOrderController>("employers/products/payment", c => c.Payment);
            Receipt = context.MapAreaRoute<NewOrderController>("employers/products/receipt", c => c.Receipt);

            context.MapRedirectRoute("employers/PricingPlan.aspx", NewOrder);

            PrepareOrder = context.MapAreaRoute<OrdersController, Guid[], CheckBoxValue, Guid?, CreditCardType?>("employers/products/prepareorder", c => c.PrepareOrder);
            PrepareCompactOrder = context.MapAreaRoute<OrdersController, Guid[]>("employers/products/preparecompactorder", c => c.PrepareCompactOrder);

            Print = context.MapAreaRoute<ProductsController, Guid>("employers/products/print/{id}", c => c.Print);
            Credits = context.MapAreaRoute<ProductsController>("employers/products/credits", c => c.Credits);
            Orders = context.MapAreaRoute<ProductsController>("employers/products/orders", c => c.Orders);
            Order = context.MapAreaRoute<ProductsController, Guid>("employers/products/order/{id}", c => c.Order);
        }
    }
}