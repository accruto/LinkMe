using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class SuggestedCandidatesEmailTests
        : EmployerMemberViewEmailTests
    {
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const string Subject = "Job candidates from LinkMe";
        private const string MatchingJobTitle = "Mentor";
        private const int MaxResultsPerJobAd = 3;
        private const int MaxJobAdsPerEmail = 20;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            MemberSearchHost.ClearIndex();
        }

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(1, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(1);

            // Send the email.

            return new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, jobAds.Count * members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, new[] { members }), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
        }

        [TestMethod]
        public void TestOneJobOneCandidate()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(1, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(1);

            // Send the email.

            var communication = new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, jobAds.Count * members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, new[] { members }), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, jobAds, new[]{members})));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestOneJobTwoCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(1, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(2);

            // Send the email.

            var communication = new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, jobAds.Count * members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, new[] { members }), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, jobAds, new[]{members})));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestOneJobFourCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(1, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(4);

            // Send the email.

            var communication = new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, jobAds.Count * members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, new[] { members }), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, jobAds, new[]{members})));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestTwoJobsTwoCandidates()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(2, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(4);

            // Send the email.

            var communication = new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, new[] { members.Take(2).ToList(), members.Skip(2).ToList() }), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, jobAds, new[]{members.Take(2).ToList(), members.Skip(2).ToList()})));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestTwelveJobsOneCandidate()
        {
            // Create a job ad.

            var employer = CreateEmployer();
            var jobAds = PostJobAds(12, employer, MatchingJobTitle);

            // Create one candidate.

            var members = CreateMembers(12);

            // Send the email.

            var communication = new SuggestedCandidatesEmail(employer.EmailAddress.Address, employer, members.Count, jobAds.Count, GetSuggestedCandidates(jobAds, Enumerable.Range(0, jobAds.Count).Select(a => new[]{members[a]}).ToArray()), _employerMemberViewsQuery.GetEmployerMemberViews(employer, members));
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(Subject);
            email.AssertHtmlView(GetBody(communication, employer, GetContent(communication, employer, jobAds, Enumerable.Range(0, jobAds.Count).Select(a => new[]{members[a]}).ToArray())));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private IList<Member> CreateMembers(int count)
        {
            IList<Member> members = new List<Member>();
            for (int index = 0; index < count; ++index)
                members.Add(CreateMember(index));
            return members;
        }

        private new Member CreateMember(int index)
        {
            var member = base.CreateMember(index);
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            candidate.DesiredSalary = new Salary {LowerBound = 40000, UpperBound = 50000, Rate = SalaryRate.Year, Currency = Currency.AUD};
            _candidatesCommand.UpdateCandidate(candidate);

            return member;
        }

        private IList<JobAd> PostJobAds(int count, IEmployer employer, string title)
        {
            IList<JobAd> jobAds = new List<JobAd>();
            for (int index = 0; index < count; ++index)
                jobAds.Add(PostJobAd(employer, title));
            return jobAds;
        }

        private JobAd PostJobAd(IEmployer employer, string title)
        {
            var jobAd = employer.CreateTestJobAd(title);
            jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(jobAd);
            return jobAd;
        }

        private new Employer CreateEmployer()
        {
            var employer = base.CreateEmployer();
            var jobPoster = new JobPoster {Id = employer.Id, SendSuggestedCandidates = true};
            _jobPostersCommand.UpdateJobPoster(jobPoster);
            return employer;
        }

        private string GetContent(TemplateEmail templateEmail, IEmployer employer, IList<JobAd> jobAds, IList<IList<Member>> members)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("<p>");

            // Header.

            if (members.Sum(m => m.Count) == 1)
            {
                sb.AppendLine("  LinkMe has found 1 suggested candidate for the position");
                sb.AppendLine("  posted on <a href=\"" + InsecureRootPath + "\">" + InsecureRootPath + "</a>.");
                sb.AppendLine("  This is not an applicant, but a suggested candidate from our database");
                sb.AppendLine("  sent to you as the contact person for this job ad.");
            }
            else
            {
                sb.AppendLine("  LinkMe has found " + members.Sum(m => m.Count) + " suggested candidates for the position" + (jobAds.Count > 1 ? "s" : ""));
                sb.AppendLine("  posted on <a href=\"" + InsecureRootPath + "\">" + InsecureRootPath + "</a>.");
                sb.AppendLine("  These are not applicants, but suggested candidates from our database");
                sb.AppendLine("  sent to you as the contact person for " + (jobAds.Count > 1 ? "these job ads" : "this job ad") + ".");
            }

            sb.AppendLine("</p>");
            sb.AppendLine();

            // Results.

            GetResultsContent(sb, templateEmail, employer, jobAds, members);
 
            // Footer.

            sb.AppendLine();
            if (employer == null)
            {
                sb.AppendLine("<p>");
                sb.AppendLine("  To see candidates' contact details");
                sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/products/neworder") + "\">request full access</a>.");
                sb.AppendLine("</p>");
            }
            else 
            {
                sb.AppendLine("<p>");
                sb.AppendLine("  If you have forgotten your LinkMe password or login ID");
                sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/accounts/newpassword", new QueryString("userId", employer.EmailAddress.Address)) + "\">reset your password</a>.");
                sb.AppendLine("</p>");
            }
            sb.AppendLine();

            sb.AppendLine("<p>");
            sb.AppendLine("  If you do not wish to receive further Suggested Candidates emails");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/ui/unregistered/unsubscribefromemail.aspx", new QueryString("emailAddress", employer.EmailAddress.Address)) + "\">click here to unsubscribe</a>.");
            sb.AppendLine("</p>");

            return sb.ToString();
        }

        private void GetResultsContent(StringBuilder sb, TemplateEmail templateEmail, IEmployer employer, IList<JobAd> jobAds, IList<IList<Member>> members)
        {
            sb.AppendLine();
            for (var index = 0; index < Math.Min(jobAds.Count, MaxJobAdsPerEmail); ++index)
                GetResultsContent(sb, templateEmail, index, employer, jobAds[index], members[index]);
            sb.AppendLine();
        }

        private void GetResultsContent(StringBuilder sb, TemplateEmail templateEmail, int index, IEmployer employer, JobAdEntry jobAd, ICollection<Member> members)
        {
            if (index > 0)
                sb.AppendLine();
            sb.AppendLine("<p>");
            sb.Append("  <span class=\"jobAdTitle\" data-jobadid=\"").Append(jobAd.Id).Append( "\">").Append(jobAd.Title).Append("</span> - ").Append(members.Count).Append(" suggested candidate").AppendLine(members.Count == 1 ? "" : "s");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");

            var resultsWritten = 0;
            AppendResults(sb, templateEmail, employer, new MemberSearchResults {MemberIds = members.Select(m => m.Id).ToArray()}, 0, Math.Min(members.Count, MaxResultsPerJobAd), true, ref resultsWritten);

            if (members.Count > MaxResultsPerJobAd)
            {
                sb.AppendLine();
                sb.AppendLine("  <div>");
                var tinyUrl = GetTinyUrl(templateEmail, true, "~/employers/login", "returnUrl", new ReadOnlyApplicationUrl("~/employers/candidates/suggested/" + jobAd.Id).PathAndQuery);
                sb.AppendLine("    <a href=\"" + tinyUrl + "\">All suggested candidates for this job ad</a>.");
                sb.AppendLine("  </div>");
            }

            sb.AppendLine();
            sb.AppendLine("</p>");
        }

        private static IList<SuggestedCandidates> GetSuggestedCandidates(IList<JobAd> jobAds, IList<Member>[] members)
        {
            var suggestedCandidates = new List<SuggestedCandidates>();
            for (var index = 0; index < jobAds.Count; ++index)
                suggestedCandidates.Add(new SuggestedCandidates { JobAd = jobAds[index], TotalCandidates = members[index].Count, CandidateIds = members[index].Select(x => x.Id).Take(MaxResultsPerJobAd).ToList() });
            return suggestedCandidates;
        }
    }
}