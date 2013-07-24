using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ApplicantCredits
{
    [TestClass]
    public abstract class ViewApplicantCreditsUsageTests
        : ViewCreditsUsageTests
    {
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            ClearSearchIndexes();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestApplyNoAllocations()
        {
            TestApply(NoAllocate, false);
        }

        [TestMethod]
        public void TestApplyNoCredits()
        {
            TestApply(NoCredits, false);
        }

        [TestMethod]
        public void TestApplySomeCredits()
        {
            TestApply(SomeCredits, true);
        }

        [TestMethod]
        public void TestApplyUnlimitedCredits()
        {
            TestApply(UnlimitedCredits, true);
        }

        private static void NoAllocate(ICreditOwner owner)
        {
        }

        private void NoCredits(ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = 0 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null });
        }

        private void SomeCredits(ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = 10 });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null });
        }

        private void UnlimitedCredits(ICreditOwner owner)
        {
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = null });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = owner.Id, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = null });
        }

        private void TestApply(Action<ICreditOwner> allocate, bool shouldExerciseCredit)
        {
            // Create everyone.

            var member = CreateMember(0);

            var employer = CreateEmployer(member);
            var owner = GetCreditOwner(employer);
            allocate(owner);

            var administrator = CreateAdministrator();

            // Check before.

            var applicantAllocation = GetAllocation<ApplicantCredit>(owner.Id);
            var jobAdAllocation = GetAllocation<JobAdCredit>(owner.Id);
            AssertCredits<ApplicantCredit>(administrator, employer, member, owner, false, applicantAllocation, applicantAllocation == null ? new Allocation[0] : new[] { applicantAllocation, jobAdAllocation });

            // Apply for the job.

            Apply(employer, member);

            // Check after.

            applicantAllocation = GetAllocation<ApplicantCredit>(owner.Id);
            jobAdAllocation = GetAllocation<JobAdCredit>(owner.Id);
            AssertCredits<ApplicantCredit>(administrator, employer, member, owner, shouldExerciseCredit, applicantAllocation, applicantAllocation == null ? new Allocation[0] : new[] { applicantAllocation, jobAdAllocation });
        }

        protected abstract Employer CreateEmployer(Member member);
        protected abstract ICreditOwner GetCreditOwner(Employer employer);

        private void Apply(IEmployer employer, IUser member)
        {
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Apply for the job.

            LogIn(member);
            Post(new ReadOnlyApplicationUrl(true, "~/members/jobs/api/" + jobAd.Id + "/applywithprofile"));
            LogOut();
        }
    }
}