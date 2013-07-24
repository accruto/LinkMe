using System;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Products;
using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Framework.Utility.Preparation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass, Ignore]
    public class SendSecurePayCommandTests
        : SerializationTests
    {
        private const string CardNumber = "4444333322221111";
        private const string Pan = "444433...111";
        private const string Cvv = "123";
        private static readonly ExpiryDate ExpiryDate = new ExpiryDate(new DateTime(2012, 10, 1));

        [TestMethod]
        public void TestEchoMessage()
        {
            // Request.

            var requestMessage = new EchoRequestMessage(CreateMessageInfo(), CreateMerchantInfo(false));
            requestMessage.Prepare();

            // Response.

            var responseMessage = GetCommand().Send(requestMessage);

            // Check.

            AssertResponse(responseMessage);
            AssertRequestResponse(requestMessage, responseMessage);
        }

        [TestMethod]
        public void TestStandardPaymentMessage()
        {
            // Request.

            var requestMessage = new PaymentRequestMessage<StandardPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            requestMessage.Payment.TxnList.Txn.Amount = 100;
            requestMessage.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            requestMessage.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            requestMessage.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            requestMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;
            requestMessage.Prepare();

            // Response.

            var responseMessage = GetCommand().Send<StandardPaymentRequestTxn, StandardPaymentResponseTxn>(requestMessage);

            // Check.

            AssertResponse(responseMessage);
            AssertRequestResponse(requestMessage, responseMessage);
        }

        [TestMethod]
        public void TestRefundMessage()
        {
            // Request.

            var paymentRequestMessage = new PaymentRequestMessage<StandardPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            paymentRequestMessage.Payment.TxnList.Txn.Amount = 100;
            paymentRequestMessage.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            paymentRequestMessage.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            paymentRequestMessage.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            paymentRequestMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;
            paymentRequestMessage.Prepare();

            // Response.

            var paymentResponseMessage = GetCommand().Send<StandardPaymentRequestTxn, StandardPaymentResponseTxn>(paymentRequestMessage);

            // Request.

            var requestMessage = new PaymentRequestMessage<RefundRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            requestMessage.Payment.TxnList.Txn.Amount = paymentResponseMessage.Payment.TxnList.Txn.Amount;
            requestMessage.Payment.TxnList.Txn.PurchaseId = paymentResponseMessage.Payment.TxnList.Txn.PurchaseId;
            requestMessage.Payment.TxnList.Txn.ExternalTransactionId = paymentResponseMessage.Payment.TxnList.Txn.ExternalTransactionId;
            requestMessage.Prepare();

            // Response.

            var responseMessage = GetCommand().Send<RefundRequestTxn, RefundResponseTxn>(requestMessage);

            // Check.

            AssertResponse(responseMessage);
            AssertRequestResponse(paymentRequestMessage, paymentResponseMessage, requestMessage, responseMessage);
        }

        private static ISendSecurePayCommand GetCommand()
        {
            return new SendSecurePayCommand(
                Resolve<IPurchaseTransactionsCommand>(),
                false,
                Resolve<string>("securepay.url"),
                Resolve<string>("securepay.antifraud.url"));
        }

        private static void AssertResponse(ResponseMessage message)
        {
            Assert.AreEqual(0, message.Status.StatusCode);
            Assert.AreEqual("Normal", message.Status.StatusDescription);
        }

        private static void AssertResponse(PaymentResponseMessage<StandardPaymentResponseTxn> message)
        {
            AssertResponse((ResponseMessage)message);

            Assert.IsTrue(message.Payment.TxnList.Txn.IsApproved);
            Assert.AreEqual("00", message.Payment.TxnList.Txn.ResponseCode);
            Assert.AreEqual("Approved", message.Payment.TxnList.Txn.ResponseText);

            Assert.AreEqual(Pan, message.Payment.TxnList.Txn.CreditCardInfo.Pan);
            Assert.AreEqual(CreditCardType.Visa, message.Payment.TxnList.Txn.CreditCardInfo.CardType);
            Assert.AreEqual(CreditCardType.Visa.ToString(), message.Payment.TxnList.Txn.CreditCardInfo.CardDescription);
        }

        private static void AssertRequestResponse(RequestMessage requestMessage, ResponseMessage responseMessage)
        {
            Assert.AreEqual(requestMessage.MessageInfo.MessageId, responseMessage.MessageInfo.MessageId);
            Assert.AreEqual(requestMessage.MessageInfo.ApiVersion, responseMessage.MessageInfo.ApiVersion);

            Assert.AreEqual(requestMessage.RequestType, responseMessage.RequestType);
        }

        private static void AssertRequestResponse(PaymentRequestMessage<StandardPaymentRequestTxn> requestMessage, PaymentResponseMessage<StandardPaymentResponseTxn> responseMessage)
        {
            AssertRequestResponse((RequestMessage)requestMessage, responseMessage);

            Assert.AreEqual(requestMessage.MerchantInfo.MerchantId, responseMessage.MerchantInfo.MerchantId);

            Assert.AreEqual(requestMessage.Payment.TxnList.Txn.Amount, responseMessage.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(requestMessage.Payment.TxnList.Txn.Currency, responseMessage.Payment.TxnList.Txn.Currency);
            Assert.AreEqual(requestMessage.Payment.TxnList.Txn.PurchaseId, responseMessage.Payment.TxnList.Txn.PurchaseId);

            Assert.AreEqual(requestMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate, responseMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate);
        }

        private static void AssertRequestResponse(PaymentRequestMessage<StandardPaymentRequestTxn> paymentRequestMessage, PaymentResponseMessage<StandardPaymentResponseTxn> paymentResponseMessage, PaymentRequestMessage<RefundRequestTxn> requestMessage, PaymentResponseMessage<RefundResponseTxn> responseMessage)
        {
            AssertRequestResponse(requestMessage, responseMessage);

            Assert.AreEqual(requestMessage.MerchantInfo.MerchantId, responseMessage.MerchantInfo.MerchantId);

            Assert.AreEqual(requestMessage.Payment.TxnList.Txn.Amount, responseMessage.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(paymentRequestMessage.Payment.TxnList.Txn.Currency, responseMessage.Payment.TxnList.Txn.Currency);
            Assert.AreEqual(paymentResponseMessage.Payment.TxnList.Txn.ExternalTransactionId, responseMessage.Payment.TxnList.Txn.PaymentExternalTransactionId);

            Assert.AreEqual(paymentResponseMessage.Payment.TxnList.Txn.CreditCardInfo.Pan, responseMessage.Payment.TxnList.Txn.CreditCardInfo.Pan);
            Assert.AreEqual(paymentResponseMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate, responseMessage.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate);
            Assert.AreEqual(paymentResponseMessage.Payment.TxnList.Txn.CreditCardInfo.CardType, responseMessage.Payment.TxnList.Txn.CreditCardInfo.CardType);
            Assert.AreEqual(paymentResponseMessage.Payment.TxnList.Txn.CreditCardInfo.CardDescription, responseMessage.Payment.TxnList.Txn.CreditCardInfo.CardDescription);
        }
    }
}
