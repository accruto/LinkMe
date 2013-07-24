using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Api.Models.Coupons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Coupons
{
    [TestClass]
    public class CouponsApiTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICouponsCommand _couponsCommand = Resolve<ICouponsCommand>();
        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();

        private const string CouponCodeFormat = "ABC00{0}";
        private const decimal PercentageDiscount = (decimal)0.1;
        private const decimal AmountDiscount = 50;
        private ReadOnlyUrl _couponUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _couponUrl = new ReadOnlyApplicationUrl("~/api/coupons");
        }

        [TestMethod]
        public void TestPercentageCoupon()
        {
            var coupon = CreateCoupon(0, true, null, null, null);
            AssertCoupon(coupon, Coupon(coupon.Code));
        }

        [TestMethod]
        public void TestAmountCoupon()
        {
            var coupon = CreateCoupon(0, false, null, null, null);
            AssertCoupon(coupon, Coupon(coupon.Code));
        }

        [TestMethod]
        public void TestUnknownCoupon()
        {
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, "unknown"), null, "300", "The coupon code cannot be found. Please confirm you've typed it in correctly.");
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, ""), null, "300", "The coupon code cannot be found. Please confirm you've typed it in correctly.");
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, null), null, "300", "The coupon code cannot be found. Please confirm you've typed it in correctly.");
        }

        [TestMethod]
        public void TestDisabledCoupon()
        {
            var coupon = CreateCoupon(0, true, null, null, null);
            _couponsCommand.DisableCoupon(coupon.Id);
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code), null, "300", "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestExpiredCoupon()
        {
            var coupon = CreateCoupon(0, true, DateTime.Now.AddDays(-20), null, null);
            _couponsCommand.DisableCoupon(coupon.Id);
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code), null, "300", "The coupon has already expired and is no longer valid.");
        }

        [TestMethod]
        public void TestProductCoupon()
        {
            var product1 = _productsQuery.GetProducts(UserType.Employer)[1];
            var product2 = _productsQuery.GetProducts(UserType.Employer)[2];
            var coupon = CreateCoupon(0, true, null, null, new[] { product1.Id });

            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code), null, "300", "The coupon does not apply to the type of products you've ordered.");
            AssertJsonSuccess(Coupon(coupon.Code, product1.Id));
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code, product2.Id), null, "300", "The coupon does not apply to the type of products you've ordered.");
        }

        [TestMethod]
        public void TestRedeemerCoupon()
        {
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(2));
            var coupon = CreateCoupon(0, true, null, new[] { employer1.Id }, null);

            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code), null, "300", "The coupon code is not valid for your account. Please login as a different user or user another coupon code.");

            LogIn(employer1);
            AssertJsonSuccess(Coupon(coupon.Code));

            LogOut();
            LogIn(employer2);
            AssertJsonError(Coupon(HttpStatusCode.Forbidden, coupon.Code), null, "300", "The coupon code is not valid for your account. Please login as a different user or user another coupon code.");
        }

        private static void AssertCoupon(Coupon coupon, JsonCouponModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(coupon.Id, model.Id);
            Assert.AreEqual(coupon.Code, model.Code);
        }

        private JsonCouponModel Coupon(string code, params Guid[] productIds)
        {
            return Coupon(null, code, productIds);
        }

        private JsonCouponModel Coupon(HttpStatusCode? expectedStatusCode, string code, params Guid[] productIds)
        {
            var response = expectedStatusCode == null
                ? Post(_couponUrl, GetParameters(code, productIds))
                : Post(expectedStatusCode.Value, _couponUrl, GetParameters(code, productIds));
            return new JavaScriptSerializer().Deserialize<JsonCouponModel>(response);
        }

        private static NameValueCollection GetParameters(string code, IEnumerable<Guid> productIds)
        {
            var parameters = new NameValueCollection { { "code", code } };
            if (productIds != null)
            {
                foreach (var productId in productIds)
                    parameters.Add("productId", productId.ToString());
            }

            return parameters;
        }

        private Coupon CreateCoupon(int index, bool percentageDiscount, DateTime? expiryDate, IList<Guid> redeemerIds, IList<Guid> productIds)
        {
            var coupon = new Coupon
            {
                Code = string.Format(CouponCodeFormat, index),
                Discount = percentageDiscount ? (CouponDiscount) new PercentageCouponDiscount { Percentage = PercentageDiscount } : new FixedCouponDiscount { Amount = AmountDiscount },
                IsEnabled = true,
                CanBeUsedOnce = false,
                ExpiryDate = expiryDate,
                RedeemerIds = redeemerIds,
                ProductIds = productIds,
            };
            _couponsCommand.CreateCoupon(coupon);
            return coupon;
        }
    }
}
