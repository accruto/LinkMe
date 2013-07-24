using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Settings
{
    [TestClass]
    public class DeactivateTests
        : SettingsTests
    {
        private HtmlRadioButtonTester _employerRadioButton;
        private HtmlTextAreaTester _commentsTextBox;
        private HtmlButtonTester _deactivateButton;
        private HtmlButtonTester _cancelButton;

        private ReadOnlyUrl _notActivatedUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _employerRadioButton = new HtmlRadioButtonTester(Browser, "Employer");
            _commentsTextBox = new HtmlTextAreaTester(Browser, "Comments");
            _deactivateButton = new HtmlButtonTester(Browser, "deactivate");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");

            _notActivatedUrl = new ReadOnlyApplicationUrl(true, "~/accounts/notactivated");
        }

        [TestMethod]
        public void TestDeactivate()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_deactivateUrl);

            // Deactivate.

            _employerRadioButton.IsChecked = true;
            _commentsTextBox.Text = "I am done.";
            _deactivateButton.Click();

            // Assert that the user is logged out.

            AssertNotLoggedIn();

            // Assert that their status has been updated.

            var updatedMember = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(updatedMember);
            Assert.IsTrue(updatedMember.IsEnabled);
            Assert.IsTrue(!updatedMember.IsActivated);

            // Try to log in again.

            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Try to get to the settings page.

            Get(_settingsUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);
        }

        [TestMethod]
        public void TestDeactivateNoReason()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_deactivateUrl);

            // Deactivate.

            _deactivateButton.Click();

            AssertUrl(_deactivateUrl);
            AssertErrorMessage("The reason is required.");

            // Assert that the user is logged out.

            _employerRadioButton.IsChecked = true;
            _deactivateButton.Click();
            AssertNotLoggedIn();

            // Assert that their status has been updated.

            var updatedMember = _membersQuery.GetMember(member.Id);
            Assert.IsNotNull(updatedMember);
            Assert.IsTrue(updatedMember.IsEnabled);
            Assert.IsTrue(!updatedMember.IsActivated);

            // Try to log in again.

            LogIn(member);
            AssertUrl(_notActivatedUrl);

            // Try to get to the settings page.

            Get(_settingsUrl);
            AssertUrlWithoutQuery(_notActivatedUrl);
        }

        [TestMethod]
        public void TestCancel()
        {
            var member = _memberAccountsCommand.CreateTestMember(1);
            LogIn(member);
            Get(_deactivateUrl);

            // Cancel.

            _cancelButton.Click();
            AssertUrl(LoggedInMemberHomeUrl);
        }
    }
}