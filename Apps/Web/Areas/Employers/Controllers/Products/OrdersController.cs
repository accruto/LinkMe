using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Affiliations;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Domain.Users.Employers.Orders.Commands;
using LinkMe.Domain.Users.Employers.Orders.Queries;
using LinkMe.Web.Areas.Employers.Models.Products;

namespace LinkMe.Web.Areas.Employers.Controllers.Products
{
    public class OrdersController
        : EmployersController
    {
        private readonly IEmployerOrdersCommand _employerOrdersCommand;
        private readonly IEmployerOrdersQuery _employerOrdersQuery;
        private readonly IProductsQuery _productsQuery;
        private readonly ICouponsQuery _couponsQuery;
        private readonly ICreditsQuery _creditsQuery;

        public OrdersController(IEmployerOrdersCommand employerOrdersCommand, IEmployerOrdersQuery employerOrdersQuery, IProductsQuery productsQuery, ICouponsQuery couponsQuery, ICreditsQuery creditsQuery)
        {
            _employerOrdersCommand = employerOrdersCommand;
            _employerOrdersQuery = employerOrdersQuery;
            _productsQuery = productsQuery;
            _couponsQuery = couponsQuery;
            _creditsQuery = creditsQuery;
        }

        public ActionResult PrepareCompactOrder(Guid[] productIds)
        {
            var order = GetOrder(productIds, null, null, null);
            return PartialView("OrderCompactDetails", _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()));
        }

        public ActionResult PrepareOrder(Guid[] productIds, CheckBoxValue useDiscount, Guid? couponId, CreditCardType? creditCardType)
        {
            var coupon = couponId == null ? null : _couponsQuery.GetCoupon(couponId.Value);
            var order = GetOrder(productIds, useDiscount, coupon, creditCardType);
            return PartialView("OrderDetails", _employerOrdersQuery.GetOrderDetails(_creditsQuery, order, _productsQuery.GetProducts()));
        }

        private Order GetOrder(IEnumerable<Guid> productIds, CheckBoxValue useDiscount, Coupon coupon, CreditCardType? creditCardType)
        {
            creditCardType = creditCardType ?? default(CreditCardType);
            return (useDiscount == null || !useDiscount.IsChecked)
                ? _employerOrdersCommand.PrepareOrder(_productsQuery, _creditsQuery, productIds, coupon, null, creditCardType.Value)
                : _employerOrdersCommand.PrepareOrder(_productsQuery, _creditsQuery, productIds, coupon, new VecciDiscount { Percentage = 0.2m }, creditCardType.Value);
        }
    }
}