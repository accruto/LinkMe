using System.Globalization;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Utility.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
    [TestClass]
    public class NewJobAdInputTests
        : MaintainJobAdTests
    {
        private const int MaxContentLength = 35000;
        private const int MaxSummaryLength = 300;
        private static Country _newZealand;

        [TestInitialize]
        public void TestInitialize()
        {
            _newZealand = _locationQuery.GetCountry("New Zealand");
        }

        [TestMethod]
        public void TestRequiredErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            _phoneNumberTextBox.Text = "";
            _firstNameTextBox.Text = "";
            _lastNameTextBox.Text = "";
            _emailAddressTextBox.Text = "";
            _secondaryEmailAddressesTextBox.Text = "";
            _locationTextBox.Text = "";

            _previewButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessages(
                "The title is required.",
                "The content is required.",
                "The email address is required.",
                "The industry is required.",
                "The job type is required.",
                "The location is required.");
        }

        [TestMethod]
        public void TestMultipleErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            _titleTextBox.Text = "Gorilla";
            _positionTitleTextBox.Text = "Vice-Manager";
            _contentTextBox.Text = "zd5676ghr.ar,t..45,7.56,6.,y.ed,grsty%^&%*UIE%^RTJFDDUHYT&^&^IU&^K";
            _emailAddressTextBox.Text = "invalid@email";
            _phoneNumberTextBox.Text = "%@A#-34567890";
            _faxNumberTextBox.Text = "AA1234567";
            _locationTextBox.Text = "30a00";
            _firstNameTextBox.Text = "Bo#b";
            _lastNameTextBox.Text = "Mar#ley";
            _residencyRequiredCheckBox.IsChecked = true;
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[1].Value };

            _previewButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessages(
                "The first name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The last name must be between 2 and 30 characters in length and not have any invalid characters.",
                "The email address must be valid and have less than 320 characters.",
                "The fax number must be between 8 and 20 characters in length and not have any invalid characters.",
                "The phone number must be between 8 and 20 characters in length and not have any invalid characters.",
                "The job type is required.");
        }

        [TestMethod]
        public void TestLocation()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // PostalSuburb

            TestAdLocation(Australia, "Sydney NSW 2000", "Sydney NSW 2000");
            TestAdLocation(Australia, "Sydney 2000", "Sydney NSW 2000");

            // PostalCode

            TestAdLocation(Australia, "0801", "0801 NT");

            // Locality

            TestAdLocation(Australia, "3124", "3124 VIC");

            // Region

            TestAdLocation(Australia, "Melbourne", "Melbourne");

            // Subdivision

            TestAdLocation(Australia, "VIC", "VIC");
            TestAdLocation(Australia, "Victoria", "VIC");
            TestAdLocation(Australia, "Tassie", "TAS");

            // Country

            TestAdLocation(_newZealand, "Auckland", "Auckland");
        }

        [TestMethod]
        public void TestEditLocation()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // Change the country.

            CreateAdLocation(_newZealand, "Auckland", "Auckland");

            // PostalSuburb.

            SetAdLocation(Australia, "Sydney NSW 2000", "Sydney NSW 2000");
            SetAdLocation(Australia, "Sydney 2000", "Sydney NSW 2000");

            // PostalCode

            SetAdLocation(Australia, "0801", "0801 NT");

            // Locality

            SetAdLocation(Australia, "3124", "3124 VIC");

            // Region

            SetAdLocation(Australia, "Melbourne", "Melbourne");

            // Subdivision

            SetAdLocation(Australia, "VIC", "VIC");
            SetAdLocation(Australia, "Victoria", "VIC");
            SetAdLocation(Australia, "Tassie", "TAS");

            // Another country.

            SetAdLocation(_newZealand, "Auckland", "Auckland");
        }

        [TestMethod]
        public void TestLengthWithLineBreaks()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // Defect 1060.

            // Fail on summary.

            EnterJobDetails();
            _summaryTextBox.Text = new string('x', MaxSummaryLength / 2 - 2) + "\r\n" + new string('x', MaxSummaryLength / 2);

            _previewButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The summary must be no more than 300 characters in length.");

            // Correct summary, but fail on ad text.

            _summaryTextBox.Text = new string('x', MaxSummaryLength / 2 - StringUtils.HTML_LINE_BREAK.Length) + "\r\n" + new string('x', MaxSummaryLength / 2);
            _contentTextBox.Text = new string('y', MaxContentLength / 2 - 2) + "\r\n" + new string('y', MaxContentLength / 2);
            _previewButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The content must be no more than 35000 characters in length.");

            // Correct ad text.

            _contentTextBox.Text = new string('y', MaxContentLength / 2 - StringUtils.HTML_LINE_BREAK.Length) + "\r\n" + new string('y', MaxContentLength / 2);
            _previewButton.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            AssertUrlWithoutQuery(GetPreviewUrl(jobAdIds[0]));
        }

        private void TestAdLocation(Country country, string location, string expected)
        {
            CreateAdLocation(country, location, expected);

            // Now post the ad.

            _previewButton.Click();
            _publishButton.Click();

            AssertPageContains("was successfully published");
        }

        private void CreateAdLocation(Country country, string location, string expected)
        {
            // Create the ad.

            Get(GetJobAdUrl(null));
            EnterJobDetails();

            // Set the location.

            SetAdLocation(country, location, expected);
        }

        private void SetAdLocation(Country country, string location, string expected)
        {
            _countryIdDropDownList.SelectedValue = country.Id.ToString(CultureInfo.InvariantCulture);
            _locationTextBox.Text = location;
            _previewButton.Click();

            // Assert.

            AssertPageContains(expected);

            // Edit the ad again.

            _editButton.Click();
            Assert.AreEqual(country.Id.ToString(CultureInfo.InvariantCulture), _countryIdDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.IsNullOrEmpty(location) ? "" : expected, _locationTextBox.Text);
        }
    }
}