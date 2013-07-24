using LinkMe.Apps.Api.Areas.Employers.Models.Candidates;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Results
{
    [TestClass]
    public class JobsTests
        : SearchTests
    {
        private const string Title = "My job title";

        [TestMethod]
        public void TestNone()
        {
            CreateMember(BusinessAnalyst);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertJobs(model);
        }

        [TestMethod]
        public void TestNoDates()
        {
            var job = new Job { Title = Title };
            CreateMember(BusinessAnalyst, job);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertJobs(model, job);
        }

        [TestMethod]
        public void TestStartDateYearMonth()
        {
            var job = new Job { Title = Title, Dates = new PartialDateRange(new PartialDate(1997, 5)) };
            CreateMember(BusinessAnalyst, job);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertJobs(model, job);
        }

        [TestMethod]
        public void TestStartDateYear()
        {
            var job = new Job { Title = Title, Dates = new PartialDateRange(new PartialDate(1997)) };
            CreateMember(BusinessAnalyst, job);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertJobs(model, job);
        }

        [TestMethod]
        public void TestBothDates()
        {
            var job = new Job { Title = Title, Dates = new PartialDateRange(new PartialDate(1997, 5), new PartialDate(2001, 7)) };
            CreateMember(BusinessAnalyst, job);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            var model = Search(criteria);
            AssertJobs(model, job);
        }

        private void CreateMember(string summary, params Job[] jobs)
        {
            var member = CreateMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            resume.Jobs = jobs;
            resume.Summary = summary;
            _candidateResumesCommand.UpdateResume(candidate, resume);
            _memberSearchService.UpdateMember(member.Id);
        }

        private static void AssertJobs(CandidatesResponseModel model, params Job[] expectedJobs)
        {
            Assert.AreEqual(1, model.Candidates.Count);
            if (expectedJobs.Length == 0)
            {
                Assert.IsNull(model.Candidates[0].Jobs);
            }
            else
            {
                Assert.IsNotNull(model.Candidates[0].Jobs);
                Assert.AreEqual(expectedJobs.Length, model.Candidates[0].Jobs.Count);
                foreach (var expectedJob in expectedJobs)
                    AssertJob(expectedJob, model.Candidates[0].Jobs[0]);
            }
        }

        private static void AssertJob(IJob expectedJob, JobModel model)
        {
            Assert.AreEqual(expectedJob.Title, model.Title);
            Assert.AreEqual(expectedJob.Company, model.Company);
            if (expectedJob.Dates == null)
            {
                Assert.IsNull(model.StartDate);
                Assert.IsNull(model.EndDate);
                Assert.AreEqual(false, model.IsCurrent);
            }
            else
            {
                Assert.AreEqual(expectedJob.Dates.Start, model.StartDate);
                Assert.AreEqual(expectedJob.Dates.End, model.EndDate);
                Assert.AreEqual(expectedJob.Dates.End == null, model.IsCurrent);
            }
        }
    }
}