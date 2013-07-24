using System;
using LinkMe.Apps.Services.External.SecurePay;
using LinkMe.Framework.Utility.Preparation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.SecurePay
{
    [TestClass]
    public class EchoSerializationTests
        : SerializationTests
    {
        private static readonly string RequestMessageFormat =
            "<SecurePayMessage>"
            + RequestMessageInfoFormat
            + string.Format(RequestMerchantInfoFormat, MerchantId, Password)
            + "<RequestType>Echo</RequestType>"
            + "</SecurePayMessage>";
        private static readonly string ResponseMessageFormat =
            "<SecurePayMessage>"
            + ResponseMessageInfoFormat
            + ResponseMerchantInfoFormat
            + "<RequestType>Echo</RequestType>"
            + "</SecurePayMessage>";

        [TestMethod]
        public void TestSerializeRequestMessage()
        {
            // Serialize.

            var message = new EchoRequestMessage(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Prepare();
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
            var serializedMessage = string.Format(ResponseMessageFormat, messageId, messageTimestamp);
            var message = Serialization.Deserialize(serializedMessage);

            // Check.

            AssertResponseMessage(messageId, messageTimestamp, message);
        }

        [TestMethod]
        public void TestCloneRequestMessage()
        {
            // Serialize.

            var message = new EchoRequestMessage(CreateMessageInfo(), CreateMerchantInfo(false));
            message.Prepare();
            var serializedMessage = Serialization.Serialize((EchoRequestMessage) message.Clone());

            // Check.

            AssertRequestMessage(message, serializedMessage);
        }

        [TestMethod]
        public void TestCloneResponseMessage()
        {
            // Deserialize.

            var messageId = MessageId.NewMessageId();
            var messageTimestamp = new MessageTimestamp(DateTime.Now);
            var serializedMessage = string.Format(ResponseMessageFormat, messageId, messageTimestamp);
            var message = (ResponseMessage)Serialization.Deserialize(serializedMessage).Clone();

            // Check.

            AssertResponseMessage(messageId, messageTimestamp, message);
        }

        private static void AssertRequestMessage(RequestMessage message, string serializedMessage)
        {
            var expectedSerializedMessage = string.Format(RequestMessageFormat, message.MessageInfo.MessageId, message.MessageInfo.MessageTimestamp);
            Assert.AreEqual(expectedSerializedMessage, serializedMessage);
        }

        private static void AssertResponseMessage(MessageId messageId, MessageTimestamp messageTimestamp, ResponseMessage message)
        {
            Assert.AreEqual(messageId, message.MessageInfo.MessageId);
            Assert.AreEqual(messageTimestamp, message.MessageInfo.MessageTimestamp);
            Assert.AreEqual(ApiVersion, message.MessageInfo.ApiVersion);
            Assert.AreEqual(MerchantId, message.MerchantInfo.MerchantId);
            Assert.AreEqual(RequestType.Echo, message.RequestType);
        }
    }
}
