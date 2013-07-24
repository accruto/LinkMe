using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Roles.Candidates;
using LinkMe.Query.Reports.Roles.Candidates.Commands;
using LinkMe.TaskRunner.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks
{
	[TestClass]
	public class ReportingEmailTaskTest
        : TaskTests
	{
        private const string Subject = "LinkMe Daily Stats";

        private readonly IResumeReportsCommand _resumeReportsCommand = Resolve<IResumeReportsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
		public void TestEmailReminderEvent()
		{
			const double interval = -1;

            _memberAccountsCommand.CreateTestMember(1);

            var two = _memberAccountsCommand.CreateTestMember(2);
            two.CreatedTime = two.CreatedTime.AddDays(-1);
            _memberAccountsCommand.UpdateMember(two);
            var candidate = _candidatesCommand.GetCandidate(two.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _resumeReportsCommand.CreateResumeEvent(new ResumeUploadEvent { CandidateId = two.Id, ResumeId = candidate.ResumeId.Value, Time = two.CreatedTime });
            
            var three = _memberAccountsCommand.CreateTestMember(3);
			three.CreatedTime = DateTime.Now.AddDays(interval - 1);
            _memberAccountsCommand.UpdateMember(three);
            candidate = _candidatesCommand.GetCandidate(three.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _resumeReportsCommand.CreateResumeEvent(new ResumeEditEvent { CandidateId = three.Id, ResumeId = candidate.ResumeId.Value, ResumeCreated = true, Time = DateTime.Now.AddDays(-1) });

            _employerAccountsCommand.CreateTestEmployer(4, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestEmployer(5, _organisationsCommand.CreateTestOrganisation(0));
            var six = _employerAccountsCommand.CreateTestEmployer(6, _organisationsCommand.CreateTestOrganisation(0));
			six.CreatedTime = DateTime.Now.AddDays(interval - 1);
            _employerAccountsCommand.UpdateEmployer(six);

            _employerAccountsCommand.CreateTestRecruiter(7, _organisationsCommand.CreateTestOrganisation(0));
            _employerAccountsCommand.CreateTestRecruiter(8, _organisationsCommand.CreateTestOrganisation(0));
            var nine = _employerAccountsCommand.CreateTestRecruiter(9, _organisationsCommand.CreateTestOrganisation(0));
			nine.CreatedTime = DateTime.Now.AddDays(interval - 1);
            _employerAccountsCommand.UpdateEmployer(nine);

			var reportingTask = new ReportingEmailTask();
			reportingTask.ExecuteTask();

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, AllStaff);
            email.AssertSubject(Subject);
            email.AssertNoAttachments();
            email.AssertHtmlViewContains("<td style=\"text-align:right;\">New resumes:</td>\r\n            <td style=\"text-align:left;\">2</td>");
		}
	}
}
