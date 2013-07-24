using System;
using System.Linq;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Integration.LinkedIn;
using LinkMe.Domain.Roles.Integration.LinkedIn.Commands;
using LinkMe.Domain.Roles.Integration.LinkedIn.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.LinkedIn
{
    [TestClass]
    public class LinkedInTests
        : WebTestClass
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly ILinkedInCommand _linkedInCommand = Resolve<ILinkedInCommand>();
        private readonly ILinkedInQuery _linkedInQuery = Resolve<ILinkedInQuery>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();

        private ReadOnlyUrl _accountUrl;
        private ReadOnlyUrl _apiLoginUrl;
        private ReadOnlyUrl _settingsUrl;

        private const string LinkedInId = "abcdefg";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string LinkedInIndustry = "Program Development";
        private const string Industry = "Consulting & Corporate Strategy";
        private const string LinkedInLocation = "Norlane";
        private const string Location = "Norlane VIC 3214";
        private const string LinkedInCountry = "au";
        private const string Country = "Australia";
        private const string OrganisationName = "Acme";
        private const string EmailAddress = "homer@test.linkme.net.au";
        private const string PhoneNumber = "99999999";

        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlTextBoxTester _organisationNameTextBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlListBoxTester _industryIdsListBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlButtonTester _joinButton;

        private class LinkedInApiProfile
            : JsonRequestModel
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Industry { get; set; }
            public string Location { get; set; }
            public string Country { get; set; }
            public string OrganisationName { get; set; }
        }

        private class LinkedInAuthenticationModel
            : JsonResponseModel
        {
            public string Status { get; set; }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _apiLoginUrl = new ReadOnlyApplicationUrl(true, "~/accounts/linkedin/api/login");
            _accountUrl = new ReadOnlyApplicationUrl(true, "~/employers/linkedin");
            _settingsUrl = new ReadOnlyApplicationUrl(true, "~/employers/settings");

            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _organisationNameTextBox = new HtmlTextBoxTester(Browser, "OrganisationName");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _industryIdsListBox = new HtmlListBoxTester(Browser, "IndustryIds");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _joinButton = new HtmlButtonTester(Browser, "join");
        }

        [TestMethod]
        public void TestAutoLogIn()
        {
            var profile = CreateProfile();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            profile.UserId = employer.Id;
            _linkedInCommand.UpdateProfile(profile);

            // Login.

            Get(EmployerLogInUrl);
            AssertUrl(EmployerLogInUrl);
            AssertAutoAuthorize(false);

            var url = LinkedInApiLogIn(AuthenticationStatus.Authenticated, CreateApiProfile());
            Assert.AreEqual(EmployerHomeUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Confirm logged in.

            Get(url);
            AssertUrl(LoggedInEmployerHomeUrl);

            // Profile should not have changed.

            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(employer.Id));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestAutoLogInReturnUrl()
        {
            var profile = CreateProfile();
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            profile.UserId = employer.Id;
            _linkedInCommand.UpdateProfile(profile);

            // Redirected to login page.

            Get(_settingsUrl);
            var loginUrl = EmployerLogInUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = _settingsUrl.PathAndQuery;
            AssertUrl(loginUrl);
            AssertAutoAuthorize(false);

            // Login.

            var url = LinkedInApiLogIn(AuthenticationStatus.Authenticated, CreateApiProfile());
            Assert.AreEqual(_settingsUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Confirm logged in.

            Get(url);
            AssertUrl(_settingsUrl);

            // Profile should not have changed.

            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(employer.Id));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestLogIn()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Login.

            Get(EmployerLogInUrl);
            AssertUrl(EmployerLogInUrl);
            AssertAutoAuthorize(false);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            Assert.AreEqual(_accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            // Confirm.

            Get(url);
            AssertUrl(_accountUrl);

            SubmitLogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);

            // Should now be associated with the employer.

            profile.UserId = employer.Id;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(employer.Id));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestLogInReturnUrl()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Redirected to login page.

            Get(_settingsUrl);
            var loginUrl = EmployerLogInUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = _settingsUrl.PathAndQuery;
            AssertUrl(loginUrl);
            AssertAutoAuthorize(false);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            var accountUrl = _accountUrl.AsNonReadOnly();
            accountUrl.QueryString["returnUrl"] = _settingsUrl.PathAndQuery;
            Assert.AreEqual(accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            // Confirm.

            Get(url);
            AssertUrl(accountUrl);

            SubmitLogIn(employer);
            AssertUrl(_settingsUrl);

            // Should now be associated with the employer.

            profile.UserId = employer.Id;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            AssertProfile(profile, _linkedInQuery.GetProfile(employer.Id));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestLogInValidation()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Login.

            Get(EmployerLogInUrl);
            AssertUrl(EmployerLogInUrl);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            Assert.AreEqual(_accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));

            // Confirm.

            Get(url);
            AssertUrl(_accountUrl);

            SubmitLogIn("", "");
            AssertUrl(_accountUrl);

            AssertErrorMessages("The username is required.", "The password is required.");

            // Should not have changed.

            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
            Assert.IsNull(_linkedInQuery.GetProfile(employer.Id));
        }

        [TestMethod]
        public void TestJoin()
        {
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Login.

            Get(EmployerLogInUrl);
            AssertUrl(EmployerLogInUrl);
            AssertAutoAuthorize(false);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            Assert.AreEqual(_accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));

            // Confirm.

            Get(url);
            AssertUrl(_accountUrl);

            // Check the screen is populated.

            Assert.AreEqual(_firstNameTextBox.Text, FirstName);
            Assert.AreEqual(_lastNameTextBox.Text, LastName);
            Assert.AreEqual(1, _industryIdsListBox.SelectedItems.Count, Industry);
            Assert.AreEqual(_industryIdsListBox.SelectedItems[0].Text, Industry);
            Assert.AreEqual(_locationTextBox.Text, Location);
            Assert.AreEqual(_organisationNameTextBox.Text, OrganisationName);

            Join();
            AssertUrl(LoggedInEmployerHomeUrl);

            // Should now be associated with the employer.

            var updatedProfile = _linkedInQuery.GetProfile(LinkedInId);
            var employerId = updatedProfile.UserId;
            profile.UserId = employerId;

            AssertProfile(profile, updatedProfile);
            AssertProfile(profile, _linkedInQuery.GetProfile(employerId));

            // Check the employer account.

            AssertEmployer(_employersQuery.GetEmployer(employerId));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestJoinReturnUrl()
        {
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Redirected to login page.

            Get(_settingsUrl);
            var loginUrl = EmployerLogInUrl.AsNonReadOnly();
            loginUrl.QueryString["returnUrl"] = _settingsUrl.PathAndQuery;
            AssertUrl(loginUrl);
            AssertAutoAuthorize(false);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            var accountUrl = _accountUrl.AsNonReadOnly();
            accountUrl.QueryString["returnUrl"] = _settingsUrl.PathAndQuery;
            Assert.AreEqual(accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));

            // Confirm.

            Get(url);
            AssertUrl(accountUrl);

            // Check the screen is populated.

            Assert.AreEqual(_firstNameTextBox.Text, FirstName);
            Assert.AreEqual(_lastNameTextBox.Text, LastName);
            Assert.AreEqual(1, _industryIdsListBox.SelectedItems.Count);
            Assert.AreEqual(_industryIdsListBox.SelectedItems[0].Text, Industry);
            Assert.AreEqual(_locationTextBox.Text, Location);
            Assert.AreEqual(_organisationNameTextBox.Text, OrganisationName);

            Join();
            AssertUrl(_settingsUrl);

            // Should now be associated with the employer.

            var updatedProfile = _linkedInQuery.GetProfile(LinkedInId);
            var employerId = updatedProfile.UserId;
            profile.UserId = employerId;

            AssertProfile(profile, updatedProfile);
            AssertProfile(profile, _linkedInQuery.GetProfile(employerId));

            // Check the employer account.

            AssertEmployer(_employersQuery.GetEmployer(employerId));

            // Logout.

            LogOut();
            AssertUrl(EmployerHomeUrl);
            AssertAutoAuthorize(true);

            Get(EmployerLogInUrl);
            AssertAutoAuthorize(true);
        }

        [TestMethod]
        public void TestJoinValidation()
        {
            Assert.IsNull(_linkedInQuery.GetProfile(LinkedInId));

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            // Login.

            Get(EmployerLogInUrl);
            AssertUrl(EmployerLogInUrl);

            // Log in.

            var url = LinkedInApiLogIn(AuthenticationStatus.Failed, CreateApiProfile());
            Assert.AreEqual(_accountUrl.PathAndQuery.ToLower(), url.PathAndQuery.ToLower());

            // Profile should be created for the anonymous user.

            var profile = CreateProfile();
            profile.UserId = anonymousId;
            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));

            // Confirm.

            Get(url);
            AssertUrl(_accountUrl);

            // Check the screen is populated.

            _joinButton.Click();
            AssertUrl(_accountUrl);

            AssertErrorMessages("The email address is required.", "The phone number is required.", "Please accept the terms and conditions.");

            // Should not have changed.

            AssertProfile(profile, _linkedInQuery.GetProfile(LinkedInId));
        }

        private static void AssertEmployer(IEmployer employer)
        {
            Assert.AreEqual(FirstName, employer.FirstName);
            Assert.AreEqual(LastName, employer.LastName);
            Assert.AreEqual(EmailAddress, employer.EmailAddress.Address);
            Assert.AreEqual(Industry, (from i in employer.Industries select i.Name).Single());
            Assert.AreEqual(PhoneNumber, employer.PhoneNumber.Number);
            Assert.AreEqual(OrganisationName, employer.Organisation.Name);
            Assert.AreEqual(Location, employer.Organisation.Address.Location.ToString());
            Assert.IsTrue(employer.IsActivated);
            Assert.IsTrue(employer.IsEnabled);
        }

        private LinkedInProfile CreateProfile()
        {
            return new LinkedInProfile
            {
                Id = LinkedInId,
                FirstName = FirstName,
                LastName = LastName,
                OrganisationName = OrganisationName,
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location),
                Industries = new[] { _industriesQuery.GetIndustry(Industry) },
            };
        }

        private static LinkedInApiProfile CreateApiProfile()
        {
            return new LinkedInApiProfile
            {
                Id = LinkedInId,
                FirstName = FirstName,
                LastName = LastName,
                Industry = LinkedInIndustry,
                Country = LinkedInCountry,
                Location = LinkedInLocation,
                OrganisationName = OrganisationName,
            };
        }

        private void Join()
        {
            // Contact details.

            _emailAddressTextBox.Text = EmailAddress;
            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _phoneNumberTextBox.Text = PhoneNumber;

            // Company details.

            _organisationNameTextBox.Text = OrganisationName;
            _locationTextBox.Text = Location;
            _industryIdsListBox.SelectedTexts = new[] { Industry };
            _acceptTermsCheckBox.IsChecked = true;

            // Join.

            _joinButton.Click();
        }

        private void AssertAutoAuthorize(bool hasLoggedOut)
        {
            if (hasLoggedOut)
                AssertPageDoesNotContain("authorize: true");
            else
                AssertPageContains("authorize: true");
        }

        private ReadOnlyUrl LinkedInApiLogIn(AuthenticationStatus expectedStatus, JsonRequestModel profile)
        {
            // Ensure element is on the screen.

            Assert.IsNotNull(Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='linkedin']/script[@type='in/Login']"));

            // Extract the urls to redirect to.

            const string find = "window.location = data.Status == \"Authenticated\" ? \"";

            var pos = Browser.CurrentPageText.IndexOf(find);
            Assert.AreNotEqual(-1, pos);
            pos = pos + find.Length;

            var endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            var authenticatedUrl = Browser.CurrentPageText.Substring(pos, endPos - pos);

            pos = endPos + 1;
            endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            pos = endPos + 1;
            endPos = Browser.CurrentPageText.IndexOf("\"", pos, StringComparison.CurrentCultureIgnoreCase);
            Assert.AreNotEqual(-1, endPos);

            var accountUrl = Browser.CurrentPageText.Substring(pos, endPos - pos);

            // Login.

            var response = Deserialize<LinkedInAuthenticationModel>(Post(_apiLoginUrl, JsonContentType, Serialize(profile)));
            AssertJsonSuccess(response);
            var status = (AuthenticationStatus)Enum.Parse(typeof(AuthenticationStatus), response.Status);

            Assert.AreEqual(expectedStatus, status);
            return expectedStatus == AuthenticationStatus.Authenticated
                ? new ReadOnlyApplicationUrl(authenticatedUrl)
                : new ReadOnlyApplicationUrl(accountUrl);
        }

        private static void AssertProfile(LinkedInProfile expectedProfile, LinkedInProfile profile)
        {
            Assert.AreEqual(expectedProfile.Id, profile.Id);
            Assert.AreEqual(expectedProfile.UserId, profile.UserId);
            Assert.AreEqual(expectedProfile.FirstName, profile.FirstName);
            Assert.AreEqual(expectedProfile.LastName, profile.LastName);
            Assert.AreEqual(expectedProfile.OrganisationName, profile.OrganisationName);
            Assert.IsTrue((expectedProfile.Industries ?? new Industry[0]).Select(i => i.Id).CollectionEqual((profile.Industries ?? new Industry[0]).Select(i => i.Id)));
            Assert.AreEqual(expectedProfile.Location, profile.Location);
        }
    }
}
