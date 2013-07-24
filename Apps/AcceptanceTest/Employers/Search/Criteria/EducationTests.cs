using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class EducationTests
        : CriteriaTests
    {
        private const string School1 = "aaaa";
        private const string School2 = "bbbb";

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.EducationKeywords = null;
            TestDisplay(criteria);
            criteria.EducationKeywords = School1;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestEducation()
        {
            // Create members.

            var member0 = CreateMember(0, School1);
            var member1 = CreateMember(1, School2);
            var member2 = CreateMember(2, School2);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no education.

            criteria.EducationKeywords = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // School 1.

            criteria.EducationKeywords = School1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);

            // School 2.

            criteria.EducationKeywords = School2;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2);
        }

        [TestMethod]
        public void TestDeleteEducation()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Schools = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do a search, should not be returned because does not have any education.

            criteria.EducationKeywords = School1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Add some education, so should be returned.

            resume.Schools = new List<School>
            {
                new School
                {
                    Institution = School1,
                    Description = School1,
                    CompletionDate = new PartialCompletionDate(new PartialDate(2006, 1, 1))
                }
            };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            // Remove the education, should no longer be returned.

            resume.Schools = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            Get(GetPartialSearchUrl(criteria));
            AssertMembers();
        }

        [TestMethod]
        public void TestUpdateEducation()
        {
            var member = CreateMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Schools = null;
            _candidateResumesCommand.UpdateResume(candidate, resume);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do a search, should not be returned because does not have any education.

            criteria.EducationKeywords = School1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            criteria.EducationKeywords = School2;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Add some education, so should be returned.

            resume.Schools = new List<School>
            {
                new School
                {
                    Institution = School1,
                    Description = School1,
                    CompletionDate = new PartialCompletionDate(new PartialDate(2006, 1, 1))
                }
            };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            criteria.EducationKeywords = School1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);

            criteria.EducationKeywords = School2;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // Updated the education.

            resume.Schools = new List<School>
            {
                new School
                {
                    Institution = School2,
                    Description = School2,
                    CompletionDate = new PartialCompletionDate(new PartialDate(2006, 1, 1))
                }
            };
            _candidateResumesCommand.UpdateResume(candidate, resume);

            criteria.EducationKeywords = School1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            criteria.EducationKeywords = School2;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member);
        }

        private Member CreateMember(int index, string school)
        {
            var member = CreateMember(index);

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Schools = new List<School>
            {
                new School
                {
                    Institution = school,
                    Description = school,
                    CompletionDate = new PartialCompletionDate(new PartialDate(2006, 1, 1))
                }
            };

            _candidateResumesCommand.UpdateResume(candidate, resume);
            return member;
        }
    }
}