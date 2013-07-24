using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public abstract class EditJobAdTests
        : MaintainJobAdTests
    {
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        protected abstract void SetValue(JobAd jobAd);
        protected abstract void SetDisplayValue();
        protected abstract void AssertSavedValue(JobAd jobAd);
        protected abstract void TestErrorValues(bool save, Guid jobAdId);

        private const string DefaultTitle = "Janitor";
        private const string DefaultContent = "Sweeping and cleaning";
        private const string DefaultEmailAddress = "employer@test.linkme.net.au";
        private const string DefaultCountry = "Australia";
        private const string DefaultLocation = "Norlane VIC 3214";

        [TestMethod]
        public void TestEmployerSavedDefault()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                },
            };
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Check.

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestAnonymousSavedDefault()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                },
            };
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            // Check.

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestEmployerSaveErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Check.

            TestErrorValues(true, jobAd.Id);
        }

        [TestMethod]
        public void TestEmployerPreviewErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Check.

            TestErrorValues(false, jobAd.Id);
        }

        [TestMethod]
        public void TestAnonymousPreviewErrors()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = DefaultEmailAddress,
                },
            };
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            // Check.

            TestErrorValues(false, jobAd.Id);
        }

        [TestMethod]
        public void TestEmployerChangeAndSaveDefaultValue()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            SetValue(jobAd);
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _saveButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertConfirmationMessage("The job ad has been saved.");

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestEmployerChangeAndPreviewDefaultValue()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            SetValue(jobAd);
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _previewButton.Click();
            AssertUrlWithoutQuery(GetPreviewUrl(jobAd.Id));

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestAnonymousSaveValue()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = DefaultEmailAddress,
                },
            };
            SetValue(jobAd);
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            Assert.IsFalse(_saveButton.IsVisible);
        }

        [TestMethod]
        public void TestAnonymousChangeAndPreviewDefaultValue()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = DefaultEmailAddress,
                },
            };
            SetValue(jobAd);
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _previewButton.Click();
            AssertUrlWithoutQuery(GetPreviewUrl(jobAd.Id));

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestEmployerSavedValue()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                },
            };
            SetValue(jobAd);
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Check.

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestAnonymousSavedValue()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                },
            };
            SetValue(jobAd);
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            // Check.

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestEmployerChangeAndSaveValue()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            SetValue(jobAd);
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _saveButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertConfirmationMessage("The job ad has been saved.");

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestEmployerChangeAndPreviewValue()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            SetValue(jobAd);
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _previewButton.Click();
            AssertUrlWithoutQuery(GetPreviewUrl(jobAd.Id));

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        [TestMethod]
        public void TestAnonymousChangeAndPreviewValue()
        {
            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            var jobAd = new JobAd
            {
                PosterId = anonymousId,
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = DefaultEmailAddress,
                },
            };
            SetValue(jobAd);
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = anonymousId });
            _jobAdsCommand.CreateJobAd(jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            SetDisplayValue();
            _previewButton.Click();
            AssertUrlWithoutQuery(GetPreviewUrl(jobAd.Id));

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            AssertSavedValue(jobAd);

            Get(GetJobAdUrl(jobAd.Id));
            AssertJobAd(jobAd);
        }

        protected void TestEdit(Action edit, Action<JobAd> assertJobAd)
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var jobAd = new JobAd
            {
                Title = DefaultTitle,
                Description =
                {
                    Content = DefaultContent,
                    Industries = new[] { _accounting },
                    JobTypes = JobTypes.FullTime,
                    Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(DefaultCountry), DefaultLocation),
                },
                ContactDetails = new ContactDetails
                {
                    EmailAddress = employer.EmailAddress.Address,
                },
            };
            _employerJobAdsCommand.CreateJobAd(employer, jobAd);

            // Change.

            Get(GetJobAdUrl(jobAd.Id));
            edit();
            _saveButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertConfirmationMessage("The job ad has been saved.");

            jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAd.Id);
            assertJobAd(jobAd);
        }

        private void AssertJobAd(JobAd jobAd)
        {
            Assert.AreEqual(jobAd.Visibility.HideCompany, _hideCompanyCheckBox.IsChecked);
            Assert.AreEqual(jobAd.Visibility.HideContactDetails, _hideContactDetailsCheckBox.IsChecked);

            Assert.AreEqual(jobAd.Title, _titleTextBox.Text);
            Assert.AreEqual(jobAd.ExpiryTime == null ? DateTime.Now.Date.AddDays(14).ToString("d") : jobAd.ExpiryTime.Value.Date.ToString("d"), _expiryTimeTextBox.Text);

            Assert.AreEqual(jobAd.Integration.ExternalReferenceId ?? "", _externalReferenceIdTextBox.Text);

            Assert.AreEqual(jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 1 ? "" : jobAd.Description.BulletPoints[0], _bulletPoint1TextBox.Text);
            Assert.AreEqual(jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 2 ? "" : jobAd.Description.BulletPoints[1], _bulletPoint2TextBox.Text);
            Assert.AreEqual(jobAd.Description.BulletPoints == null || jobAd.Description.BulletPoints.Count < 3 ? "" : jobAd.Description.BulletPoints[2], _bulletPoint3TextBox.Text);

            Assert.AreEqual(jobAd.Description.CompanyName ?? "", _companyNameTextBox.Text);
            Assert.AreEqual(jobAd.Description.PositionTitle ?? "", _positionTitleTextBox.Text);
            Assert.AreEqual(jobAd.Description.Summary ?? "", _summaryTextBox.Text.Trim());
            Assert.AreEqual(jobAd.Description.Content ?? "", _contentTextBox.Text.Trim());
            Assert.AreEqual(jobAd.Description.Package ?? "", _packageTextBox.Text);
            Assert.AreEqual(jobAd.Description.Location == null ? "1" : jobAd.Description.Location.Country.Id.ToString(CultureInfo.InvariantCulture), _countryIdDropDownList.SelectedValue);
            Assert.AreEqual(jobAd.Description.Location == null ? "" : jobAd.Description.Location.ToString(), _locationTextBox.Text);
            Assert.AreEqual(jobAd.Description.ResidencyRequired, _residencyRequiredCheckBox.IsChecked);

            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.EmailAddress ?? "", _emailAddressTextBox.Text);
            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.SecondaryEmailAddresses ?? "", _secondaryEmailAddressesTextBox.Text);
            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.FaxNumber ?? "", _faxNumberTextBox.Text);
            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.FirstName ?? "", _firstNameTextBox.Text);
            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.LastName ?? "", _lastNameTextBox.Text);
            Assert.AreEqual(jobAd.ContactDetails == null ? "" : jobAd.ContactDetails.PhoneNumber ?? "", _phoneNumberTextBox.Text);

            var industryIds = jobAd.Description.Industries == null
                ? new List<string>()
                : jobAd.Description.Industries.Select(i => i.Id.ToString());
            Assert.IsTrue(_industryIdsListBox.SelectedValues.CollectionEqual(industryIds));

            Assert.AreEqual(jobAd.Description.JobTypes.IsFlagSet(JobTypes.FullTime), _fullTimeCheckBox.IsChecked);
            Assert.AreEqual(jobAd.Description.JobTypes.IsFlagSet(JobTypes.PartTime), _partTimeCheckBox.IsChecked);
            Assert.AreEqual(jobAd.Description.JobTypes.IsFlagSet(JobTypes.Contract), _contractCheckBox.IsChecked);
            Assert.AreEqual(jobAd.Description.JobTypes.IsFlagSet(JobTypes.Temp), _tempCheckBox.IsChecked);
            Assert.AreEqual(jobAd.Description.JobTypes.IsFlagSet(JobTypes.JobShare), _jobShareCheckBox.IsChecked);

            Assert.AreEqual(jobAd.Description.Salary == null || jobAd.Description.Salary.LowerBound == null ? "" : ((int)jobAd.Description.Salary.LowerBound.Value).ToString(), _salaryLowerBoundTextBox.Text);
            Assert.AreEqual(jobAd.Description.Salary == null || jobAd.Description.Salary.UpperBound == null ? "" : ((int)jobAd.Description.Salary.UpperBound.Value).ToString(), _salaryUpperBoundTextBox.Text);
        }

        protected void AssertErrorValue(bool save, Guid jobAdId, Action action, params string[] expectedErrorMessages)
        {
            // Save.

            Get(GetJobAdUrl(jobAdId));
            action();
            if (save)
                _saveButton.Click();
            else
                _previewButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(jobAdId));
            AssertErrorMessages(expectedErrorMessages);
        }
    }
}
