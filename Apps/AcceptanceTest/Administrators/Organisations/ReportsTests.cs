using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Organisations
{
    [TestClass]
    public class ReportsTests
        : OrganisationsTests
    {
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string EmailAddress = "mburns@test.linkme.net.au";

        private HtmlCheckBoxTester _includeChildOrganisationsCheckBox;
        private HtmlCheckBoxTester _includeDisabledUsersCheckBox;
        private HtmlTextBoxTester _promoCodeTextBox;
        private HtmlCheckBoxTester _sendToAccountManagerCheckBox;
        private HtmlCheckBoxTester _sendToClientCheckBox;
        private HtmlButtonTester _saveButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _includeChildOrganisationsCheckBox = new HtmlCheckBoxTester(Browser, "IncludeChildOrganisations");
            _includeDisabledUsersCheckBox = new HtmlCheckBoxTester(Browser, "IncludeDisabledUsers");
            _promoCodeTextBox = new HtmlTextBoxTester(Browser, "PromoCode");
            _sendToAccountManagerCheckBox = new HtmlCheckBoxTester(Browser, "SendToAccountManager");
            _sendToClientCheckBox = new HtmlCheckBoxTester(Browser, "SendToClient");
            _saveButton = new HtmlButtonTester(Browser, "save");
        }

        [TestMethod]
        public void TestResumeSearchActivityDefaults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "ResumeSearchActivityReport");
            Get(url);

            AssertResumeSearchActivityParameters(true, false, false, false);
        }

        [TestMethod]
        public void TestEditResumeSearchActivity()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "ResumeSearchActivityReport");
            Get(url);

            // Update child organisations.

            _includeChildOrganisationsCheckBox.IsChecked = false;
            _saveButton.Click();
            AssertResumeSearchActivityParameters(false, false, false, false);

            // Update disabled users.

            _includeDisabledUsersCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertResumeSearchActivityParameters(false, true, false, false);

            // Update send to account manager.

            _sendToAccountManagerCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertResumeSearchActivityParameters(false, true, true, false);

            // Update send to client.

            _sendToClientCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertResumeSearchActivityParameters(false, true, true, true);

            // Update all.

            _includeChildOrganisationsCheckBox.IsChecked = true;
            _includeDisabledUsersCheckBox.IsChecked = false;
            _sendToAccountManagerCheckBox.IsChecked = false;
            _sendToClientCheckBox.IsChecked = false;

            _saveButton.Click();
            AssertResumeSearchActivityParameters(true, false, false, false);
        }

        [TestMethod]
        public void TestCandidateCareDefaults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "CandidateCareReport");
            Get(url);

            AssertCandidateCareParameters(true, string.Empty, false, false);
        }

        [TestMethod]
        public void TestEditCandidateCare()
        {
            const string promoCode = "ABCD";

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "CandidateCareReport");
            Get(url);

            // Update child organisations.

            _includeChildOrganisationsCheckBox.IsChecked = false;
            _saveButton.Click();
            AssertCandidateCareParameters(false, string.Empty, false, false);

            // Update promo code.

            _promoCodeTextBox.Text = promoCode;
            _saveButton.Click();
            AssertCandidateCareParameters(false, promoCode, false, false);

            // Update send to account manager.

            _sendToAccountManagerCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertCandidateCareParameters(false, promoCode, true, false);

            // Update send to client.

            _sendToClientCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertCandidateCareParameters(false, promoCode, true, true);

            // Update all.

            _includeChildOrganisationsCheckBox.IsChecked = true;
            _promoCodeTextBox.Text = string.Empty;
            _sendToAccountManagerCheckBox.IsChecked = false;
            _sendToClientCheckBox.IsChecked = false;

            _saveButton.Click();
            AssertCandidateCareParameters(true, string.Empty, false, false);
        }

        [TestMethod]
        public void TestJobBoardActivityDefaults()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "JobBoardActivityReport");
            Get(url);

            AssertJobBoardActivityParameters(true, false, false);
        }

        [TestMethod]
        public void TestEditJobBoardActivity()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(organisation, "JobBoardActivityReport");
            Get(url);

            // Update child organisations.

            _includeChildOrganisationsCheckBox.IsChecked = false;
            _saveButton.Click();
            AssertJobBoardActivityParameters(false, false, false);

            // Update send to account manager.

            _sendToAccountManagerCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertJobBoardActivityParameters(false, true, false);

            // Update send to client.

            _sendToClientCheckBox.IsChecked = true;
            _saveButton.Click();
            AssertJobBoardActivityParameters(false, true, true);

            // Update all.

            _includeChildOrganisationsCheckBox.IsChecked = true;
            _sendToAccountManagerCheckBox.IsChecked = false;
            _sendToClientCheckBox.IsChecked = false;

            _saveButton.Click();
            AssertJobBoardActivityParameters(true, false, false);
        }

        [TestMethod]
        public void TestAccountManager()
        {
            var administrator1 = _administratorAccountsCommand.CreateTestAdministrator(1);
            var administrator2 = _administratorAccountsCommand.CreateTestAdministrator(2);

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator1.Id);

            LogIn(administrator1);
            var url = GetReportUrl(organisation, "JobBoardActivityReport");

            Get(url);
            AssertEmailLink(administrator1.FullName, administrator1.EmailAddress.Address);

            // Update the account manager.

            organisation.AccountManagerId = administrator2.Id;
            _organisationsCommand.UpdateOrganisation(organisation);

            Get(url);
            AssertEmailLink(administrator2.FullName, administrator2.EmailAddress.Address);
        }

        [TestMethod]
        public void TestContactDetails()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var parent = _organisationsCommand.CreateTestVerifiedOrganisation(1, null, administrator.Id);
            var child = _organisationsCommand.CreateTestVerifiedOrganisation(2, parent, administrator.Id);

            LogIn(administrator);
            var url = GetReportUrl(child, "JobBoardActivityReport");

            Get(url);
            AssertPageContains("email not set");

            // Set on the child.

            child.ContactDetails = new ContactDetails {FirstName = FirstName, LastName = LastName, EmailAddress = EmailAddress};
            _organisationsCommand.UpdateOrganisation(child);

            Get(url);
            AssertPageDoesNotContain("email not set");
            AssertEmailLink(child.ContactDetails.FullName, child.ContactDetails.EmailAddress);

            // Remove.

            child.ContactDetails = null;
            _organisationsCommand.UpdateOrganisation(child);

            Get(url);
            AssertPageContains("email not set");

            // Set on parent.

            parent.ContactDetails = new ContactDetails { FirstName = FirstName, LastName = LastName, EmailAddress = EmailAddress };
            _organisationsCommand.UpdateOrganisation(parent);

            Get(url);
            AssertPageDoesNotContain("email not set");
            AssertEmailLink(parent.ContactDetails.FullName, parent.ContactDetails.EmailAddress);
        }

        private void AssertEmailLink(string name, string emailAddress)
        {
            AssertPageContains("<a href=\"mailto:" + emailAddress + "\">" + name + "</a>");
        }

        private void AssertCandidateCareParameters(bool includeChildOrganisations, string promoCode, bool sendToAccountManager, bool sendToClient)
        {
            Assert.AreEqual(includeChildOrganisations, _includeChildOrganisationsCheckBox.IsChecked);
            Assert.IsFalse(_includeDisabledUsersCheckBox.IsVisible);
            Assert.AreEqual(promoCode, _promoCodeTextBox.Text);
            Assert.AreEqual(sendToAccountManager, _sendToAccountManagerCheckBox.IsChecked);
            Assert.AreEqual(sendToClient, _sendToClientCheckBox.IsChecked);
        }

        private void AssertResumeSearchActivityParameters(bool includeChildOrganisations, bool includeDisabledUsers, bool sendToAccountManager, bool sendToClient)
        {
            Assert.AreEqual(includeChildOrganisations, _includeChildOrganisationsCheckBox.IsChecked);
            Assert.AreEqual(includeDisabledUsers, _includeDisabledUsersCheckBox.IsChecked);
            Assert.IsFalse(_promoCodeTextBox.IsVisible);
            Assert.AreEqual(sendToAccountManager, _sendToAccountManagerCheckBox.IsChecked);
            Assert.AreEqual(sendToClient, _sendToClientCheckBox.IsChecked);
        }

        private void AssertJobBoardActivityParameters(bool includeChildOrganisations, bool sendToAccountManager, bool sendToClient)
        {
            Assert.AreEqual(includeChildOrganisations, _includeChildOrganisationsCheckBox.IsChecked);
            Assert.IsFalse(_includeDisabledUsersCheckBox.IsVisible);
            Assert.IsFalse(_promoCodeTextBox.IsVisible);
            Assert.AreEqual(sendToAccountManager, _sendToAccountManagerCheckBox.IsChecked);
            Assert.AreEqual(sendToClient, _sendToClientCheckBox.IsChecked);
        }
    }
}
