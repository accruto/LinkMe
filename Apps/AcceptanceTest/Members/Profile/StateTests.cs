using System;
using System.Globalization;
using LinkMe.Apps.Agents.Profiles.Commands;
using LinkMe.Apps.Agents.Profiles.Queries;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile
{
    [TestClass]
    public class StateTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IProfilesCommand _profilesCommand = Resolve<IProfilesCommand>();
        private readonly IProfilesQuery _profilesQuery = Resolve<IProfilesQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();

        private ReadOnlyUrl _profileUrl;
        private ReadOnlyUrl _termsUrl;
        private ReadOnlyUrl _joinUrl;

        private string _joinFormId;
        private string _personalDetailsFormId;

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlPasswordTester _passwordTextBox;
        private HtmlPasswordTester _confirmPasswordTextBox;
        private HtmlCheckBoxTester _acceptTermsCheckBox;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlTextBoxTester _salaryLowerBoundTextBox;
        private HtmlRadioButtonTester _openToOffersCheckBox;

        private const string ProfilePromptText = "We want to ensure that you have the best chance of being contacted by employers";
        private const string ProfileReminderText = "We've made some changes and additions to candidate profiles";
        private const string TermsProfileText = "We've made some changes to our terms and conditions.";

        private const string FirstName = "Waylon";
        private const string LastName = "Smithers";
        private const string EmailAddress = "waylon@test.linkme.net.au";
        private const string Location = "Norlane VIC 3214";
        private const string MobilePhoneNumber = "1111 1111";
        private const string Password = "password";
        private const decimal SalaryLowerBound = 100000;

        [TestInitialize]
        public void TestInitialize()
        {
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
            _termsUrl = new ReadOnlyApplicationUrl(true, "~/terms");
            _joinUrl = new ReadOnlyApplicationUrl(true, "~/join");

            _joinFormId = "JoinForm";

            _personalDetailsFormId = "PersonalDetailsForm";
            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _confirmPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmPassword");
            _acceptTermsCheckBox = new HtmlCheckBoxTester(Browser, "AcceptTerms");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _openToOffersCheckBox = new HtmlRadioButtonTester(Browser, "OpenToOffers");
        }

        [TestMethod]
        public void TestLegacyMember()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // No profile before logging in.

            Assert.IsNull(_profilesQuery.GetMemberProfile(member.Id));

            // As this is the first log in the user should be at home page and profile prompt and terms reminder should be shown.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);

            // Turned off for now
            //AssertProfilePrompt();
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            //AssertTermsReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, false, true, false, member.Id);

            // Get the page again.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            //AssertProfileReminder();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, false, true, false, member.Id);

            // Log out and back in again.

            LogOut();
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            //AssertProfileReminder();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, false, true, false, member.Id);

            // Go to the profile page.

            Get(_profileUrl);
            AssertUrl(_profileUrl);

            // Go back to the home page.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, true, true, false, member.Id);

            // Go to the terms page.

            Get(_termsUrl);
            AssertUrl(_termsUrl);

            // Go back to the home page.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, true, true, true, member.Id);
        }

        [TestMethod]
        public void TestLegacyMemberProfileTimeOut()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);

            // As this is the first log in the user should be at home page and prompt should be shown.

            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);

            // Turned off for now
            //AssertProfilePrompt();
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            //AssertTermsReminder();
            AssertNoTermsReminder();
            AssertMemberState(/*true*/ false, false, true, false, member.Id);

            // Go beyond reminder time.

            var profile = _profilesQuery.GetMemberProfile(member.Id);
            profile.UpdateStatusReminder.FirstShownTime = DateTime.Now.AddMonths(-1).AddDays(-1);
            _profilesCommand.UpdateMemberProfile(member.Id, profile);

            // Get the page again.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(true, false, true, false, member.Id);

            // Log out and back in again.

            LogOut();
            LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(true, false, true, false, member.Id);

            // Go to the profile page.

            Get(_profileUrl);
            AssertUrl(_profileUrl);

            // Go back to the home page.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(true, true, true, false, member.Id);
        }

        [TestMethod]
        public void TestNewMember()
        {
            // Join.

            Get(_joinUrl);
            Browser.Submit(_joinFormId);

            _firstNameTextBox.Text = FirstName;
            _lastNameTextBox.Text = LastName;
            _emailAddressTextBox.Text = EmailAddress;
            _phoneNumberTextBox.Text = MobilePhoneNumber;
            _locationTextBox.Text = Location;
            _salaryLowerBoundTextBox.Text = SalaryLowerBound.ToString(CultureInfo.InvariantCulture);
            _openToOffersCheckBox.IsChecked = true;
            _passwordTextBox.Text = Password;
            _confirmPasswordTextBox.Text = Password;
            _acceptTermsCheckBox.IsChecked = true;
            Browser.Submit(_personalDetailsFormId);

            // As this is the first log in the user should be at home page.

            var memberId = _loginCredentialsQuery.GetUserId(EmailAddress).Value;
            _userAccountsCommand.ActivateUserAccount(new Member { Id = memberId }, memberId);

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(false, true, false, true, memberId);

            // Get the page again.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(false, true, false, true, memberId);

            // Go to the profile page.

            Get(_profileUrl);
            AssertUrl(_profileUrl);

            // Go back to the home page.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(false, true, false, true, memberId);

            // Go to the terms page.

            Get(_termsUrl);
            AssertUrl(_termsUrl);

            // Go back to the home page.

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
            AssertNoProfilePrompt();
            AssertNoProfileReminder();
            AssertNoTermsReminder();
            AssertMemberState(false, true, false, true, memberId);
        }

        private void AssertMemberState(bool expectedProfileReminderDateSet, bool expectedHideProfileReminder, bool expectedTermsReminderDateSet, bool expectedHideTermsReminder, Guid memberId)
        {
            var profile = _profilesQuery.GetMemberProfile(memberId);
            Assert.IsNotNull(profile);

            Assert.AreEqual(expectedProfileReminderDateSet, profile.UpdateStatusReminder.FirstShownTime != null);
            Assert.AreEqual(true, profile.UpdateStatusReminder.Hide);

            /* Turned off for now
            Assert.AreEqual(expectedProfileReminderDateSet, profile.UpdateStatusReminder.FirstShownTime != null);
            Assert.AreEqual(expectedHideProfileReminder, profile.UpdateStatusReminder.Hide);
            */

            Assert.AreEqual(false, profile.UpdatedTermsReminder.FirstShownTime != null);
            Assert.AreEqual(expectedHideTermsReminder, profile.UpdatedTermsReminder.Hide);

            /* Turned off for now
            Assert.AreEqual(expectedTermsReminderDateSet, state.UpdatedTermsReminder.FirstShownTime != null);
            Assert.AreEqual(expectedHideTermsReminder, state.UpdatedTermsReminder.Hide);
            */
        }

        private void AssertProfilePrompt()
        {
            // Better tests for prompt needed when it is complete.

            AssertPageContains(ProfilePromptText);
        }

        private void AssertNoProfilePrompt()
        {
            AssertPageDoesNotContain(ProfilePromptText);
        }

        private void AssertProfileReminder()
        {
            AssertPageContains(ProfileReminderText);
        }

        private void AssertNoProfileReminder()
        {
            AssertPageDoesNotContain(ProfileReminderText);
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
