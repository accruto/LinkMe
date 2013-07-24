using LinkMe.Web.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Helper
{
    [TestClass]
    public class EncryptionHelperTest
    {
        private const string TestData = "encrypt or hash THIS!";

        [TestMethod]
        public void EncryptDecrypt()
        {
            string encrypted = EncryptionHelper.EncryptToHexString(TestData);
            Assert.AreEqual(64, encrypted.Length);

            string decrypted = EncryptionHelper.DecryptFromHexString(encrypted);
            Assert.AreEqual(TestData, decrypted);
        }

        [TestMethod]
        public void Hash()
        {
            string hash = EncryptionHelper.GetKeyedHashAsHexString(TestData);
            Assert.AreEqual(40, hash.Length);

            Assert.IsTrue(EncryptionHelper.IsKeyedHashValid(TestData, hash));
            Assert.IsFalse(EncryptionHelper.IsKeyedHashValid(TestData, hash.Replace('0', '1')));
        }
    }
}
