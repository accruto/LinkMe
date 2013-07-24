using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public class CloseJobAdsTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdsQuery _jobAdsQuery = Resolve<IJobAdsQuery>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUnverifiedEmployer()
        {
            var organisation = CreateOrganisation(0, false);
            var employer = CreateEmployer(0, organisation);
            TestJobAd(employer.Id, employer);
        }

        [TestMethod]
        public void TestUnverifiedOrganisation()
        {
            var organisation = CreateOrganisation(0, false);
            var employer = CreateEmployer(0, organisation);
            TestJobAd(organisation.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedEmployer()
        {
            var organisation = CreateOrganisation(0, true);
            var employer = CreateEmployer(0, organisation);
            TestJobAd(employer.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedOrganisation()
        {
            var organisation = CreateOrganisation(0, true);
            var employer = CreateEmployer(0, organisation);
            TestJobAd(organisation.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedParentOrganisation()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = CreateOrganisation(1, parentOrganisation);
            var employer = CreateEmployer(0, organisation);
            TestJobAd(parentOrganisation.Id, employer);
        }

        [TestMethod]
        public void TestUnverifiedEmployerJobAds()
        {
            var organisation = CreateOrganisation(0, false);
            var employer = CreateEmployer(0, organisation);
            TestJobAds(employer.Id, employer);
        }

        [TestMethod]
        public void TestUnverifiedOrganisationJobAds()
        {
            var organisation = CreateOrganisation(0, false);
            var employer = CreateEmployer(0, organisation);
            TestJobAds(organisation.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedEmployerJobAds()
        {
            var organisation = CreateOrganisation(0, true);
            var employer = CreateEmployer(0, organisation);
            TestJobAds(employer.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedOrganisationJobAds()
        {
            var organisation = CreateOrganisation(0, true);
            var employer = CreateEmployer(0, organisation);
            TestJobAds(organisation.Id, employer);
        }

        [TestMethod]
        public void TestVerifiedParentOrganisationJobAds()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = CreateOrganisation(1, parentOrganisation);
            var employer = CreateEmployer(0, organisation);
            TestJobAds(parentOrganisation.Id, employer);
        }

        [TestMethod]
        public void TestUnverifiedEmployers()
        {
            var organisation = CreateOrganisation(0, false);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);
            TestEmployers(employer1.Id, employer1, employer2);
        }

        [TestMethod]
        public void TestUnverifiedOrganisationEmployers()
        {
            var organisation = CreateOrganisation(0, false);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);
            TestEmployers(organisation.Id, employer1, employer2);
        }

        [TestMethod]
        public void TestVerifiedEmployers()
        {
            var organisation = CreateOrganisation(0, true);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);
            TestEmployers(employer1.Id, employer1, employer2);
        }

        [TestMethod]
        public void TestVerifiedOrganisationEmployers()
        {
            var organisation = CreateOrganisation(0, true);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);
            TestEmployers(organisation.Id, employer1, employer2);
        }

        [TestMethod]
        public void TestVerifiedParentOrganisationEmployers()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = CreateOrganisation(1, parentOrganisation);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);
            TestEmployers(parentOrganisation.Id, employer1, employer2);
        }

        [TestMethod]
        public void TestEmployerHasCredits()
        {
            var organisation = CreateOrganisation(0, true);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);

            // Allocate to parent organisation and to second employer.

            Allocate(organisation.Id, 2, null);
            Allocate(employer2.Id, 2, null);

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer1);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer2);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Submit(member.Id, jobAd1);

            AssertAllocations(organisation.Id, 1, null);
            AssertAllocations(employer2.Id, 2, null);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(1);
            Submit(member.Id, jobAd1);

            AssertAllocations(organisation.Id, 0);
            AssertAllocations(employer2.Id, 2, null);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);

            // This job ad remains open because employer2 has their own credits.

            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);
        }

        [TestMethod]
        public void TestOrganisationHasCredits()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = CreateOrganisation(1, parentOrganisation);
            var employer1 = CreateEmployer(1, organisation);
            var employer2 = CreateEmployer(2, organisation);

            // Allocate to organisations.

            Allocate(parentOrganisation.Id, 2, null);
            Allocate(organisation.Id, 2, null);

            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer1);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer2);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Submit(member.Id, jobAd1);

            AssertAllocations(parentOrganisation.Id, 2, null);
            AssertAllocations(organisation.Id, 1, null);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(1);
            Submit(member.Id, jobAd1);

            AssertAllocations(parentOrganisation.Id, 2, null);
            AssertAllocations(organisation.Id, 0);

            // These job ads remain open because parent organisation has their own credits.

            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(2);
            Submit(member.Id, jobAd1);

            AssertAllocations(parentOrganisation.Id, 1, null);
            AssertAllocations(organisation.Id, 0);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(3);
            Submit(member.Id, jobAd1);

            AssertAllocations(parentOrganisation.Id, 0);
            AssertAllocations(organisation.Id, 0);

            // All credits gone now.

            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);
        }

        private void TestJobAd(Guid ownerId, IEmployer employer)
        {
            Allocate(ownerId, 2, null);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Submit(member.Id, jobAd);

            AssertAllocations(ownerId, 1, null);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(1);
            Submit(member.Id, jobAd);

            AssertAllocations(ownerId, 0);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);
        }

        private void TestJobAds(Guid ownerId, IEmployer employer)
        {
            Allocate(ownerId, 2, null);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Submit(member.Id, jobAd1);

            AssertAllocations(ownerId, 1, null);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(1);
            Submit(member.Id, jobAd2);

            AssertAllocations(ownerId, 0);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);
        }

        private void TestEmployers(Guid ownerId, IEmployer employer1, IEmployer employer2)
        {
            Allocate(ownerId, 2, null);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer1);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer2);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Submit(member.Id, jobAd1);

            AssertAllocations(ownerId, 1, null);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);

            // Submit again.

            member = _memberAccountsCommand.CreateTestMember(1);
            Submit(member.Id, jobAd1);

            AssertAllocations(ownerId, 0);
            Assert.AreEqual(ownerId == employer2.Id ? JobAdStatus.Open : JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd1.Id).Status);
            Assert.AreEqual(ownerId == employer1.Id ? JobAdStatus.Open : JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd2.Id).Status);
        }

        private void AssertAllocations(Guid ownerId, int? applicantCredits, int? jobAdCredits)
        {
            var allocations = _allocationsQuery.GetActiveAllocations<ApplicantCredit>(ownerId);
            Assert.AreEqual(1, allocations.Count);
            Assert.AreEqual(applicantCredits, allocations[0].RemainingQuantity);

            allocations = _allocationsQuery.GetActiveAllocations<JobAdCredit>(ownerId);
            Assert.AreEqual(1, allocations.Count);
            Assert.AreEqual(jobAdCredits, allocations[0].RemainingQuantity);
        }

        private void AssertAllocations(Guid ownerId, int? applicantCredits)
        {
            var allocations = _allocationsQuery.GetActiveAllocations<ApplicantCredit>(ownerId);
            Assert.AreEqual(1, allocations.Count);
            Assert.AreEqual(applicantCredits, allocations[0].RemainingQuantity);

            allocations = _allocationsQuery.GetActiveAllocations<JobAdCredit>(ownerId);
            Assert.AreEqual(0, allocations.Count);
        }

        private void Allocate(Guid ownerId, int? applicantCredits, int? jobAdCredits)
        {
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, OwnerId = ownerId, InitialQuantity = applicantCredits });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, OwnerId = ownerId, InitialQuantity = jobAdCredits });
        }

        private Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, organisation);
        }

        private Organisation CreateOrganisation(int index, bool verified)
        {
            return verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(index)
                : _organisationsCommand.CreateTestOrganisation(index);
        }

        private Organisation CreateOrganisation(int index, Organisation parentOrganisation)
        {
            return _organisationsCommand.CreateTestVerifiedOrganisation(index, parentOrganisation, Guid.NewGuid());
        }

        private void Submit(Guid memberId, JobAdEntry jobAd)
        {
            var application = new InternalApplication { ApplicantId = memberId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
        }
    }
}
