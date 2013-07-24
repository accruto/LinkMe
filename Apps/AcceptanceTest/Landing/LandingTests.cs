using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Landing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Landing
{
    [TestClass]
    public class LandingTests
        : WebTestClass
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IReferralsQuery _referralsQuery = Resolve<IReferralsQuery>();

        private ReadOnlyUrl _landingJoinUrl;
        private ReadOnlyUrl _landingSampleJoinUrl;
        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _personalDetailsUrl;
        private ReadOnlyUrl _jobDetailsUrl;
        private ReadOnlyUrl _activateUrl;

        private HtmlTextBoxTester _txtFirstName;
        private HtmlTextBoxTester _txtLastName;
        private HtmlTextBoxTester _txtEmail;
        private HtmlPasswordTester _txtPassword;
        private HtmlCheckBoxTester _chkAcceptTermsAndConditions;
        private HtmlButtonTester _btnJoin;

        private HtmlTextBoxTester _txtSampleFirstName;
        private HtmlTextBoxTester _txtSampleLastName;
        private HtmlTextBoxTester _txtSampleEmail;
        private HtmlPasswordTester _txtSamplePassword;
        private HtmlCheckBoxTester _chkSampleAcceptTermsAndConditions;
        private HtmlButtonTester _btnSampleJoin;

        private string _joinFormId;
        private string _personalDetailsFormId;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _passwordTextBox;
        private HtmlTextBoxTester _confirmPasswordTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlTextBoxTester _salaryLowerBoundTextBox;
        private HtmlRadioButtonTester _openToOffersRadioButton;
        private HtmlRadioButtonTester _maleRadioButton;
        private HtmlDropDownListTester _dateOfBirthMonthDropDownList;
        private HtmlDropDownListTester _dateOfBirthYearDropDownList;
        private string _jobDetailsFormId;

        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string Password = "password";
        private const string Location = "Armadale VIC 3143";
        private const string PhoneNumber = "99999999";
        private const string SalaryLowerBound = "100000";

        private const string BadName = "?$%";
        private const string BadEmailAddress1 = "xxx";
        private const string BadEmailAddress2 = "abc@";

        private const string Pcode = "XXXXXXXX";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _landingJoinUrl = new ReadOnlyApplicationUrl("~/landing/join.aspx");
            _landingSampleJoinUrl = new ReadOnlyApplicationUrl("~/landing/samplejoin.aspx");
            _joinUrl = new ReadOnlyApplicationUrl(true, "~/join");
            _personalDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/personaldetails");
            _jobDetailsUrl = new ReadOnlyApplicationUrl(true, "~/join/jobdetails");
            _activateUrl = new ReadOnlyApplicationUrl(true, "~/join/activate");

            _txtFirstName = new HtmlTextBoxTester(Browser, "ctl00_Body_ucJoin_txtFirstName");
            _txtLastName = new HtmlTextBoxTester(Browser, "ctl00_Body_ucJoin_txtLastName");
            _txtEmail = new HtmlTextBoxTester(Browser, "ctl00_Body_ucJoin_txtEmail");
            _txtPassword = new HtmlPasswordTester(Browser, "ctl00_Body_ucJoin_txtPassword");
            _chkAcceptTermsAndConditions = new HtmlCheckBoxTester(Browser, "ctl00_Body_ucJoin_chkAcceptTermsAndConditions", false);
            _btnJoin = new HtmlButtonTester(Browser, "ctl00_Body_btnJoin");

            // To make it portable, names on the sample form are prefixed by "linkme_".
            // (Portable = may be copied to and used on other websites)
            //
            _txtSampleFirstName = new HtmlTextBoxTester(Browser, "linkme_txtFirstName");
            _txtSampleLastName = new HtmlTextBoxTester(Browser, "linkme_txtLastName");
            _txtSampleEmail = new HtmlTextBoxTester(Browser, "linkme_txtUsername");
            _txtSamplePassword = new HtmlPasswordTester(Browser, "linkme_txtPassword");
            _chkSampleAcceptTermsAndConditions = new HtmlCheckBoxTester(Browser, "linkme_chkAcceptTermsAndConditions", false);
            _btnSampleJoin = new HtmlButtonTester(Browser, "linkme_btnJoin");

            _joinFormId = "JoinForm";

            _personalDetailsFormId = "PersonalDetailsForm";
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _passwordTextBox = new HtmlTextBoxTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlTextBoxTester(Browser, "ConfirmPassword");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");

            _jobDetailsFormId = "JobDetailsForm";
            _maleRadioButton = new HtmlRadioButtonTester(Browser, "Male");
            _dateOfBirthMonthDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthMonth");
            _dateOfBirthYearDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthYear");
        }

        [TestMethod]
        public void TestJoinDefaults()
        {
            Get(_landingJoinUrl);
            AssertJoinDefaults();
        }

        [TestMethod]
        public void TestJoin()
        {
            Get(_landingJoinUrl);

            Join(FirstName, LastName, EmailAddress, Password, true);

            // Should be thrown into the join process already logged in.

            AssertUrl(_joinUrl);

            // Check the user.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertMember(member, FirstName, LastName, EmailAddress);

            // Go to next page.

            Browser.Submit(_joinFormId);

            // Should be pre-populated.

            AssertUrlWithoutQuery(_personalDetailsUrl);

            Assert.IsFalse(_passwordTextBox.IsVisible);
            Assert.IsFalse(_confirmPasswordTextBox.IsVisible);
            Assert.IsFalse(_acceptTermsCheckBox.IsVisible);
            Assert.AreEqual(EmailAddress, _emailAddressTextBox.Text);
            Assert.AreEqual(FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(LastName, _lastNameTextBox.Text);

            _locationTextBox.Text = Location;
            _phoneNumberTextBox.Text = PhoneNumber;
            _salaryLowerBoundTextBox.Text = SalaryLowerBound;
            _openToOffersRadioButton.IsChecked = true;

            Browser.Submit(_personalDetailsFormId);
            AssertUrlWithoutQuery(_jobDetailsUrl);

            _maleRadioButton.IsChecked = true;
            _dateOfBirthMonthDropDownList.SelectedIndex = 1;
            _dateOfBirthYearDropDownList.SelectedIndex = 1;

            Browser.Submit(_jobDetailsFormId);
            AssertUrlWithoutQuery(_activateUrl);
        }

        [TestMethod]
        public void TestPcodeJoin()
        {
            var landingJoinUrl = _landingJoinUrl.AsNonReadOnly();
            landingJoinUrl.QueryString["pcode"] = Pcode;
            Get(landingJoinUrl);

            Join(FirstName, LastName, EmailAddress, Password, true);
            AssertUrl(_joinUrl);

            // Check the user.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertMember(member, FirstName, LastName, EmailAddress);
            AssertPcode(member, Pcode);
        }

        [TestMethod]
        public void TestJoinErrors()
        {
            Get(_landingJoinUrl);

            // Nothing set.

            Join(string.Empty, string.Empty, string.Empty, string.Empty, false);
            AssertJoinErrors(
                "First Name field is required.",
                "Last Name field is required.",
                "Email Address field is required.",
                "Password field is required.",
                "In order to proceed, please agree to the <b>terms of use</b>.");

            // 1 field not set.

            Join(string.Empty, LastName, EmailAddress, Password, true);
            AssertJoinErrors("First Name field is required.");

            Join(FirstName, string.Empty, EmailAddress, Password, true);
            AssertJoinErrors("Last Name field is required.");

            Join(FirstName, LastName, string.Empty, Password, true);
            AssertJoinErrors("Email Address field is required.");

            Join(FirstName, LastName, EmailAddress, string.Empty, true);
            AssertJoinErrors("Password field is required.");

            Join(FirstName, LastName, EmailAddress, Password, false);
            AssertJoinErrors("In order to proceed, please agree to the <b>terms of use</b>.");

            // Invalid names.

            Join(BadName, LastName, BadEmailAddress1, Password, true);
            AssertJoinErrors("First Name entered includes invalid characters! Valid example: 'Jean-L`och' or 'Henry the 3rd'");
            Join(FirstName, BadName, BadEmailAddress1, Password, true);
            AssertJoinErrors("Last Name entered includes invalid characters! Valid example: 'O`connor' or 'Lacroix the 2nd'");

            // Invalid emails.

            Join(FirstName, LastName, BadEmailAddress1, Password, true);
            AssertJoinErrors("The email address must be valid and have less than 320 characters.");

            Join(FirstName, LastName, BadEmailAddress2, Password, true);
            AssertJoinErrors("The email address must be valid and have less than 320 characters.");

            // Duplicate user.

            CreateMember(FirstName, LastName, EmailAddress, Password);
            Join(FirstName, LastName, EmailAddress, Password, true);
            AssertJoinErrors("Login ID already exists. Please choose another one.");
        }

        [TestMethod]
        public void TestLandingJoin()
        {
            var landingSampleJoinUrl = _landingSampleJoinUrl.AsNonReadOnly();
            landingSampleJoinUrl.QueryString["pcode"] = Pcode;
            Get(landingSampleJoinUrl);

            SampleJoin(FirstName, LastName, EmailAddress, Password, true);
            AssertUrl(_joinUrl);

            // Check the user.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertMember(member, FirstName, LastName, EmailAddress);

            // The sample page uses a pcode.

            AssertPcode(member, Pcode);
        }

        [TestMethod]
        public void TestLandingJoinErrors()
        {
            Get(_landingSampleJoinUrl);

            // Nothing set.

            SampleJoin(string.Empty, string.Empty, string.Empty, string.Empty, false);
            AssertJoinErrors(
                "First Name field is required.",
                "Last Name field is required.",
                "Email Address field is required.",
                "Password field is required.",
                "In order to proceed, please agree to the <b>terms of use</b>.");

            // 1 field not set.

            Get(_landingSampleJoinUrl);
            SampleJoin(string.Empty, LastName, EmailAddress, Password, true);
            AssertJoinErrors("First Name field is required.");

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, string.Empty, EmailAddress, Password, true);
            AssertJoinErrors("Last Name field is required.");

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, string.Empty, Password, true);
            AssertJoinErrors("Email Address field is required.");

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, EmailAddress, string.Empty, true);
            AssertJoinErrors("Password field is required.");

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, EmailAddress, Password, false);
            AssertJoinErrors("In order to proceed, please agree to the <b>terms of use</b>.");

            // Invalid names.

            Get(_landingSampleJoinUrl);
            SampleJoin(BadName, LastName, BadEmailAddress1, Password, true);
            AssertJoinErrors("First Name entered includes invalid characters! Valid example: 'Jean-L`och' or 'Henry the 3rd'");
            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, BadName, BadEmailAddress1, Password, true);
            AssertJoinErrors("Last Name entered includes invalid characters! Valid example: 'O`connor' or 'Lacroix the 2nd'");

            // Invalid emails.

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, BadEmailAddress1, Password, true);
            AssertJoinErrors("The email address must be valid and have less than 320 characters.");

            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, BadEmailAddress2, Password, true);
            AssertJoinErrors("The email address must be valid and have less than 320 characters.");

            // Duplicate user.

            CreateMember(FirstName, LastName, EmailAddress, Password);
            Get(_landingSampleJoinUrl);
            SampleJoin(FirstName, LastName, EmailAddress, Password, true);
            AssertJoinErrors("Login ID already exists. Please choose another one.");
        }

        private void AssertJoinDefaults()
        {
            Assert.IsTrue(_txtFirstName.IsVisible);
            Assert.AreEqual(string.Empty, _txtFirstName.Text);
            Assert.IsTrue(_txtLastName.IsVisible);
            Assert.AreEqual(string.Empty, _txtLastName.Text);
            Assert.IsTrue(_txtEmail.IsVisible);
            Assert.AreEqual(string.Empty, _txtEmail.Text);
            Assert.IsTrue(_txtPassword.IsVisible);
            Assert.AreEqual(string.Empty, _txtPassword.Text);
            Assert.IsTrue(_chkAcceptTermsAndConditions.IsVisible);
            Assert.AreEqual(false, _chkAcceptTermsAndConditions.IsChecked);
            Assert.IsTrue(_btnJoin.IsVisible);
        }

        private void Join(string firstName, string lastName, string email, string password, bool acceptTermsAndConditions)
        {
            _txtFirstName.Text = firstName;
            _txtLastName.Text = lastName;
            _txtEmail.Text = email;
            _txtPassword.Text = password;
            _chkAcceptTermsAndConditions.IsChecked = acceptTermsAndConditions;
            _btnJoin.Click();
        }

        private void SampleJoin(string firstName, string lastName, string email, string password, bool acceptTermsAndConditions)
        {
            _txtSampleFirstName.Text = firstName;
            _txtSampleLastName.Text = lastName;
            _txtSampleEmail.Text = email;
            _txtSamplePassword.Text = password;
            _chkSampleAcceptTermsAndConditions.IsChecked = acceptTermsAndConditions;
            _btnSampleJoin.Click();
        }

        private void CreateMember(string firstName, string lastName, string email, string password)
        {
            _memberAccountsCommand.CreateTestMember(email, password, firstName, lastName);
        }

        private void AssertMember(Member member, string firstName, string lastName, string email)
        {
            Assert.AreEqual(firstName, member.FirstName);
            Assert.AreEqual(lastName, member.LastName);
            Assert.AreEqual(email, member.EmailAddresses[0].Address);
            Assert.AreEqual(email, member.GetLoginId());
            var location = new LocationReference();
            _locationQuery.ResolveLocation(location, Australia, string.Empty);
            Assert.AreEqual(location, member.Address.Location);
            Assert.AreEqual(true, member.IsEnabled);
            Assert.AreEqual(false, member.IsActivated);
        }

        private void AssertPcode(Member member, string pcode)
        {
            var referral = _referralsQuery.GetAffiliationReferral(member.Id);
            if (referral == null)
            {
                Assert.Fail("No join referrals were saved for user " + member.GetLoginId());
                return;
            }

            if (referral.PromotionCode != pcode)
                Assert.Fail("Join referral '" + pcode + "' was not saved for user " + member.GetLoginId());
        }

        private void AssertJoinErrors(params string[] errors)
        {
            AssertPage<Join>();
            foreach (var error in errors)
                AssertPageContains(error);
        }
    }
}
