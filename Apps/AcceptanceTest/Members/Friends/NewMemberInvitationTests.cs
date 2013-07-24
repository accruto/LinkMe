using System.Xml;
using LinkMe.AcceptanceTest.ui;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Members.Friends;
using LinkMe.Apps.Asp.Navigation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Friends
{
    [TestClass]
    public class NewMemberInvitationTests
        : WebFormDataTestCase
    {
        private const string Email = "linkme10@test.linkme.net.au";
        private const string Email2 = "linkme12@test.linkme.net.au";
        private const string Email3 = "linkme13@test.linkme.net.au";
        private const string NewEmail = "linkme11@test.linkme.net.au";
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string FirstName2 = "Waylon";
        private const string LastName2 = "Smithers";
        private const string FirstName3 = "Homer";
        private const string LastName3 = "Simpson";
        private const string Location = "Armadale VIC 3143";
        private const string Location2 = "Sydney NSW 2000";
        private const string PhoneNumber = "99999999";
        private const string SalaryLowerBound = "100000";

        private const string InviteAcceptedFormat = "You are now linked to <a href=\"{0}\">{1}</a>{2} network.";

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
        private HtmlRadioButtonTester _openToOffersRadioButton;
        private string _jobDetailsFormId;
        private HtmlRadioButtonTester _maleRadioButton;
        private HtmlDropDownListTester _dateOfBirthMonthDropDownList;
        private HtmlDropDownListTester _dateOfBirthYearDropDownList;

        private HtmlTextAreaTester _txtEmailAddresses;
        private HtmlButtonTester _btnSendInvitations;
        private HtmlButtonTester _btnAccept;

        private ReadOnlyUrl _joinUrl;
        private ReadOnlyUrl _profileUrl;
        private ReadOnlyUrl _activateUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();

            // Join.

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
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");

            _jobDetailsFormId = "JobDetailsForm";
            _maleRadioButton = new HtmlRadioButtonTester(Browser, "Male");
            _dateOfBirthMonthDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthMonth");
            _dateOfBirthYearDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthYear");

            // InviteContactsForm

            _txtEmailAddresses = new HtmlTextAreaTester(Browser, AddBodyPrefix("FormContent_Content") + "_ucInviteFriends_txtEmailAddresses");
            _btnSendInvitations = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_Content") + "_btnSendInvitations");

            // ReceivedInvitiationsFrom

            _btnAccept = new HtmlButtonTester(Browser, AddBodyPrefix("FormContent_LeftContent") + "_ucReceivedNetworkInvitationList_rptInvitations_ctl00_btnAccept");

            _joinUrl = new ReadOnlyApplicationUrl(true, "~/join");
            _activateUrl = new ReadOnlyApplicationUrl(true, "~/join/activate");
            _profileUrl = new ReadOnlyApplicationUrl(true, "~/members/profile");
        }

        [TestMethod]
        public void JoinTest()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            // Browse to the invite contacts form.

            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();

            // Send an invitation.

            _txtEmailAddresses.Text = Email;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            LogOut();

            // Check that an email was sent.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, Email);
            email.AssertHtmlViewContains(member.FullName + " has sent you a request to join their personal network.");
            var url = Get(email.GetHtmlView().Body);

            // Navigate to the join page.

            Get(url);
            AssertUrlWithoutQuery(_joinUrl);

            // Join.

            Browser.Submit(_joinFormId);
            EnterDetails(Email, FirstName, LastName);
            Browser.Submit(_personalDetailsFormId);

            // No email should have been sent to the user, but one should have been sent to the inviter.

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member);
            
            // Continue.

            _maleRadioButton.IsChecked = true;
            _dateOfBirthMonthDropDownList.SelectedIndex = 1;
            _dateOfBirthYearDropDownList.SelectedIndex = 1;
            Browser.Submit(_jobDetailsFormId);

            // Check that the user has been sent to the welcome form.

            AssertUrl(_profileUrl);
        }

        [TestMethod]
        public void JoinChangeEmailTest()
        {
            // LogIn.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            // Browse to the invite contacts form.

            GetPage<InviteFriends>();
            AssertPage<InviteFriends>();

            // Send an invitation.

            _txtEmailAddresses.Text = Email;
            _btnSendInvitations.Click();
            AssertPage<InviteFriends>();
            LogOut();

            // Check that an email was sent.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, Email);
            email.AssertHtmlViewContains(member.FullName + " has sent you a request to join their personal network.");
            var joinUrl = Get(email.GetHtmlView().Body);

            // Navigate to the networker join page.

            Get(joinUrl);

            // Join.

            Browser.Submit(_joinFormId);
            EnterDetails(NewEmail, FirstName, LastName);
            Browser.Submit(_personalDetailsFormId);

            // An email should have been sent to the user.

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(NewEmail, FirstName + " " + LastName));
            var activationUrl = Get(email.GetHtmlView().Body);

            // Continue.

            _maleRadioButton.IsChecked = true;
            _dateOfBirthMonthDropDownList.SelectedIndex = 1;
            _dateOfBirthYearDropDownList.SelectedIndex = 1;
            Browser.Submit(_jobDetailsFormId);

            // Check that the user has been sent to the activation email sent form.

            AssertUrlWithoutQuery(_activateUrl);

            // Navigate to the activation page which should activate the account.

            Get(activationUrl);
            AssertUrl(GetEmailUrl("ActivationEmail", _profileUrl));
        }

        [TestMethod]
        public void MultipleInvitationsTest()
        {
            // First member sends an invite.

            var member1 = CreateMember(Email, FirstName, LastName, Location);
            LogIn(member1);

            // Browse to the invite contacts form and send an invitation.

            GetPage<InviteFriends>();
            _txtEmailAddresses.Text = Email3;
            _btnSendInvitations.Click();
            LogOut();

            // Check that an email was sent.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member1, Return, Email3);
            email.AssertHtmlViewContains(member1.FullName + " has sent you a request to join their personal network.");
            var networkerJoinUrl = Get(email.GetHtmlView().Body);

            // Second member sends an invite.

            var member2 = CreateMember(Email2, FirstName2, LastName2, Location2);
            LogIn(member2);

            // Browse to the invite contacts form and send an invitation.

            GetPage<InviteFriends>();
            _txtEmailAddresses.Text = Email3;
            _btnSendInvitations.Click();
            LogOut();

            // Check that an email was sent.

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member2, Return, Email3);
            email.AssertHtmlViewContains(member2.FullName + " has sent you a request to join their personal network.");

            // Navigate to the networker join page and join.

            Get(networkerJoinUrl);
            Browser.Submit(_joinFormId);
            EnterDetails(Email3, FirstName3, LastName3);
            Browser.Submit(_personalDetailsFormId);
            Browser.Submit(_jobDetailsFormId);

            // No email should have been sent to the user, but one should have been sent to the original inviter.

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, member1);

            // Navigate to the friends list form and check that the first member is a friend but the second is not yet.

            GetPage<ViewFriends>();
            AssertPage<ViewFriends>();
            AssertPageContains(member1.FullName);
            AssertPageContains(member1.Address.Location.ToString());

            // The second member's name will be in the notifications in the side bar but they should not
            // be in the main list, which incldues their location.

            AssertPageContains(member2.FullName);
            AssertPageDoesNotContain(member2.Address.Location.ToString());

            // Accept the invitation.

            GetPage<Invitations>();
            AssertPage<Invitations>();
            AssertPageContains(member2.FullName);
            _btnAccept.Click();
            AssertPage<Invitations>();

            var profileUrl = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, member2.Id.ToString()).PathAndQuery;
            AssertPageContains(string.Format(InviteAcceptedFormat, profileUrl, member2.FullName, member2.FullName.GetNamePossessiveSuffix()), true);

            // Check they are both in the list.

            GetPage<ViewFriends>();
            AssertPage<ViewFriends>();
            AssertPageContains(member1.FullName);
            AssertPageContains(member1.Address.Location.ToString());
            AssertPageContains(member2.FullName);
            AssertPageContains(member2.Address.Location.ToString());
        }

        private Member CreateMember(string email, string firstName, string lastName, string location)
        {
            var member = _memberAccountsCommand.CreateTestMember(email, firstName, lastName);
            _locationQuery.ResolvePostalSuburb(member.Address.Location, Australia, location);
            _memberAccountsCommand.UpdateMember(member);
            return member;
        }
        
        private static Url Get(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);

            var xmlNode = document.SelectSingleNode("//div[@class='body']/p/a");
            return xmlNode != null && xmlNode.Attributes != null ? new ApplicationUrl(xmlNode.Attributes["href"].InnerText) : null;
        }

        private void EnterDetails(string emailAddress, string firstName, string lastName)
        {
            _emailAddressTextBox.Text = emailAddress;
            _passwordTextBox.Text = "password";
            _confirmPasswordTextBox.Text = "password";
            _firstNameTextBox.Text = firstName;
            _lastNameTextBox.Text = lastName;
            _locationTextBox.Text = Location;
            _phoneNumberTextBox.Text = PhoneNumber;
            _salaryLowerBoundTextBox.Text = SalaryLowerBound;
            _openToOffersRadioButton.IsChecked = true;
            _acceptTermsCheckBox.IsChecked = true;
        }
    }
}