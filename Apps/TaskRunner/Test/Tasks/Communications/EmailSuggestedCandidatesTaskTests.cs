using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
    public class EmailSuggestedCandidatesTaskTests
        : TaskTests
    {
        private const int MaxJobAds = 10;
        private const int MaxResultsPerJobAd = 3;
        private const int LowerBound = 40000;
        private const int UpperBound = 50000;
        private const string Title = "Mentor";
        private const string OtherTitle = "Archeologist";
        private const string ContactEmailAddress = "contact@test.linkme.net.au";

        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly ISuggestedMembersQuery _suggestedMembersQuery = Resolve<ISuggestedMembersQuery>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand = Resolve<IExecuteMemberSearchCommand>();

        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer.ClearEmails();
            MemberSearchHost.ClearIndex();
        }

        [TestMethod]
        public void TestNoSalaryMatch()
        {
            CreateMembers(MaxResultsPerJobAd + 1);
            CreateJobAd(0, CreateEmployer(), null, LowerBound / 2, Title, Title, null);
            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestNoTitlesMatch()
        {
            CreateMembers(MaxResultsPerJobAd + 1);
            CreateJobAd(0, CreateEmployer(), LowerBound, UpperBound, OtherTitle, OtherTitle, null);
            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestTitleMatch()
        {
            var members = CreateMembers(MaxResultsPerJobAd + 1);
            var employer = CreateEmployer();
            CreateJobAd(0, employer, LowerBound, UpperBound, Title, null, null);
            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            AssertEmail(employer, members, email);
        }

        [TestMethod]
        public void TestPositionTitleMatch()
        {
            var members = CreateMembers(MaxResultsPerJobAd + 1);
            var employer = CreateEmployer();
            CreateJobAd(0, employer, LowerBound, UpperBound, OtherTitle, Title, null);
            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            AssertEmail(employer, members, email);
        }

        [TestMethod]
        public void TestMultipleJobAds()
        {
            var members = CreateMembers(MaxResultsPerJobAd + 1);
            var employer = CreateEmployer();

            // Create 2 job ads.

            CreateJobAd(0, employer, LowerBound, UpperBound, Title, Title, null);
            CreateJobAd(1, employer, LowerBound, UpperBound, Title, Title, null);

            // 2 emails should be sent.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            AssertEmail(employer, members, email);
        }

        [TestMethod]
        public void TestMultipleJobAdsOneMatch()
        {
            var members = CreateMembers(MaxResultsPerJobAd + 1);
            var employer = CreateEmployer();

            // Create 2 job ads.

            CreateJobAd(0, employer, LowerBound, UpperBound, OtherTitle, OtherTitle, null);
            CreateJobAd(1, employer, LowerBound, UpperBound, Title, Title, null);

            // 1 email should be sent.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            AssertEmail(employer, members, email);
        }

        [TestMethod]
        public void TestContact()
        {
            var members = CreateMembers(MaxResultsPerJobAd + 1);
            var employer = CreateEmployer();
            CreateJobAd(0, employer, LowerBound, UpperBound, Title, Title, ContactEmailAddress);

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(ContactEmailAddress));
            AssertEmail(employer, members, email);
        }

        [TestMethod]
        public void TestOneJobOneCandidate()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(0, employer, LowerBound, UpperBound, Title, null, null);

            // Create one candidate.

            var members = CreateMembers(1);

            // Send the email.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            AssertEmail(email, new[] { jobAd }, new[] { members });
        }

        [TestMethod]
        public void TestOneJobTwoCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(0, employer, LowerBound, UpperBound, Title, null, null);

            // Create two candidates.

            var members = CreateMembers(2);

            // Send the email.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            AssertEmail(email, new[] { jobAd }, new[] { members });
        }

        [TestMethod]
        public void TestOneJobFourCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(0, employer, LowerBound, UpperBound, Title, null, null);

            // Create one candidate.

            var members = CreateMembers(4);

            // Send the email.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            AssertEmail(email, new[] { jobAd }, new[] { members.Take(MaxResultsPerJobAd).ToList() });
        }

        [TestMethod]
        public void TestTwoJobsTwoCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAd1 = CreateJobAd(0, employer, LowerBound, UpperBound, Title, null, null);
            var jobAd2 = CreateJobAd(1, employer, LowerBound, UpperBound, Title, null, null);

            // Create one candidate.

            var members = CreateMembers(2);

            // Send the email.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            AssertEmail(email, new[] { jobAd1, jobAd2 }, new[] { members, members });
        }

        [TestMethod]
        public void TestTwelveJobsOneCandidate()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            const int count = 12;
            var jobAds = new JobAd[count];
            for (var index = 0; index < count; ++index)
                jobAds[index] = CreateJobAd(index, employer, LowerBound, UpperBound, Title, null, null);

            // Create one candidate.

            var members = CreateMembers(12);

            // Send the email.

            ExecuteTask();
            var email = _emailServer.AssertEmailSent();
            AssertEmail(email, jobAds.Take(MaxJobAds).ToList(), (from i in Enumerable.Range(0, MaxJobAds) select (IList<Member>)members.Take(MaxResultsPerJobAd).ToList()).Take(MaxJobAds).ToList());
        }

        private void AssertEmail(IEmployer employer, IList<Member> members, MockEmail email)
        {
            email.AssertSubject("Job candidates from LinkMe");
            email.AssertHtmlViewContains("4 suggested candidates");
            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, members[0]);
            email.AssertHtmlViewContains(view.GetDisplayText(false));
        }

        private static void AssertEmail(MockEmail email, IList<JobAd> jobAds, IList<IList<Member>> members)
        {
            var document = new HtmlDocument();
            document.LoadHtml(email.GetHtmlView().Body);

            var spans = document.DocumentNode.SelectNodes("//p/span[@class='jobAdTitle']");
            var divs = document.DocumentNode.SelectNodes("//p/div[@class='alert-container']");
            Assert.AreEqual(jobAds.Count, members.Count);
            Assert.AreEqual(jobAds.Count, spans.Count);
            Assert.AreEqual(jobAds.Count, divs.Count);

            for (var index = 0; index < jobAds.Count; ++index)
            {
                Assert.AreEqual(jobAds[index].Id.ToString(), spans[index].Attributes["data-jobadid"].Value);
                var candidateDivs = divs[index].SelectNodes("div[@class='candidate']");
                Assert.AreEqual(members[index].Count, candidateDivs.Count);

                for (var candidateIndex = 0; candidateIndex < candidateDivs.Count; ++candidateIndex)
                {
                    var id = candidateDivs[candidateIndex].Attributes["data-id"].Value;
                    Assert.AreEqual(members[index][candidateIndex].Id.ToString(), id);
                }
            }
        }

        private void ExecuteTask()
        {
            new EmailSuggestedCandidatesTask(_emailsCommand, _jobAdsQuery, _suggestedMembersQuery, _employersQuery, _employerMemberViewsQuery, _employerCreditsQuery, _memberSearchesCommand, _executeMemberSearchCommand).ExecuteTask();
        }

        private IList<Member> CreateMembers(int count)
        {
            return (from i in Enumerable.Range(0, count) select CreateMember(i)).ToList();
        }

        private Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            candidate.DesiredSalary = new Salary { LowerBound = LowerBound, UpperBound = UpperBound, Rate = SalaryRate.Year };
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        private Employer CreateEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            var jobPoster = _jobPostersCommand.GetJobPoster(employer.Id);
            jobPoster.SendSuggestedCandidates = true;
            _jobPostersCommand.UpdateJobPoster(jobPoster);
            return employer;
        }

        private JobAd CreateJobAd(int index, IEmployer employer, decimal? salaryLowerBound, decimal? salaryUpperBound, string title, string positionTitle, string contactEmailAddress)
        {
            // Create a job ad that doesn't have any candidate matches on the salary - no email should be sent.

            var jobAd = employer.CreateTestJobAd();
            jobAd.CreatedTime = DateTime.Now.AddDays(-1).AddMinutes(-1 * index);
            jobAd.Description.Salary = salaryLowerBound == null && salaryUpperBound == null
                ? null
                : new Salary { LowerBound = salaryLowerBound, UpperBound = salaryUpperBound, Rate = SalaryRate.Year, Currency = Currency.AUD };
            jobAd.Title = title;
            jobAd.Description.PositionTitle = positionTitle;
            if (!string.IsNullOrEmpty(contactEmailAddress))
                jobAd.ContactDetails.EmailAddress = contactEmailAddress;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
