using System.IO;
using LinkMe.Domain.Files;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Candidates.Resumes
{
    [TestClass]
    public class ValidateResumeFilesTests
        : TestClass
    {
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();

        [TestMethod, ExpectedException(typeof(InvalidResumeExtensionException))]
        public void TestNoFileName()
        {
            _candidateResumeFilesCommand.ValidateFile(null, null);
        }

        [TestMethod, ExpectedException(typeof(InvalidResumeExtensionException))]
        public void TestNoExtension()
        {
            _candidateResumeFilesCommand.ValidateFile("filename", null);
        }

        [TestMethod, ExpectedException(typeof(InvalidResumeExtensionException))]
        public void TestUnknownExtension()
        {
            _candidateResumeFilesCommand.ValidateFile("filename.xyz", null);
        }

        [TestMethod, ExpectedException(typeof(FileTooLargeException))]
        public void TestTooLargeFile()
        {
            using (var stream = new MemoryStream())
            {
                // Mote than 2 MB.

                var buffer = new byte[3 * 1024 * 1024];
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                _candidateResumeFilesCommand.ValidateFile("filename.doc", new StreamFileContents(stream));
            }
        }
    }
}
