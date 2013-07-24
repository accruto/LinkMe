using System;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Images;
using LinkMe.Domain.Users.Employers.Logos.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Logos
{
    [TestClass]
    public class EmployerLogosTests
        : TestClass
    {
        private readonly IEmployerLogosCommand _employerLogosCommand = Resolve<IEmployerLogosCommand>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        [TestMethod]
        public void TestSave()
        {
            Guid fileReferenceId;

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                fileReferenceId = _employerLogosCommand.SaveLogo(new StreamFileContents(stream), filePath).Id;
            }

            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(Path.GetFileName(filePath), fileReference.FileName);
            Assert.AreEqual("image/jpeg", fileReference.MediaType);
            Assert.AreEqual(".jpg", fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.CompanyLogo, fileReference.FileData.FileType);
        }

        [TestMethod]
        public void TestSavePng()
        {
            Guid fileReferenceId;

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.png", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                fileReferenceId = _employerLogosCommand.SaveLogo(new StreamFileContents(stream), filePath).Id;
            }

            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(Path.GetFileName(filePath), fileReference.FileName);
            Assert.AreEqual("image/png", fileReference.MediaType);
            Assert.AreEqual(".png", fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.CompanyLogo, fileReference.FileData.FileType);
        }

        [TestMethod, ExpectedException(typeof(InvalidImageExtensionException))]
        public void TestInvalidExtension()
        {
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\ProfessionalResume.doc", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                _employerLogosCommand.SaveLogo(new StreamFileContents(stream), filePath);
            }
        }
    }
}
