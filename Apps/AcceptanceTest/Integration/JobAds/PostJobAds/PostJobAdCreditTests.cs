using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public abstract class PostJobAdCreditTests
        : PostJobAdsTests
    {
        private Guid? _ownerId;

        [TestInitialize]
        public void TestInitialize()
        {
            _ownerId = null;
        }

        [TestMethod]
        public void TestNoAllocations()
        {
            TestCredits(false, 0, 0, null, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestZeroCredits()
        {
            TestCredits(true, 0, null, null, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestSomeCredits()
        {
            TestCredits(true, 10, null, null);
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            TestCredits(true, null, null, null);
        }

        [TestMethod]
        public void TestSomeCreditsNoApplicantCredits()
        {
            TestCredits(true, 10, 0, null, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestUnlimitedCreditsNoApplicantCredits()
        {
            TestCredits(true, null, 0, null, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestOpenNoAllocations()
        {
            TestCredits(false, 0, 0, JobAdStatus.Open, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestOpenZeroCredits()
        {
            TestCredits(true, 0, null, JobAdStatus.Open, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestOpenSomeCredits()
        {
            TestCredits(true, 10, null, JobAdStatus.Open);
        }

        [TestMethod]
        public void TestOpenUnlimitedCredits()
        {
            TestCredits(true, null, null, JobAdStatus.Open);
        }

        [TestMethod]
        public void TestOpenSomeCreditsNoApplicantCredits()
        {
            TestCredits(true, 10, 0, JobAdStatus.Open, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestOpenUnlimitedCreditsNoApplicantCredits()
        {
            TestCredits(true, null, 0, JobAdStatus.Open, "You need 1 credit to perform this action but you have none available.");
        }

        [TestMethod]
        public void TestDraftNoAllocations()
        {
            TestCredits(false, 0, 0, JobAdStatus.Draft);
        }

        [TestMethod]
        public void TestDraftZeroCredits()
        {
            TestCredits(true, 0, null, JobAdStatus.Draft);
        }

        [TestMethod]
        public void TestDraftSomeCredits()
        {
            TestCredits(true, 10, null, JobAdStatus.Draft);
        }

        [TestMethod]
        public void TestDraftUnlimitedCredits()
        {
            TestCredits(true, null, null, JobAdStatus.Draft);
        }

        [TestMethod]
        public void TestDraftSomeCreditsNoApplicantCredits()
        {
            TestCredits(true, 10, 0, JobAdStatus.Draft);
        }

        [TestMethod]
        public void TestDraftUnlimitedCreditsNoApplicantCredits()
        {
            TestCredits(true, null, 0, JobAdStatus.Draft);
        }

        private void TestCredits(bool allocate, int? jobAdCredits, int? applicantCredits, JobAdStatus? status, params string[] errors)
        {
            var integratorUser = CreateIntegratorUser(0);
            var employer = CreateEmployer(allocate, jobAdCredits, applicantCredits);

            // Post it.

            var jobAd = CreateJobAd(1, j => j.Status = status);
            PostJobAds(integratorUser, employer, true, new[] { jobAd }, errors.Length == 0 ? 1 : 0, 0, 0, errors.Length > 0 ? 1 : 0, errors);

            var expectedStatus = errors.Length != 0
                ? JobAdStatus.Draft
                : status == null
                    ? JobAdStatus.Open
                    : status.Value;

            var expectedCredits = expectedStatus == JobAdStatus.Draft
                ? jobAdCredits
                : jobAdCredits == null
                    ? (int?) null
                    : jobAdCredits.Value - 1;

            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, expectedStatus));
            AssertCredits(allocate, jobAdCredits, expectedCredits);

            // Post it again to update it.

            PostJobAds(integratorUser, employer, true, new[] { jobAd }, 0, errors.Length == 0 ? 1 : 0, 0, errors.Length > 0 ? 1 : 0, errors);
            AssertJobAd(jobAd, AssertJobAd(integratorUser, employer.Id, jobAd.ExternalReferenceId, expectedStatus));
            AssertCredits(allocate, jobAdCredits, expectedCredits);
        }

        private void AssertCredits(bool allocate, int? initialQuantity, int? remainingQuantity)
        {
            if (_ownerId != null)
            {
                var allocations = _allocationsQuery.GetAllocationsByOwnerId<JobAdCredit>(_ownerId.Value);
                if (allocate)
                {
                    Assert.AreEqual(1, allocations.Count);
                    Assert.AreEqual(initialQuantity, allocations[0].InitialQuantity);
                    Assert.AreEqual(remainingQuantity, allocations[0].RemainingQuantity);
                }
                else
                {
                    Assert.AreEqual(0, allocations.Count);
                }
            }
        }

        protected abstract Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits);

        protected void CreateAllocation(Guid ownerId, bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            if (allocate)
            {
                _ownerId = ownerId;
                _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id, InitialQuantity = jobAdCredits, OwnerId = ownerId });
                _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = applicantCredits, OwnerId = ownerId });
            }
            else
            {
                _ownerId = null;
            }
        }
    }
}
