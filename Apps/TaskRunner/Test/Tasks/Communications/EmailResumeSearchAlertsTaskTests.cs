using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
	public class EmailResumeSearchAlertsTaskTests
        : TaskTests
	{
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();
        private readonly IMemberSearchesQuery _memberSearchesQuery = Resolve<IMemberSearchesQuery>();
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();
	    private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand = Resolve<IExecuteMemberSearchCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

	    private const string JobTitle = "archeologist";
	    private const string SearchAlertNameFormat = "My search alert {0}";

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            MemberSearchHost.ClearIndex();
        }

        [TestMethod]
		public void TestJobTitleAlert()
		{
            // Create a search alert.

            CreateMember(JobTitle);
            var employer = CreateEmployer();
            CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle });

            // Run the task.

			ExecuteTask();
            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void TestDesiredJobTitleAlert()
        {
            const string desiredJobTitle = "picker or packer";

            // Create a search alert.

            var member = CreateMember(JobTitle);
            UpdateCandidate(member.Id, c => c.DesiredJobTitle = "mushroom picker");
            var employer = CreateEmployer();
            CreateSearchAlert(employer, 1, new MemberSearchCriteria { DesiredJobTitle = desiredJobTitle });

            // Run the task.

            ExecuteTask();
            AssertEmail(employer, "Desired job title contains " + desiredJobTitle);
        }

        [TestMethod]
        public void TestAndNotJobTitle()
        {
            // Create a search alert with a bad title.

            CreateMember(JobTitle);
            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } });
            UpdateCriteria(search, "JobTitle", "AND NOT " + JobTitle);

            // Create a search alert.

            CreateSearchAlert(employer, 2, new MemberSearchCriteria { JobTitle = JobTitle, SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } });

            // Run the task.

            ExecuteTask();

            // The first alert will throw an exception but the second should still get through.

            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void Test0JobsToSearch()
        {
            // Create a search alert with JobsToSearch = 0.

            CreateMember(JobTitle);
            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, JobTitlesToSearch = 0, SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } });

            // Run the task.

            Assert.AreEqual(JobsToSearch.AllJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();
            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void TestEmptyJobsToSearch()
        {
            CreateMember(JobTitle);
            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, JobTitlesToSearch = 0, SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } });
            UpdateCriteria(search, "JobsToSearch", string.Empty);

            // Run the task.

            Assert.AreEqual(JobsToSearch.RecentJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();

            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void Test2JobsToSearch()
        {
            CreateMember(JobTitle);
            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, JobTitlesToSearch = 0, SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated } });
            UpdateCriteria(search, "JobsToSearch", "2");

            // Run the task.

            Assert.AreEqual(JobsToSearch.RecentJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();

            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void TestIndustry()
        {
            var industry = _industriesQuery.GetIndustries()[0];
            var member = CreateMember(JobTitle);
            UpdateCandidate(member.Id, c => c.Industries = new List<Industry> { industry });

            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, IndustryIds = new [] { industry.Id } });

            // Run the task.

            Assert.AreEqual(JobsToSearch.RecentJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();

            AssertEmail(employer, JobTitle + ", " + industry.Name);
        }

        [TestMethod]
        public void TestUnknownIndustry()
        {
            CreateMember(JobTitle);
            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, IndustryIds = new[] { Guid.NewGuid() } });

            // Run the task.

            Assert.AreEqual(JobsToSearch.RecentJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();

            AssertEmail(employer, JobTitle);
        }

        [TestMethod]
        public void TestIndustryAndUnknownIndustry()
        {
            var industry = _industriesQuery.GetIndustries()[0];
            var member = CreateMember(JobTitle);
            UpdateCandidate(member.Id, c => c.Industries = new List<Industry> { industry });

            var employer = CreateEmployer();
            var search = CreateSearchAlert(employer, 1, new MemberSearchCriteria { JobTitle = JobTitle, IndustryIds = new[] { industry.Id, Guid.NewGuid() } });

            // Run the task.

            Assert.AreEqual(JobsToSearch.RecentJobs, _memberSearchesQuery.GetMemberSearch(search.Id).Criteria.JobTitlesToSearch);
            ExecuteTask();

            AssertEmail(employer, JobTitle + ", " + industry.Name);
        }

        private void ExecuteTask()
        {
            new EmailResumeSearchAlertsTask(_executeMemberSearchCommand, _memberSearchesQuery, _memberSearchAlertsCommand, _memberSearchAlertsQuery, _employersQuery, _emailsCommand, _industriesQuery, _employerMemberViewsQuery).ExecuteTask();
        }

        private Member CreateMember(string jobTitle)
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            resume.Jobs[0].Title = jobTitle;
            _candidateResumesCommand.UpdateResume(candidate, resume);
            return member;
        }

        private void UpdateCandidate(Guid memberId, Action<Candidate> updateCandidate)
        {
            var candidate = _candidatesCommand.GetCandidate(memberId);
            updateCandidate(candidate);
            _candidatesCommand.UpdateCandidate(candidate);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        private MemberSearch CreateSearchAlert(IUser employer, int index, MemberSearchCriteria criteria)
        {
            var search = new MemberSearch { Criteria = criteria, Name = string.Format(SearchAlertNameFormat, index) };
            _memberSearchAlertsCommand.CreateMemberSearchAlert(employer, search, AlertType.Email);
            _memberSearchAlertsCommand.UpdateLastRunTime(search.Id, DateTime.Now.AddDays(-3), AlertType.Email);
            return search;
        }

        private void UpdateCriteria(MemberSearch search, string name, string value)
        {
            // Update directly to avoid checks etc in code.

            if (value == null)
                DatabaseHelper.ExecuteNonQuery(
                    _connectionFactory,
                    "UPDATE ResumeSearchCriteria SET value = NULL WHERE setId = @id AND name = @name",
                    "@name", name,
                    "@id", search.Criteria.Id);
            else
                DatabaseHelper.ExecuteNonQuery(
                    _connectionFactory,
                    "UPDATE ResumeSearchCriteria SET value = @value WHERE setId = @id AND name = @name",
                    "@name", name,
                    "@value", value,
                    "@id", search.Criteria.Id);
        }

        private static string GetSubject(string jobTitle)
        {
            return HtmlUtil.StripHtmlTags("Candidate alert" + (jobTitle.Length == 0 ? string.Empty : ": " + jobTitle));
        }

        private static string GetViewResumeUrl(string body)
	    {
            var start = body.IndexOf("<a href=\"");
            if (start != -1)
            {
                start += "<a href=\"".Length;
                var end = body.IndexOf("\" class=", start, StringComparison.Ordinal);
                if (end != -1)
                    return body.Substring(start, end - start);
            }

            return string.Empty;
	    }

        private void AssertEmail(ICommunicationUser employer, string criteriaText)
        {
            // Check for the email.

            var email = _emailServer.AssertEmailSent();
            email.AssertSubject(GetSubject(criteriaText));
            email.AssertAddresses(Return, Return, employer);
            email.AssertHtmlViewContains("<strong>1 candidate</strong> was found for: " + criteriaText);
            email.AssertNoAttachments();

            // Check the link.

            var viewResumeUrl = GetViewResumeUrl(email.GetHtmlView().Body);
            Assert.IsTrue(viewResumeUrl.Contains("/url/"));
        }
    }
}
