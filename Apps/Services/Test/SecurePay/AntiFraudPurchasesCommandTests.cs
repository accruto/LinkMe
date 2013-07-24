using System;
using LinkMe.Apps.Mocks.Services.SecurePay;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Roles.Orders.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    public abstract class AntiFraudPurchasesCommandTests
        : PurchasesCommandTests
    {
        [TestMethod]
        public void TestInvalidCardCountry()
        {
            var purchasesCommand = CreatePurchaseCommand(null, FraudFailure.IpCountryCardCountry);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(NonAustralianCreditCardNumber);

            AssertFraudException(
                (o, p, c) => purchasesCommand.PurchaseOrder(o, p, c),
                order,
                purchaser,
                creditCard,
                "AUS",
                "",
                FraudFailure.IpCountryCardCountry);
        }

        [TestMethod]
        public void TestInvalidIpCountry()
        {
            var purchasesCommand = CreatePurchaseCommand(null, FraudFailure.IpRiskCountryFail);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(NonAustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertFraudException(
                (o, p, c) => purchasesCommand.PurchaseOrder(o, p, c),
                order,
                purchaser,
                creditCard,
                "USA",
                "AUS",
                FraudFailure.IpRiskCountryFail);
        }

        protected abstract IPurchasesCommand CreatePurchaseCommand(string url, FraudFailure failure);

        private static void AssertFraudException<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, string ipCountry, string cardCountry, FraudFailure failure)
        {
            try
            {
                func(t1, t2, t3);
                Assert.Fail("Expected an exception.");
            }
            catch (SecurePayFraudException ex)
            {
                Assert.IsNull(ex.InnerException);
                Assert.AreEqual("450", ex.Code);
                Assert.AreEqual("Suspected fraud", ex.Message);
                Assert.AreEqual(100, ex.FraudGuard.Score);
                Assert.AreEqual(ipCountry, ex.FraudGuard.InfoIpCountry);
                Assert.AreEqual(cardCountry, ex.FraudGuard.InfoCardCountry);
                Assert.AreEqual(false, ex.FraudGuard.IpCountryFail);
                Assert.AreEqual(false, ex.FraudGuard.MinAmountFail);
                Assert.AreEqual(false, ex.FraudGuard.MaxAmountFail);
                Assert.AreEqual(0, ex.FraudGuard.OpenProxyFail);
                Assert.AreEqual(failure == FraudFailure.IpCountryCardCountry ? 100 : 0, ex.FraudGuard.IpCountryCardCountryFail);
                Assert.AreEqual(0, ex.FraudGuard.IpCardFail);
                Assert.AreEqual(0, ex.FraudGuard.IpRiskCountryFail);
                Assert.AreEqual(0, ex.FraudGuard.IpBillingFail);
                Assert.AreEqual(0, ex.FraudGuard.IpDeliveryFail);
                Assert.AreEqual(0, ex.FraudGuard.BillingDeliveryFail);
                Assert.AreEqual(0, ex.FraudGuard.FreeEmailFail);
                Assert.AreEqual(0, ex.FraudGuard.TooManySameBank);
                Assert.AreEqual(0, ex.FraudGuard.TooManyDeclined);
                Assert.AreEqual(0, ex.FraudGuard.TooManySameIp);
                Assert.AreEqual(0, ex.FraudGuard.TooManySameCard);
                Assert.AreEqual(0, ex.FraudGuard.LowHighAmount);
                Assert.AreEqual(0, ex.FraudGuard.TooManySameEmail);
            }
        }
    }
}
