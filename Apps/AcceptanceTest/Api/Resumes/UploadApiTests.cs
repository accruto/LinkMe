using System;
using System.IO;
using System.Web.Script.Serialization;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Resumes
{
    [TestClass]
    public class UploadApiTests
        : ResumesApiTests
    {
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        [TestMethod]
        public void TestUpload()
        {
            const string file = @"Test\Data\Resumes\ProfessionalResume.doc";
            var fileName = Path.GetFileName(file);
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertModel(fileName, model);
            AssertFile(fileName, "application/msword", model.Id);
        }

        [TestMethod]
        public void TestUploadInvalidExtension()
        {
            const string file = @"Test\Data\Resumes\ProfessionalResume.xyz";
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertJsonError(model, null, "The resume extension '.xyz' is not supported.");
        }

        [TestMethod]
        public void TestUploadTooLarge()
        {
            const string file = @"Test\Data\Resumes\TooLarge.doc";
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertJsonError(model, null, "The size of the file exceeds the maximum allowed of 2MB.");
        }

        [TestMethod]
        public void TestUploadNoFile()
        {
            var response = Post(_uploadUrl);
            var model = new JavaScriptSerializer().Deserialize<JsonResumeModel>(response);
            AssertJsonError(model, "File", "The file is required.");
        }

        private void AssertFile(string expectedFileName, string expectedMimeType, Guid fileReferenceId)
        {
            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(expectedFileName, fileReference.FileName);
            Assert.AreEqual(expectedMimeType, fileReference.MediaType);
            Assert.AreEqual(Path.GetExtension(expectedFileName), fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.Resume, fileReference.FileData.FileType);
        }

        private static void AssertModel(string expectedFileName, JsonResumeModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedFileName, model.Name);
        }
    }
}