using System.IO;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Resumes
{
    [TestClass]
    public class DownloadTests
        : ResumesTests
    {
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();

        private ReadOnlyUrl _downloadUrl;
        private ReadOnlyUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/download");
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        [TestMethod]
        public void TestAuthorisation()
        {
            Get(_downloadUrl);
            var loginUrl = _loginUrl.AsNonReadOnly();
            loginUrl.QueryString.Add("returnUrl", _downloadUrl.Path);
            AssertUrl(loginUrl);
        }

        [TestMethod]
        public void TestDownload()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            LogIn(member);

            using (var response = Download(_downloadUrl))
            {
                AssertFile(response, member);
            }
        }

        [TestMethod]
        public void TestDownloadNoResume()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            Assert.IsNull(candidate.ResumeId);

            LogIn(member);

            using (var response = Download(_downloadUrl))
            {
                AssertFile(response, member);
            }
        }

        private void AssertFile(Stream stream, Member member)
        {
            Assert.AreEqual("application/msword", GetContentType());

            string contents;
            using (var reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd();
            }

            // Check got resume file.

            var resumeFile = GetResumeFile(member);
            Assert.AreEqual(resumeFile.Contents, contents);
        }

        private ResumeFile GetResumeFile(Member member)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return _resumeFilesQuery.GetResumeFile(member, member, candidate, resume);
        }
    }
}
