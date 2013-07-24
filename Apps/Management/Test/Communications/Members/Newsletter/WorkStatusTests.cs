using LinkMe.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.Communications.Members.Newsletter
{
    [TestClass]
    public class WorkStatusTests
        : NewsletterTests
    {
        private const string HeaderText = "Work status";

        [TestMethod]
        public void TestUnspecified()
        {
            var member = CreateMember(CandidateStatus.Unspecified, null);
            GetNewsletter(member.Id);
            AssertPageContains(HeaderText);
            AssertPageContains("Unspecified");
        }

        [TestMethod]
        public void TestActivelyLooking()
        {
            var member = CreateMember(CandidateStatus.ActivelyLooking, null);
            GetNewsletter(member.Id);
            AssertPageContains(HeaderText);
            AssertPageContains("Actively looking");
        }

        [TestMethod]
        public void TestNotLooking()
        {
            var member = CreateMember(CandidateStatus.NotLooking, null);
            GetNewsletter(member.Id);
            AssertPageContains(HeaderText);
            AssertPageContains("Not looking");
        }

        [TestMethod]
        public void TestOpenToOffers()
        {
            var member = CreateMember(CandidateStatus.OpenToOffers, null);
            GetNewsletter(member.Id);
            AssertPageContains(HeaderText);
            AssertPageContains("Not looking but happy to talk");
        }
    }
}