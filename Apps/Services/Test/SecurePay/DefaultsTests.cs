using System;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Preparation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass]
    public class DefaultsTests
    {
        [TestMethod]
        public void TestMessageInfoDefaults()
        {
            var message = new EchoRequestMessage();
            Assert.AreEqual(null, message.MessageInfo.MessageId);
            Assert.AreEqual(null, message.MessageInfo.MessageTimestamp);
            Assert.AreEqual(0, message.MessageInfo.Timeout);
            Assert.AreEqual(null, message.MessageInfo.ApiVersion);

            message.Prepare();
            Assert.IsNotNull(message.MessageInfo.MessageId);
            Assert.IsNotNull(message.MessageInfo.MessageTimestamp);
            Assert.AreEqual(60, message.MessageInfo.Timeout);
            Assert.AreEqual("xml-4.2", message.MessageInfo.ApiVersion);
        }

        [TestMethod]
        public void TestMerchantInfoDefaults()
        {
            var message = new EchoRequestMessage();
            Assert.AreEqual(null, message.MerchantInfo.MerchantId);
            Assert.AreEqual(null, message.MerchantInfo.Password);

            message.Prepare();
            Assert.AreEqual(null, message.MerchantInfo.MerchantId);
            Assert.AreEqual(null, message.MerchantInfo.Password);
        }

        [TestMethod]
        public void TestStandardPaymentTxnDefaults()
        {
            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>();
            Assert.AreEqual(0, message.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(Currency.AUD, message.Payment.TxnList.Txn.Currency);
            Assert.AreEqual(Guid.Empty, message.Payment.TxnList.Txn.PurchaseId);
            Assert.IsNull(message.Payment.TxnList.Txn.CreditCardInfo.CardNumber);
            Assert.IsNull(message.Payment.TxnList.Txn.CreditCardInfo.Cvv);
            Assert.AreEqual(ExpiryDate.MinValue, message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate);

            message.Prepare();
            Assert.AreEqual(0, message.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(Currency.AUD, message.Payment.TxnList.Txn.Currency);
            Assert.AreEqual(Guid.Empty, message.Payment.TxnList.Txn.PurchaseId);
            Assert.IsNull(message.Payment.TxnList.Txn.CreditCardInfo.CardNumber);
            Assert.IsNull(message.Payment.TxnList.Txn.CreditCardInfo.Cvv);
            Assert.AreEqual(ExpiryDate.MinValue, message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate);
        }

        [TestMethod]
        public void TestRefundTxnDefaults()
        {
            var message = new PaymentRequestMessage<RefundRequestTxn>();
            Assert.AreEqual(0, message.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(Guid.Empty, message.Payment.TxnList.Txn.PurchaseId);
            Assert.IsNull(message.Payment.TxnList.Txn.ExternalTransactionId);

            message.Prepare();
            Assert.AreEqual(0, message.Payment.TxnList.Txn.Amount);
            Assert.AreEqual(Guid.Empty, message.Payment.TxnList.Txn.PurchaseId);
            Assert.IsNull(message.Payment.TxnList.Txn.ExternalTransactionId);
        }
    }
}
