using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Domain;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Status
{
    [TestClass]
    public class CandidateAvailableConfirmationEmailTests
        : StatusEmailTests
    {
        private const string Definition = "CandidateAvailableConfirmationEmail";

        [TestMethod]
        public void TestLoggedInConfirm()
        {
            TestConfirm(true);
        }

        [TestMethod]
        public void TestLoggedOutConfirm()
        {
            TestConfirm(false);
        }

        [TestMethod]
        public void TestOtherConfirm()
        {
            // Create 2 members.

            const CandidateStatus status1 = CandidateStatus.AvailableNow;
            var member1 = CreateMember(1, status1);
            const CandidateStatus status2 = CandidateStatus.ActivelyLooking;
            var member2 = CreateMember(2, status2);

            // Send email to member1.

            _emailsCommand.TrySend(new CandidateAvailableConfirmationEmail(member1));

            // Follow link logged in as other member.

            var expectedUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/status/update", new ReadOnlyQueryString("status", status1.ToString()));
            LogIn(member2);
            AssertLink(GetLinkUrl(0, 3), GetEmailUrl(Definition, expectedUrl));

            // Status should have been changed for other member.

            AssertPageContains("Your work status has been updated to");

            // Should not be shown options.

            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            Assert.AreEqual(status1, _candidatesCommand.GetCandidate(member1.Id).Status);
            Assert.AreEqual(status1, _candidatesCommand.GetCandidate(member2.Id).Status);
        }

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

        [TestMethod]
        public void TestOtherChangeStatus()
        {
            const CandidateStatus previousStatus = CandidateStatus.AvailableNow;
            const CandidateStatus newStatus = CandidateStatus.ActivelyLooking;

            // Create 2 members.

            const CandidateStatus status1 = previousStatus;
            var member1 = CreateMember(1, status1);
            const CandidateStatus status2 = previousStatus;
            var member2 = CreateMember(2, status2);

            // Send email to member 1.

            _emailsCommand.TrySend(new CandidateAvailableConfirmationEmail(member1));

            // Follow link logged in as other member.

            LogIn(member2);
            AssertLink(GetLinkUrl(1, 3), GetEmailUrl(Definition, GetUpdateStatusUrl()));

            // Change the status.

            Assert.IsTrue(_availableNowRadioButton.IsVisible);
            Assert.IsTrue(_activelyLookingRadioButton.IsVisible);
            Assert.IsTrue(_openToOffersRadioButton.IsVisible);
            Assert.IsTrue(_notLookingRadioButton.IsVisible);
            Assert.IsTrue(_saveButton.IsVisible);

            Assert.AreEqual(previousStatus == CandidateStatus.AvailableNow, _availableNowRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.ActivelyLooking, _activelyLookingRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.OpenToOffers, _openToOffersRadioButton.IsChecked);
            Assert.AreEqual(previousStatus == CandidateStatus.NotLooking, _notLookingRadioButton.IsChecked);

            // Change status.

            _activelyLookingRadioButton.IsChecked = true;
            _saveButton.Click();

            var url = GetUpdateStatusUrl(newStatus);
            AssertUrl(url);

            AssertPageContains("Your work status has been updated to");
            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            AssertStatus(member1.Id, previousStatus);
            AssertStatus(member2.Id, newStatus);
        }

        [TestMethod]
        public void TestOldConfirm()
        {
            var member = CreateMember(0, CandidateStatus.AvailableNow);
            Assert.AreEqual(CandidateStatus.AvailableNow, _candidatesCommand.GetCandidate(member.Id).Status);

            // Follow link.

            var oldUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/status/updateavailablenow");
            var expectedUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/status/update", new ReadOnlyQueryString("status", CandidateStatus.AvailableNow.ToString()));
            var loginUrl = GetLoginUrl(expectedUrl);
            AssertLink(oldUrl, loginUrl);
            SubmitLogIn(member);

            // Check status is same.

            ConfirmStatus(member, CandidateStatus.AvailableNow);
        }

        private void TestConfirm(bool isLoggedIn)
        {
            // Create member.

            const CandidateStatus status = CandidateStatus.AvailableNow;
            var member = CreateMember(0, status);
            Assert.AreEqual(status, _candidatesCommand.GetCandidate(member.Id).Status);

            // Send email.

            _emailsCommand.TrySend(new CandidateAvailableConfirmationEmail(member));

            // Follow link.

            var expectedUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/status/update", new ReadOnlyQueryString("status", status.ToString()));
            if (isLoggedIn)
                LogIn(member);
            else
                LogOut();
            AssertLink(GetLinkUrl(0, 3), GetEmailUrl(Definition, expectedUrl));

            // Confirm status.

            ConfirmStatus(member, status);
        }

        private void TestChangeStatus(bool isLoggedIn)
        {
            const CandidateStatus previousStatus = CandidateStatus.AvailableNow;
            const CandidateStatus newStatus = CandidateStatus.ActivelyLooking;

            var member = CreateMember(0, previousStatus);
            Assert.AreEqual(previousStatus, _candidatesCommand.GetCandidate(member.Id).Status);

            // Send email.

            _emailsCommand.TrySend(new CandidateAvailableConfirmationEmail(member));

            // Follow link.

            if (isLoggedIn)
                LogIn(member);
            else
                LogOut();
            AssertLink(GetLinkUrl(1, 3), GetEmailUrl(Definition, GetUpdateStatusUrl()));

            // Change the status.

            ChangeStatus(member, previousStatus, newStatus);
        }
    }
}
