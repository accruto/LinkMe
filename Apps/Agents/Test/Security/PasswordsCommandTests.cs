using LinkMe.Apps.Agents.Security.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Security
{
    [TestClass]
    public class PasswordsCommandTests
    {
        private readonly IPasswordsCommand _passwordsCommand = new PasswordsCommand();

        [TestMethod]
        public void TestGeneratePassword()
        {
            var password = _passwordsCommand.GenerateRandomPassword();
            Assert.IsNotNull(password);
            Assert.AreEqual(password.Length, 10);
        }
    }
}