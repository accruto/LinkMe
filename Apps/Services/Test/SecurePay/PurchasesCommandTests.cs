using System;
using System.Net;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Products.Queries;
using LinkMe.Domain.Roles.Orders;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    public abstract class PurchasesCommandTests
        : TestClass
    {
        private readonly IProductsQuery _productsQuery = Resolve<IProductsQuery>();

        protected const string AustralianCreditCardNumber = "4444333322220000"; // Insert valid credit card number here to run tests against SecurePay anti-fraud.

        protected const string NonAustralianCreditCardNumber = "4444333322221111";
        private const CreditCardType AustralianCreditCardType = CreditCardType.MasterCard;
        private const CreditCardType NonAustralianCreditCardType = CreditCardType.Visa;
        private const string Cvv = "123";
        private const string CardHolderName = "Marge Simpson";
        protected const string AustralianIpAddress = "211.27.226.1";
        protected const string NonAustralianIpAddress = "98.201.110.1";

        [TestMethod]
        public void TestPurchaseOrder()
        {
            var purchasesCommand = CreatePurchaseCommand(null, null, null, null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            var receipt = purchasesCommand.PurchaseOrder(order, purchaser, creditCard);
            AssertReceipt(receipt);
        }

        [TestMethod]
        public void TestReferToCardIssuer()
        {
            var purchasesCommand = CreatePurchaseCommand(null, null, null, 1);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var order = CreateOrder(purchasesCommand);

            // The 0.01 should trigger the error in the test environment.

            order.AdjustedPrice += (decimal)0.01;
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertNotApprovedException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                "01",
                "Refer to Card Issuer");
        }

        [TestMethod]
        public void TestPickUpCardSpecialConditions()
        {
            var purchasesCommand = CreatePurchaseCommand(null, null, null, 7);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var order = CreateOrder(purchasesCommand);

            // The 0.07 should trigger the error in the test environment.

            order.AdjustedPrice += (decimal)0.07;
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertNotApprovedException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                "07",
                "Pick Up Card, Special Conditions");
        }

        [TestMethod]
        public void TestInvalidMerchantId()
        {
            var purchasesCommand = CreatePurchaseCommand(null, "xyz", null, null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertValidationException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                "MerchantId",
                typeof(LengthValidationError));
        }

        [TestMethod]
        public void TestInvalidPassword()
        {
            var purchasesCommand = CreatePurchaseCommand(null, null, "a", null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertValidationException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                "Password",
                typeof(LengthRangeValidationError));
        }

        [TestMethod]
        public void TestUnknownMerchantId()
        {
            var purchasesCommand = CreatePurchaseCommand(null, "XYZ1234", null, null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertStatusException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                504,
                "Invalid merchant ID");
        }

        [TestMethod]
        public void TestBadPassword()
        {
            var purchasesCommand = CreatePurchaseCommand(null, null, "xxxxxxx", null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertStatusException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                504, //550
                "Invalid merchant ID"); //"Invalid password");
        }

        [TestMethod]
        public void TestBadUrl()
        {
            var purchasesCommand = CreatePurchaseCommand("http://sgdhgdfjkg.com", null, null, null);
            var order = CreateOrder(purchasesCommand);
            var purchaser = CreatePurchaser(AustralianIpAddress);
            var creditCard = CreateCreditCard(AustralianCreditCardNumber);

            AssertSystemException(
                purchasesCommand.PurchaseOrder,
                order,
                purchaser,
                creditCard,
                "The order cannot be processed.",
                "The remote name could not be resolved: 'sgdhgdfjkg.com'");
        }

        private static void AssertValidationException<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, string name, Type errorType)
        {
            try
            {
                func(t1, t2, t3);
                Assert.Fail("Expected an exception.");
            }
            catch (PurchaseSystemException ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ValidationErrorsException));
                var exception = (ValidationErrorsException)ex.InnerException;
                Assert.AreEqual(1, exception.Errors.Count);
                Assert.AreEqual(name, exception.Errors[0].Name);
                Assert.IsInstanceOfType(exception.Errors[0], errorType);
            }
        }

        private static void AssertStatusException<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, int code, string description)
        {
            try
            {
                func(t1, t2, t3);
                Assert.Fail("Expected an exception.");
            }
            catch (SecurePayStatusException ex)
            {
                Assert.IsNull(ex.InnerException);
                Assert.AreEqual(code, ex.Code);
                Assert.AreEqual(description, ex.Message);
            }
        }

        private static void AssertSystemException<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, string message, string innerMessage)
        {
            try
            {
                func(t1, t2, t3);
                Assert.Fail("Expected an exception.");
            }
            catch (PurchaseSystemException ex)
            {
                Assert.AreEqual(message, ex.Message);
                Assert.IsNotNull(ex.InnerException);
                Assert.IsInstanceOfType(ex.InnerException, typeof(WebException));
                Assert.AreEqual(innerMessage, ex.InnerException.Message);
            }
        }

        private static void AssertNotApprovedException<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, string code, string description)
        {
            try
            {
                func(t1, t2, t3);
                Assert.Fail("Expected an exception.");
            }
            catch (SecurePayNotApprovedException ex)
            {
                Assert.IsNull(ex.InnerException);
                Assert.AreEqual(code, ex.Code);
                Assert.AreEqual(description, ex.Message);
            }
        }

        private static void AssertReceipt(CreditCardReceipt receipt)
        {
            Assert.IsNotNull(receipt);
            Assert.IsNotNull(receipt.CreditCard);
            if (receipt.CreditCard.Pan == AustralianCreditCardNumber.GetCreditCardPan())
            {
                Assert.AreEqual(AustralianCreditCardType, receipt.CreditCard.Type);
            }
            else
            {
                Assert.AreEqual(NonAustralianCreditCardNumber.GetCreditCardPan(), receipt.CreditCard.Pan);
                Assert.AreEqual(NonAustralianCreditCardType, receipt.CreditCard.Type);
            }
        }

        protected abstract IPurchasesCommand CreatePurchaseCommand(string url, string merchantId, string password, int? responseCode);

        protected static Purchaser CreatePurchaser(string ipAddress)
        {
            return new Purchaser
            {
                Id = Guid.NewGuid(),
                IpAddress = ipAddress,
                EmailAddress = "bill@test.linkme.net.au",
            };
        }

        protected static CreditCard CreateCreditCard(string cardNumber)
        {
            return new CreditCard
            {
                CardHolderName = CardHolderName,
                CardNumber = cardNumber,
                CardType = CreditCardType.MasterCard,
                Cvv = Cvv,
                ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(1))
            };
        }

        protected Order CreateOrder(IPurchasesCommand puchasesCommand)
        {
            var product = _productsQuery.GetProduct("Contacts5");

            IOrdersCommand ordersCommand = new OrdersCommand(
                Resolve<IOrdersRepository>(),
                Resolve<IOrderPricesCommand>(),
                _productsQuery,
                Resolve<IAllocationsCommand>(),
                Resolve<IAllocationsQuery>(),
                puchasesCommand);
            return ordersCommand.PrepareOrder(new[] { product.Id }, null, null, CreateCreditCard(AustralianCreditCardNumber).CardType);
        }
    }
}
