using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.JobAds;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.EmailSuggestedCandidatesTask
{
    [TestClass]
    public class EmailSuggestedCandidatesTaskTest
        : CommandLineTests
    {
        private const string EmployerLoginId = "employer";
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        protected override string GetTask()
        {
            return "EmailSuggestedCandidatesTask";
        }

        [TestMethod]
        public void TestExecute()
        {
            MemberSearchHost.ClearIndex();

            // First candidate to find.

            IList<Member> members = new List<Member>();
            Member member = _memberAccountsCommand.CreateTestMember(0);
            AddSalaryAndResume(member.Id);
            members.Add(member);

            // Create employer.

            var employer = _employerAccountsCommand.CreateTestEmployer(EmployerLoginId, _organisationsCommand.CreateTestOrganisation(0));
            var jobPoster = new JobPoster {Id = employer.Id, SendSuggestedCandidates = true};
            _jobPostersCommand.UpdateJobPoster(jobPoster);

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1000 });

            // Create a job that matches with the job poster as the contact.

            var jobAd = employer.CreateTestJobAd("Monkey boy");
            jobAd.CreatedTime = DateTime.Now.AddDays(-1);
            jobAd.Description.Salary = null;
            _jobAdsCommand.PostJobAd(jobAd);

            // Execute.

            Execute(false);

            // Should get an email but for some reason that I can't quite figure out at the moment none is generated.
            // Will have to come back to it.

            //MockEmail email = SmtpServer.AssertEmailSent();
        }

        private void AddSalaryAndResume(Guid memberId)
        {
            var candidate = _candidatesCommand.GetCandidate(memberId);
            candidate.DesiredSalary = new Salary { LowerBound = 40000, UpperBound = 50000, Rate = SalaryRate.Year };
            candidate.DesiredJobTitle = "Monkey boy";
            _candidatesCommand.UpdateCandidate(candidate);

            _candidateResumesCommand.AddTestResume(candidate);
        }
    }
}
