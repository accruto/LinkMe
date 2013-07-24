using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class AccountsTests
        : SearchTests
    {
        private const string JoinLoginId = "TestLoginId";
        private const string JoinPassword = "password";
        private const string FirstName = "Paul";
        private const string LastName = "Hodgman";
        private const string OrganisationName = "LinkMe";
        private const string Location = "Norlane VIC 3214";
        private const string EmailAddress = "test@test.linkme.net.au";
        private const string PhoneNumber = "99999999";

        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _joinUrl;

        private HtmlTextBoxTester _loginIdTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlButtonTester _loginButton;

        private HtmlTextBoxTester _joinLoginIdTextBox;
        private HtmlPasswordTester _joinPasswordTextBox;
        private HtmlPasswordTester _joinConfirmPasswordTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl("~/employers/login");
            _joinUrl = new ReadOnlyApplicationUrl("~/employers/join");

            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _loginButton = new HtmlButtonTester(Browser, "login");

            _joinLoginIdTextBox = new HtmlTextBoxTester(Browser, "JoinLoginId");
            _joinPasswordTextBox = new HtmlPasswordTester(Browser, "JoinPassword");
            _joinConfirmPasswordTextBox = new HtmlPasswordTester(Browser, "JoinConfirmPassword");
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");
        }

        [TestMethod]
        public void TestSearchLogIn()
        {
            var employer = CreateEmployer();
            var member = CreateMember(0);

            // Do an anonymous search.

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(member);

            // Look for the login overlay link and follow it.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@id='login-overlay-login']");
            Assert.AreEqual("Log in", node.InnerText);
            Get(new ReadOnlyApplicationUrl(node.Attributes["href"].Value));

            // Should be on the login page.

            AssertPath(_loginUrl);
            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = employer.GetPassword();
            _loginButton.Click();

            // Should be on the current search page with the same member.

            AssertPath(GetResultsUrl());
            AssertMembers(member);
        }

        [TestMethod]
        public void TestSearchJoin()
        {
            var member = CreateMember(0);

            // Do an anonymous search.

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(member);

            // Look for the login overlay link and follow it.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@id='login-overlay-join']");
            Assert.AreEqual("create", node.InnerText);
            Get(new ReadOnlyApplicationUrl(node.Attributes["href"].Value));

            // Should be on the join page.

            AssertPath(_joinUrl);
            _joinLoginIdTextBox.Text = JoinLoginId;
            _joinPasswordTextBox.Text = JoinPassword;
            _joinConfirmPasswordTextBox.Text = JoinPassword;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = PhoneNumber;
            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();

            // Should be on the current search page with the same member.

            AssertPath(GetResultsUrl());
            AssertMembers(member);
        }

        [TestMethod]
        public void TestCandidatesLogIn()
        {
            var employer = CreateEmployer();
            var member = CreateMember(0);

            // View the candidate.

            var url = GetCandidatesUrl(member.Id);
            Get(url);
            AssertCandidate(member);
            url = new ReadOnlyUrl(Browser.CurrentUrl);

            // Look for the login overlay link and follow it.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@id='login-overlay-login']");
            Assert.AreEqual("Log in", node.InnerText);
            Get(new ReadOnlyApplicationUrl(node.Attributes["href"].Value));

            // Should be on the login page.

            AssertPath(_loginUrl);
            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = employer.GetPassword();
            _loginButton.Click();

            // Should be back on the candidates page with the same member.

            AssertUrl(url);
            AssertCandidate(member);
        }

        [TestMethod]
        public void TestCandidatesJoin()
        {
            var member = CreateMember(0);

            // View the candidate.

            var url = GetCandidatesUrl(member.Id);
            Get(url);
            AssertCandidate(member);
            url = new ReadOnlyUrl(Browser.CurrentUrl);

            // Look for the login overlay link and follow it.

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//a[@id='login-overlay-join']");
            Assert.AreEqual("create", node.InnerText);
            Get(new ReadOnlyApplicationUrl(node.Attributes["href"].Value));

            // Should be on the join page.

            AssertPath(_joinUrl);
            _joinLoginIdTextBox.Text = JoinLoginId;
            _joinPasswordTextBox.Text = JoinPassword;
            _joinConfirmPasswordTextBox.Text = JoinPassword;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = PhoneNumber;
            _acceptTermsCheckBox.IsChecked = true;
            _joinButton.Click();

            // Should be back on the candidates page with the same member.

            AssertUrl(url);
            AssertCandidate(member);
        }
    }
}