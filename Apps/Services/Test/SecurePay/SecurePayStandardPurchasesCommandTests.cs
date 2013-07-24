using System;
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
    public class SecurePayStandardPurchasesCommandTests
        : PurchasesCommandTests
    {
        private readonly IPurchaseTransactionsCommand _purchaseTransactionsCommand = Resolve<IPurchaseTransactionsCommand>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly Country _country;
        private readonly string _url = Resolve<string>("securepay.url");
        private readonly string _merchantId = Resolve<string>("securepay.merchantid");
        private readonly string _password = Resolve<string>("securepay.password");
        private readonly string _antiFraudUrl = Resolve<string>("securepay.antifraud.url");
        private readonly string _antiFraudMerchantId = Resolve<string>("securepay.antifraud.merchantid");
        private readonly string _antiFraudPassword = Resolve<string>("securepay.antifraud.password");

        public SecurePayStandardPurchasesCommandTests()
        {
            _country = _locationQuery.GetCountry("Australia");
        }

        protected override IPurchasesCommand CreatePurchaseCommand(string url, string merchantId, string password, int? responseCode)
        {
            return new PurchasesCommand(
                new SendSecurePayCommand(_purchaseTransactionsCommand, false, url ?? _url, url ?? _antiFraudUrl),
                _locationQuery,
                false,
                merchantId ?? _merchantId,
                password ?? _password,
                _antiFraudMerchantId,
                _antiFraudPassword,
                _country.Name);
        }

        [TestMethod]
        public void TestInvalidResponseIgnoreCents()
        {
            var requestMessage = CreateRequestMessage();

            var command = (ISendSecurePayCommand)new SendSecurePayCommand(_purchaseTransactionsCommand, false, _url, _antiFraudUrl);
            var responseMessage = command.Send<StandardPaymentRequestTxn, StandardPaymentResponseTxn>(requestMessage);

            Assert.AreEqual("20", responseMessage.Payment.TxnList.Txn.ResponseCode);

            // Ignore the cents.

            requestMessage = CreateRequestMessage();
            var originalAmount = requestMessage.Payment.TxnList.Txn.Amount;

            command = new SendSecurePayCommand(_purchaseTransactionsCommand, false, _url, _antiFraudUrl, true);
            responseMessage = command.Send<StandardPaymentRequestTxn, StandardPaymentResponseTxn>(requestMessage);

            Assert.AreEqual("00", responseMessage.Payment.TxnList.Txn.ResponseCode);
            Assert.AreEqual(originalAmount, responseMessage.Payment.TxnList.Txn.Amount);
        }

        private PaymentRequestMessage<StandardPaymentRequestTxn> CreateRequestMessage()
        {
            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>
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

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "4444333322221111";
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "123";
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = new ExpiryDate(DateTime.Now.AddYears(2));

            message.Prepare();
            message.Validate();

            return message;
        }
    }
}
