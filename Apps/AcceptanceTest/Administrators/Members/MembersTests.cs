using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Members
{
    [TestClass]
    public abstract class MembersTests
        : WebTestClass
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        protected HtmlTextBoxTester _fullNameTextBox;
        protected HtmlTextBoxTester _primaryPhoneNumberTextBox;
        protected HtmlRadioButtonTester _primaryPhoneNumberMobileRadioButton;
        protected HtmlRadioButtonTester _primaryPhoneNumberHomeRadioButton;
        protected HtmlRadioButtonTester _primaryPhoneNumberWorkRadioButton;
        protected HtmlTextBoxTester _secondaryPhoneNumberTextBox;
        protected HtmlRadioButtonTester _secondaryPhoneNumberMobileRadioButton;
        protected HtmlRadioButtonTester _secondaryPhoneNumberHomeRadioButton;
        protected HtmlRadioButtonTester _secondaryPhoneNumberWorkRadioButton;
        protected HtmlTextBoxTester _tertiaryPhoneNumberTextBox;
        protected HtmlRadioButtonTester _tertiaryPhoneNumberMobileRadioButton;
        protected HtmlRadioButtonTester _tertiaryPhoneNumberHomeRadioButton;
        protected HtmlRadioButtonTester _tertiaryPhoneNumberWorkRadioButton;
        protected HtmlTextBoxTester _genderTextBox;
        protected HtmlTextBoxTester _ageTextBox;
        protected HtmlTextBoxTester _countryTextBox;
        protected HtmlTextBoxTester _locationTextBox;
        protected HtmlTextBoxTester _loginIdTextBox;
        protected HtmlTextBoxTester _createdTimeTextBox;
        protected HtmlTextBoxTester _isEnabledTextBox;
        protected HtmlTextBoxTester _isActivatedTextBox;

        protected HtmlButtonTester _enableButton;
        protected HtmlButtonTester _disableButton;
        protected HtmlButtonTester _activateButton;
        protected HtmlButtonTester _deactivateButton;

        protected HtmlTextBoxTester _changePasswordTextBox;
        protected HtmlCheckBoxTester _sendPasswordEmailCheckBox;
        protected HtmlButtonTester _changeButton;

        protected HtmlPasswordTester _passwordTextBox;
        protected HtmlPasswordTester _newPasswordTextBox;
        protected HtmlPasswordTester _confirmNewPasswordTextBox;
        protected HtmlButtonTester _saveButton;

        private ReadOnlyUrl _membersUrl;
        private ReadOnlyUrl _searchMembersUrl;

        [TestInitialize]
        public void MembersTestsInitialize()
        {
            _fullNameTextBox = new HtmlTextBoxTester(Browser, "FullName");
            _primaryPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "PrimaryPhoneNumber");
            _primaryPhoneNumberMobileRadioButton = new HtmlRadioButtonTester(Browser, "PrimaryPhoneNumberMobile");
            _primaryPhoneNumberHomeRadioButton = new HtmlRadioButtonTester(Browser, "PrimaryPhoneNumberHome");
            _primaryPhoneNumberWorkRadioButton = new HtmlRadioButtonTester(Browser, "PrimaryPhoneNumberWork");
            _secondaryPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "SecondaryPhoneNumber");
            _secondaryPhoneNumberMobileRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryPhoneNumberMobile");
            _secondaryPhoneNumberHomeRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryPhoneNumberHome");
            _secondaryPhoneNumberWorkRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryPhoneNumberWork");
            _tertiaryPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "TertiaryPhoneNumber");
            _tertiaryPhoneNumberMobileRadioButton = new HtmlRadioButtonTester(Browser, "TertiaryPhoneNumberMobile");
            _tertiaryPhoneNumberHomeRadioButton = new HtmlRadioButtonTester(Browser, "TertiaryPhoneNumberHome");
            _tertiaryPhoneNumberWorkRadioButton = new HtmlRadioButtonTester(Browser, "TertiaryPhoneNumberWork");
            _genderTextBox = new HtmlTextBoxTester(Browser, "Gender");
            _ageTextBox = new HtmlTextBoxTester(Browser, "Age");
            _countryTextBox = new HtmlTextBoxTester(Browser, "Country");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _loginIdTextBox = new HtmlTextBoxTester(Browser, "LoginId");
            _createdTimeTextBox = new HtmlTextBoxTester(Browser, "CreatedTime");
            _isEnabledTextBox = new HtmlTextBoxTester(Browser, "IsEnabled");
            _isActivatedTextBox = new HtmlTextBoxTester(Browser, "IsActivated");

            _enableButton = new HtmlButtonTester(Browser, "enable");
            _disableButton = new HtmlButtonTester(Browser, "disable");
            _activateButton = new HtmlButtonTester(Browser, "activate");
            _deactivateButton = new HtmlButtonTester(Browser, "deactivate");

            _changePasswordTextBox = new HtmlTextBoxTester(Browser, "Password");
            _sendPasswordEmailCheckBox = new HtmlCheckBoxTester(Browser, "SendPasswordEmail");
            _changeButton = new HtmlButtonTester(Browser, "change");

            _passwordTextBox = new HtmlPasswordTester(Browser, "Password");
            _newPasswordTextBox = new HtmlPasswordTester(Browser, "NewPassword");
            _confirmNewPasswordTextBox = new HtmlPasswordTester(Browser, "ConfirmNewPassword");
            _saveButton = new HtmlButtonTester(Browser, "save");

            _membersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/members");
            _searchMembersUrl = new ReadOnlyApplicationUrl(true, "~/administrators/members/search");
        }

        protected ReadOnlyUrl GetMemberUrl(IMember member)
        {
            return new ReadOnlyApplicationUrl((_membersUrl.AbsoluteUri + "/").AddUrlSegments(member.Id.ToString().ToLower()));
        }

        protected ReadOnlyUrl GetSearchMembersUrl()
        {
            return _searchMembersUrl;
        }

        protected void AssertMember(IMember member, bool isExternal)
        {
            Assert.AreEqual(_fullNameTextBox.Text, member.FullName);
            AssertPageContains(member.GetBestEmailAddress().Address);

            var phoneNumber = member.GetPrimaryPhoneNumber();
            Assert.AreEqual(_primaryPhoneNumberTextBox.Text, (phoneNumber == null ? null : phoneNumber.Number) ?? string.Empty);
            Assert.AreEqual(_primaryPhoneNumberMobileRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Mobile);
            Assert.AreEqual(_primaryPhoneNumberHomeRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Home);
            Assert.AreEqual(_primaryPhoneNumberWorkRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Work);

            phoneNumber = member.GetSecondaryPhoneNumber();
            Assert.AreEqual(_secondaryPhoneNumberTextBox.Text, (phoneNumber == null ? null : phoneNumber.Number) ?? string.Empty);
            Assert.AreEqual(_secondaryPhoneNumberMobileRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Mobile);
            Assert.AreEqual(_secondaryPhoneNumberHomeRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Home);
            Assert.AreEqual(_secondaryPhoneNumberWorkRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Work);

            phoneNumber = member.GetTertiaryPhoneNumber();
            Assert.AreEqual(_tertiaryPhoneNumberTextBox.Text, (phoneNumber == null ? null : phoneNumber.Number) ?? string.Empty);
            Assert.AreEqual(_tertiaryPhoneNumberMobileRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Mobile);
            Assert.AreEqual(_tertiaryPhoneNumberHomeRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Home);
            Assert.AreEqual(_tertiaryPhoneNumberWorkRadioButton.IsChecked, phoneNumber != null && phoneNumber.Type == PhoneNumberType.Work);

            Assert.AreEqual(_genderTextBox.Text, member.Gender.ToString());
            Assert.AreEqual(_ageTextBox.Text, member.DateOfBirth.GetAgeDisplayText());
            Assert.AreEqual(_countryTextBox.Text, member.Address.Location.Country.Name);
            Assert.AreEqual(_locationTextBox.Text, member.Address.Location.ToString());
            Assert.AreEqual(_loginIdTextBox.Text, isExternal ? "" : member.EmailAddresses[0].Address);
            Assert.AreEqual(_createdTimeTextBox.Text, member.CreatedTime.ToShortDateString());
            Assert.AreEqual(_isEnabledTextBox.Text, member.IsEnabled ? "Yes" : "No");

            if (!isExternal)
                Assert.AreEqual(_isActivatedTextBox.Text, member.IsActivated ? "Yes" : "No");
            else
                Assert.IsFalse(_isActivatedTextBox.IsVisible);

            Assert.AreEqual(_enableButton.IsVisible, !member.IsEnabled);
            Assert.AreEqual(_disableButton.IsVisible, member.IsEnabled);

            Assert.AreEqual(_activateButton.IsVisible, !isExternal && !member.IsActivated);
            Assert.AreEqual(_deactivateButton.IsVisible, !isExternal && member.IsActivated);

            Assert.AreEqual(_changePasswordTextBox.IsVisible, !isExternal);
            Assert.AreEqual(_sendPasswordEmailCheckBox.IsVisible, !isExternal);
            Assert.AreEqual(_changeButton.IsVisible, !isExternal);
        }
    }
}
