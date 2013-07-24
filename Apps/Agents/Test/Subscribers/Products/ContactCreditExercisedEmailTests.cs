using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public class ContactCreditExercisedEmailTests
        : CreditExercisedEmailTests<ContactCredit>
    {
        [TestMethod]
        public void Test10ContactCredits()
        {
            TestCredits(10, 8);
        }

        [TestMethod]
        public void Test70ContactCredits()
        {
            TestCredits(70, 56);
        }

        [TestMethod]
        public void Test200ContactCredits()
        {
            TestCredits(200, 160);
        }

        [TestMethod]
        public void Test303ContactCredits()
        {
            TestCredits(303, 258);
        }

        [TestMethod]
        public void Test500ContactCredits()
        {
            TestCredits(500, 425);
        }

        [TestMethod]
        public void Test600ContactCredits()
        {
            TestCredits(600, 540);
        }

        protected override void AssertNoCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            AssertCreditsEmail(accountManager, recipients, "A notice from LinkMe: You have no remaining 'candidate contact credits'", "To purchase more candidate contact credits for continued access to LinkMe's");
        }

        protected override void AssertLowCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            AssertCreditsEmail(accountManager, recipients, "A reminder from LinkMe: Your remaining 'candidate contact credits' are running low", "This is just a reminder to plan your next purchase of candidate contact credits");
        }
    }
}