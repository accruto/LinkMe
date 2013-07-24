using System;
using System.IO;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Files.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds
{
    [TestClass]
    public class FilesTests
        : IntegrationTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IFilesQuery _filesQuery = Resolve<IFilesQuery>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();

        private const string OtherUserName = "otherintegrator";
        private const string OtherPassword = "secretpass";

        [TestMethod]
        public void TestNoFileName()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);
            var contents = GetContents(fileReference);

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            Assert.AreEqual(contents, Get(GetFileUrl(fileReference.Id, null), integratorUser, Password, true));
        }

        [TestMethod]
        public void TestFileName()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);
            var contents = GetContents(fileReference);

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            Assert.AreEqual(contents, Get(GetFileUrl(fileReference.Id, "resume.doc"), integratorUser, Password, true));
        }

        [TestMethod]
        public void TestSecurity()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);

            // Incorrect password.

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var response = Get(GetFileUrl(fileReference.Id, null), integratorUser, "badpass", true);
            Assert.AreEqual("Error: Web service authorization failed: the password for user '" + integratorUser.LoginId + "' is incorrect.", response);

            // Integrator with no access to resumes.

            var otherIntegratorUser = _integrationCommand.CreateTestIntegratorUser(OtherUserName, OtherPassword, IntegratorPermissions.GetJobAds | IntegratorPermissions.PostJobAds);
            response = Get(GetFileUrl(fileReference.Id, null), otherIntegratorUser, OtherPassword, true);
            Assert.AreEqual("Error: Web service authorization failed: user 'otherintegrator' does not have permission to access the requested service.", response);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestUnknownFile()
        {
            // No such file.

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var badId = Guid.NewGuid();
            Get(GetFileUrl(badId, "resume.doc"), integratorUser, Password, true);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestUnknownFileNoIntegrator()
        {
            // No such file.

            var badId = Guid.NewGuid();
            Get(GetFileUrl(badId, null));
        }

        private string GetContents(FileReference file)
        {
            using (var stream = _filesQuery.OpenFile(file))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private FileReference GetResumeFile()
        {
            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }

        private void AddResume(Guid memberId, FileReference fileReference)
        {
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);
        }

        private static ReadOnlyUrl GetFileUrl(Guid fileReferenceId, string fileName)
        {
            return new ReadOnlyApplicationUrl(true, "~/file/" + fileReferenceId.ToString("n") + (string.IsNullOrEmpty(fileName) ? "" : "/" + fileName));
        }
    }
}
