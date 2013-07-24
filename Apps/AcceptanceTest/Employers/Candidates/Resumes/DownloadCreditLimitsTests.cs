using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Resumes
{
    [TestClass]
    public class DownloadCreditLimitsTests
        : ActionCreditLimitsTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private ReadOnlyUrl _downloadUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _downloadUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/download");
        }

        protected override Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        protected override MemberAccessReason GetReason()
        {
            return MemberAccessReason.ResumeDownloaded;
        }

        protected override void PerformAction(Employer employer, Member member)
        {
            Get(GetDownloadUrl(new[] {member}));
            AssertPageContains("Please call LinkMe on 1800 546-563 to contact additional candidates.");
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
