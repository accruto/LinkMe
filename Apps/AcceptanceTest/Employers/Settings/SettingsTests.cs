using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Credits;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Administrators.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Test.Administrators;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Settings
{
	[TestClass]
	public class SettingsTests
        : WebTestClass
	{
        private const string UnlimitedQuantityText = "unlimited";

        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorsCommand _administratorsCommand = Resolve<IAdministratorsCommand>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
	    private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const string NewLoginId = "NewUser";
        private const string NewCompanyName = "NewCompany";
        private const string NewEmailAddress = "barney@test.linkme.net.au";
	    private const string NewJobTitle = "Lion tamer";
	    private const string NewPhoneNumber = "76767676";

	    private ReadOnlyUrl _settingsUrl;

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlRadioButtonTester _employerRadioButton;
        private HtmlRadioButtonTester _recruiterRadioButton;
        private HtmlTextBoxTester _jobTitleTextBox;
		private HtmlButtonTester _saveButton;
		private HtmlButtonTester _cancelButton;
        private HtmlCheckBoxTester _showSuggestedCandidatesCheckBox;
        private HtmlCheckBoxTester _sendSuggestedCandidatesCheckBox;
        private HtmlCheckBoxTester _receiveSuggestedCandidatesCheckBox;
        private HtmlCheckBoxTester _emailEmployerUpdateCheckBox;
        private HtmlCheckBoxTester _emailCampaignCheckBox;

        [TestInitialize]
        public void TestInitialize()
		{
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _employerRadioButton = new HtmlRadioButtonTester(Browser, "Employer");
            _recruiterRadioButton = new HtmlRadioButtonTester(Browser, "Recruiter");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _jobTitleTextBox = new HtmlTextBoxTester(Browser, "JobTitle");
            _saveButton = new HtmlButtonTester(Browser, "save");
            _cancelButton = new HtmlButtonTester(Browser, "cancel");
            _showSuggestedCandidatesCheckBox = new HtmlCheckBoxTester(Browser, "ShowSuggestedCandidates");
            _sendSuggestedCandidatesCheckBox = new HtmlCheckBoxTester(Browser, "SendSuggestedCandidates");
            _receiveSuggestedCandidatesCheckBox = new HtmlCheckBoxTester(Browser, "ReceiveSuggestedCandidates");
            _emailEmployerUpdateCheckBox = new HtmlCheckBoxTester(Browser, "EmailEmployerUpdate");
            _emailCampaignCheckBox = new HtmlCheckBoxTester(Browser, "EmailCampaign");

            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");
		}

        [TestMethod]
        public void TestDefaults()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Assert visibility and defaults.

            AssertUrl(_settingsUrl);
            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.AreEqual(employer.GetLoginId(), _loginIdTextBox.Text);
            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.AreEqual(employer.FirstName, _firstNameTextBox.Text);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.AreEqual(employer.LastName, _lastNameTextBox.Text);

            Assert.IsTrue(_employerRadioButton.IsVisible);
            Assert.AreEqual(EmployerSubRole.Employer, employer.SubRole);
            Assert.AreEqual(employer.SubRole == EmployerSubRole.Employer, _employerRadioButton.IsChecked);
            Assert.IsTrue(_recruiterRadioButton.IsVisible);
            Assert.AreEqual(employer.SubRole == EmployerSubRole.Recruiter, _recruiterRadioButton.IsChecked);

            Assert.AreEqual(_phoneNumberTextBox.Text, employer.PhoneNumber == null ? null : employer.PhoneNumber.Number);
            Assert.AreEqual(_jobTitleTextBox.Text, employer.JobTitle ?? "");

            Assert.IsTrue(GetSelectedIndustryIds().CollectionEqual(employer.Industries == null ? new Guid[0] : employer.Industries.Select(i => i.Id)));
        }

	    [TestMethod]
        public void TestSecure()
        {
            // Assert it is secure.

            AssertSecureUrl(_settingsUrl, EmployerLogInUrl);
        }

        [TestMethod]
        public void TestNameValidation()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            _firstNameTextBox.Text = new string('b', 30);
            _lastNameTextBox.Text = new string('b', 31);
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessage("The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            _firstNameTextBox.Text = new string('b', 31);
            _lastNameTextBox.Text = new string('b', 50);
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            _firstNameTextBox.Text = new string('b', 50);
            _lastNameTextBox.Text = new string('b', 30);
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessage("The first name must be between 2 and 30 characters in length and not have any invalid characters.");

            _firstNameTextBox.Text = "@> dodgy first n@me";
            _lastNameTextBox.Text = "@dodgy last name>";
            _saveButton.Click();
            AssertUrl(_settingsUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            // Success - page redirects

            var firstName = new string('a', 15);
            var lastName = new string('b', 15);
            _firstNameTextBox.Text = firstName;
            _lastNameTextBox.Text = lastName;
            _saveButton.Click();
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);

            employer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(firstName, employer.FirstName);
            Assert.AreEqual(lastName, employer.LastName);
        }

        [TestMethod]
		public void TestLoginIdChanged()
		{
			var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);
            AssertHeaderLoggedInText(employer.FullName);

            // Update the user id.

            _loginIdTextBox.Text = NewLoginId;
            _saveButton.Click();

            // Assert employer.

            var id = _loginCredentialsQuery.GetUserId(NewLoginId);
			var updatedEmployer = _employersQuery.GetEmployer(id.Value);
            Assert.AreEqual(employer.FirstName, updatedEmployer.FirstName);
            Assert.AreEqual(employer.LastName, updatedEmployer.LastName);

            // Assert page.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(NewLoginId, _loginIdTextBox.Text);
            Assert.AreEqual(updatedEmployer.FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(updatedEmployer.LastName, _lastNameTextBox.Text);
            AssertHeaderLoggedInText(updatedEmployer.FullName);
		}

        [TestMethod]
        public void TestLoginIdChangedToDuplicate()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Create another user.

            var employer1 = CreateEmployer(1);

            // Update the user id.

            _loginIdTextBox.Text = employer1.GetLoginId();
            _saveButton.Click();

            // Assert.

            AssertErrorMessage("The username is already being used.");
            AssertUrl(_settingsUrl);
        }

        [TestMethod]
        public void TestEmailAddressChanged()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Try to update.

            Assert.IsTrue(_emailAddressTextBox.IsReadOnly);
            _emailAddressTextBox.Text = NewEmailAddress;
            _saveButton.Click();

            // Assert employer.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(employer.EmailAddress, updatedEmployer.EmailAddress);

            // Assert that no email was sent.

            _emailServer.AssertNoEmailSent();

            // Assert page contains no mention of email of verification.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            AssertPageContains(ValidationInfoMessages.CHANGES_SAVED);

            Get(_settingsUrl);
            Assert.AreEqual(employer.EmailAddress.Address, _emailAddressTextBox.Text);
        }

        [TestMethod]
        public void TestPhoneNumberChanged()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Try to update.

            _phoneNumberTextBox.Text = NewPhoneNumber;
            _saveButton.Click();

            // Assert employer.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(NewPhoneNumber, updatedEmployer.PhoneNumber.Number);

            Get(_settingsUrl);
            Assert.AreEqual(updatedEmployer.PhoneNumber.Number, _phoneNumberTextBox.Text);
        }

        [TestMethod]
        public void TestIndustriesChanged()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Try to update.

            var newIndustryIds = new[] { _industriesQuery.GetIndustry("Banking & Financial Services").Id, _industriesQuery.GetIndustry("Hospitality & Tourism").Id };
            SelectIndustryIds(newIndustryIds);
            _saveButton.Click();

            // Assert employer.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.IsTrue(newIndustryIds.CollectionEqual(updatedEmployer.Industries.Select(i => i.Id)));

            Get(_settingsUrl);
            Assert.IsTrue(GetSelectedIndustryIds().CollectionEqual(updatedEmployer.Industries.Select(i => i.Id)));
        }

	    [TestMethod]
        public void TestCompanyNameChanged()
        {
            const string verifiedCompanyName = "Verified Company";

            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Update the unverified company name

            Assert.IsFalse(_organisationNameTextBox.IsReadOnly);
            Assert.AreEqual(employer.Organisation.FullName, _organisationNameTextBox.Text);
            _organisationNameTextBox.Text = NewCompanyName;
            _saveButton.Click();

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            LogOut();

            // Check that the update succeeded.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(NewCompanyName, updatedEmployer.Organisation.FullName);

            // Make a verified company.

            var admin = _administratorsCommand.CreateTestAdministrator(0);
            var organisation = new VerifiedOrganisation {Name = verifiedCompanyName, VerifiedById = admin.Id, AccountManagerId = admin.Id};
            _organisationsCommand.CreateOrganisation(organisation);
            employer.Organisation = organisation;
            _employerAccountsCommand.UpdateEmployer(employer);

            LogIn(employer);
            Get(_settingsUrl);

            // Try to change name again.

            Assert.AreEqual(verifiedCompanyName, _organisationNameTextBox.Text);
            Assert.IsTrue(_organisationNameTextBox.IsReadOnly);
            _organisationNameTextBox.Text = NewCompanyName;
            _saveButton.Click();

            // Check that the update was NOT made.

            updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(verifiedCompanyName, updatedEmployer.Organisation.FullName);

            // Assert page.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(verifiedCompanyName, _organisationNameTextBox.Text);
        }

		[TestMethod]
		public void TestSubRoleChanged()
		{
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Update.

            _recruiterRadioButton.IsChecked = true;
            _saveButton.Click();

            // Assert employer.

		    var id = _loginCredentialsQuery.GetUserId(employer.GetLoginId());
            var updatedEmployer = _employersQuery.GetEmployer(id.Value);
            Assert.AreEqual(EmployerSubRole.Recruiter, updatedEmployer.SubRole);

            // Assert page.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.IsTrue(!_employerRadioButton.IsChecked);
            Assert.IsTrue(_recruiterRadioButton.IsChecked);
		}

        [TestMethod]
        public void TestJobTitleChanged()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Update.

            _jobTitleTextBox.Text = NewJobTitle;
            _saveButton.Click();

            // Assert employer.

            var id = _loginCredentialsQuery.GetUserId(employer.GetLoginId());
            var updatedEmployer = _employersQuery.GetEmployer(id.Value);
            Assert.AreEqual(NewJobTitle, updatedEmployer.JobTitle);

            // Assert page.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(NewJobTitle, _jobTitleTextBox.Text);
        }

        [TestMethod]
        public void TestCancel()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Click the cancel button.

            _cancelButton.Click();
            AssertUrl(LoggedInEmployerHomeUrl);
        }

        [TestMethod]
		public void TestSomeContactCredits()
		{
            // Grant contact credits.

			var employer = CreateEmployer(0);

            _allocationsCommand.DeallocateAll(_allocationsQuery, employer.Id);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 10 });

            // Assert.

            LogIn(employer);
            Get(_settingsUrl);
            AssertPageContains(_creditsQuery.GetCredit<ContactCredit>().ShortDescription + ": " + GetCurrentContactCreditQuantity(employer.Id) + " remaining");
            AssertPageDoesNotContain(UnlimitedQuantityText);
		}

	    [TestMethod]
		public void TestNoContactCredits()
		{
            // Clear products.

            var employer = CreateEmployer(0);
            _allocationsCommand.DeallocateAll(_allocationsQuery, employer.Id);

            // Assert.

            LogIn(employer);
            Get(_settingsUrl);
            AssertPageContains("Contacts: none remaining");
		}

		[TestMethod]
		public void TestUnlimitedContactCredits()
		{
            // Grant credits.

            var employer = CreateEmployer(0);
            _allocationsCommand.DeallocateAll(_allocationsQuery, employer.Id);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });

            // Assert.

            LogIn(employer);
            Get(_settingsUrl);
            AssertPageContains("Contacts: unlimited, never expiring");
		}

        [TestMethod]
        public void TestSuggestedCandidatesEmailVerified()
        {
            // The default suggested candidates should not depend on the email being verified.

            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            // Assert.

            AssertDefaultSuggestedCandidates();

            // Change the ReceiveSuggestedCandidatesEmails.

            var oldValue = _receiveSuggestedCandidatesCheckBox.IsChecked;
            _receiveSuggestedCandidatesCheckBox.IsChecked = !_receiveSuggestedCandidatesCheckBox.IsChecked;
            _saveButton.Click();

            // Assert employer.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(employer.EmailAddress, updatedEmployer.EmailAddress);

            // Assert that no email was sent.

            _emailServer.AssertNoEmailSent();

            // Assert page contains no mention of email of verification.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            AssertPageContains(ValidationInfoMessages.CHANGES_SAVED);

            Get(_settingsUrl);
            Assert.AreEqual(employer.EmailAddress.Address, _emailAddressTextBox.Text);
            Assert.AreEqual(!oldValue, _receiveSuggestedCandidatesCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestSuggestedCandidatesEmailNotVerified()
        {
            // The default suggested candidates should not depend on the email being verified.

            var employer = CreateEmployer(0);
            employer.IsActivated = false;
            _employerAccountsCommand.UpdateEmployer(employer);
            LogIn(employer);
            Get(_settingsUrl);

            // Assert.

            AssertDefaultSuggestedCandidates();

            // Change the email and the ReceiveSuggestedCandidatesEmails at the same time.

            var oldValue = _receiveSuggestedCandidatesCheckBox.IsChecked;
            _receiveSuggestedCandidatesCheckBox.IsChecked = !_receiveSuggestedCandidatesCheckBox.IsChecked;
            _saveButton.Click();

            // Assert employer.

            var updatedEmployer = _employersQuery.GetEmployer(employer.Id);
            Assert.AreEqual(employer.EmailAddress, updatedEmployer.EmailAddress);

            // Assert that no email was sent.

            _emailServer.AssertNoEmailSent();

            // Assert page contains no mention of email of verification.

            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            AssertPageContains(ValidationInfoMessages.CHANGES_SAVED);

            Get(_settingsUrl);
            Assert.AreEqual(employer.EmailAddress.Address, _emailAddressTextBox.Text);
            Assert.AreEqual(!oldValue, _receiveSuggestedCandidatesCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestEmployerUpdate()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            AssertCategory("EmployerUpdate", null, _settingsQuery.GetSettings(employer.Id));
            Assert.AreEqual(true, _emailEmployerUpdateCheckBox.IsChecked);

            // Turn it off.

            _emailEmployerUpdateCheckBox.IsChecked = false;
            _saveButton.Click();

            AssertCategory("EmployerUpdate", Frequency.Never, _settingsQuery.GetSettings(employer.Id));
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(false, _emailEmployerUpdateCheckBox.IsChecked);

            // Turn it on again.

            _emailEmployerUpdateCheckBox.IsChecked = true;
            _saveButton.Click();

            AssertCategory("EmployerUpdate", Frequency.Immediately, _settingsQuery.GetSettings(employer.Id));
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(true, _emailEmployerUpdateCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestCampaign()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            Get(_settingsUrl);

            AssertCategory("Campaign", null, _settingsQuery.GetSettings(employer.Id));
            Assert.AreEqual(true, _emailCampaignCheckBox.IsChecked);

            // Turn it off.

            _emailCampaignCheckBox.IsChecked = false;
            _saveButton.Click();

            AssertCategory("Campaign", Frequency.Never, _settingsQuery.GetSettings(employer.Id));
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(false, _emailCampaignCheckBox.IsChecked);

            // Turn it on again.

            _emailCampaignCheckBox.IsChecked = true;
            _saveButton.Click();

            AssertCategory("Campaign", Frequency.Immediately, _settingsQuery.GetSettings(employer.Id));
            AssertUrlWithoutQuery(LoggedInEmployerHomeUrl);
            Get(_settingsUrl);
            Assert.AreEqual(true, _emailCampaignCheckBox.IsChecked);
        }

        private IEnumerable<Guid> GetSelectedIndustryIds()
        {
            return (from item in _industryIdsListBox.Items where item.IsSelected select new Guid(item.Value)).ToList();
        }

	    private void SelectIndustryIds(IEnumerable<Guid> industryIds)
	    {
	        _industryIdsListBox.SelectedValues = (from i in industryIds select i.ToString()).ToList();
        }

        private void AssertCategory(string name, Frequency? expectedFrequency, RecipientSettings settings)
        {
            var category = _settingsQuery.GetCategory(name);
            if (expectedFrequency == null)
            {
                if (settings != null)
                    Assert.IsNull((from c in settings.CategorySettings where c.CategoryId == category.Id select c).SingleOrDefault());
            }
            else
            {
                Assert.IsNotNull(settings);
                var categorySettings = (from c in settings.CategorySettings where c.CategoryId == category.Id select c).SingleOrDefault();
                Assert.IsNotNull(categorySettings);
                Assert.AreEqual(expectedFrequency.Value, categorySettings.Frequency);
            }
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(0));
            employer.JobTitle = "Archeologist";
            _employerAccountsCommand.UpdateEmployer(employer);
            return employer;
        }

        private void AssertHeaderLoggedInText(string displayText)
        {
            AssertPageContains("Logged in as <div class=\"user-name\">" + displayText);
        }

        private void AssertDefaultSuggestedCandidates()
        {
            // Assert the checkbox settings for visible, enabled and checked.

            Assert.IsTrue(_showSuggestedCandidatesCheckBox.IsVisible);
            Assert.IsTrue(_sendSuggestedCandidatesCheckBox.IsVisible);
            Assert.IsTrue(_receiveSuggestedCandidatesCheckBox.IsVisible);

            Assert.IsTrue(_showSuggestedCandidatesCheckBox.IsEnabled);
            Assert.IsTrue(_sendSuggestedCandidatesCheckBox.IsEnabled);
            Assert.IsTrue(_receiveSuggestedCandidatesCheckBox.IsEnabled);

            Assert.IsTrue(_showSuggestedCandidatesCheckBox.IsChecked);
            Assert.IsTrue(!_sendSuggestedCandidatesCheckBox.IsChecked);
            Assert.IsTrue(_receiveSuggestedCandidatesCheckBox.IsChecked);
        }

        private int GetCurrentContactCreditQuantity(Guid userId)
        {
            var total = 0;
            foreach (var allocation in _allocationsQuery.GetActiveAllocations<ContactCredit>(userId))
            {
                if (!allocation.IsUnlimited)
                    total += allocation.RemainingQuantity.Value;
            }

            return total;
        }
	}
}
