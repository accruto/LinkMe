using LinkMe.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Status
{
    [TestClass]
    public class UpdateStatusTests
        : StatusTests
    {
        [TestMethod]
        public void TestLoggedInUpdateStatus()
        {
            TestUpdateStatus(true);
        }

        [TestMethod]
        public void TestNotLoggedInUpdateStatus()
        {
            TestUpdateStatus(false);
        }

        [TestMethod]
        public void TestLoggedInConfirmStatus()
        {
            TestConfirmStatus(true);
        }

        [TestMethod]
        public void TestNotLoggedInConfirmStatus()
        {
            TestConfirmStatus(false);
        }

        [TestMethod]
        public void TestLoggedInSelectStatus()
        {
            TestSelectStatus(true);
        }

        [TestMethod]
        public void TestNotLoggedInSelectStatus()
        {
            TestSelectStatus(false);
        }

        private void TestUpdateStatus(bool loggedIn)
        {
            const CandidateStatus previousStatus = CandidateStatus.AvailableNow;
            const CandidateStatus newStatus = CandidateStatus.ActivelyLooking;

            var member = CreateMember(0, previousStatus);
            AssertStatus(member.Id, previousStatus);

            if (loggedIn)
                LogIn(member);

            var url = GetUpdateStatusUrl(newStatus);
            Get(url);

            if (!loggedIn)
            {
                // Should be prompted to log in.

                var loginUrl = GetLoginUrl(url);
                AssertUrl(loginUrl);
                SubmitLogIn(member);
            }

            AssertUrl(url);

            // Simply navigating to the page should update the status.

            AssertPageContains("Your work status has been updated to");
            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            AssertStatus(member.Id, newStatus);
        }

        private void TestConfirmStatus(bool loggedIn)
        {
            const CandidateStatus status = CandidateStatus.AvailableNow;

            var member = CreateMember(0, status);
            AssertStatus(member.Id, status);

            if (loggedIn)
                LogIn(member);

            var url = GetUpdateStatusUrl(status);
            Get(url);

            if (!loggedIn)
            {
                // Should be prompted to log in.

                var loginUrl = GetLoginUrl(url);
                AssertUrl(loginUrl);
                SubmitLogIn(member);
            }

            AssertUrl(url);

            // Simply navigating to the page should update the status.

            AssertPageContains("Your work status has been confirmed as");
            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            AssertStatus(member.Id, status);
        }

        private void TestSelectStatus(bool loggedIn)
        {
            const CandidateStatus previousStatus = CandidateStatus.AvailableNow;
            const CandidateStatus newStatus = CandidateStatus.ActivelyLooking;

            var member = CreateMember(0, previousStatus);
            AssertStatus(member.Id, previousStatus);

            if (loggedIn)
                LogIn(member);

            var url = GetUpdateStatusUrl();
            Get(url);

            if (!loggedIn)
            {
                // Should be prompted to log in.

                var loginUrl = GetLoginUrl(url);
                AssertUrl(loginUrl);
                SubmitLogIn(member);
            }

            AssertUrl(url);

            // Should be shown options.

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

            url = GetUpdateStatusUrl(newStatus);
            AssertUrl(url);

            AssertPageContains("Your work status has been updated to");
            Assert.IsFalse(_availableNowRadioButton.IsVisible);
            Assert.IsFalse(_activelyLookingRadioButton.IsVisible);
            Assert.IsFalse(_openToOffersRadioButton.IsVisible);
            Assert.IsFalse(_notLookingRadioButton.IsVisible);
            Assert.IsFalse(_saveButton.IsVisible);

            AssertStatus(member.Id, newStatus);
        }
    }
}
