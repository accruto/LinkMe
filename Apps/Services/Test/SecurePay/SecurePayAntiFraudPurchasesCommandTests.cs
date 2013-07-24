using System;
using LinkMe.Apps.Mocks.Services.SecurePay;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass, Ignore]
    public class SecurePayAntiFraudPurchasesCommandTests
        : AntiFraudPurchasesCommandTests
    {
        private readonly IPurchaseTransactionsCommand _purchaseTransactionsCommand = Resolve<IPurchaseTransactionsCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly Country _country;
        private readonly string _url = Resolve<string>("securepay.url");
        private readonly string _merchantId = Resolve<string>("securepay.merchantid");
        private readonly string _password = Resolve<string>("securepay.password");
        private readonly string _antiFraudUrl = Resolve<string>("securepay.antifraud.url");
        private readonly string _antitfraudMerchantId = Resolve<string>("securepay.antifraud.merchantid");
        private readonly string _antitfraudPassword = Resolve<string>("securepay.antifraud.password");
        private const string Country = "AUS";
        private const string EmailAddress = "bill@test.linkme.net.au";

        public SecurePayAntiFraudPurchasesCommandTests()
        {
            _country = _locationQuery.GetCountry("Australia");
        }

        protected override IPurchasesCommand CreatePurchaseCommand(string url, string merchantId, string password, int? responseCode)
        {
            return new PurchasesCommand(
                new SendSecurePayCommand(_purchaseTransactionsCommand, true, url ?? _url, url ?? _antiFraudUrl),
                _locationQuery,
                true,
                _merchantId,
                _password,
                merchantId ?? _antitfraudMerchantId,
                password ?? _antitfraudPassword,
                _country.Name);
        }

        protected override IPurchasesCommand CreatePurchaseCommand(string url, FraudFailure failure)
        {
            return new PurchasesCommand(
                new SendSecurePayCommand(_purchaseTransactionsCommand, true, url ?? _url, url ?? _antiFraudUrl),
                _locationQuery,
                true,
                _merchantId,
                _password,
                _antitfraudMerchantId,
                _antitfraudPassword,
                _country.Name);
        }

        [TestMethod]
        public void TestInvalidResponseIgnoreCents()
        {
            var requestMessage = CreateRequestMessage();

            var command = (ISendSecurePayCommand)new SendSecurePayCommand(_purchaseTransactionsCommand, true, _url, _antiFraudUrl);
            var responseMessage = command.Send<AntiFraudPaymentRequestTxn, AntiFraudPaymentResponseTxn>(requestMessage);

            Assert.AreEqual("20", responseMessage.Payment.TxnList.Txn.ResponseCode);

            // Ignore the cents.

            requestMessage = CreateRequestMessage();
            var originalAmount = requestMessage.Payment.TxnList.Txn.Amount;

            command = new SendSecurePayCommand(_purchaseTransactionsCommand, true, _url, _antiFraudUrl, true);
            responseMessage = command.Send<AntiFraudPaymentRequestTxn, AntiFraudPaymentResponseTxn>(requestMessage);

            Assert.AreEqual("00", responseMessage.Payment.TxnList.Txn.ResponseCode);
            Assert.AreEqual(originalAmount, responseMessage.Payment.TxnList.Txn.Amount);
        }

        private PaymentRequestMessage<AntiFraudPaymentRequestTxn> CreateRequestMessage()
        {
            var message = new PaymentRequestMessage<AntiFraudPaymentRequestTxn>
            {
                MerchantInfo =
                {
                    MerchantId = _merchantId,
                    Password = _password
                }
            };

            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.Amount = 37620;
            message.Payment.TxnList.Txn.Currency = Domain.Currency.AUD;

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = AustralianCreditCardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "123";
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(2));

            message.Payment.TxnList.Txn.BuyerInfo.Ip = AustralianIpAddress;
            message.Payment.TxnList.Txn.BuyerInfo.EmailAddress = EmailAddress;
            message.Payment.TxnList.Txn.BuyerInfo.BillingCountry = Country;
            message.Payment.TxnList.Txn.BuyerInfo.DeliveryCountry = Country;

            message.Prepare();
            message.Validate();

            return message;
        }
    }
}
