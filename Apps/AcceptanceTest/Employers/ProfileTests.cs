using System;
using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers
{
    [TestClass]
    public class ProfileTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IProfilesQuery _profilesQuery = Resolve<IProfilesQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

        private ReadOnlyUrl _termsUrl;
        private ReadOnlyUrl _joinUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlRadioButtonTester _roleRadioButton;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        private const string TermsProfileText = "We've made some changes to our terms and conditions.";

        private const string FirstName = "Waylon";
        private const string LastName = "Smithers";
        private const string EmailAddress = "waylon@test.linkme.net.au";
        private const string Location = "Norlane VIC 3214";
        private const string PhoneNumber = "1111 1111";
        private const string LoginId = "wsmithers";
        private const string Password = "password";
        private const string OrganisationName = "Acme";

        [TestInitialize]
        public void TestInitialize()
        {
            _termsUrl = new ReadOnlyApplicationUrl(true, "~/terms");
            _joinUrl = new ReadOnlyApplicationUrl(true, "~/employers/join");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _roleRadioButton = new HtmlRadioButtonTester(Browser, "Employer");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");
        }

        [TestMethod]
        public void TestLegacyEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // No profile before logging in.

            Assert.IsNull(_profilesQuery.GetEmployerProfile(employer.Id));

            // As this is the first log in the user should be at home page and profile prompt and terms reminder should be shown.

            LogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);
            //AssertTermsReminder();
            AssertNoTermsReminder();    // Turned off for now
            AssertEmployerState(true, false, employer.Id);

            // Get the page again.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(true, false, employer.Id);

            // Log out and back in again.

            LogOut();
            LogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(true, false, employer.Id);

            // Go to the terms page.

            Get(_termsUrl);
            AssertUrl(_termsUrl);

            // Go back to the home page.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(true, true, employer.Id);
        }

        [TestMethod]
        public void TestNewEmployer()
        {
            // Join.

            Get(_joinUrl);

            // Login details.

            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;

            // Contact details.

            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _phoneNumberTextBox.Text = PhoneNumber;

            // Company details.

            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _roleRadioButton.IsChecked = true;
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[0].Value };

            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();

            // As this is the first log in the user should be at home page.

            var employerId = _loginCredentialsQuery.GetUserId(LoginId).Value;

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(false, true, employerId);

            // Get the page again.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(false, true, employerId);

            // Go to the terms page.

            Get(_termsUrl);
            AssertUrl(_termsUrl);

            // Go back to the home page.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
            AssertNoTermsReminder();
            AssertEmployerState(false, true, employerId);
        }

        private void AssertEmployerState(bool expectedTermsReminderDateSet, bool expectedHideTermsReminder, Guid employerId)
        {
            var state = _profilesQuery.GetEmployerProfile(employerId);
            if (state != null)
            {
                Assert.AreEqual(false, state.UpdatedTermsReminder.FirstShownTime != null);
                Assert.AreEqual(expectedHideTermsReminder, state.UpdatedTermsReminder.Hide);
            }

            /* Turned off for now
            Assert.IsNotNull(state);
            Assert.AreEqual(expectedTermsReminderDateSet, state.UpdatedTermsReminder.FirstShownTime != null);
            Assert.AreEqual(expectedHideTermsReminder, state.UpdatedTermsReminder.Hide);
            */
        }

        private void AssertTermsReminder()
        {
            AssertPageContains(TermsProfileText);
        }

        private void AssertNoTermsReminder()
        {
            AssertPageDoesNotContain(TermsProfileText);
        }
    }
}