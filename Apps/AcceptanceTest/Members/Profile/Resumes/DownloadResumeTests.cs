using System;
using System.IO;
using System.Linq;
using System.Net;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Resumes
{
    [TestClass]
    public class DownloadResumeTests
        : ResumesTests
    {
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();

        private ReadOnlyUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        [TestMethod]
        public void TestAuthorisation()
        {
            var url = GetDownloadUrl(Guid.NewGuid());
            Get(url);
            var loginUrl = _loginUrl.AsNonReadOnly();
            loginUrl.QueryString.Add("returnUrl", url.Path);
            AssertUrl(loginUrl);
        }

        [TestMethod]
        public void TestUnknownResume()
        {
            var member = CreateMember(0);
            LogIn(member);
            Get(HttpStatusCode.NotFound, GetDownloadUrl(Guid.NewGuid()));
        }

        [TestMethod]
        public void TestDownload()
        {
            var member = CreateMember(0);
            var fileReference = GetResumeFile(TestResume.Complete);
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });

            LogIn(member);
            using (var response = Download(GetDownloadUrl(fileReference.Id)))
            {
                AssertFile(response, TestResume.Complete);
            }
        }

        [TestMethod]
        public void TestDownloadOtherMembersResume()
        {
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            var fileReference = GetResumeFile(TestResume.Complete);
            _candidateResumeFilesCommand.CreateResumeFile(member0.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });

            LogIn(member0);
            using (var response = Download(GetDownloadUrl(fileReference.Id)))
            {
                AssertFile(response, TestResume.Complete);
            }

            LogOut();
            LogIn(member1);
            Get(HttpStatusCode.NotFound, GetDownloadUrl(fileReference.Id));
        }

        private static ReadOnlyUrl GetDownloadUrl(Guid fileReferenceId)
        {
            return new ReadOnlyApplicationUrl("~/members/profile/resumes/" + fileReferenceId + "/download");
        }

        private void AssertFile(Stream stream, TestResume testResume)
        {
            Assert.AreEqual("application/msword", GetContentType());

            var contents = new byte[0];
            using (var reader = new BinaryReader(stream))
            {
                var buffer = new byte[1024];
                var read = reader.Read(buffer, 0, 1024);
                while (read != 0)
                {
                    contents = contents.Concat(buffer.Take(read)).ToArray();
                    read = reader.Read(buffer, 0, 1024);
                }
            }

            // Check got resume file.

            Assert.IsTrue(testResume.GetData().SequenceEqual(contents));
        }

        private FileReference GetResumeFile(TestResume testResume)
        {
            const string fileName = "resume.doc";
            var data = testResume.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }
    }
}
