using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Accounts.Queries;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using LinkMe.Query.Reports.Roles.Candidates.Queries;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
    public class EmailEmployerUsageTaskTest
        : TaskTests
    {
        private const string EmployerUserIdFormat = "employer{0}";
        private const string EmployerFirstNameFormat = "Homer{0}";
        private const string EmployerLastName = "Simpson";
        private const string MemberEmailAddressFormat = "member{0}@test.linkme.net.au";
        private const int EmployerPeriod = 2; // months
        private const int RecruiterPeriod = 14; // days

        private const int UploadResume = 8;
        private const int EditResume = 9;
        private const int ReloadResume = 17;

        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IResumeReportsCommand _resumeReportsCommand = Resolve<IResumeReportsCommand>();
        private readonly IUserSessionsRepository _userSessionsRepository = Resolve<IUserSessionsRepository>();
        private readonly IResumeReportsQuery _resumeReportsQuery = Resolve<IResumeReportsQuery>();
        private readonly IAccountReportsQuery _accountReportsQuery = Resolve<IAccountReportsQuery>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoEmails()
        {
            // No employers.

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // No candidates.

            DateTime now = DateTime.Now;
            CreateEmployers(now.AddDays(-14), true, 0, 1);

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestJoinedCandidate()
        {
            DateTime now = DateTime.Now;

            // Joined since last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddDays(-1), null, null, true);

            // Joined before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), null, null, false);

            // Joined day of last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-1 * EmployerPeriod), null, null, true);

            // Joined day before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-1 * EmployerPeriod).AddDays(- 1), null, null, false);

            // Joined day after last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-1 * EmployerPeriod).AddDays(1), null, null, true);
        }

        [TestMethod]
        public void TestUploadedCandidate()
        {
            DateTime now = DateTime.Now;

            // Uploaded since last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddDays(-1), true);

            // Uploaded before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddMonths(-2 * EmployerPeriod), false);

            // Uploaded day of last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddMonths(-1 * EmployerPeriod), true);

            // Uploaded day before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddMonths(-1 * EmployerPeriod).AddDays(-1), false);

            // Uploaded day after last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddMonths(-1 * EmployerPeriod).AddDays(1), true);
        }

        [TestMethod]
        public void TestEditedCandidate()
        {
            DateTime now = DateTime.Now;

            // Edited since last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), EditResume, now.AddDays(-1), true);

            // Edited before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), EditResume, now.AddMonths(-2 * EmployerPeriod), false);

            // Edited day of last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), EditResume, now.AddMonths(-1 * EmployerPeriod), true);

            // Edited day before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), EditResume, now.AddMonths(-1 * EmployerPeriod).AddDays(-1), false);

            // Edited day after last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), EditResume, now.AddMonths(-1 * EmployerPeriod).AddDays(1), true);
        }

        [TestMethod]
        public void TestReloadedCandidate()
        {
            DateTime now = DateTime.Now;

            // Reloaded since last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), ReloadResume, now.AddDays(-1), true);

            // Reloaded before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), ReloadResume, now.AddMonths(-2 * EmployerPeriod), false);

            // Reloaded day of last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), ReloadResume, now.AddMonths(-1 * EmployerPeriod), true);

            // Reloaded day before last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), ReloadResume, now.AddMonths(-1 * EmployerPeriod).AddDays(-1), false);

            // Reloaded day after last log in.

            TestSingleCandidate(now.AddMonths(-1 * EmployerPeriod), now.AddMonths(-2 * EmployerPeriod), ReloadResume, now.AddMonths(-1 * EmployerPeriod).AddDays(1), true);
        }

        [TestMethod]
        public void TestMultipleActionCandidate()
        {
            var now = DateTime.Now;

            // Create an employer.

            var employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 1)[0];

            // Join in period, edit in period, edit in period.

            var member = CreateMembers(now.AddDays(-3), UploadResume, now.AddDays(-2), 0, 1)[0];
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            CheckSingleCandidate(employer, true);

            ClearEmployer(employer);
            AddEvent(ReloadResume, now.AddDays(-1), candidate);
            CheckSingleCandidate(employer, true);

            // Join before period, edit before period, edit in period.

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 1)[0];
            member = CreateMembers(now.AddMonths(-3 * EmployerPeriod), null, null, 0, 1)[0];
            candidate = _candidatesCommand.GetCandidate(member.Id);
            CheckSingleCandidate(employer, false);

            AddEvent(ReloadResume, now.AddMonths(-2 * EmployerPeriod), candidate);
            CheckSingleCandidate(employer, false);

            AddEvent(EditResume, now.AddDays(-2), candidate);
            CheckSingleCandidate(employer, true);

            // Join before period, edit in period, edit in period.

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 1)[0];
            member = CreateMembers(now.AddMonths(-3 * EmployerPeriod), null, null, 0, 1)[0];
            candidate = _candidatesCommand.GetCandidate(member.Id);
            CheckSingleCandidate(employer, false);

            AddEvent(ReloadResume, now.AddDays(-3), candidate);
            CheckSingleCandidate(employer, true);

            ClearEmployer(employer);
            AddEvent(EditResume, now.AddDays(-2), candidate);
            CheckSingleCandidate(employer, true);
        }

        [TestMethod]
        public void TestMultipleCandidates()
        {
            DateTime now = DateTime.Now;
            int totalNew = 0;
            int total = 0;

            // Create an employer.

            Employer employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 1)[0];

            // Create some members.

            int members = CreateMembers(now.AddDays(-3), null, null, total, 2).Count;
            total += members;
            totalNew += members;
            CheckCandidates(employer, totalNew);

            // Add some more.

            members = CreateMembers(now.AddDays(-2), null, null, total, 4).Count;
            total += members;
            totalNew += members;
            ClearEmployer(employer);
            CheckCandidates(employer, totalNew);

            // Add some who join before the last login.

            members = CreateMembers(now.AddMonths(-2 * EmployerPeriod), null, null, total, 3).Count;
            total += members;
            ClearEmployer(employer);
            CheckCandidates(employer, totalNew);

            // Add some who join and edit.

            members = CreateMembers(now.AddDays(-2), UploadResume, now.AddDays(-1), total, 5).Count;
            total += members;
            totalNew += members;
            ClearEmployer(employer);
            CheckCandidates(employer, totalNew);

            // Add some who edit.

            members = CreateMembers(now.AddMonths(-2 * EmployerPeriod), UploadResume, now.AddDays(-5), total, 3).Count;
            totalNew += members;
            ClearEmployer(employer);
            CheckCandidates(employer, totalNew);
        }

        [TestMethod]
        public void TestNoResend()
        {
            DateTime now = DateTime.Now;

            // Create an employer.

            Employer employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 1)[0];

            // Create some members.

            int members = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;
            CheckCandidates(employer, members);

            // Resend.

            CheckCandidates(employer, 0);

            // Add some more candidates, should still not resend.

            CreateMembers(now.AddDays(-2), null, null, members, 3);
            CheckCandidates(employer, 0);
        }

        [TestMethod]
        public void TestDisabledEmployer()
        {
            DateTime now = DateTime.Now;

            // Create an employer.

            Employer employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), false, 0, 1)[0];

            // Create some members, should not get anyone.

            int members = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;
            CheckCandidates(employer, 0);

            // Resend.

            CheckCandidates(employer, 0);

            // Add some more candidates, should still not resend.

            CreateMembers(now.AddDays(-2), null, null, members, 3);
            CheckCandidates(employer, 0);
        }

        [TestMethod]
        public void TestMultipleEmployers()
        {
            DateTime now = DateTime.Now;

            // Create an employer.

            IList<Employer> employers = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 0, 3);

            // Create some members.

            int members = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;
            CheckCandidates(employers, members);

            // Create some more employers.

            ClearEmployers(employers);
            IList<Employer> newEmployers = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, 3, 2);
            foreach (Employer employer in newEmployers)
                employers.Add(employer);

            CheckCandidates(employers, members);
        }

        [TestMethod]
        public void TestPeriods()
        {
            // Create some members.

            DateTime now = DateTime.Now;
            int members = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;

            // Create a recruiter.

            int employerCount = 0;
            Employer recruiter = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod), true, employerCount++, 1)[0];
            CheckCandidates(recruiter, members);

            // Create recruiters on other days.

            recruiter = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod - 1), true, employerCount++, 1)[0];
            CheckCandidates(recruiter, 0);

            recruiter = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod + 1), true, employerCount++, 1)[0];
            CheckCandidates(recruiter, 0);

            recruiter = CreateRecruiters(now.AddDays(-2 * RecruiterPeriod), true, employerCount++, 1)[0];
            CheckCandidates(recruiter, 0);

            recruiter = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod), true, employerCount++, 1)[0];
            CheckCandidates(recruiter, members);

            // Create an employer.

            Employer employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, employerCount++, 1)[0];
            CheckCandidates(employer, members);

            // Create employers on other days.

            employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(-1), true, employerCount++, 1)[0];
            CheckCandidates(employer, 0);

            employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(1), true, employerCount++, 1)[0];
            CheckCandidates(employer, 0);

            employer = CreateEmployers(now.AddMonths(-2 * EmployerPeriod), true, employerCount++, 1)[0];
            CheckCandidates(employer, 0);

            employer = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, employerCount, 1)[0];
            CheckCandidates(employer, members);
        }

        [TestMethod]
        public void TestMixAddMembers()
        {
            DateTime now = DateTime.Now;
            int employerCount = 0;

            // Create employers and recruiters who logged in on the period.

            IList<Employer> employers = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, employerCount, 3);
            employerCount += employers.Count;
            IList<Employer> recruiters = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod), true, employerCount, 4);
            employerCount += recruiters.Count;

            IList<Employer> all = new List<Employer>();
            foreach (Employer employer in employers)
                all.Add(employer);
            foreach (Employer recruiter in recruiters)
                all.Add(recruiter);

            // Create some who did not.

            employerCount += CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(5), true, employerCount, 5).Count;
            employerCount += CreateRecruiters(now.AddMonths(-1 * RecruiterPeriod).AddDays(3), true, employerCount, 6).Count;
            employerCount += CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(-6), true, employerCount, 5).Count;
            CreateRecruiters(now.AddMonths(-1 * RecruiterPeriod).AddDays(4), true, employerCount, 6);

            // Members in periods. Employers and recruiters see all.

            ClearEmployers(all);
            int firstMembers = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(all.Count);
            for (int index = 0; index < employers.Count; index++)
                AssertEmail(emails[index], employers[index], firstMembers);
            for (int index = 0; index < recruiters.Count; index++)
                AssertEmail(emails[employers.Count + index], recruiters[index], firstMembers);

            // Some between periods. Only employers see new ones.

            ClearEmployers(all);
            int secondMembers = CreateMembers(now.AddDays(-1 * RecruiterPeriod).AddDays(-2), null, null, firstMembers, 3).Count;

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            emails = _emailServer.AssertEmailsSent(all.Count);
            for (int index = 0; index < employers.Count; index++)
                AssertEmail(emails[index], employers[index], firstMembers + secondMembers);
            for (int index = 0; index < recruiters.Count; index++)
                AssertEmail(emails[employers.Count + index], recruiters[index], firstMembers);

            // Some before period. No-one sees new ones.

            ClearEmployers(all);
            CreateMembers(now.AddMonths(-1 * EmployerPeriod).AddDays(-2), null, null, firstMembers + secondMembers, 4);

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            emails = _emailServer.AssertEmailsSent(all.Count);
            for (int index = 0; index < employers.Count; index++)
                AssertEmail(emails[index], employers[index], firstMembers + secondMembers);
            for (int index = 0; index < recruiters.Count; index++)
                AssertEmail(emails[employers.Count + index], recruiters[index], firstMembers);
        }

        [TestMethod]
        public void TestMixAddEmployers()
        {
            DateTime now = DateTime.Now;
            int employerCount = 0;

            // Create some members.

            int firstMembers = CreateMembers(now.AddDays(-3), null, null, 0, 2).Count;
            int secondMembers = CreateMembers(now.AddDays(-1 * RecruiterPeriod).AddDays(-2), null, null, firstMembers, 3).Count;
            CreateMembers(now.AddMonths(-1 * RecruiterPeriod).AddDays(-2), null, null, firstMembers + secondMembers, 4);

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Create some employers and recruiters.

            employerCount += CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(5), true, employerCount, 5).Count;
            employerCount += CreateRecruiters(now.AddMonths(-1 * RecruiterPeriod).AddDays(3), true, employerCount, 6).Count;
            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Create employers and recruiters who logged in on the period.

            IList<Employer> employers = CreateEmployers(now.AddMonths(-1 * EmployerPeriod), true, employerCount, 3);
            employerCount += employers.Count;
            IList<Employer> recruiters = CreateRecruiters(now.AddDays(-1 * RecruiterPeriod), true, employerCount, 4);
            employerCount += recruiters.Count;
            IList<Employer> all = new List<Employer>();
            foreach (Employer employer in employers)
                all.Add(employer);
            foreach (Employer recruiter in recruiters)
                all.Add(recruiter);

            // Members in periods. Employers and recruiters see all.

            ClearEmployers(all);
            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(all.Count);
            for (int index = 0; index < employers.Count; index++)
                AssertEmail(emails[index], employers[index], firstMembers + secondMembers);
            for (int index = 0; index < recruiters.Count; index++)
                AssertEmail(emails[employers.Count + index], recruiters[index], firstMembers);

            // Add some more employers, should not see members.

            employerCount += CreateEmployers(now.AddMonths(-1 * EmployerPeriod).AddDays(-6), true, employerCount, 5).Count;
            CreateRecruiters(now.AddMonths(-1 * RecruiterPeriod).AddDays(4), true, employerCount, 6);
            ClearEmployers(all);

            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            emails = _emailServer.AssertEmailsSent(all.Count);
            for (int index = 0; index < employers.Count; index++)
                AssertEmail(emails[index], employers[index], firstMembers + secondMembers);
            for (int index = 0; index < recruiters.Count; index++)
                AssertEmail(emails[employers.Count + index], recruiters[index], firstMembers);
        }

        private void ClearEmployers(IEnumerable<Employer> employers)
        {
            foreach (Employer employer in employers)
                ClearEmployer(employer);
        }

        private void ClearEmployer(IHasId<Guid> employer)
        {
            var definition = _settingsQuery.GetDefinition(typeof(EmployerUsageEmail).Name);
            _settingsCommand.SetLastSentTime(employer.Id, definition.Id, null);
        }

        private void CheckSingleCandidate(ICommunicationUser employer, bool expected)
        {
            CheckCandidates(employer, expected ? 1 : 0);
        }

        private void CheckCandidates(ICommunicationUser employer, int expected)
        {
            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            if (expected > 0)
                AssertEmail(_emailServer.AssertEmailSent(), employer, expected);
            else
                _emailServer.AssertNoEmailSent();
        }

        private void CheckCandidates(IList<Employer> employers, int expected)
        {
            new EmailEmployerUsageTask(_emailsCommand, _resumeReportsQuery, _accountReportsQuery, _employersQuery).ExecuteTask();
            if (expected > 0)
            {
                var emails = _emailServer.AssertEmailsSent(employers.Count);
                for (int index = 0; index < employers.Count; index++)
                    AssertEmail(emails[index], employers[index], expected);
            }
            else
            {
                _emailServer.AssertNoEmailSent();
            }
        }

        private void TestSingleCandidate(DateTime lastLoggedInTime, DateTime joinTime, int? eventType, DateTime? eventTime, bool expected)
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            // Create an employer.

            Employer employer = CreateEmployers(lastLoggedInTime, true, 0, 1)[0];

            // Create a member.

            CreateMembers(joinTime, eventType, eventTime, 0, 1);

            // Check.

            CheckSingleCandidate(employer, expected);
        }

        private static void AssertEmail(MockEmail email, ICommunicationUser employer, int newCandidates)
        {
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject(newCandidates));
            email.AssertHtmlViewContains(employer.FirstName);
            email.AssertHtmlViewContains(GetBodySnippet(newCandidates));
        }

        private static string GetSubject(int newCandidates)
        {
            return newCandidates + " fresh candidates";
        }

        private static string GetBodySnippet(int newCandidates)
        {
            return newCandidates + " candidates have uploaded";
        }

        private IList<Employer> CreateEmployers(DateTime lastLoggedIn, bool enabled, int start, int count)
        {
            return CreateEmployers(lastLoggedIn, enabled, EmployerSubRole.Employer, start, count);
        }

        private IList<Employer> CreateRecruiters(DateTime lastLoggedIn, bool enabled, int start, int count)
        {
            return CreateEmployers(lastLoggedIn, enabled, EmployerSubRole.Recruiter, start, count);
        }

        private IList<Employer> CreateEmployers(DateTime lastLoggedIn, bool enabled, EmployerSubRole subRole, int start, int count)
        {
            IList<Employer> employers = new List<Employer>();

            for (int index = start; index < start + count; index++)
            {
                Employer employer = _employerAccountsCommand.CreateTestEmployer(string.Format(EmployerUserIdFormat, index), _organisationsCommand.CreateTestOrganisation(0));
                employer.FirstName = string.Format(EmployerFirstNameFormat, index);
                employer.LastName = EmployerLastName;
                employer.CreatedTime = lastLoggedIn;
                employer.SubRole = subRole;
                employer.IsEnabled = enabled;
                _employerAccountsCommand.UpdateEmployer(employer);

                _userSessionsRepository.CreateUserLogin(new UserLogin {Id = Guid.NewGuid(), UserId = employer.Id, Time = lastLoggedIn, AuthenticationStatus = AuthenticationStatus.Authenticated});
                employers.Add(employer);
            }

            return employers;
        }

        private IList<Member> CreateMembers(DateTime joined, int? eventType, DateTime? eventTime, int start, int count)
        {
            IList<Member> members = new List<Member>();

            for (int index = start; index < start + count; index++)
            {
                Member member = _memberAccountsCommand.CreateTestMember(string.Format(MemberEmailAddressFormat, index));
                member.CreatedTime = joined;
                _memberAccountsCommand.UpdateMember(member);
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);

                if (eventType != null)
                    AddEvent(eventType.Value, eventTime.Value, candidate);

                members.Add(member);
            }

            return members;
        }

        private void AddEvent(int eventType, DateTime eventTime, ICandidate candidate)
        {
            switch (eventType)
            {
                case UploadResume:
                    _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = eventTime });
                    break;

                case EditResume:
                    _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = eventTime });
                    break;

                case ReloadResume:
                    _resumeReportsCommand.CreateResumeEvent(new ResumeReloadEvent { CandidateId = candidate.Id, ResumeId = candidate.ResumeId.Value, Time = eventTime });
                    break;
            }
        }
    }
}
