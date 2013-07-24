using LinkMe.Apps.Agents.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Security
{
    [TestClass]
    public class PasswordHashTests
    {
        [TestMethod]
        public void TestHash()
        {
            Assert.IsTrue("password" != LoginCredentials.HashToString("password"));
            Assert.AreEqual(LoginCredentials.HashToString("password"), LoginCredentials.HashToString("password"));
            Assert.AreEqual("DMF1ucDxtqgxw5niaXcmYQ==", LoginCredentials.HashToString("a"));
        }
    }
}