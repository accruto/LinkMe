using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.ReportingEmail
{
    [TestClass]
    public class ReportingEmailTest
        : CommandLineTests
    {
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IResumeReportsCommand _resumeReportsCommand = Resolve<IResumeReportsCommand>();

        private const string Subject = "LinkMe Daily Stats";

        protected override string GetTaskGroup()
        {
            return "ReportingEmail";
        }

        [TestMethod]
		public void TestStatsEmail()
		{
            CreateMember(1, DateTime.Now.AddDays(-1));
            
            Execute(true);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, AllStaff);
            email.AssertSubject(Subject);
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">New resumes:</td>\r\n            <td style=\"text-align:left;\">1</td>");
		}

        private void CreateMember(int index, DateTime createdTime)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            member.CreatedTime = createdTime;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = member.Id, ResumeId = candidate.ResumeId.Value, Time = member.CreatedTime });
        }
    }
}
