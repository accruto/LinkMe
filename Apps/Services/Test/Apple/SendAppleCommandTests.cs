using System;
using System.IO;
using LinkMe.Apps.Services.External.Apple.AppStore;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Services.Test.Apple
{
    [TestClass]
    public class SendAppleCommandTests
        : TestClass
    {
        private ISendAppleCommand _sendAppleCommand;

        [TestInitialize]
        public void TestInitialize()
        {
            _sendAppleCommand = Resolve<ISendAppleCommand>();
        }

        [TestMethod]
        public void SendFailingRequest()
        {
            var response = _sendAppleCommand.Verify(string.Empty);
            Assert.AreNotEqual(0, response.Status);
        }

        [TestMethod]
        public void SendSuccessfulRequest()
        {
            var verificationString = GetVerificationString();
            var response = _sendAppleCommand.Verify(verificationString);

            Assert.AreEqual(0, response.Status);
            Assert.IsNull(response.Exception);
            Assert.AreEqual(1, response.Receipt.Quantity);
            Assert.AreEqual("com.bilue.Link-Me", response.Receipt.ApplicationBundleIdentifier);
            Assert.AreEqual("com.bilue.linkme.credit.1", response.Receipt.ProductId);
            Assert.AreEqual("1000000011299971", response.Receipt.TransactionId);
            Assert.AreEqual("1000000011299971", response.Receipt.OriginalTransactionId);
            Assert.AreEqual(new DateTime(2011, 10, 27, 2, 9, 27), response.Receipt.PurchaseDate);
            Assert.AreEqual(new DateTime(2011, 10, 27, 2, 9, 27), response.Receipt.OriginalPurchaseDate);
        }

        private static string GetVerificationString()
        {
            var path = FileSystem.GetAbsolutePath(@"Apps\Services\Test\Apple\Resources\VerificationString.txt", RuntimeEnvironment.GetSourceFolder());
            return File.ReadAllText(path);
        }
    }
}
