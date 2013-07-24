using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.Communications.Members.Newsletter
{
    [TestClass]
    public class UnsubscribeTests
        : NewsletterTests
    {
        private const string UnsubscribeText = "unsubscribe";
        private const string SettingsText = "edit your settings";

        [TestMethod]
        public void TestContents()
        {
            var member = CreateMember();
            GetNewsletter(member.Id);
            AssertPageContains(UnsubscribeText);
            AssertPageContains(SettingsText);
        }
    }
}