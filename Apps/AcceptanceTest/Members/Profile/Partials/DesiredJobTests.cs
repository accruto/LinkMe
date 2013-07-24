using System.Linq;
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
    public class DesiredJobTests
        : PartialProfileTests
    {
        private ReadOnlyUrl _desiredJobUrl;

        private HtmlTextBoxTester _desiredJobTitleTextBox;
        private HtmlCheckBoxTester _fullTimeCheckBox;
        private HtmlCheckBoxTester _partTimeCheckBox;
        private HtmlCheckBoxTester _contractCheckBox;
        private HtmlCheckBoxTester _tempCheckBox;
        private HtmlCheckBoxTester _jobShareTextBox;
        private HtmlRadioButtonTester _availableNowRadioButton;
        private HtmlRadioButtonTester _activelyLookingRadioButton;
        private HtmlRadioButtonTester _openToOffersRadioButton;
        private HtmlRadioButtonTester _notLookingRadioButton;
        private HtmlRadioButtonTester _salaryRateYearRadioButton;
        private HtmlRadioButtonTester _salaryRateHourRadioButton;
        private HtmlTextBoxTester _salaryLowerBoundTextBox;
        private HtmlCheckBoxTester _isSalaryNotVisibleCheckBox;
        private HtmlCheckBoxTester _sendSuggestedJobsCheckBox;
        private HtmlRadioButtonTester _relocationPreferenceYesRadioButton;
        private HtmlRadioButtonTester _relocationPreferenceNoRadioButton;
        private HtmlRadioButtonTester _relocationPreferenceWouldConsiderRadioButton;
        private HtmlListBoxTester _relocationCountryIdsListBox;
        private HtmlListBoxTester _relocationCountryLocationIdsListBox;

        [TestInitialize]
        public void TestInitialize()
        {
            _desiredJobUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/desiredjob");

            _desiredJobTitleTextBox = new HtmlTextBoxTester(Browser, "DesiredJobTitle");
            _fullTimeCheckBox = new HtmlCheckBoxTester(Browser, "FullTime");
            _partTimeCheckBox = new HtmlCheckBoxTester(Browser, "PartTime");
            _contractCheckBox = new HtmlCheckBoxTester(Browser, "Contract");
            _tempCheckBox = new HtmlCheckBoxTester(Browser, "Temp");
            _jobShareTextBox = new HtmlCheckBoxTester(Browser, "JobShare");
            _availableNowRadioButton = new HtmlRadioButtonTester(Browser, "AvailableNow");
            _activelyLookingRadioButton = new HtmlRadioButtonTester(Browser, "ActivelyLooking");
            _openToOffersRadioButton = new HtmlRadioButtonTester(Browser, "OpenToOffers");
            _notLookingRadioButton = new HtmlRadioButtonTester(Browser, "NotLooking");
            _salaryRateYearRadioButton = new HtmlRadioButtonTester(Browser, "SalaryRateYear");
            _salaryRateHourRadioButton = new HtmlRadioButtonTester(Browser, "SalaryRateHour");
            _salaryLowerBoundTextBox = new HtmlTextBoxTester(Browser, "SalaryLowerBound");
            _isSalaryNotVisibleCheckBox = new HtmlCheckBoxTester(Browser, "IsSalaryNotVisible");
            _sendSuggestedJobsCheckBox = new HtmlCheckBoxTester(Browser, "SendSuggestedJobs");
            _relocationPreferenceYesRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceYes");
            _relocationPreferenceNoRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceNo");
            _relocationPreferenceWouldConsiderRadioButton = new HtmlRadioButtonTester(Browser, "RelocationPreferenceWouldConsider");
            _relocationCountryIdsListBox = new HtmlListBoxTester(Browser, "RelocationCountryIds");
            _relocationCountryLocationIdsListBox = new HtmlListBoxTester(Browser, "RelocationCountryLocationIds");
        }

        protected override ReadOnlyUrl GetPartialUrl()
        {
            return _desiredJobUrl;
        }

        protected override void AssertMember(IMember member, ICandidate candidate, IResume resume)
        {
            Assert.AreEqual(candidate.DesiredJobTitle ?? "", _desiredJobTitleTextBox.Text);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.FullTime), _fullTimeCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.PartTime), _partTimeCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.Contract), _contractCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.Temp), _tempCheckBox.IsChecked);
            Assert.AreEqual(candidate.DesiredJobTypes.IsFlagSet(JobTypes.JobShare), _jobShareTextBox.IsChecked);

            Assert.AreEqual(candidate.Status == CandidateStatus.AvailableNow, _availableNowRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.ActivelyLooking, _activelyLookingRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.OpenToOffers, _openToOffersRadioButton.IsChecked);
            Assert.AreEqual(candidate.Status == CandidateStatus.NotLooking, _notLookingRadioButton.IsChecked);

            Assert.AreEqual(candidate.DesiredSalary == null || candidate.DesiredSalary.Rate == SalaryRate.Year, _salaryRateYearRadioButton.IsChecked);
            Assert.AreEqual(candidate.DesiredSalary != null && candidate.DesiredSalary.Rate == SalaryRate.Hour, _salaryRateHourRadioButton.IsChecked);
            Assert.AreEqual(candidate.DesiredSalary == null || candidate.DesiredSalary.LowerBound == null ? "" : candidate.DesiredSalary.LowerBound.Value.ToString(), _salaryLowerBoundTextBox.Text);
            Assert.AreEqual(!member.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Salary), _isSalaryNotVisibleCheckBox.IsChecked);

            Assert.AreEqual(true, _sendSuggestedJobsCheckBox.IsChecked);

            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.Yes, _relocationPreferenceYesRadioButton.IsChecked);
            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.No, _relocationPreferenceNoRadioButton.IsChecked);
            Assert.AreEqual(candidate.RelocationPreference == RelocationPreference.WouldConsider, _relocationPreferenceWouldConsiderRadioButton.IsChecked);

            var relocationCountryIds = candidate.RelocationLocations == null
                ? null
                : (from l in candidate.RelocationLocations where l.IsCountry select l.Country.Id);
            Assert.IsTrue(relocationCountryIds.NullableCollectionEqual(from i in _relocationCountryIdsListBox.SelectedItems select int.Parse(i.Value)));

            var relocationCountryLocationIds = candidate.RelocationLocations == null
                ? null
                : (from l in candidate.RelocationLocations where !l.IsCountry select l.NamedLocation.Id);
            Assert.IsTrue(relocationCountryLocationIds.NullableCollectionEqual(from i in _relocationCountryLocationIdsListBox.SelectedItems select int.Parse(i.Value)));
        }
    }
}
