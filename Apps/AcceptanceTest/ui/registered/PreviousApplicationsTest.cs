using System;
using LinkMe.Apps.Agents.UserEvents.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.UserEvents;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.UI.Registered.Networkers;
using LinkMe.Common.Test;
using NUnit.Framework;

namespace LinkMe.AcceptanceTest.ui.registered
{
	[TestFixture]
	public class PreviousApplicationsTest
        : WebTestFixture
	{
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Container.Current.Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Container.Current.Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Container.Current.Resolve<IJobAdsCommand>();
	    private readonly IUserEventsCommand _userEventsCommand = Container.Current.Resolve<IUserEventsCommand>();

	    private const string EmailAddress = "homer@test.linkme.net.au";
	    private const string EmployerUserId = "monty";

        protected override void SetUp()
        {
            base.SetUp();
            Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
        }

		[Test]
		public void TestDisplay()
		{
            var member = CreateMember();
            ApplyForJob(_candidatesCommand.GetCandidate(member.Id));

            LogIn(member);
            GetPage<PreviousApplications>();
            AssertPageDoesNotContain("You currently have no job applications.");
		}

	    [Test]
        public void TestDisplayNoApplications()
        {
            var member = CreateMember();
            LogIn(member);
            GetPage<PreviousApplications>();
            AssertPageContains("You currently have no job applications.");
        }

        [Test]
        public void TestExternalApplication()
        {
            var member = CreateMember();
            ApplyForExternalJob(member);

            LogIn(member);
            GetPage<PreviousApplications>();
            AssertPageDoesNotContain("You currently have no job applications.");
        }

        [Test]
        public void TestExternalApplicationTwice()
        {
            var member = CreateMember();

            // Applying for the job twice just generates 2 events.

            var jobAd = ApplyForExternalJob(member);
            ApplyForExternalJob(member, jobAd);

            LogIn(member);
            GetPage<PreviousApplications>();
            AssertPageDoesNotContain("You currently have no job applications.");
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(EmailAddress);
        }

        private Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(EmployerUserId, _organisationsCommand.CreateTestOrganisation(0));
        }

        private void ApplyForJob(Candidate candidate)
		{
            var employer = CreateEmployer();
			var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            TestObjectMother.ApplyForTestJobAd(jobAd, candidate, "This is a cover letter");
		}

        private JobAd ApplyForExternalJob(IHasId<Guid> member)
        {
            var employer = CreateEmployer();
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            ApplyForExternalJob(member, jobAd);
            return jobAd;
        }

        private void ApplyForExternalJob(IHasId<Guid> member, JobAdEntry jobAd)
        {
            _userEventsCommand.CreateUserEvent(new ApplyForExternalJobAdUserEvent { ActorId = member.Id, JobAdId = jobAd.Id});
        }
	}
}
