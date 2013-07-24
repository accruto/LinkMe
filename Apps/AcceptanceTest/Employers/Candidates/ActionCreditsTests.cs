using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Reports.Users.Employers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates
{
    [TestClass]
    public abstract class ActionCreditsTests
        : ApiTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery = Resolve<IEmployerMemberAccessReportsQuery>();
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IEmployerCreditsCommand _employerCreditsCommand = Resolve<IEmployerCreditsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery = Resolve<IExercisedCreditsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();

        [TestInitialize]
        public void ActionCreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestSingleMemberAnonymous()
        {
            TestAction(1, false, new AnonymousCreditInfo(_employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoAllocations()
        {
            TestAction(1, false, new NoAllocationsCreditInfo(false, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoAllocations()
        {
            TestAction(1, true, new NoAllocationsCreditInfo(false, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCredits()
        {
            TestAction(1, false, new NoCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCredits()
        {
            TestAction(1, true, new NoCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsExpired()
        {
            TestAction(1, false, new NoCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsExpired()
        {
            TestAction(1, true, new NoCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsUsedCredit()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsUsedCredit()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCredits()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCredits()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsExpired()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsExpired()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsUsedCredit()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsUsedCredit()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedUnlimitedCredits()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedUnlimitedCredits()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedUnlimitedCreditsExpired()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedUnlimitedCreditsExpired()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoAllocations()
        {
            TestAction(1, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoAllocations()
        {
            TestAction(1, true, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCredits()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCredits()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpired()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpired()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsUsedCredit()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsUsedCredit()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCredits()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCredits()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpired()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpired()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsUsedCredit()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsUsedCredit()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCredits()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCredits()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCreditsExpired()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCreditsExpired()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsExpiredOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsExpiredOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedUnlimitedCreditsOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedUnlimitedCreditsOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberUnverifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantUnverifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoAllocationsOrganisation()
        {
            TestAction(1, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoAllocationsOrganisation()
        {
            TestAction(1, true, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpiredOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpiredOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCreditsOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCreditsOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoAllocationsParentOrganisation()
        {
            TestAction(1, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoAllocationsParentOrganisation()
        {
            TestAction(1, true, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsParentOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsParentOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpiredParentOrganisation()
        {
            TestAction(1, false, new NoCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpiredParentOrganisation()
        {
            TestAction(1, true, new NoCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsUsedCreditParentOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsUsedCreditParentOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedNoCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(1, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedNoCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(1, true, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsParentOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsParentOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpiredParentOrganisation()
        {
            TestAction(1, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpiredParentOrganisation()
        {
            TestAction(1, true, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsUsedCreditParentOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsUsedCreditParentOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedSomeCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(1, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedSomeCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(1, true, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCreditsParentOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCreditsParentOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleMemberVerifiedUnlimitedCreditsExpiredParentOrganisation()
        {
            TestAction(1, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSingleApplicantVerifiedUnlimitedCreditsExpiredParentOrganisation()
        {
            TestAction(1, true, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersAnonymous()
        {
            TestAction(3, false, new AnonymousCreditInfo(_employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoAllocations()
        {
            TestAction(3, false, new NoAllocationsCreditInfo(false, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCredits()
        {
            TestAction(3, false, new NoCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsExpired()
        {
            TestAction(3, false, new NoCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsUsedCredit()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCredits()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsExpired()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsUsedCredit()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedUnlimitedCredits()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedUnlimitedCreditsExpired()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoAllocations()
        {
            TestAction(3, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCredits()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpired()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsUsedCredit()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpiredUsedCredit()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCredits()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpired()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsUsedCredit()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpiredUsedCredit()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCredits()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCreditsExpired()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsExpiredOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedUnlimitedCreditsOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersUnverifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(false, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoAllocationsOrganisation()
        {
            TestAction(3, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpiredOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsUsedCreditOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpiredOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsUsedCreditOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpiredUsedCreditOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCreditsOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCreditsExpiredOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoAllocationsParentOrganisation()
        {
            TestAction(3, false, new NoAllocationsCreditInfo(true, _employerAccountsCommand, _organisationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsParentOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpiredParentOrganisation()
        {
            TestAction(3, false, new NoCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsUsedCreditParentOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedNoCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(3, false, new NoCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsParentOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpiredParentOrganisation()
        {
            TestAction(3, false, new SomeCreditsCreditInfo(10, true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsUsedCreditParentOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedSomeCreditsExpiredUsedCreditParentOrganisation()
        {
            TestAction(3, false, new SomeCreditsUsedCreditCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand, _employerCreditsCommand, _employerMemberViewsQuery));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCreditsParentOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, false, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestMultipleMembersVerifiedUnlimitedCreditsExpiredParentOrganisation()
        {
            TestAction(3, false, new UnlimitedCreditsCreditInfo(true, true, CreditAllocation.ParentOrganisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        private void TestAction(int memberCount, bool areApplicants, CreditInfo creditInfo)
        {
            var members = new Member[memberCount];
            for (var index = 0; index < memberCount; ++index)
                members[index] = CreateMember(index);

            var employer = creditInfo.CreateEmployer(members);
            var initialCredits = employer == null ? 0 : GetCredits(creditInfo, employer);

            if (employer != null && areApplicants)
            {
                // Create a job and have the members apply.

                var jobAd = _jobAdsCommand.PostTestJobAd(employer);
                foreach (var member in members)
                {
                    var application = new InternalApplication { ApplicantId = member.Id };
                    _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                    _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
                }
            }

            _emailServer.ClearEmails();
            if (employer != null)
                LogIn(employer);

            // Perform the action.

            var reason = PerformAction(areApplicants, creditInfo, employer != null, employer, members);

            var finalCredits = employer == null ? 0 : GetCredits(creditInfo, employer);
            AssertCredits(areApplicants, creditInfo, reason, initialCredits, finalCredits, memberCount);
            AssertContacts(areApplicants, creditInfo, employer, members, reason, initialCredits, finalCredits);
            AssertAction();
        }

        protected abstract MemberAccessReason? PerformAction(bool isApplicant, CreditInfo creditInfo, bool isLoggedIn, Employer employer, Member[] members);

        protected virtual void AssertAction()
        {
        }

        protected virtual Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        private int? GetCredits(CreditInfo creditInfo, IEmployer employer)
        {
            if (creditInfo.CreditAllocation == null)
                return 0;

            var credits = 0;

            Guid ownerId;
            switch (creditInfo.CreditAllocation.Value)
            {
                case CreditAllocation.ParentOrganisation:
                    ownerId = ((Organisation) employer.Organisation).ParentId.Value;
                    break;

                case CreditAllocation.Organisation:
                    ownerId = employer.Organisation.Id;
                    break;

                default:
                    ownerId = employer.Id;
                    break;
            }

            var allocations = _allocationsQuery.GetActiveAllocations(ownerId);
            foreach (var allocation in allocations)
            {
                if (allocation.IsUnlimited)
                    return null;
                credits += allocation.RemainingQuantity.Value;
            }

            return credits;
        }

        private static void AssertCredits(bool areApplicants, CreditInfo creditInfo, MemberAccessReason? reason, int? initialCredits, int? finalCredits, int? creditsUsed)
        {
            if (reason != null && !areApplicants && creditInfo.ShouldUseCredit)
            {
                if (initialCredits == finalCredits)
                    Assert.Fail("A credit should have been used.");
                if (initialCredits == null || finalCredits == null)
                    Assert.Fail("No credit should have been used for unlimited credits.");
                if (initialCredits.Value == 0)
                    Assert.Fail("No credit should have been used when non were allocated.");
                if (finalCredits.Value != initialCredits.Value - creditsUsed.Value)
                    Assert.Fail("A single credit should have been used but it appears that more have been.");
            }
            else
            {
                if (initialCredits != finalCredits)
                    Assert.Fail("No credit should have been used.");
            }
        }

        private void AssertContacts(bool areApplicants, CreditInfo creditInfo, IEmployer employer, IEnumerable<Member> members, MemberAccessReason? reason, int? initialCredits, int? finalCredits)
        {
            if (reason != null)
            {
                Assert.AreEqual(true, employer != null);

                foreach (var member in members)
                {
                    var memberId = member.Id;
                    Assert.AreEqual(1, _employerMemberAccessReportsQuery.GetMemberAccesses(memberId, reason.Value));
                    if (employer != null)
                        AssertContacts(areApplicants, creditInfo, employer, memberId, reason.Value, initialCredits, finalCredits, _employerViewsRepository.GetMemberAccesses(employer.Id, memberId));
                }

                if (employer != null)
                    AssertContacts(areApplicants, creditInfo, employer, members, reason.Value, initialCredits, finalCredits);
            }
            else
            {
                foreach (var member in members)
                {
                    var memberId = member.Id;
                    Assert.AreEqual(0, _employerMemberAccessReportsQuery.GetMemberAccesses(memberId));
                    if (employer != null)
                        Assert.AreEqual(0, _employerViewsRepository.GetMemberAccesses(employer.Id, memberId).Count);
                }
            }
        }

        private void AssertContacts(bool areApplicants, CreditInfo creditInfo, IEmployer employer, Guid memberId, MemberAccessReason reason, int? initialCredits, int? finalCredits, ICollection<MemberAccess> accesses)
        {
            Assert.AreEqual(1, accesses.Count);
            foreach (var access in accesses)
                AssertContacts(access, areApplicants, creditInfo, employer, memberId, reason, initialCredits, finalCredits);
        }

        private void AssertContacts(bool areApplicants, CreditInfo creditInfo, IEmployer employer, IEnumerable<Member> members, MemberAccessReason reason, int? initialCredits, int? finalCredits)
        {
            foreach (var member in members)
            {
                var memberId = member.Id;
                var access = _employerViewsRepository.GetMemberAccesses(employer.Id, memberId)[0];
                AssertContacts(access, areApplicants, creditInfo, employer, memberId, reason, initialCredits, finalCredits);
            }
        }

        private void AssertContacts(MemberAccess access, bool areApplicants, CreditInfo creditInfo, IEmployer employer, Guid memberId, MemberAccessReason reason, int? initialCredits, int? finalCredits)
        {
            Assert.AreEqual(employer.Id, access.EmployerId);
            Assert.AreEqual(memberId, access.MemberId);
            Assert.AreEqual(reason, access.Reason);

            if (initialCredits == null)
            {
                AssertExercisedCredit(creditInfo, employer, memberId, access);
            }
            else
            {
                if (initialCredits.Value == 0)
                {
                    Assert.AreEqual(null, access.ExercisedCreditId);
                }
                else
                {
                    if (initialCredits.Value != finalCredits.Value)
                    {
                        AssertExercisedCredit(creditInfo, employer, memberId, access);
                    }
                    else
                    {
                        // No credit used.

                        if (areApplicants && !creditInfo.HasUsedCredit)
                            AssertExercisedCredit(creditInfo, employer, memberId, access);
                        else
                            Assert.AreEqual(null, access.ExercisedCreditId);
                    }
                }
            }
        }

        private void AssertExercisedCredit(CreditInfo creditInfo, IEmployer employer, Guid memberId, MemberAccess access)
        {
            Assert.IsNotNull(access.ExercisedCreditId);
            var exercisedCredit = _exercisedCreditsQuery.GetExercisedCredit(access.ExercisedCreditId.Value);

            var allocationId = GetAllocationId(creditInfo, employer);
            Assert.IsNotNull(allocationId);
            Assert.AreEqual(allocationId.Value, exercisedCredit.AllocationId);
            Assert.AreEqual(employer.Id, exercisedCredit.ExercisedById);
            Assert.AreEqual(memberId, exercisedCredit.ExercisedOnId);
        }

        private Guid? GetAllocationId(CreditInfo creditInfo, IEmployer employer)
        {
            Guid ownerId;
            switch (creditInfo.CreditAllocation.Value)
            {
                case CreditAllocation.ParentOrganisation:
                    ownerId = ((Organisation)employer.Organisation).ParentId.Value;
                    break;

                case CreditAllocation.Organisation:
                    ownerId = employer.Organisation.Id;
                    break;

                default:
                    ownerId = employer.Id;
                    break;
            }

            var allocations = _allocationsQuery.GetActiveAllocations(ownerId);
            return allocations.Count == 0 ? (Guid?)null : allocations[0].Id;
        }
    }
}
