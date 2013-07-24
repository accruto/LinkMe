using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public class EmployerJoinTests
        : CommunityTests
    {
        private const string LoginId = "testlogin";
        private const string EmailAddress = "testlogin@test.linkme.net.au";
        private const string Password = "password";
        private const string FirstName = "monty";
        private const string LastName = "burns";
        private const string PrimaryPhone = "99999999";
        private const string OrganisationName = "Acme";
        private const string Location = "Norlane VIC 3214";

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _searchUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            InitialiseTesters();
            _emailServer.ClearEmails();

            _joinUrl = new ReadOnlyApplicationUrl(true, "~/employers/join");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
        }

        [TestMethod]
        public void TestJoin()
        {
            // Create community.

            var community = TestCommunity.BusinessSpectator.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Hit the join page.

            var url = GetCommunityUrl(community, "employers/join.aspx");
            Get(url);
            var joinUrl = _joinUrl.AsNonReadOnly();
            joinUrl.Host = "localhost.jobs.businessspectator.com.au";
            AssertUrl(joinUrl);

            Join();
            var searchUrl = _searchUrl.AsNonReadOnly();
            searchUrl.Host = "localhost.jobs.businessspectator.com.au";
            AssertUrl(searchUrl);

            // Logout.

            LogOut();

            // Try to log in again and assert that the home page is reached.

            var employer = _employerAccountsQuery.GetEmployer(LoginId);
            LogIn(employer);
            AssertUrl(searchUrl);

            // Employer joining through the community site is associated with that community.

            AssertEmployer(LoginId, community);
        }

        private void InitialiseTesters()
        {
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");
        }

        private void Join()
        {
            EnterStandardDetails();
            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();
        }

        private void EnterStandardDetails()
        {
            _loginIdTextBox.Text = LoginId;
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _phoneNumberTextBox.Text = PrimaryPhone;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
        }
    }
}
