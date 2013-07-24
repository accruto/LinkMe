using System;
using System.Web.Mvc;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Orders.Queries;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Web.Areas.Api.Models.Coupons;

namespace LinkMe.Web.Areas.Api.Controllers
{
    public class CouponsApiController
        : ApiController
    {
        private readonly ICouponsQuery _couponsQuery;
        private readonly IOrdersCommand _ordersCommand;

        public CouponsApiController(ICouponsQuery couponsQuery, IOrdersCommand ordersCommand)
        {
            _couponsQuery = couponsQuery;
            _ordersCommand = ordersCommand;
        }

        [HttpPost]
        public ActionResult Coupon(string code, Guid[] productIds)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                    throw new CouponNotFoundException();

                var coupon = _couponsQuery.GetCoupon(code);
                if (coupon == null)
                    throw new CouponNotFoundException();
                
                var user = CurrentRegisteredUser;
                _ordersCommand.ValidateCoupon(coupon, user == null ? (Guid?) null : user.Id, productIds);

                return Json(new JsonCouponModel { Id = coupon.Id, Code = coupon.Code });
            }
            catch (UserException ex)
            {
                ModelState.AddModelError(ex, new StandardErrorHandler());
            }

            return Json(new JsonResponseModel());
        }
    }
}
