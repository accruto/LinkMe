using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Domain.Products;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass]
    public class ValidationTests
        : DataContractTests
    {
        private const string CardNumber = "4444333322221111";
        private const string Cvv = "123";
        private readonly ExpiryDate ExpiryDate = new ExpiryDate(new DateTime(2012, 10, 1));
        private const string IpAddress = "127.0.0.1";
        private const string EmailAddress = "bill@test.linkme.net.au";
        private const string Country = "AUS";

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa' is not a valid message id.")]
        public void TestTooLongMessageId()
        {
            new MessageId(new string('a', 50));
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value 'abs123&de' is not a valid message id.")]
        public void TestNotAlphaNumericMessageId()
        {
            new MessageId("abs123&de");
        }
  
        [TestMethod]
        public void TestMessageInfoMessageIdValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                new RequestMessageInfo
                {
                    MessageTimestamp = new MessageTimestamp(DateTime.Now),
                    Timeout = 60,
                    ApiVersion = ApiVersion
                },
                CreateMerchantInfo(false));

            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("MessageId"));

            // Valid.

            message.MessageInfo.MessageId = MessageId.NewMessageId();
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value 'AAAA2406171216789000+600' is not a valid format.")]
        public void TestNotNumericMessageTimestamp()
        {
            MessageTimestamp.Parse("AAAA2406171216789000+600");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value '20092406171299789000+600' is not a valid format.")]
        public void TestOutOfRangeMessageTimestamp()
        {
            MessageTimestamp.Parse("20092406171299789000+600");
        }

        [TestMethod]
        public void TestParseMessageTimestamp()
        {
            var now = DateTime.Now;
            var messageTimestamp1 = new MessageTimestamp(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond));
            var messageTimestamp2 = MessageTimestamp.Parse(messageTimestamp1.ToString());
            Assert.AreEqual(messageTimestamp2, messageTimestamp1);
        }

        [TestMethod]
        public void TestMessageInfoMessageTimestampValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                new RequestMessageInfo
                    {
                        MessageId = MessageId.NewMessageId(),
                        Timeout = 60,
                        ApiVersion = ApiVersion
                    },
                CreateMerchantInfo(false));

            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("MessageTimestamp"));

            // Valid.

            message.MessageInfo.MessageTimestamp = new MessageTimestamp(DateTime.Now);
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod]
        public void TestMessageInfoTimeoutValueValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                new RequestMessageInfo
                    {
                        MessageId = MessageId.NewMessageId(),
                        MessageTimestamp = new MessageTimestamp(DateTime.Now),
                        ApiVersion = ApiVersion
                    },
                CreateMerchantInfo(false));

            AssertValidationErrors(message.GetValidationErrors(), new NumericValueRangeValidationError("Timeout", 1, 999));

            // Invalid.

            message.MessageInfo.Timeout = -23;
            AssertValidationErrors(message.GetValidationErrors(), new NumericValueRangeValidationError("Timeout", 1, 999));

            message.MessageInfo.Timeout = 1050;
            AssertValidationErrors(message.GetValidationErrors(), new NumericValueRangeValidationError("Timeout", 1, 999));

            // Valid.

            message.MessageInfo.Timeout = 60;
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod]
        public void TestMessageInfoApiVersionValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                new RequestMessageInfo
                    {
                        MessageId = MessageId.NewMessageId(),
                        MessageTimestamp = new MessageTimestamp(DateTime.Now),
                        Timeout = 60
                    },
                CreateMerchantInfo(false));

            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("ApiVersion"));

            // Invalid.

            message.MessageInfo.ApiVersion = "xml-4.3";
            AssertValidationErrors(message.GetValidationErrors(), new RegexValidationError("ApiVersion"));

            // Valid.

            message.MessageInfo.ApiVersion = ApiVersion;
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod]
        public void TestMerchantInfoMerchantIdValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                CreateMessageInfo(),
                new RequestMerchantInfo { MerchantId = null, Password = "password" });

            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("MerchantId"));

            // Invalid.

            message.MerchantInfo.MerchantId = "ABC001";
            AssertValidationErrors(message.GetValidationErrors(), new LengthValidationError("MerchantId", 7));

            message.MerchantInfo.MerchantId = "ABC00001";
            AssertValidationErrors(message.GetValidationErrors(), new LengthValidationError("MerchantId", 7));

            // Valid.

            message.MerchantInfo.MerchantId = MerchantId;
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod]
        public void TestMerchantInfoPasswordValidation()
        {
            // Required.

            var message = new EchoRequestMessage(
                CreateMessageInfo(),
                new RequestMerchantInfo { MerchantId = MerchantId, Password = null });

            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("Password"));

            // Invalid.

            message.MerchantInfo.Password = "12345";
            AssertValidationErrors(message.GetValidationErrors(), new LengthRangeValidationError("Password", 6, 20));

            message.MerchantInfo.Password = new string('a', 30);
            AssertValidationErrors(message.GetValidationErrors(), new LengthRangeValidationError("Password", 6, 20));

            // Valid.

            message.MerchantInfo.Password = Password;
            AssertValidationErrors(message.GetValidationErrors());
        }

        [TestMethod]
        public void TestStandardPaymentTxnValidation()
        {
            var message = new PaymentRequestMessage<StandardPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Payment.TxnList.Txn.Amount = 1;
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;

            AssertValidationErrors(message.GetValidationErrors());

            // Amount.

            message.Payment.TxnList.Txn.Amount = 0;
            AssertValidationErrors(message.GetValidationErrors(), new NumericMinimumValueValidationError("Amount", 1));

            message.Payment.TxnList.Txn.Amount = int.MaxValue;
            AssertValidationErrors(message.GetValidationErrors());

            // PurchaseId

            message.Payment.TxnList.Txn.PurchaseId = Guid.Empty;
            AssertValidationErrors(message.GetValidationErrors(), new IsSetValidationError("PurchaseId"));
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();

            // CardNumber.

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("CardNumber"));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "abcdefghijklm";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "1234";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "1234567890123456789";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;

            // Cvv.

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("Cvv"));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "abc";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "12";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "1234567";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
        }

        [TestMethod]
        public void TestAntiFraudPaymentTxnValidation()
        {
            var message = new PaymentRequestMessage<AntiFraudPaymentRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(true));
            message.Payment.TxnList.Txn.Amount = 1;
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;
            message.Payment.TxnList.Txn.CreditCardInfo.ExpiryDate = ExpiryDate;
            message.Payment.TxnList.Txn.BuyerInfo.Ip = IpAddress;
            message.Payment.TxnList.Txn.BuyerInfo.EmailAddress = EmailAddress;
            message.Payment.TxnList.Txn.BuyerInfo.BillingCountry = Country;
            message.Payment.TxnList.Txn.BuyerInfo.DeliveryCountry = Country;

            AssertValidationErrors(message.GetValidationErrors());

            // Amount.

            message.Payment.TxnList.Txn.Amount = 0;
            AssertValidationErrors(message.GetValidationErrors(), new NumericMinimumValueValidationError("Amount", 1));

            message.Payment.TxnList.Txn.Amount = int.MaxValue;
            AssertValidationErrors(message.GetValidationErrors());

            // PurchaseId

            message.Payment.TxnList.Txn.PurchaseId = Guid.Empty;
            AssertValidationErrors(message.GetValidationErrors(), new IsSetValidationError("PurchaseId"));
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();

            // CardNumber.

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("CardNumber"));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "abcdefghijklm";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "1234";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));

            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = "1234567890123456789";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("CardNumber", 13, 16));
            message.Payment.TxnList.Txn.CreditCardInfo.CardNumber = CardNumber;

            // Cvv.

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("Cvv"));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "abc";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "12";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));

            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = "1234567";
            AssertValidationErrors(message.GetValidationErrors(), new NumericLengthRangeValidationError("Cvv", 3, 4));
            message.Payment.TxnList.Txn.CreditCardInfo.Cvv = Cvv;

            // IpAddress.

            message.Payment.TxnList.Txn.BuyerInfo.Ip = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("Ip"));
            message.Payment.TxnList.Txn.BuyerInfo.Ip = IpAddress;

            // EmailAddress.

            message.Payment.TxnList.Txn.BuyerInfo.EmailAddress = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("EmailAddress"));
            message.Payment.TxnList.Txn.BuyerInfo.EmailAddress = EmailAddress;

            // BillingCountry.

            message.Payment.TxnList.Txn.BuyerInfo.BillingCountry = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("BillingCountry"));
            message.Payment.TxnList.Txn.BuyerInfo.BillingCountry = Country;

            // DeliveryCountry.

            message.Payment.TxnList.Txn.BuyerInfo.DeliveryCountry = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("DeliveryCountry"));
            message.Payment.TxnList.Txn.BuyerInfo.DeliveryCountry = Country;
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value 'aa/bb' is not a valid format.")]
        public void TestInvalidExpiryDate1()
        {
            ExpiryDate.Parse("aa/bb");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value '0210' is not a valid format.")]
        public void TestInvalidExpiryDate2()
        {
            ExpiryDate.Parse("0210");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value '02/100' is not a valid format.")]
        public void TestInvalidExpiryDate3()
        {
            ExpiryDate.Parse("02/100");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value '00/12' is not a valid format.")]
        public void TestInvalidExpiryDate4()
        {
            ExpiryDate.Parse("00/12");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException), "The value '14/12' is not a valid format.")]
        public void TestInvalidExpiryDate5()
        {
            ExpiryDate.Parse("14/12");
        }

        [TestMethod]
        public void TestValidExpiryDate()
        {
            ExpiryDate.Parse("01/12");
        }

        [TestMethod]
        public void TestRefundTxnValidation()
        {
            var message = new PaymentRequestMessage<RefundRequestTxn>(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Payment.TxnList.Txn.Amount = 1;
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();
            message.Payment.TxnList.Txn.ExternalTransactionId = Guid.NewGuid().ToString();

            AssertValidationErrors(message.GetValidationErrors());

            // Amount.

            message.Payment.TxnList.Txn.Amount = 0;
            AssertValidationErrors(message.GetValidationErrors(), new NumericMinimumValueValidationError("Amount", 1));

            message.Payment.TxnList.Txn.Amount = int.MaxValue;
            AssertValidationErrors(message.GetValidationErrors());

            // PurchaseId.

            message.Payment.TxnList.Txn.PurchaseId = Guid.Empty;
            AssertValidationErrors(message.GetValidationErrors(), new IsSetValidationError("PurchaseId"));
            message.Payment.TxnList.Txn.PurchaseId = Guid.NewGuid();

            // ExternalTransactionId.

            message.Payment.TxnList.Txn.ExternalTransactionId = null;
            AssertValidationErrors(message.GetValidationErrors(), new RequiredValidationError("ExternalTransactionId"));
            message.Payment.TxnList.Txn.ExternalTransactionId = Guid.NewGuid().ToString();
        }

        private static void AssertValidationErrors(IEnumerable<ValidationError> errors, params ValidationError[] expectedErrors)
        {
            Assert.AreEqual(expectedErrors.Length, errors.Count());
            for (var index = 0; index < expectedErrors.Length; ++index)
            {
                var expectedError = expectedErrors[index];
                var error = errors.ElementAt(index);
                Assert.AreEqual(expectedError.Name, error.Name);
                Assert.AreEqual(expectedError.Message, error.Message);
            }
        }
    }
}
