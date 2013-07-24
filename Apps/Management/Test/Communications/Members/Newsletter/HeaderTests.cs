using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.Communications.Members.Newsletter
{
    [TestClass]
    public class HeaderTests
        : NewsletterTests
    {
        private const string EmailAddress1 = "homer@test.linkme.net.au";
        private const string FirstName1 = "Homer";
        private const string LastName1 = "Simpson";

        private const string EmailAddress2 = "barney@test.linkme.net.au";
        private const string FirstName2 = "Barney";
        private const string LastName2 = "Gumble";

        [TestMethod]
        public void TestContent()
        {
            var member = CreateMember(EmailAddress1, FirstName1, LastName1);
            GetNewsletter(member.Id);
            AssertPageContains(member.FirstName);

            member = CreateMember(EmailAddress2, FirstName2, LastName2);
            GetNewsletter(member.Id);
            AssertPageContains(member.FirstName);
        }
    }
}