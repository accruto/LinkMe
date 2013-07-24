using System;
using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Images;
using LinkMe.Domain.Users.Members.Photos.Commands;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Photos
{
    [TestClass]
    public class MemberPhotosTests
        : TestClass
    {
        private readonly IMemberPhotosCommand _memberPhotosCommand = Resolve<IMemberPhotosCommand>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();

        [TestMethod]
        public void TestSave()
        {
            Guid fileReferenceId;

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.jpg", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                fileReferenceId = _memberPhotosCommand.SavePhoto(new StreamFileContents(stream), filePath).Id;
            }

            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(Path.GetFileName(filePath), fileReference.FileName);
            Assert.AreEqual("image/jpeg", fileReference.MediaType);
            Assert.AreEqual(".jpg", fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.ProfilePhoto, fileReference.FileData.FileType);
        }

        [TestMethod]
        public void TestSavePng()
        {
            Guid fileReferenceId;

            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Photos\ProfilePhoto.png", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                fileReferenceId = _memberPhotosCommand.SavePhoto(new StreamFileContents(stream), filePath).Id;
            }

            // All photos get converted to jpg for storage.

            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(Path.ChangeExtension(Path.GetFileName(filePath), ".jpg"), fileReference.FileName);
            Assert.AreEqual("image/jpeg", fileReference.MediaType);
            Assert.AreEqual(".jpg", fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.ProfilePhoto, fileReference.FileData.FileType);
        }

        [TestMethod, ExpectedException(typeof(InvalidImageExtensionException))]
        public void TestInvalidExtension()
        {
            var filePath = FileSystem.GetAbsolutePath(@"Test\Data\Resumes\ProfessionalResume.doc", RuntimeEnvironment.GetSourceFolder());
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                _memberPhotosCommand.SavePhoto(new StreamFileContents(stream), filePath);
            }
        }
    }
}
