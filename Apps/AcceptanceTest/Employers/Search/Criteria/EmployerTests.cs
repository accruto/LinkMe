using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class EmployerTests
        : CriteriaTests
    {
        private const string Employer1 = "LinkMe";
        private const string Employer2 = "Infosys";

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.CompanyKeywords = null;
            criteria.CompaniesToSearch = JobsToSearch.AllJobs;
            TestDisplay(criteria);

            criteria.CompanyKeywords = Employer1;
            criteria.CompaniesToSearch = JobsToSearch.AllJobs;
            TestDisplay(criteria);

            criteria.CompaniesToSearch = JobsToSearch.RecentJobs;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestEmployer()
        {
            // Create members.

            var member0 = CreateMember(0, Employer1, true);
            var member1 = CreateMember(1, Employer2, true);
            var member2 = CreateMember(2, Employer2, true);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no employer.

            criteria.CompanyKeywords = null;
            criteria.CompaniesToSearch = JobsToSearch.AllJobs;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // Employer 1.

            criteria.CompanyKeywords = Employer1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);

            // Employer 2.

            criteria.CompanyKeywords = Employer2;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2);
        }

        [TestMethod]
        public void TestEmployerJobsToSearch()
        {
            // Create members.

            var member0 = CreateMember(0, Employer1, true);
            var member1 = CreateMember(1, Employer2, false);
            var member2 = CreateMember(2, Employer2, true);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no employer.

            criteria.CompanyKeywords = null;
            criteria.CompaniesToSearch = JobsToSearch.AllJobs;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // Employer 1.

            criteria.CompanyKeywords = Employer1;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);

            // Employer 2, all jobs.

            criteria.CompanyKeywords = Employer2;
            criteria.CompaniesToSearch = JobsToSearch.AllJobs;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2);

            // Employer 2, current jobs.

            criteria.CompanyKeywords = Employer2;
            criteria.CompaniesToSearch = JobsToSearch.LastJob;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member2);

            // Employer 2, recent jobs.

            criteria.CompanyKeywords = Employer2;
            criteria.CompaniesToSearch = JobsToSearch.RecentJobs;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2);
        }

        private Member CreateMember(int index, string company, bool isCurrent)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);

            resume.Jobs = new List<Job>
                              {
                                  new Job
                                      {
                                          Company = "Something",
                                          Title = BusinessAnalyst,
                                          Dates = new PartialDateRange(new PartialDate(2003, 1, 1))
                                      },
                                  new Job
                                      {
                                          Company = company,
                                          Title = company + " " + BusinessAnalyst,
                                          Dates = isCurrent
                                            ? new PartialDateRange(new PartialDate(2005, 1, 1))
                                            : new PartialDateRange(new PartialDate(2005, 1, 1), new PartialDate(2006, 1, 1)),
                                      }
                              };

            _candidateResumesCommand.UpdateResume(candidate, resume);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}