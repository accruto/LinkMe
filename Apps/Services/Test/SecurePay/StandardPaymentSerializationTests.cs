using System;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass]
    public class StandardPaymentSerializationTests
        : SerializationTests
    {
        private const string CardNumber = "4444333322221111";
        private const string Pan = "444433...111";
        private const string Cvv = "123";
        private static readonly ExpiryDate ExpiryDate = new ExpiryDate(new DateTime(2012, 10, 1));
        private const string ResponseCode = "00";
        private const string ResponseText = "Approved";
        private static readonly SettlementDate SettlementDate = new SettlementDate(new DateTime(2013, 11, 2));
        private const string StatusCode = "000";
        private const string StatusDescription = "Normal";

        private static readonly string RequestMessageFormat =
            "<SecurePayMessage>"
                + RequestMessageInfoFormat
                + string.Format(RequestMerchantInfoFormat, MerchantId, Password)
                + "<RequestType>Payment</RequestType>"
                + "<Payment>"
                    + "<TxnList count=\"1\">"
                        + "<Txn ID=\"1\">"
                            + "<txnType>0</txnType>"
                            + "<txnSource>23</txnSource>"
                            + "<amount>{2}</amount>"
                            + "<currency>AUD</currency>"
                            + "<purchaseOrderNo>{3}</purchaseOrderNo>"
                            + "<CreditCardInfo>"
                                + "<cardNumber>" + CardNumber + "</cardNumber>"
                                + "<cvv>" + Cvv + "</cvv>"
                                + "<expiryDate>" + ExpiryDate + "</expiryDate>"
                            + "</CreditCardInfo>"
                        + "</Txn>"
                    + "</TxnList>"
                + "</Payment>"
            + "</SecurePayMessage>";

        private static readonly string ResponseMessageFormat =
            "<SecurePayMessage>"
                + ResponseMessageInfoFormat
                + ResponseMerchantInfoFormat
                + "<RequestType>Payment</RequestType>"
                + "<Status>"
                    + "<statusCode>" + StatusCode + "</statusCode>"
                    + "<statusDescription>" + StatusDescription + "</statusDescription>"
                + "</Status>"
                + "<Payment>"
                    + "<TxnList count=\"1\">"
                        + "<Txn ID=\"1\">"
                            + "<txnType>0</txnType>"
                            + "<txnSource>23</txnSource>"
                            + "<amount>{2}</amount>"
                            + "<currency>AUD</currency>"
                            + "<purchaseOrderNo>{3}</purchaseOrderNo>"
                            + "<approved>Yes</approved>"
                            + "<responseCode>" + ResponseCode + "</responseCode>"
                            + "<responseText>" + ResponseText + "</responseText>"
                            + "<settlementDate>" + SettlementDate + "</settlementDate>"
                            + "{5}"
                            + "<txnID>{4}</txnID>"
                            + "<CreditCardInfo>"
                                + "<pan>" + Pan + "</pan>"
                                + "<expiryDate>" + ExpiryDate + "</expiryDate>"
                                + "<cardType>6</cardType>"
                                + "<cardDescription>Visa</cardDescription>"
                            + "</CreditCardInfo>"
                        + "</Txn>"
                    + "</TxnList>"
                + "</Payment>"
            + "</SecurePayMessage>";

        private const string Thinlink =
            "<thinlinkResponseCode>100</thinlinkResponseCode>"
            + "<thinlinkResponseText>000</thinlinkResponseText>"
            + "<thinlinkEventStatusCode>000</thinlinkEventStatusCode>"
            + "<thinlinkEventStatusText>Normal</thinlinkEventStatusText>";

        [TestMethod]
        public void TestSerializeRequestMessage()
        {
            // Serialize.

            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Payment.TxnList.Txn.Amount = 1;
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;

            message.Prepare();
            message.Validate();
            var serializedMessage = Serialization.Serialize(message);

            // Check.

            AssertRequestMessage(message, serializedMessage);
        }

        [TestMethod]
        public void TestDeserializeResponseMessage()
        {
            // Deserialize.

            var messageId = MessageId.NewMessageId();
            var messageTimestamp = new MessageTimestamp(DateTime.Now);
            const int amount = 10;
            var purchaseId = Guid.NewGuid();
            var externalTransactionId = Guid.NewGuid().ToString();
            var serializedMessage = string.Format(ResponseMessageFormat, messageId, messageTimestamp, amount, purchaseId, externalTransactionId, "");
            var message = Serialization.Deserialize<StandardPaymentResponseTxn>(serializedMessage);

            // Check.

            AssertResponseMessage(messageId, messageTimestamp, amount, purchaseId, externalTransactionId, message);
        }

        [TestMethod]
        public void TestCloneRequestMessage()
        {
            // Serialize.

            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Payment.TxnList.Txn.Amount = 1;
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;

            message.Prepare();
            message.Validate();
            var serializedMessage = Serialization.Serialize((PaymentRequestMessage<StandardPaymentRequestTxn>)message.Clone());

            // Check.

            AssertRequestMessage(message, serializedMessage);
        }

        [TestMethod]
        public void TestCloneResponseMessage()
        {
            // Deserialize.

            var messageId = MessageId.NewMessageId();
            var messageTimestamp = new MessageTimestamp(DateTime.Now);
            const int amount = 10;
            var purchaseId = Guid.NewGuid();
            var externalTransactionId = Guid.NewGuid().ToString();
            var serializedMessage = string.Format(ResponseMessageFormat, messageId, messageTimestamp, amount, purchaseId, externalTransactionId, "");
            var message = (PaymentResponseMessage<StandardPaymentResponseTxn>) Serialization.Deserialize<StandardPaymentResponseTxn>(serializedMessage).Clone();

            // Check.

            AssertResponseMessage(messageId, messageTimestamp, amount, purchaseId, externalTransactionId, message);
        }

        [TestMethod]
        public void TestDeserializeResponseMessageWithThinlink()
        {
            // Deserialize.

            var messageId = MessageId.NewMessageId();
            var messageTimestamp = new MessageTimestamp(DateTime.Now);
            const int amount = 10;
            var purchaseId = Guid.NewGuid();
            var externalTransactionId = Guid.NewGuid().ToString();
            var serializedMessage = string.Format(ResponseMessageFormat, messageId, messageTimestamp, amount, purchaseId, externalTransactionId, Thinlink);
            var message = Serialization.Deserialize<StandardPaymentResponseTxn>(serializedMessage);

            // Check.

            AssertResponseMessage(messageId, messageTimestamp, amount, purchaseId, externalTransactionId, message);
        }

        private static void AssertRequestMessage(PaymentRequestMessage<StandardPaymentRequestTxn> message, string serializedMessage)
        {
            var expectedSerializedMessage = string.Format(RequestMessageFormat, message.MessageInfo.MessageId, message.MessageInfo.MessageTimestamp, message.Payment.TxnList.Txn.Amount, message.Payment.TxnList.Txn.PurchaseId);
            Assert.AreEqual(expectedSerializedMessage, serializedMessage);
        }

        private static void AssertResponseMessage(MessageId messageId, MessageTimestamp messageTimestamp, int amount, Guid purchaseId, string externalTransactionId, PaymentResponseMessage<StandardPaymentResponseTxn> message)
        {
            Assert.AreEqual(messageId, message.MessageInfo.MessageId);
            Assert.AreEqual(messageTimestamp, message.MessageInfo.MessageTimestamp);
            Assert.AreEqual(ApiVersion, message.MessageInfo.ApiVersion);
            Assert.AreEqual(MerchantId, message.MerchantInfo.MerchantId);
            Assert.AreEqual(RequestType.Payment, message.RequestType);

            Assert.AreEqual(int.Parse(StatusCode), message.Status.StatusCode);
            Assert.AreEqual(StatusDescription, message.Status.StatusDescription);

            Assert.AreEqual(amount, message.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(Currency.AUD, message.Payment.TxnList.Txn.Currency);
            Assert.AreEqual(purchaseId, message.Payment.TxnList.Txn.PurchaseId);
            Assert.AreEqual(true, message.Payment.TxnList.Txn.IsApproved);
            Assert.AreEqual(ResponseCode, message.Payment.TxnList.Txn.ResponseCode);
            Assert.AreEqual(ResponseText, message.Payment.TxnList.Txn.ResponseText);
            Assert.AreEqual(SettlementDate, message.Payment.TxnList.Txn.SettlementDate);
            Assert.AreEqual(externalTransactionId, message.Payment.TxnList.Txn.ExternalTransactionId);

            Assert.AreEqual(Pan, message.Payment.TxnList.Txn.CreditCardInfo.Pan);
            Assert.AreEqual(ExpiryDate, message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate);
            Assert.AreEqual(CreditCardType.Visa, message.Payment.TxnList.Txn.CreditCardInfo.CardType);
            Assert.AreEqual(CreditCardType.Visa.ToString(), message.Payment.TxnList.Txn.CreditCardInfo.CardDescription);
        }
    }
}
