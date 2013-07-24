using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Environment;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Resumes
{
    [TestClass]
    public class UploadTests
        : ResumesTests
    {
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();
        private readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();

        private ReadOnlyUrl _uploadUrl;
        private ReadOnlyUrl _parseUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _uploadUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/upload");
            _parseUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/parse");
        }

        [TestMethod]
        public void TestAuthorisation()
        {
            // Upload.

            const string file = @"Test\Data\Resumes\ProfessionalResume.doc";
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));
            AssertJsonError(model, null, "100", "The user is not logged in.");

            // Parse.

            AssertJsonError(Parse(HttpStatusCode.Forbidden, Guid.NewGuid()), null, "100", "The user is not logged in.");
        }

        [TestMethod]
        public void TestUpload()
        {
            var member = CreateMember(0);
            LogIn(member);

            const string file = @"Test\Data\Resumes\ProfessionalResume.doc";
            var fileName = Path.GetFileName(file);
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertModel(fileName, model);
            AssertResumeFile(member.Id, model.Id);
            AssertFile(fileName, "application/msword", model.Id);
        }

        [TestMethod]
        public void TestUploadInvalidExtension()
        {
            var member = CreateMember(0);
            LogIn(member);

            const string file = @"Test\Data\Resumes\ProfessionalResume.xyz";
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertJsonError(model, null, "The resume extension '.xyz' is not supported.");
        }

        [TestMethod]
        public void TestUploadTooLargeFile()
        {
            var member = CreateMember(0);
            LogIn(member);

            const string file = @"Test\Data\Resumes\TooLarge.doc";
            var model = Upload(FileSystem.GetAbsolutePath(file, RuntimeEnvironment.GetSourceFolder()));

            AssertJsonError(model, null, "The size of the file exceeds the maximum allowed of 2MB.");
        }

        [TestMethod]
        public void TestUploadNoFile()
        {
            var member = CreateMember(0);
            LogIn(member);

            var model = Upload(null);
            AssertJsonError(model, "File", "The file is required.");
        }

        [TestMethod]
        public void TestParse()
        {
            TestParse(TestResume.Complete, "Resume.doc");
        }

        [TestMethod]
        public void TestParseDocx()
        {
            TestParse(TestResume.Complete, "Resume.docx");
        }

        [TestMethod]
        public void TestOverrideParse()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume1 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume1);

            Assert.AreEqual(0, _candidateResumeFilesQuery.GetResumeFiles(member.Id).Count);
            Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resume1.Id));

            LogIn(member);

            // Upload and parse.

            var fileReferenceId1 = Upload(TestResume.Complete, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId1));

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume2 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume2);
            Assert.AreEqual(resume1.Id, resume2.Id);

            // Show that the resume is associated with the uploaded file.

            var references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(1, references.Count);
            var resumeFileReference1 = references[0];
            var resumeFileReference2 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            var resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(resume2.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference2.Id);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);

            // Upload and parse another resume.

            var fileReferenceId2 = Upload(TestResume.NoPhoneNumber, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId2));

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume3 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume3);
            Assert.AreEqual(resume1.Id, resume2.Id);
            Assert.AreEqual(resume1.Id, resume3.Id);

            // Show that the resume is associated with the new uploaded file.

            references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(2, references.Count);
            resumeFileReference1 = (from r in references where r.FileReferenceId == fileReferenceId1 select r).Single();
            resumeFileReference2 = (from r in references where r.FileReferenceId == fileReferenceId2 select r).Single();

            resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            var resumeFileReference4 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId2);

            var resumeFileReference5 = _candidateResumeFilesQuery.GetResumeFile(resume3.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference4.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference5.FileReferenceId);

            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);
            Assert.AreEqual(resumeFileReference2.Id, resumeFileReference4.Id);
            Assert.AreEqual(resumeFileReference2.Id, resumeFileReference5.Id);
        }

        [TestMethod]
        public void TestOverrideParseWithSameFile()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume1 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume1);

            Assert.AreEqual(0, _candidateResumeFilesQuery.GetResumeFiles(member.Id).Count);
            Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resume1.Id));

            LogIn(member);

            // Upload and parse.

            var fileReferenceId1 = Upload(TestResume.Complete, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId1));

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume2 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume2);
            Assert.AreEqual(resume1.Id, resume2.Id);

            // Show that the resume is associated with the uploaded file.

            var references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(1, references.Count);
            var resumeFileReference1 = references[0];
            var resumeFileReference2 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            var resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(resume2.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference2.Id);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);

            // Upload and parse another resume.

            var fileReferenceId2 = Upload(TestResume.Complete, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId2));
            Assert.AreEqual(fileReferenceId1, fileReferenceId2);

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume3 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume3);
            Assert.AreEqual(resume1.Id, resume2.Id);
            Assert.AreEqual(resume1.Id, resume3.Id);

            // Show that the resume is associated with the new uploaded file.

            references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(1, references.Count);
            resumeFileReference1 = references[0];
            resumeFileReference2 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(resume3.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);

            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference2.Id);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);
        }

        [TestMethod]
        public void TestOverrideParseWithApplication()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume1 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume1);

            Assert.AreEqual(0, _candidateResumeFilesQuery.GetResumeFiles(member.Id).Count);
            Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resume1.Id));

            // Apply for job ad with that resume.

            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var application = new InternalApplication { ApplicantId = member.Id, ResumeId = resume1.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            LogIn(member);

            // Upload and parse.

            var fileReferenceId1 = Upload(TestResume.Complete, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId1));

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume2 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume2);
            Assert.AreEqual(resume1.Id, resume2.Id);

            // Show that the resume is associated with the uploaded file.

            var references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(1, references.Count);
            var resumeFileReference1 = references[0];
            var resumeFileReference2 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            var resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(resume2.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference2.Id);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);

            // Upload and parse another resume.

            var fileReferenceId2 = Upload(TestResume.NoPhoneNumber, "Resume.doc");
            AssertJsonSuccess(Parse(fileReferenceId2));

            // Confirm resume.

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume3 = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume3);
            Assert.AreEqual(resume1.Id, resume2.Id);
            Assert.AreEqual(resume1.Id, resume3.Id);

            // Show that the resume is associated with the new uploaded file.

            references = _candidateResumeFilesQuery.GetResumeFiles(member.Id);
            Assert.AreEqual(2, references.Count);
            resumeFileReference1 = (from r in references where r.FileReferenceId == fileReferenceId1 select r).Single();
            resumeFileReference2 = (from r in references where r.FileReferenceId == fileReferenceId2 select r).Single();

            resumeFileReference3 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId1);
            var resumeFileReference4 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId2);

            var resumeFileReference5 = _candidateResumeFilesQuery.GetResumeFile(resume3.Id);

            Assert.AreEqual(fileReferenceId1, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(fileReferenceId1, resumeFileReference3.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference4.FileReferenceId);
            Assert.AreEqual(fileReferenceId2, resumeFileReference5.FileReferenceId);

            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference3.Id);
            Assert.AreEqual(resumeFileReference2.Id, resumeFileReference4.Id);
            Assert.AreEqual(resumeFileReference2.Id, resumeFileReference5.Id);
        }

        [TestMethod]
        public void TestInvalidResume()
        {
            var member = CreateMember(0);
            LogIn(member);

            var fileReferenceId = Upload(TestResume.Invalid, "Resume.doc");
            var model = Parse(fileReferenceId);
            AssertJsonError(model, null, "Our system is unable to extract your profile information from this file. Please try another document or create your profile manually.");
        }

        [TestMethod]
        public void TestUnknownFileReferenceId()
        {
            var member = CreateMember(0);
            LogIn(member);

            var model = Parse(HttpStatusCode.NotFound, Guid.NewGuid());
            AssertJsonError(model, null, "400", "The file cannot be found.");
        }

        private void TestParse(TestResume testResume, string fileName)
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNull(candidate.ResumeId);

            LogIn(member);

            // Upload and parse.

            var fileReferenceId = Upload(testResume, fileName);
            var model = Parse(fileReferenceId);

            // Assert.

            AssertJsonSuccess(model);
            AssertResumeFile(member.Id, fileReferenceId);

            candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNotNull(candidate.ResumeId);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            Assert.IsNotNull(resume);

            // Show that the resume is associated with the uploaded file.

            var resumeFileReference1 = _candidateResumeFilesQuery.GetResumeFile(member.Id, fileReferenceId);
            var resumeFileReference2 = _candidateResumeFilesQuery.GetResumeFile(resume.Id);

            Assert.AreEqual(fileReferenceId, resumeFileReference1.FileReferenceId);
            Assert.AreEqual(fileReferenceId, resumeFileReference2.FileReferenceId);
            Assert.AreEqual(resumeFileReference1.Id, resumeFileReference2.Id);
        }

        private JsonResumeFileModel Upload(string file)
        {
            var files = file == null
                ? new NameValueCollection()
                : new NameValueCollection { { "file", file } };
            var response = Post(_uploadUrl, null, files);
            return new JavaScriptSerializer().Deserialize<JsonResumeFileModel>(response);
        }

        private Guid Upload(TestResume resume, string fileName)
        {
            using (var files = _filesCommand.SaveTempFile(resume.GetData(), fileName))
            {
                var path = files.FilePaths[0];
                return Upload(path).Id;
            }
        }

        private JsonProfileModel Parse(Guid fileReferenceId)
        {
            var response = Post(_parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } });
            return new JavaScriptSerializer().Deserialize<JsonProfileModel>(response);
        }

        private JsonProfileModel Parse(HttpStatusCode expectedStatusCode, Guid fileReferenceId)
        {
            var response = Post(expectedStatusCode, _parseUrl, new NameValueCollection { { "fileReferenceId", fileReferenceId.ToString() } });
            return new JavaScriptSerializer().Deserialize<JsonProfileModel>(response);
        }

        private void AssertResumeFile(Guid memberId, Guid fileReferenceId)
        {
            var references = _candidateResumeFilesQuery.GetResumeFiles(memberId);
            Assert.AreEqual(1, references.Count);
            Assert.AreEqual(fileReferenceId, references[0].FileReferenceId);
        }

        private void AssertFile(string expectedFileName, string expectedMimeType, Guid fileReferenceId)
        {
            var fileReference = _filesQuery.GetFileReference(fileReferenceId);
            Assert.AreEqual(expectedFileName, fileReference.FileName);
            Assert.AreEqual(expectedMimeType, fileReference.MediaType);
            Assert.AreEqual(Path.GetExtension(expectedFileName), fileReference.FileData.FileExtension);
            Assert.AreEqual(FileType.Resume, fileReference.FileData.FileType);
        }

        private static void AssertModel(string expectedFileName, JsonResumeFileModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(expectedFileName, model.Name);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }
    }
}
