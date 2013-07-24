using System;
using System.Net;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Api.Resumes
{
    [TestClass]
    public class ParseApiTests
        : ResumesApiTests
    {
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        [TestMethod]
        public void TestParse()
        {
            var fileReferenceId = Upload(TestResume.Complete);
            var model = Parse(fileReferenceId);
            AssertJsonSuccess(model);
            AssertParsedResume(model.Id.Value);
        }

        [TestMethod]
        public void TestInvalidResume()
        {
            var fileReferenceId = Upload(TestResume.Invalid);
            var model = Parse(fileReferenceId);
            AssertJsonError(model, null, "Our system is unable to extract your profile information from this file. Please try another document or create your profile manually.");
        }

        [TestMethod]
        public void TestUnknownFileReferenceId()
        {
            var model = Parse(HttpStatusCode.NotFound, Guid.NewGuid());
            AssertJsonError(model, null, "400", "The file cannot be found.");
        }

        private Guid Upload(TestResume resume)
        {
            const string fileName = "resume.doc";
            using (var files = _filesCommand.SaveTempFile(resume.GetData(), fileName))
            {
                var path = files.FilePaths[0];
                return Upload(path).Id;
            }
        }

        private void AssertParsedResume(Guid parsedResumeId)
        {
            var parsedResume = _resumesQuery.GetParsedResume(parsedResumeId);
            Assert.IsNotNull(parsedResume);
        }
    }
}