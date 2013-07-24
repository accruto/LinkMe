using System;
using System.IO;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.CreateJobAds
{
    [TestClass]
    public class LogoTests
        : MaintainJobAdTests
    {
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        private HtmlHiddenTester _logoId;
        private HtmlFileTester _logo;
        private const string DefaultTitle = "Janitor";
        private const string DefaultContent = "Sweeping and cleaning";
        private const string DefaultLocation = "Norlane VIC 3214";

        [TestInitialize]
        public void TestInitialize()
        {
            _logoId = new HtmlHiddenTester(Browser, "LogoId");
            _logo = new HtmlFileTester(Browser, "Logo");
        }

        [TestMethod]
        public void TestDefaults()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            Assert.IsFalse(_logoId.IsVisible);
            Assert.AreEqual("", _logo.FilePath);
        }

        [TestMethod]
        public void TestLastUsed()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            SetValues();
            _previewButton.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            Assert.IsNotNull(jobAd.LogoId);

            // Creating a new job ad should show the last used job ad.

            Get(GetJobAdUrl(null));
            Assert.IsTrue(_logoId.IsVisible);
            Assert.AreEqual(jobAd.LogoId.Value.ToString(), _logoId.Text);
        }

        [TestMethod]
        public void TestUploadPreview()
        {
            TestUpload(true, _previewButton);
        }

        [TestMethod]
        public void TestUploadSave()
        {
            TestUpload(false, _saveButton);
        }

        [TestMethod]
        public void TestUploadWrongExtension()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\Complete.doc", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            SetValues();
            _previewButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The extension 'doc' is not supported.");

            Assert.IsFalse(_logoId.IsVisible);
            Assert.AreEqual("", _logo.FilePath);

            Assert.AreEqual(DefaultTitle, _titleTextBox.Text);
            Assert.AreEqual(DefaultContent, _contentTextBox.Text.Trim());
            Assert.IsTrue(_industryIdsListBox.SelectedValues.CollectionEqual(new[] { _accounting.Id.ToString() }));
            Assert.IsTrue(_fullTimeCheckBox.IsChecked);
        }

        [TestMethod]
        public void TestUploadOtherError()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            SetValues();
            _titleTextBox.Text = "";
            _previewButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The title is required.");

            Assert.IsTrue(_logoId.IsVisible);
            Assert.AreEqual("", _logo.FilePath);

            Assert.AreEqual("", _titleTextBox.Text);
            Assert.AreEqual(DefaultContent, _contentTextBox.Text.Trim());
            Assert.IsTrue(_industryIdsListBox.SelectedValues.CollectionEqual(new[] { _accounting.Id.ToString() }));
            Assert.IsTrue(_fullTimeCheckBox.IsChecked);

            // The file itself should still be saved.

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            Assert.AreEqual(0, jobAdIds.Count);

            var logoId = new Guid(_logoId.Text);
            AssertFile(logoId, filePath);
        }

        [TestMethod]
        public void TestRemoveLogo()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            // Need to generate an error.

            SetValues();
            _titleTextBox.Text = "";
            _previewButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The title is required.");

            // Removing is effectively ...

            _logoId.Text = "";
            _titleTextBox.Text = DefaultTitle;
            _previewButton.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            Assert.IsNull(jobAd.LogoId);
        }

        [TestMethod]
        public void TestReplaceLogo()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            // Need to generate an error.

            SetValues();
            _titleTextBox.Text = "";
            _previewButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(null));
            AssertErrorMessage("The title is required.");

            // Replacing is effectively ...

            _logoId.Text = "";
            filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.png", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;
            _titleTextBox.Text = DefaultTitle;
            _previewButton.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            Assert.IsNotNull(jobAd.LogoId);
            AssertFile(jobAd.LogoId.Value, filePath);
        }

        private void TestUpload(bool isPreview, HtmlButtonTester button)
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            Get(GetJobAdUrl(null));
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;

            SetValues();
            button.Click();

            var jobAdIds = _jobAdsQuery.GetJobAdIds(employer.Id, JobAdStatus.Draft);
            AssertUrlWithoutQuery(isPreview ? GetPreviewUrl(jobAdIds[0]) : GetJobAdUrl(jobAdIds[0]));

            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdIds[0]);
            Assert.IsNotNull(jobAd.LogoId);
            AssertFile(jobAd.LogoId.Value, filePath);
        }

        private void AssertFile(Guid fileId, string filePath)
        {
            var file = _filesQuery.GetFileReference(fileId);
            var extension = Path.GetExtension(filePath);
            Assert.IsNotNull(file);
            Assert.AreEqual(Path.GetFileName(filePath), file.FileName);
            Assert.AreEqual(extension == ".jpg" ? "image/jpeg" : "image/png", file.MediaType);
            Assert.AreEqual(extension, file.FileData.FileExtension);
            Assert.AreEqual(FileType.CompanyLogo, file.FileData.FileType);
        }

        private void SetValues()
        {
            _titleTextBox.Text = DefaultTitle;
            _contentTextBox.Text = DefaultContent;
            _industryIdsListBox.SelectedValues = new[] { _accounting.Id.ToString() };
            _fullTimeCheckBox.IsChecked = true;
            _locationTextBox.Text = DefaultLocation;
        }
    }
}
