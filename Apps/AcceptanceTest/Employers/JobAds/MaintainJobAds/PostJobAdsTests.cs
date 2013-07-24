using System;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds
{
    [TestClass]
    public class PostJobAdsTests
        : MaintainJobAdTests
    {
        [TestMethod]
        public void TestSecondaryContactEmails()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            const string email2 = "second@test.linkme.net.au";
            const string email3 = "third@test.linkme.net.au";

            // Create job ad.

            Get(GetJobAdUrl(null));
            CreateJobAd(employer.EmailAddress.Address, email2, false, null);
            _previewButton.Click();
            AssertNewJobAdPreview();

            // Check that it is saved.

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.IsTrue(ids.Count == 1);
            var jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(ids[0]);
            Assert.AreEqual(jobAd.ContactDetails.SecondaryEmailAddresses, email2);

            // Edit the job ad.

            Get(GetJobAdUrl(ids[0]));
            Assert.AreEqual(_secondaryEmailAddressesTextBox.Text, email2);
            _secondaryEmailAddressesTextBox.Text = email2 + "," + email3;
            _saveButton.Click();

            // Check that it is saved.

            jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(ids[0]);
            Assert.AreEqual(jobAd.ContactDetails.SecondaryEmailAddresses, email2 + "," + email3);

            // Edit it again.

            Get(GetJobAdUrl(ids[0]));
            Assert.AreEqual(_secondaryEmailAddressesTextBox.Text, email2 + "," + email3);
            _secondaryEmailAddressesTextBox.Text = string.Empty;
            _saveButton.Click();

            // Check that it is saved.

            jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(ids[0]);
            Assert.IsTrue(string.IsNullOrEmpty(jobAd.ContactDetails.SecondaryEmailAddresses));

            // Edit it again.

            Get(GetJobAdUrl(ids[0]));
            Assert.IsTrue(string.IsNullOrEmpty(_secondaryEmailAddressesTextBox.Text));
            _secondaryEmailAddressesTextBox.Text = email3;
            _saveButton.Click();

            // Check that it is saved.

            jobAd = _jobAdsCommand.GetJobAd<JobAdEntry>(ids[0]);
            Assert.AreEqual(jobAd.ContactDetails.SecondaryEmailAddresses, email3);
        }

        [TestMethod]
        public void TestExpiryDate()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            // Create job ad.

            Get(GetJobAdUrl(null));
            CreateJobAd(employer.EmailAddress.Address, null, true, null);
            _previewButton.Click();
            AssertNewJobAdPreview();

            // Check that it is saved.

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(ids[0]);

            // Edit it.

            Get(GetJobAdUrl(jobAd.Id));
            var expiryDate = _jobAdsCommand.GetDefaultExpiryTime(JobAdFeatures.None);
            Assert.AreEqual(expiryDate.ToShortDateString(), _expiryTimeTextBox.Text);
            expiryDate = DateTime.Now.AddDays(5);
            _expiryTimeTextBox.Text = expiryDate.ToShortDateString();
            _saveButton.Click();

            // Check that it is saved.

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(ids[0]);
            Assert.AreEqual(expiryDate.Date.AddDays(1).AddSeconds(-1), jobAd.ExpiryTime);

            // Edit it again.

            Get(GetJobAdUrl(jobAd.Id));
            Assert.AreEqual(expiryDate.ToShortDateString(), _expiryTimeTextBox.Text);
            expiryDate = DateTime.Now.AddDays(10);
            _expiryTimeTextBox.Text = expiryDate.ToShortDateString();
            _saveButton.Click();

            // Check that it is saved.

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(ids[0]);
            Assert.AreEqual(expiryDate.Date.AddDays(1).AddSeconds(-1), jobAd.ExpiryTime);
        }

        private void CreateJobAd(string emailAddress, string secondaryContactEmails, bool hideCompany, DateTime? expiryDate)
        {
            _titleTextBox.Text = "Code Monkey";
            _positionTitleTextBox.Text = "Code Monkey Position - lots of peanuts";
            _bulletPoint1TextBox.Text = "lots of peanuts";
            _bulletPoint2TextBox.Text = "and bananas";
            _bulletPoint3TextBox.Text = "and work hours";
            _summaryTextBox.Text = "Code monkey positon available - lots of hours, and bananas";
            _contentTextBox.Text = "Code monkey positon available - lots of hours, and bananas and peanuts";
            if (expiryDate != null)
                _expiryTimeTextBox.Text = expiryDate.Value.ToShortDateString();
            _companyNameTextBox.Text = "Acme";
            _emailAddressTextBox.Text = emailAddress;
            if (secondaryContactEmails != null)
                _secondaryEmailAddressesTextBox.Text = secondaryContactEmails;
            _hideCompanyCheckBox.IsChecked = hideCompany;
            _hideContactDetailsCheckBox.IsChecked = hideCompany;
            _locationTextBox.Text = "Armadale, Vic";
            _industryIdsListBox.SelectedValues = new[] { _industryIdsListBox.Items[1].Value };
            _residencyRequiredCheckBox.IsChecked = true;
            _fullTimeCheckBox.IsChecked = true;
        }

        private void AssertNewJobAdPreview()
        {
            AssertPageContains("Code monkey");
        }
    }
}