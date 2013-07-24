using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class KeywordsTests
        : SearchTests
    {
        private const string Archeologist = "Archeologist";

        [TestMethod]
        public void TestKeywords()
        {
            var member0 = CreateMember(0, BusinessAnalyst);
            var member1 = CreateMember(1, Archeologist);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria);
            AssertMembers(model, member0);

            criteria.SetKeywords(Archeologist);
            model = Search(criteria);
            AssertMembers(model, member1);
        }

        [TestMethod]
        public void TestKeywordsLoggedIn()
        {
            var member0 = CreateMember(0, BusinessAnalyst);
            var member1 = CreateMember(1, Archeologist);

            var employer = CreateEmployer();

            //login
            AssertJsonSuccess(LogIn(employer));

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria);
            AssertMembers(model, member0);

            criteria.SetKeywords(Archeologist);
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
