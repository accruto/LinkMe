using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public abstract class CreateJobAdTests
        : MaintainJobAdTests
    {
        protected abstract void AssertDefaultDisplayValue(IEmployer employer);
        protected abstract void AssertSavedDefaultValue(IEmployer employer, JobAd jobAd);
        protected abstract void SetDisplayValue();
        protected abstract void AssertSavedValue(JobAd jobAd);
        protected abstract void TestErrorValues(bool save, IEmployer employer);

        protected const string DefaultTitle = "Janitor";
        protected const string DefaultContent = "Sweeping and cleaning";
        protected const string DefaultEmailAddress = "employer@test.linkme.net.au";
        protected const string DefaultLocation = "Camberwell VIC 3124";
        protected const JobTypes DefaultJobTypes = JobTypes.FullTime;

        protected Industry DefaultIndustry
        {
            get { return _accounting; }
        }

        [TestMethod]
        public void TestEmployerDefault()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            AssertDefaultDisplayValue(employer);
        }

        [TestMethod]
        public void TestAnonymousDefault()
        {
            Get(GetJobAdUrl(null));
            AssertDefaultDisplayValue(null);
        }

        [TestMethod]
        public void TestEmployerPreviewErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            TestErrorValues(false, employer);
        }

        [TestMethod]
        public void TestEmployerSaveErrors()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            TestErrorValues(true, employer);
        }

        [TestMethod]
        public void TestAnonymousPreviewErrors()
        {
            TestErrorValues(false, null);
        }

        [TestMethod]
        public void TestEmployerPreviewDefault()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // Create the job.

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            _previewButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];
            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));

            // Check what is saved.

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedDefaultValue(employer, jobAd);
        }

        [TestMethod]
        public void TestAnonymousPreviewDefault()
        {
            Get(GetJobAdUrl(null));
            var anonymousId = GetAnonymousId();
            Get(GetJobAdUrl(null));

            // Create the job.

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _emailAddressTextBox.Text = DefaultEmailAddress;
            _locationTextBox.Text = DefaultLocation;

            _previewButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(anonymousId, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];
            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));

            // Check what is saved.

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedDefaultValue(null, jobAd);
        }

        [TestMethod]
        public void TestEmployerPreviewValues()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            SetDisplayValue();
            _previewButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];

            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedValue(jobAd);
        }

        [TestMethod]
        public void TestAnonymousPreviewValues()
        {
            Get(GetJobAdUrl(null));
            var anonymousId = GetAnonymousId();
            Get(GetJobAdUrl(null));

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _emailAddressTextBox.Text = DefaultEmailAddress;
            _locationTextBox.Text = DefaultLocation;

            SetDisplayValue();
            _previewButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(anonymousId, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];

            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedValue(jobAd);
        }

        [TestMethod]
        public void TestEmployerSaveDefault()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // Create the job.

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;
            _saveButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];
            AssertUrlWithoutQuery(GetJobAdUrl(jobAdId));
            AssertConfirmationMessage("The job ad has been saved as a draft.");

            // Check what is saved.

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedDefaultValue(employer, jobAd);
        }

        [TestMethod]
        public void TestAnonymousSave()
        {
            Get(GetJobAdUrl(null));
            Assert.IsFalse(_saveButton.IsVisible);
        }

        [TestMethod]
        public void TestEmployerSaveValues()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            SetDisplayValue();
            _saveButton.Click();

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];
            AssertUrlWithoutQuery(GetJobAdUrl(jobAdId));
            AssertConfirmationMessage("The job ad has been saved as a draft.");

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            AssertSavedValue(jobAd);
        }

        protected void TestInput(Action edit, Action<JobAd> assertJobAd)
        {
            var employer = CreateEmployer(null);
            LogIn(employer);
            Get(GetJobAdUrl(null));

            // Create the job.

            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;

            edit();
            _previewButton.Click();
            AssertNoErrorMessages();

            var ids = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(1, ids.Count);
            var jobAdId = ids[0];
            AssertUrlWithoutQuery(GetPreviewUrl(jobAdId));

            // Check what is saved.

            var jobAd = _jobAdsCommand.GetJobAd<JobAd>(jobAdId);
            assertJobAd(jobAd);
        }

        protected void AssertErrorValue(bool save, IEmployer employer, Action action, params string[] expectedErrorMessages)
        {
            Get(GetJobAdUrl(null));
            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { DefaultIndustry.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;
            if (employer == null)
                _emailAddressTextBox.Text = DefaultEmailAddress;

            action();

            if (save)
                _saveButton.Click();
            else
                _previewButton.Click();
            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessages(expectedErrorMessages);
        }
    }
}
