using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public class JobAdCreditExercisedEmailTests
        : CreditExercisedEmailTests<JobAdCredit>
    {
        [TestMethod]
        public void Test10JobAdCredits()
        {
            TestCredits(10, 8);
        }

        [TestMethod]
        public void Test240JobAdCredits()
        {
            TestCredits(240, 192);
        }

        [TestMethod]
        public void Test503JobAdCredits()
        {
            TestCredits(503, 428);
        }

        [TestMethod]
        public void Test600JobAdCredits()
        {
            TestCredits(600, 510);
        }

        [TestMethod]
        public void Test700JobAdCredits()
        {
            TestCredits(700, 630);
        }

        protected override void AssertLowCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            _emailServer.AssertNoEmailSent();
        }

        protected override void AssertNoCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            _emailServer.AssertNoEmailSent();
        }
    }
}