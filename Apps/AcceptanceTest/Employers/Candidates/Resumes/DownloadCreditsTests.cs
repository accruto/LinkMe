using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Ionic.Zip;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class DownloadCreditsTests
        : ActionCreditsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private ReadOnlyUrl _downloadUrl;
        private ReadOnlyUrl _loginUrl;
        private string _contentType;

        [TestInitialize]
        public void TestInitialize()
        {
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/download");
            _loginUrl = new ReadOnlyApplicationUrl(true, "~/employers/login");

            _contentType = null;
        }

        protected override Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected override MemberAccessReason? PerformAction(bool isApplicant, CreditInfo creditInfo, bool isLoggedIn, Employer employer, Member[] members)
        {
            var url = GetDownloadUrl(members);
            if (!isLoggedIn)
            {
                Get(url);
                Assert.AreEqual(_loginUrl.Path.ToLower(), Browser.CurrentUrl.AbsolutePath.ToLower());
                return null;
            }

            if (isApplicant || ((creditInfo.CanContact || creditInfo.HasUsedCredit) && !creditInfo.HasExpired))
            {
                using (var response = Download(url))
                {
                    return AssertFile(response, employer, members);
                }
            }

            Get(url);
            AssertPageContains("You need " + (members.Length == 1 ? "1 credit" : members.Length + " credits") + " to perform this action but you have none available.");
            return null;
        }

        private Stream Download(ReadOnlyUrl url)
        {
            // Create the request, copying appropriate cookies.

            var webRequest = (HttpWebRequest)WebRequest.Create(url.AbsoluteUri);
            webRequest.Method = "GET";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
            webRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(Browser.Cookies.GetCookies(new Uri(url.AbsoluteUri)));

            // Check the response.

            using (var webResponse = webRequest.GetResponse())
            {
                var responseStream = webResponse.GetResponseStream();
                var stream = new MemoryStream();
                var buffer = new byte[65536];
                int read;
                do
                {
                    read = responseStream.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, read);
                } while (read != 0);
                stream.Seek(0, SeekOrigin.Begin);

                _contentType = webResponse.Headers["Content-Type"];
                return stream;
            }
        }

        private MemberAccessReason? AssertFile(Stream stream, IEmployer employer, Member[] members)
        {
            return members.Length == 1
                ? AssertDocFile(stream, employer, members[0])
                : AssertZipFile(stream, employer, members);
        }

        private MemberAccessReason? AssertZipFile(Stream stream, IEmployer employer, ICollection<Member> members)
        {
            Assert.AreEqual("application/zip", _contentType);

            using (var zipFile = ZipFile.Read(stream))
            {
                Assert.AreEqual(members.Count, zipFile.Entries.Count);
                foreach (var member in members)
                {
                    var entry = zipFile[member.FullName + ".doc"];
                    using (var entryStream = new MemoryStream())
                    {
                        entry.Extract(entryStream);
                        entryStream.Position = 0;
                        AssertContents(entryStream, employer, member);
                    }
                }
            }

            return MemberAccessReason.ResumeDownloaded;
        }

        private MemberAccessReason? AssertDocFile(Stream stream, IEmployer employer, Member member)
        {
            Assert.AreEqual("application/msword", _contentType);
            AssertContents(stream, employer, member);
            return MemberAccessReason.ResumeDownloaded;
        }

        private void AssertContents(Stream stream, IEmployer employer, Member member)
        {
            string contents;
            using (var reader = new StreamReader(stream))
            {
                contents = reader.ReadToEnd();
            }

            // Check got resume file.

            var resumeFile = GetResumeFile(employer, member);
            Assert.AreEqual(resumeFile.Contents, contents);
        }

        private ResumeFile GetResumeFile(IEmployer employer, Member member)
        {
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = candidate.ResumeId == null ? null : _resumesQuery.GetResume(candidate.ResumeId.Value);
            return _resumeFilesQuery.GetResumeFile(_employerMemberViewsQuery.GetEmployerMemberView(employer, member), resume);
        }

        private ReadOnlyUrl GetDownloadUrl(IEnumerable<Member> members)
        {
            var downloadUrl = _downloadUrl.AsNonReadOnly();
            if (members != null)
            {
                foreach (var member in members)
                    downloadUrl.QueryString.Add("candidateId", member.Id.ToString());
            }

            return downloadUrl;
        }
    }
}
