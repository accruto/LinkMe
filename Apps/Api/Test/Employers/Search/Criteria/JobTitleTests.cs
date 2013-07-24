using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class JobTitleTests
        : SearchTests
    {
        private const string Archeologist = "Archeologist";

        [TestMethod]
        public void TestJobTitle()
        {
            var member0 = CreateMember(0, BusinessAnalyst);
            var member1 = CreateMember(1, Archeologist);

            var criteria = new MemberSearchCriteria { JobTitle = BusinessAnalyst };
            var model = Search(criteria);
            AssertMembers(model, member0);

            criteria.JobTitle = Archeologist;
            model = Search(criteria);
            AssertMembers(model, member1);
        }

        private Member CreateMember(int index, string jobTitle)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);

            resume.Jobs = new List<Job> { resume.Jobs[0] };
            resume.Jobs[0].Title = jobTitle;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            _memberSearchService.UpdateMember(member.Id);

            return member;
        }
    }
}
