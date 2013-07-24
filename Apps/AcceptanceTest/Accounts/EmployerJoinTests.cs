using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class EmployerJoinTests
        : WebTestClass
    {
        private const string Password = "password";
        private const string OtherPassword = "otherPassword";
        private const string Country = "Australia";
        private const string Location = "Sydney NSW 2000";

        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

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

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _searchUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _joinUrl = new ReadOnlyApplicationUrl(true, "~/employers/join");
            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");
            Get(_joinUrl);
            AssertUrl(_joinUrl);
            InitialiseControlTesters();
        }

        [TestMethod]
        public void TestControlsOnPage()
        {
            AssertControlVisibility();
            AssertTitleVisibility();
        }

        [TestMethod]
        public void TestPageValidations()
        {
            // Join without entering any details.

            _joinButton.Click();
            AssertErrorMessages(
                "The username is required.",
                "The first name is required.",
                "The last name is required.",
                "The email address is required.",
                "The phone number is required.",
                "The organisation name is required.",
                "The location is required.",
                "The password is required.",
                "The confirm password is required.",
                "Please accept the terms and conditions.");
        }

        [TestMethod]
        public void TestJoin()
        {
            // Enter all details except for the password.

            var employer = AssertValidEntries();

            // Join.

            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _joinButton.Click();

            // Should be directed to the default employer page with no errors.

            AssertUrl(LoggedInEmployerHomeUrl);

            // Check that the employer was saved.

            AssertEmployer(employer);
        }

        [TestMethod]
        public void TestDuplicateEmployer()
        {
            // Join.

            var employer = CreateEmployer();

            // Join again while logged in.

            Get(_joinUrl);
            AssertUrl(_joinUrl);
            InitialiseControlTesters();
            Join(employer, true, Password);

            // Should redirect to the default page.

            AssertUrl(LoggedInEmployerHomeUrl);

            // Log out and join again as the same user.

            LogOut();
            Get(_joinUrl);
            AssertUrl(_joinUrl);
            InitialiseControlTesters();
            Join(employer, true, Password);

            // Look for the error.

            AssertErrorMessage("The username is already being used.");
            AssertUrl(_joinUrl);
        }

        [TestMethod]
        public void TestPasswordsMustMatch()
        {
            // Enter different passwords.

            var employer = CreateEmployer();
            FillJoin(employer, true, Password, OtherPassword, employer.Organisation.Name, employer.Organisation.Address.Location.ToString());
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = OtherPassword;
            _joinButton.Click();
            AssertErrorMessage("The confirm password and password must match.");

            // Enter same passwords.

            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _joinButton.Click();
            AssertNoErrorMessage("The confirm password and password must match.");
        }

        [TestMethod]
        public void TestEmailValidation()
        {
            // Enter invalid email.

            var employer = CreateEmployer();
            FillJoin(employer, true, Password, Password, employer.Organisation.Name, employer.Organisation.Address.Location.ToString());
            _emailAddressTextBox.Text = "Not an Email Address";
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessage("The email address must be valid and have less than 320 characters.");

            // Enter valid email.

            _emailAddressTextBox.Text = "email@somewhere.com";
            _joinButton.Click();
            AssertUrl(_searchUrl);
        }

        [TestMethod]
        public void TestNameValidation()
        {
            // Bug 3214, don't set organisation name so there is at least one other error.

            var employer = CreateEmployer();
            FillJoin(employer, true, Password, Password, string.Empty, employer.Organisation.Address.Location.ToString());
            _firstNameTextBox.Text = new string('b', 15);
            _lastNameTextBox.Text = new string('b', 15);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertNoErrorMessage("The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertNoErrorMessage("The last name must be between 2 and 30 characters in length and not have any invalid characters.");

            _firstNameTextBox.Text = new string('b', 30);
            _lastNameTextBox.Text = new string('b', 31);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages("The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The organisation name is required.");

            _firstNameTextBox.Text = new string('b', 31);
            _lastNameTextBox.Text = new string('b', 50);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The organisation name is required.");

            _firstNameTextBox.Text = new string('b', 50);
            _lastNameTextBox.Text = new string('b', 30);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The organisation name is required.");

            _firstNameTextBox.Text = "@> dodgy first n@me";
            _lastNameTextBox.Text = "@dodgy last name>";
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The organisation name is required.");
        }

        [TestMethod]
        public void TestOrganisationValidation()
        {
            // Don't set organisation location so there is at least one other error.

            var employer = CreateEmployer();
            FillJoin(employer, true, Password, Password, employer.Organisation.Name, string.Empty);

            // Name length limited to 100.

            _organisationNameTextBox.Text = new string('a', 50);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertNoErrorMessage("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.");

            _organisationNameTextBox.Text = new string('a', 99);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertNoErrorMessage("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.");

            _organisationNameTextBox.Text = new string('a', 100);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertNoErrorMessage("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.");

            _organisationNameTextBox.Text = new string('a', 101);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages(
                "The organisation name must be between 1 and 100 characters in length and not have any invalid characters.",
                "The location is required.");

            _organisationNameTextBox.Text = new string('a', 200);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.",
                "The location is required.");

            _organisationNameTextBox.Text = new string('a', 1000);
            _joinButton.Click();
            AssertUrl(_joinUrl);
            AssertErrorMessages("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.",
                "The location is required.");

            // Bug 3260.

            const string badName = "Nursery Rhymes are Us and them and Others and More and Less and who knows what and who cares and I don't know what to do and do you know what to do and i am starting to run out of data which I can type here and it seems to go forever and ever and ever";
            Assert.IsTrue(badName.Length > 100);
            Join(employer, true, Password, Password, badName, Location);
            AssertUrl(_joinUrl);
            AssertErrorMessages("The organisation name must be between 1 and 100 characters in length and not have any invalid characters.");
        }

        private void AssertControlVisibility()
        {
            Assert.IsTrue(_loginIdTextBox.IsVisible);
            Assert.IsTrue(_passwordTextBox.IsVisible);
            Assert.IsTrue(_confirmPasswordTextBox.IsVisible);
            Assert.IsTrue(_emailAddressTextBox.IsVisible);
            Assert.IsTrue(_firstNameTextBox.IsVisible);
            Assert.IsTrue(_lastNameTextBox.IsVisible);
            Assert.IsTrue(_phoneNumberTextBox.IsVisible);
            Assert.IsTrue(_organisationNameTextBox.IsVisible);
            Assert.IsTrue(_roleRadioButton.IsVisible);
            Assert.IsTrue(_industryIdsListBox.IsVisible);
            Assert.IsTrue(_locationTextBox.IsVisible);
            Assert.IsTrue(_acceptTermsCheckBox.IsVisible);
            Assert.IsTrue(_joinButton.IsVisible);
        }

        private void AssertTitleVisibility()
        {
            AssertPageContains("Account details");
            AssertPageContains("Contact details");
        }

        private void InitialiseControlTesters()
        {
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

        private Employer AssertValidEntries()
        {
            // Join knowing passwords don't match.

            var employer = CreateEmployer();
            Join(employer, true, Password, OtherPassword, employer.Organisation.Name, employer.Organisation.Address.Location.ToString());

            // Check that everything is valid except for the password validation.

            AssertErrorMessage("The confirm password and password must match.");
            return employer;
        }

        private Employer CreateEmployer()
        {
            var location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location);

            return new Employer
            {
                FirstName = "Bob",
                LastName = "Smith",
                EmailAddress = new EmailAddress { Address = "linkme100@test.linkme.net.au", IsVerified = true },
                Organisation = new Organisation { Name = "LinkMe", Address = new Address { Location = location } },
                PhoneNumber = new PhoneNumber { Number = "+612344 567", Type = PhoneNumberType.Work },
                SubRole = EmployerSubRole.Employer,
                Industries = new[] { _industriesQuery.GetIndustry("Accounting"), _industriesQuery.GetIndustry("Automotive"), _industriesQuery.GetIndustry("Banking & Financial Services") }
            };
        }

        private void Join(Employer employer, bool acceptTermsofUse, string password)
        {
            Join(employer, acceptTermsofUse, password, password, employer.Organisation.Name, employer.Organisation.Address.Location.ToString());
        }

        private void Join(Employer employer, bool acceptTermsofUse, string password, string passwordConfirmation, string organisationName, string location)
        {
            FillJoin(employer, acceptTermsofUse, password, passwordConfirmation, organisationName, location);
            _joinButton.Click();
        }

        private void FillJoin(Employer employer, bool acceptTermsofUse, string password, string passwordConfirmation, string organisationName, string location)
        {
            // Login details.

            _loginIdTextBox.Text = employer.GetLoginId();
            _passwordTextBox.Text = password;
            _confirmPasswordTextBox.Text = passwordConfirmation;

            // Contact details.

            _emailAddressTextBox.Text = employer.EmailAddress.Address;
            _firstNameTextBox.Text = employer.FirstName;
            _lastNameTextBox.Text = employer.LastName;
            _phoneNumberTextBox.Text = employer.PhoneNumber.Number;

            // Company details.

            _organisationNameTextBox.Text = organisationName;
            _locationTextBox.Text = location;
            _roleRadioButton.IsChecked = employer.SubRole == EmployerSubRole.Employer;
            _industryIdsListBox.SelectedValues = new[]
            {
                _industryIdsListBox.Items[0].Value,
                _industryIdsListBox.Items[3].Value,
                _industryIdsListBox.Items[4].Value
            };

            _acceptTermsCheckBox.IsChecked = acceptTermsofUse;
        }

        private void AssertEmployer(Employer expectedEmployer)
        {
            // Load the employer.

            var id = _loginCredentialsQuery.GetUserId(expectedEmployer.GetLoginId());
            var employer = _employersQuery.GetEmployer(id.Value);

            // Compare.

            Assert.IsNotNull(employer);
            Assert.AreEqual(expectedEmployer.GetLoginId(), employer.GetLoginId());
            Assert.AreEqual(expectedEmployer.EmailAddress, employer.EmailAddress);
            Assert.AreEqual(expectedEmployer.FirstName, employer.FirstName);
            Assert.AreEqual(expectedEmployer.LastName, employer.LastName);
            Assert.AreEqual(expectedEmployer.SubRole, employer.SubRole);
            foreach (var item in expectedEmployer.Industries)
                Assert.AreEqual(employer.Industries.Contains(_industriesQuery.GetIndustry(item.Id)), true);
            Assert.AreEqual(expectedEmployer.JobTitle, employer.JobTitle);
            Assert.AreEqual(expectedEmployer.PhoneNumber, employer.PhoneNumber);
            Assert.AreEqual(expectedEmployer.Organisation.Name, employer.Organisation.Name);
            Assert.AreEqual(expectedEmployer.Organisation.Address.Location, employer.Organisation.Address.Location);
        }
    }
}