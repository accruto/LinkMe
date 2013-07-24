using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Members.Models.Profiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Profile.Api
{
    [TestClass]
    public class EmploymentHistoryTests
        : ApiTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private ReadOnlyUrl _employmentHistoryUrl;

        private Industry _accounting;
        private Industry _administration;

        private const string CompanyFormat = "Company {0}";
        private const string TitleFormat = "Title {0}";
        private const string DescriptionFormat = "Description {0}";
        private const string NewTitle = "My new title";
        private const string NewDescription = "My new description";

        [TestInitialize]
        public void TestInitialize()
        {
            _employmentHistoryUrl = new ReadOnlyApplicationUrl(true, "~/members/profile/api/employmenthistory");

            _accounting = _industriesQuery.GetIndustry("Accounting");
            _administration = _industriesQuery.GetIndustry("Administration");
        }

        [TestMethod]
        public void TestRecentProfession()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.RecentProfession = Profession.Strategy;
            var parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            candidate.RecentProfession = Profession.QualityAssurance;
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            candidate.RecentProfession = null;
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestRecentSeniority()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.RecentSeniority = Seniority.Internship;
            var parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            candidate.RecentSeniority = Seniority.MidSenior;
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            candidate.RecentSeniority = null;
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestIndustries()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Set it.

            candidate.Industries = new List<Industry> { _accounting };
            var parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Change it.

            candidate.Industries = new List<Industry> { _accounting, _administration };
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);

            // Reset it.

            candidate.Industries = null;
            parameters = GetParameters(candidate, null);
            AssertModel(false, EmploymentHistory(parameters));
            AssertMember(member, candidate, null, true);
        }

        [TestMethod]
        public void TestAddJob()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Add job with dates.

            var startDate = DateTime.Now.AddMonths(-6).Date;
            var endDate = DateTime.Now.AddMonths(-3).Date;

            var job1 = CreateJob(1, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Jobs = new List<Job> { job1 } };
            var parameters = GetParameters(candidate, job1);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);

            // Add job with no start date.

            var job2 = CreateJob(2, new PartialDateRange(null, new PartialDate(endDate.Year, endDate.Month)));
            resume.Jobs.Add(job2);
            parameters = GetParameters(candidate, job2);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);

            // Add job with no end date.

            var job3 = CreateJob(3, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month)));
            resume.Jobs.Add(job3);
            parameters = GetParameters(candidate, job3);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);

            // Add job with no dates.

            var job4 = CreateJob(4, new PartialDateRange());
            resume.Jobs.Add(job4);
            parameters = GetParameters(candidate, job4);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);

            // Add job with reversed dates.

            var job5 = CreateJob(5, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            resume.Jobs.Add(job5);
            parameters = GetParameters(candidate, job5);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAddJobReversedDates()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Add job with dates.

            var startDate = DateTime.Now.AddMonths(-6).Date;
            var endDate = DateTime.Now.AddMonths(-3).Date;

            var job = CreateJob(1, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Jobs = new List<Job> { job } };
            var parameters = GetParameters(candidate, job);

            // Reverse the dates.

            var startDateMonth = parameters["StartDateMonth"];
            var startDateYear = parameters["StartDateYear"];
            var endDateMonth = parameters["EndDateMonth"];
            var endDateYear = parameters["EndDateYear"];

            parameters["StartDateMonth"] = endDateMonth;
            parameters["StartDateYear"] = endDateYear;
            parameters["EndDateMonth"] = startDateMonth;
            parameters["EndDateYear"] = startDateYear;

            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestAddJobErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            LogIn(member);

            // Add job with no title.

            var job = CreateJob(1, new PartialDateRange(new PartialDate(DateTime.Now.AddMonths(-6)), new PartialDate(DateTime.Now.AddMonths(-3))));
            job.Title = null;
            var parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Title", "The title is required.");

            // Add job with no company.

            job = CreateJob(2, new PartialDateRange(new PartialDate(DateTime.Now.AddMonths(-6)), new PartialDate(DateTime.Now.AddMonths(-3))));
            job.Company = null;
            parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Company", "The company is required.");

            // Add job with no dates.

            job = CreateJob(2, null);
            parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Date", "The date is required.");
        }

        [TestMethod]
        public void TestUpdateJob()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Create some jobs.

            var startDate = DateTime.Now.AddMonths(-6).Date;
            var endDate = DateTime.Now.AddMonths(-3).Date;

            var job1 = CreateJob(1, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            var job2 = CreateJob(2, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month)));
            var resume = new Resume { Jobs = new List<Job> { job1, job2 } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Update one.

            job1.Title = NewTitle;
            var parameters = GetParameters(candidate, job1);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);

            // Update another.

            job2.Description = NewDescription;
            parameters = GetParameters(candidate, job2);
            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestUpdateJobReversedDates()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Create some jobs.

            var startDate = DateTime.Now.AddMonths(-6).Date;
            var endDate = DateTime.Now.AddMonths(-3).Date;

            var job = CreateJob(1, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Jobs = new List<Job> { job } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Update the job.

            job.Title = NewTitle;
            var parameters = GetParameters(candidate, job);

            var startDateMonth = parameters["StartDateMonth"];
            var startDateYear = parameters["StartDateYear"];
            var endDateMonth = parameters["EndDateMonth"];
            var endDateYear = parameters["EndDateYear"];

            parameters["StartDateMonth"] = endDateMonth;
            parameters["StartDateYear"] = endDateYear;
            parameters["EndDateMonth"] = startDateMonth;
            parameters["EndDateYear"] = startDateYear;

            AssertModel(true, EmploymentHistory(parameters));
            AssertMember(member, candidate, resume, true);
        }

        [TestMethod]
        public void TestUpdateJobErrors()
        {
            var member = CreateMember();
            var candidate = _candidatesQuery.GetCandidate(member.Id);

            // Create some jobs.

            var startDate = DateTime.Now.AddMonths(-6).Date;
            var endDate = DateTime.Now.AddMonths(-3).Date;

            var job = CreateJob(1, new PartialDateRange(new PartialDate(startDate.Year, startDate.Month), new PartialDate(endDate.Year, endDate.Month)));
            var resume = new Resume { Jobs = new List<Job> { job } };
            _candidateResumesCommand.CreateResume(candidate, resume);
            LogIn(member);

            // Add job with no title.

            var originalTitle = job.Title;
            job.Title = null;
            var parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Title", "The title is required.");
            job.Title = originalTitle;

            // Add job with no company.

            var originalCompany = job.Company;
            job.Company = null;
            parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Company", "The company is required.");
            job.Company = originalCompany;

            // Add job with no dates.

            job.Dates = null;
            parameters = GetParameters(candidate, job);
            AssertJsonError(EmploymentHistory(parameters), "Date", "The date is required.");
        }

        private static NameValueCollection GetParameters(ICandidate candidate, IJob job)
        {
            var parameters = new NameValueCollection
            {
                {"RecentProfession", candidate.RecentProfession == null ? null : candidate.RecentProfession.Value.ToString()},
                {"RecentSeniority", candidate.RecentSeniority == null ? null : candidate.RecentSeniority.Value.ToString()},
            };

            if (candidate.Industries != null)
            {
                foreach (var industry in candidate.Industries)
                    parameters.Add("IndustryIds", industry.Id.ToString());
            }
            else
            {
                parameters.Add("IndustryIds", null);
            }

            if (job != null)
            {
                if (job.Id != Guid.Empty)
                    parameters.Add("Id", job.Id.ToString());
                parameters.Add("Company", job.Company);
                parameters.Add("Title", job.Title);
                parameters.Add("Description", job.Description);
                parameters.Add("StartDateMonth", job.Dates == null || job.Dates.Start == null || job.Dates.Start.Value.Month == null ? null : job.Dates.Start.Value.Month.Value.ToString());
                parameters.Add("StartDateYear", job.Dates == null || job.Dates.Start == null ? null : job.Dates.Start.Value.Year.ToString());
                parameters.Add("EndDateMonth", job.Dates == null || job.Dates.End == null || job.Dates.End.Value.Month == null ? null : job.Dates.End.Value.Month.Value.ToString());
                parameters.Add("EndDateYear", job.Dates == null || job.Dates.End == null ? null : job.Dates.End.Value.Year.ToString());
                parameters.Add("IsCurrent", (job.Dates != null && job.Dates.End == null).ToString());
            }

            return parameters;
        }

        private JsonProfileJobModel EmploymentHistory(NameValueCollection parameters)
        {
            var response = Post(_employmentHistoryUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonProfileJobModel>(response);
        }

        private static void AssertModel(bool expectJobId, JsonProfileJobModel model)
        {
            AssertJsonSuccess(model);
            if (expectJobId)
            {
                Assert.IsNotNull(model.JobId);
                Assert.AreNotEqual(Guid.Empty, model.JobId.Value);
            }
            else
            {
                Assert.IsNull(model.JobId);
            }
        }

        private static Job CreateJob(int index, PartialDateRange dates)
        {
            return new Job
            {
                Id = Guid.NewGuid(),
                Company = string.Format(CompanyFormat, index),
                Title = string.Format(TitleFormat, index),
                Description = string.Format(DescriptionFormat, index),
                Dates = dates,
            };
        }

        protected override JsonResponseModel Call()
        {
            return EmploymentHistory(new NameValueCollection());
        }
    }
}
