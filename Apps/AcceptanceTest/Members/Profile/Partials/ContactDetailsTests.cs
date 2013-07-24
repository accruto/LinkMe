using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Partials
{
    [TestClass]
    public class ContactDetailsTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _contactDetailsUrl;

        private HtmlTextBoxTester _firstNameTextBox;
        private HtmlTextBoxTester _lastNameTextBox;
        private HtmlTextBoxTester _emailAddressTextBox;
        private HtmlTextBoxTester _secondaryEmailAddressTextBox;
        private HtmlDropDownListTester _countryIdDropDownList;
        private HtmlTextBoxTester _locationTextBox;
        private HtmlTextBoxTester _phoneNumberTextBox;
        private HtmlRadioButtonTester _mobilePrimaryRadioButton;
        private HtmlRadioButtonTester _homePrimaryRadioButton;
        private HtmlRadioButtonTester _workPrimaryRadioButton;
        private HtmlTextBoxTester _secondaryPhoneNumberTextBox;
        private HtmlRadioButtonTester _mobileSecondaryRadioButton;
        private HtmlRadioButtonTester _homeSecondaryRadioButton;
        private HtmlRadioButtonTester _workSecondaryRadioButton;
        private HtmlTextBoxTester _citizenshipTextBox;
        private HtmlRadioButtonTester _citizenRadioButton;
        private HtmlRadioButtonTester _unrestrictedWorkViasRadioButton;
        private HtmlRadioButtonTester _restrictedWorkVisaRadioButton;
        private HtmlRadioButtonTester _noWorkVisaRadioButton;
        private HtmlRadioButtonTester _notApplicableRadioButton;
        private HtmlCheckBoxTester _aboriginalCheckBox;
        private HtmlCheckBoxTester _torresIslanderCheckBox;
        private HtmlRadioButtonTester _maleRadionButton;
        private HtmlRadioButtonTester _femaleRadioButton;
        private HtmlDropDownListTester _dateOfBirthMonthDropDownList;
        private HtmlDropDownListTester _dateOfBirthYearDropDownList;

        [TestInitialize]
        public void TestInitialize()
        {
            _contactDetailsUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/contactdetails");

            _firstNameTextBox = new HtmlTextBoxTester(Browser, "FirstName");
            _lastNameTextBox = new HtmlTextBoxTester(Browser, "LastName");
            _emailAddressTextBox = new HtmlTextBoxTester(Browser, "EmailAddress");
            _secondaryEmailAddressTextBox = new HtmlTextBoxTester(Browser, "SecondaryEmailAddress");
            _countryIdDropDownList = new HtmlDropDownListTester(Browser, "CountryId");
            _locationTextBox = new HtmlTextBoxTester(Browser, "Location");
            _phoneNumberTextBox = new HtmlTextBoxTester(Browser, "PhoneNumber");
            _mobilePrimaryRadioButton = new HtmlRadioButtonTester(Browser, "Mobile");
            _homePrimaryRadioButton = new HtmlRadioButtonTester(Browser, "Home");
            _workPrimaryRadioButton = new HtmlRadioButtonTester(Browser, "Work");
            _secondaryPhoneNumberTextBox = new HtmlTextBoxTester(Browser, "SecondaryPhoneNumber");
            _mobileSecondaryRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryMobile");
            _homeSecondaryRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryHome");
            _workSecondaryRadioButton = new HtmlRadioButtonTester(Browser, "SecondaryWork");
            _citizenshipTextBox = new HtmlTextBoxTester(Browser, "Citizenship");
            _citizenRadioButton = new HtmlRadioButtonTester(Browser, "Citizen");
            _unrestrictedWorkViasRadioButton = new HtmlRadioButtonTester(Browser, "UnrestrictedWorkVisa");
            _restrictedWorkVisaRadioButton = new HtmlRadioButtonTester(Browser, "RestrictedWorkVisa");
            _noWorkVisaRadioButton = new HtmlRadioButtonTester(Browser, "NoWorkVisa");
            _notApplicableRadioButton = new HtmlRadioButtonTester(Browser, "NotApplicable");
            _aboriginalCheckBox = new HtmlCheckBoxTester(Browser, "Aboriginal");
            _torresIslanderCheckBox = new HtmlCheckBoxTester(Browser, "TorresIslander");
            _maleRadionButton = new HtmlRadioButtonTester(Browser, "Male");
            _femaleRadioButton = new HtmlRadioButtonTester(Browser, "Female");
            _dateOfBirthMonthDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthMonth");
            _dateOfBirthYearDropDownList = new HtmlDropDownListTester(Browser, "DateOfBirthYear");
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _contactDetailsUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(member.FirstName, _firstNameTextBox.Text);
            Assert.AreEqual(member.LastName, _lastNameTextBox.Text);
            Assert.AreEqual(member.EmailAddresses[0].Address, _emailAddressTextBox.Text);
            Assert.AreEqual(member.EmailAddresses.Count > 1 ? member.EmailAddresses[1].Address : string.Empty, _secondaryEmailAddressTextBox.Text);

            Assert.AreEqual(member.Address.Location.Country.Id.ToString(), _countryIdDropDownList.SelectedItem.Value);
            Assert.AreEqual(member.Address.Location.ToString(), _locationTextBox.Text);

            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 0 ? member.PhoneNumbers[0].Number : string.Empty, _phoneNumberTextBox.Text);
            Assert.AreEqual(member.PhoneNumbers == null || member.PhoneNumbers.Count <= 0 || member.PhoneNumbers[0].Type == PhoneNumberType.Mobile, _mobilePrimaryRadioButton.IsChecked);
            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 0 && member.PhoneNumbers[0].Type == PhoneNumberType.Home, _homePrimaryRadioButton.IsChecked);
            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 0 && member.PhoneNumbers[0].Type == PhoneNumberType.Work, _workPrimaryRadioButton.IsChecked);

            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 1 ? member.PhoneNumbers[1].Number : string.Empty, _secondaryPhoneNumberTextBox.Text);
            Assert.AreEqual(member.PhoneNumbers == null || member.PhoneNumbers.Count <= 1 || member.PhoneNumbers[1].Type == PhoneNumberType.Mobile, _mobileSecondaryRadioButton.IsChecked);
            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 1 && member.PhoneNumbers[1].Type == PhoneNumberType.Home, _homeSecondaryRadioButton.IsChecked);
            Assert.AreEqual(member.PhoneNumbers != null && member.PhoneNumbers.Count > 1 && member.PhoneNumbers[1].Type == PhoneNumberType.Work, _workSecondaryRadioButton.IsChecked);

            Assert.AreEqual(resume == null ? "" : resume.Citizenship, _citizenshipTextBox.Text);

            Assert.AreEqual(candidate.VisaStatus == VisaStatus.Citizen, _citizenRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.UnrestrictedWorkVisa, _unrestrictedWorkViasRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.RestrictedWorkVisa, _restrictedWorkVisaRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.NoWorkVisa, _noWorkVisaRadioButton.IsChecked);
            Assert.AreEqual(candidate.VisaStatus == VisaStatus.NotApplicable, _notApplicableRadioButton.IsChecked);

            Assert.AreEqual(member.EthnicStatus.IsFlagSet(EthnicStatus.Aboriginal), _aboriginalCheckBox.IsChecked);
            Assert.AreEqual(member.EthnicStatus.IsFlagSet(EthnicStatus.TorresIslander), _torresIslanderCheckBox.IsChecked);

            Assert.AreEqual(member.Gender == Gender.Male, _maleRadionButton.IsChecked);
            Assert.AreEqual(member.Gender == Gender.Female, _femaleRadioButton.IsChecked);

            Assert.AreEqual(member.DateOfBirth == null || member.DateOfBirth.Value.Month == null ? "" : member.DateOfBirth.Value.Month.Value.ToString(), _dateOfBirthMonthDropDownList.SelectedItem.Value);
            Assert.AreEqual(member.DateOfBirth == null ? "" : member.DateOfBirth.Value.Year.ToString(), _dateOfBirthYearDropDownList.SelectedItem.Value);
        }
    }
}
