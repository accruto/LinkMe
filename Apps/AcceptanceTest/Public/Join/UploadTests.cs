using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Web.Script.Serialization;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Web.Areas.Api.Models.Resumes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Public.Join
{
    [TestClass]
    public class UploadTests
        : JoinTests
    {
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();

        private const string NewJobTitle = "Beekeeper";

        [TestMethod]
        public void TestCompleteResume()
        {
            Test(TestResume.Complete, "Resume.doc");
        }

        [TestMethod]
        public void TestCompleteResumeDocx()
        {
            Test(TestResume.Complete, "Resume.docx");
        }

        [TestMethod]
        public void TestInvalidResume()
        {
            Test(TestResume.Invalid, "Resume.doc");
        }

        [TestMethod]
        public void TestEmptyResume()
        {
            Test(TestResume.Empty, "Resume.doc");
        }

        [TestMethod]
        public void TestUnavailableResume()
        {
            Test(TestResume.Unavailable, "Resume.doc");
        }

        [TestMethod]
        public void TestNoPhoneNumberResume()
        {
            Test(TestResume.NoPhoneNumber, "Resume.doc");
        }

        private void Test(TestResume testResume, string fileName)
        {
            // Upload the file.

            var fileReferenceId = Upload(testResume, fileName);

            // Parse the resume.

            var parsedResumeId = Parse(fileReferenceId, testResume);

            // Join.

            Get(GetJoinUrl());
            SubmitJoin(fileReferenceId, parsedResumeId);
            var instanceId = GetInstanceId();

            var member = CreateMember(FirstName, LastName, EmailAddress);
            UpdateMember(member, MobilePhoneNumber, PhoneNumberType.Mobile, Location);
            UpdateMember(member, Gender, DateOfBirth);
            var candidate = CreateCandidate();
            UpdateCandidate(candidate, SalaryLowerBound, SalaryRate);
            SubmitPersonalDetails(instanceId, member, candidate, Password);

            // Make sure a resume is created.

            var resume = CreateResume();
            if (testResume != TestResume.Complete)
                resume.Jobs = new List<Job> { new Job { Title = NewJobTitle } };
            SubmitJobDetails(instanceId, member, candidate, resume, false, null, parsedResumeId != null);

            // Assert.

            AssertCandidateResume(member.GetBestEmailAddress().Address, fileName, fileReferenceId, parsedResumeId);
        }

        private Guid Upload(TestResume resume, string fileName)
        {
            using (var files = _filesCommand.SaveTempFile(resume.GetData(), fileName))
            {
                var path = files.FilePaths[0];
                return Upload(path);
            }
        }

        private void AssertCandidateResume(string emailAddress, string expectedFileName, Guid? fileReferenceId, Guid? parsedResumeId)
        {
            var memberId = _loginCredentialsQuery.GetUserId(emailAddress);
            var candidate = _candidatesQuery.GetCandidate(memberId.Value);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);

            // FileReference should always be created.

            Assert.IsNotNull(fileReferenceId);
            AssertFileReference(expectedFileName, fileReferenceId.Value);

            // Even if the resume was not parsed the file should still be saved against the member.

            var resumeFile1 = _candidateResumeFilesQuery.GetResumeFile(memberId.Value, fileReferenceId.Value);
            AssertResumeFileReference(fileReferenceId.Value, resumeFile1);

            if (parsedResumeId != null)
            {
                // Parsed with no errors so the resume should be associated with the file reference.

                var resumeFile2 = _candidateResumeFilesQuery.GetResumeFile(resume.Id);
                Assert.AreEqual(resumeFile1.Id, resumeFile2.Id);
                AssertResumeFileReference(fileReferenceId.Value, resumeFile2);
            }
            else
            {
                // There was a problem parsing the resume so the file references should be there, but the resume itself should not be associated with the file.

                Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resume.Id));
            }

            // Check all resume files for the member.

            var resumeFiles = _candidateResumeFilesQuery.GetResumeFiles(memberId.Value);
            Assert.AreEqual(1, resumeFiles.Count);
            Assert.AreEqual(resumeFile1.Id, resumeFiles[0].Id);
            AssertResumeFileReference(fileReferenceId.Value, resumeFiles[0]);
        }

        private static void AssertResumeFileReference(Guid fileReferenceId, ResumeFileReference resumeFileReference)
        {
            Assert.AreEqual(fileReferenceId, resumeFileReference.FileReferenceId);
        }

        private void AssertFileReference(string expectedFileName, Guid fileReferenceId)
        {
            var expectedFileNameExtension = Path.GetExtension(expectedFileName);
            var file = _filesQuery.GetFileReference(fileReferenceId);

            Assert.IsNotNull(file);
            Assert.AreEqual(expectedFileName, file.FileName);
            Assert.AreEqual(expectedFileNameExtension, file.FileData.FileExtension);
            Assert.AreEqual(expectedFileNameExtension == ".doc" ? "application/msword" : "application/octet-stream", file.MediaType);
            Assert.AreEqual(FileType.Resume, file.FileData.FileType);
        }

        private Guid Upload(string file)
        {
            var files = new NameValueCollection { { "file", file } };
            var response = Post(_uploadUrl, null, files);
            return new JavaScriptSerializer().Deserialize<JsonResumeModel>(response).Id;
        }

        private Guid? Parse(Guid? fileReferenceId, TestResume resume)
        {
            var response = Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } });
            var model = new JavaScriptSerializer().Deserialize<JsonParsedResumeModel>(response);

            if (resume == TestResume.Invalid || resume == TestResume.Empty)
            {
                AssertJsonError(model, null, "Our system is unable to extract your profile information from this file. Please try another document or create your profile manually.");
                return null;
            }

            if (resume == TestResume.Unavailable)
            {
                AssertJsonError(model, null, "Our system is unable to extract your profile information from this file at this time. Please try again later or create your profile manually. We apologise for the inconvenience.");
                return null;
            }

            AssertJsonSuccess(model);
            return model.Id;
        }
    }
}
