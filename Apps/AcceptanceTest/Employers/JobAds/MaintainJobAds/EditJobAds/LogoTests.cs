using System;
using System.IO;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.EditJobAds
{
    [TestClass]
    public class LogoTests
        : MaintainJobAdTests
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        private HtmlHiddenTester _logoId;
        private HtmlFileTester _logo;

        [TestInitialize]
        public void TestInitialize()
        {
            _logoId = new HtmlHiddenTester(Browser, "LogoId");
            _logo = new HtmlFileTester(Browser, "Logo");
        }

        [TestMethod]
        public void TestNoLogoDefaults()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            Assert.IsFalse(_logoId.IsVisible);
            Assert.AreEqual("", _logo.FilePath);
        }

        [TestMethod]
        public void TestLogoDefaults()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            AddLogo(jobAd);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            Assert.IsTrue(_logoId.IsVisible);
            Assert.IsNotNull(jobAd.LogoId);
            Assert.AreEqual(jobAd.LogoId.Value.ToString(), _logoId.Text);
        }

        [TestMethod]
        public void TestAddLogo()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            Assert.IsNull(jobAd.LogoId);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;
            _saveButton.Click();

            Assert.IsTrue(_logoId.IsVisible);
            var logoId = new Guid(_logoId.Text);
            AssertFile(logoId, filePath);

            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(logoId, jobAd.LogoId);
            AssertFile(logoId, filePath);
        }

        [TestMethod]
        public void TestRemoveLogo()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            AddLogo(jobAd);
            Assert.IsNotNull(jobAd.LogoId);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            _logoId.Text = "";
            _saveButton.Click();

            Assert.IsFalse(_logoId.IsVisible);
            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.IsNull(jobAd.LogoId);
        }

        [TestMethod]
        public void TestReplaceLogo()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);
            AddLogo(jobAd);
            Assert.IsNotNull(jobAd.LogoId);
            var originalLogoId = jobAd.LogoId;
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            AssertFile(jobAd.LogoId.Value, filePath);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            // Replace.

            filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.png", RuntimeEnvironment.GetSourceFolder());
            _logoId.Text = "";
            _logo.FilePath = filePath;
            _saveButton.Click();

            Assert.IsTrue(_logoId.IsVisible);
            var logoId = new Guid(_logoId.Text);
            AssertFile(logoId, filePath);

            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.AreEqual(logoId, jobAd.LogoId);

            Assert.AreNotEqual(originalLogoId, logoId);
        }

        [TestMethod]
        public void TestUploadWrongExtension()
        {
            var employer = CreateEmployer(null);
            var jobAd = CreateJobAd(employer, JobAdStatus.Draft);

            LogIn(employer);
            Get(GetJobAdUrl(jobAd.Id));

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\Complete.doc", RuntimeEnvironment.GetSourceFolder());
            _logo.FilePath = filePath;
            _saveButton.Click();

            AssertUrlWithoutQuery(GetJobAdUrl(jobAd.Id));
            AssertErrorMessage("The extension 'doc' is not supported.");

            Assert.IsFalse(_logoId.IsVisible);
            Assert.AreEqual("", _logo.FilePath);

            jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id);
            Assert.IsNull(jobAd.LogoId);
        }

        private void AddLogo(JobAd jobAd)
        {
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    // Load the contents from the file.

                    using (var reader = new BinaryReader(File.OpenRead(filePath)))
                    {
                        var count = 4096;
                        var buffer = new byte[count];
                        while ((count = reader.Read(buffer, 0, count)) > 0)
                            writer.Write(buffer, 0, count);
                    }

                    stream.Position = 0;
                    var fileReference = _filesCommand.SaveFile(FileType.CompanyLogo, new StreamFileContents(stream), Path.GetFileName(filePath));

                    jobAd.LogoId = fileReference.Id;
                    _jobAdsCommand.UpdateJobAd(jobAd);
                }
            }
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
    }
}
