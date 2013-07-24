using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Status
{
    [TestClass]
    public class CandidatePassiveNotificationEmailTests
        : StatusEmailTests
    {
        private const string Definition = "CandidatePassiveNotificationEmail";

        [TestMethod]
        public void TestLoggedInChangeStatus()
        {
            TestChangeStatus(true);
        }

        [TestMethod]
        public void TestLoggedOutChangeStatus()
        {
            TestChangeStatus(false);
        }

        private void TestChangeStatus(bool isLoggedIn)
        {
            const CandidateStatus previousStatus = CandidateStatus.NotLooking;
            const CandidateStatus newStatus = CandidateStatus.AvailableNow;

            var member = CreateMember(0, previousStatus);
            Assert.AreEqual(previousStatus, _candidatesCommand.GetCandidate(member.Id).Status);

            // Send email.

            _emailsCommand.TrySend(new CandidatePassiveNotificationEmail(member));

            // Follow link.

            if (isLoggedIn)
                LogIn(member);
            else
                LogOut();
            AssertLink(GetLinkUrl(0, 2), GetEmailUrl(Definition, GetUpdateStatusUrl()));

            // Change the status.

            ChangeStatus(member, previousStatus, newStatus);
        }
    }
}
