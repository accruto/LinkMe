using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.AcceptanceTest.Employers.Search.Results
{
    [TestClass]
    public class ResumeLastUpdatedTests
        : SearchTests
    {
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

        [TestMethod]
        public void TestWithCandidateLastUpdatedTime()
        {
            var candidateLastUpdatedTime = DateTime.Now.AddMonths(-2);
            var resumeLastUpdatedTime = DateTime.Now.AddMonths(-1);
            var member = CreateMember(candidateLastUpdatedTime, resumeLastUpdatedTime);

            // Search.

            AssertLastUpdated(member, resumeLastUpdatedTime);
        }

        [TestMethod]
        public void TestCandidateUpdated()
        {
            var candidateLastUpdatedTime = DateTime.Now.AddMonths(-1);
            var resumeLastUpdatedTime = DateTime.Now.AddMonths(-2);
            var member = CreateMember(candidateLastUpdatedTime, resumeLastUpdatedTime);

            // Search.

            AssertLastUpdated(member, candidateLastUpdatedTime);
        }

        private Member CreateMember(DateTime candidateLastUpdatedTime, DateTime resumeLastUpdatedTime)
        {
            // Create the member.

            var member = CreateMember(0);
            member.CreatedTime = DateTime.Now.AddMonths(-3);
            member.LastUpdatedTime = member.CreatedTime;
            _membersRepository.UpdateMember(member);

            // Update the candidate.

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.LastUpdatedTime = candidateLastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            // Update the resume.

            Assert.IsNotNull(candidate.ResumeId);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            resume.LastUpdatedTime = resumeLastUpdatedTime;
            _resumesRepository.UpdateResume(resume);

            return member;
        }

        private void AssertLastUpdated(Member member, DateTime date)
        {
            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            Get(GetSearchUrl(criteria));
            AssertMembers(member);
            AssertLastUpdated(date);

            // Candidate.

            Get(GetCandidatesUrl(member.Id));
            AssertLastUpdated(date);
            AssertResumeLastUpdated(date);
        }

        private void AssertLastUpdated(DateTime date)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@class='last-updated js_ellipsis']");
            Assert.IsNotNull(node);
            Assert.AreEqual("Updated: " + date.ToString("dd MMM yyyy"), node.InnerText);
        }

        private void AssertResumeLastUpdated(DateTime date)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//tr[@class='last-updated']/td[@class='date']");
            Assert.IsNotNull(node);
            Assert.AreEqual(date.ToString("dd MMM yyyy"), node.InnerText);
        }
    }
}
