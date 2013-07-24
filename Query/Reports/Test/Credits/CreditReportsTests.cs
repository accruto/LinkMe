using System;
using LinkMe.Domain;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Query.Reports.Credits;
using LinkMe.Query.Reports.Credits.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Credits
{
    [TestClass]
    public class CreditReportsTests
        : TestClass
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly ICreditReportsQuery _creditReportsQuery = Resolve<ICreditReportsQuery>();
        private readonly ICreditsRepository _creditsRepository = Resolve<ICreditsRepository>();

        [TestMethod]
        public void TestLimitedCreatedBeforeExpiredBefore()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedBeforeExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 30, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedInBetweenExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 30, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedInBetweenExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 0, 0, 30);
        }

        [TestMethod]
        public void TestLimitedCreatedAfterExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.End.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedBeforeExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 0, 0, 30);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeExpiredBefore()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, null, 0, 0, null, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedInBetweenExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, null, 0, null, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedInBetweenExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, null, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestUnlimitedCreatedAfterExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.End.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestLimitedCreatedBeforeDeallocatedBefore()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedBeforeDeallocatedInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 0, 30, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedInBetweenDeallocatedInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 0, 30, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedInBetweenDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 0, 0, 30);
        }

        [TestMethod]
        public void TestLimitedCreatedAfterDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.End.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestLimitedCreatedBeforeDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 0, 0, 30);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeDeallocatedBefore()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeDeallocatedInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, null, 0, 0, 0, null, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedInBetweenDeallocatedInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, null, 0, 0, null, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedInBetweenDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, null, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestUnlimitedCreatedAfterDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.End.Value.AddDays(1));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUnlimitedCreatedBeforeDeallocatedAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), null, dateRange.Start.Value.AddDays(-7));
            Deallocate(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestUseLimitedCreatedBeforeExpiredBefore()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUseLimitedCreatedBeforeExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 30, 0, 0);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 20, 0, 0, 20, 0, 0);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20, 0, 10, 10, 0, 0);
        }

        [TestMethod]
        public void TestUseLimitedCreatedInBetweenExpiredInBetween()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 30, 0, 0);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 10, 20, 0, 0);
        }

        [TestMethod]
        public void TestUseLimitedCreatedInBetweenExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 0, 0, 0, 30);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 10, 0, 0, 20);

            Use(allocation, 10, dateRange.End.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 0, 30, 20, 0, 0, 10);
        }

        [TestMethod]
        public void TestUseLimitedCreatedAfterExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.End.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);

            Use(allocation, 10, dateRange.End.Value.AddDays(5));
            AssertReport(allocation.OwnerId, dateRange, 0, 0, 0, 0, 0, 0);
        }

        [TestMethod]
        public void TestUseLimitedCreatedBeforeExpiredAfter()
        {
            var dateRange = GetDateRange();
            var allocation = Allocate(Guid.NewGuid(), 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 30, 0, 0, 0, 0, 30);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 20, 0, 0, 0, 0, 20);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20, 0, 10, 0, 0, 10);

            Use(allocation, 5, dateRange.End.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20, 0, 10, 0, 0, 10);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedBeforeExpiredBefore()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredBefore(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedBeforeExpiredInBetween()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedInBetweenExpiredInBetween()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedInBetweenExpiredAfter()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedInBetweenExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedAfterExpiredAfter()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedAfterExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        [TestMethod]
        public void TestMultipleUseLimitedCreatedBeforeExpiredAfter()
        {
            var dateRange = GetDateRange();
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), 40, 0, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, 40, 0, 40, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 40, 0, 0, 0, 40);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(40, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), 40, 0, 0, 0, 0, 40);

            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.Start.Value.AddDays(-1), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(-1), null, 0, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(-1), 0, null, 0, null, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, null, 0, 0, 0, null);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.End.Value.AddDays(1), dateRange.End.Value.AddDays(7), 0, 0, 0, 0, 0, 0);
            TestMultipleUseLimitedCreatedBeforeExpiredAfter(null, dateRange, dateRange.Start.Value.AddDays(-7), dateRange.End.Value.AddDays(7), null, 0, 0, 0, 0, null);
        }

        private void TestMultipleUseLimitedCreatedBeforeExpiredBefore(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.Start.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 0 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 0 + closing);
        }

        private void TestMultipleUseLimitedCreatedBeforeExpiredInBetween(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 30 + opening, 0 + added, 0 + used, 30 + expired, 0 + deallocated, 0 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 20 + opening, 0 + added, 0 + used, 20 + expired, 0 + deallocated, 0 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20 + opening, 0 + added, 10 + used, 10 + expired, 0 + deallocated, 0 + closing);
        }

        private void TestMultipleUseLimitedCreatedInBetweenExpiredInBetween(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(-1));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 30 + added, 0 + used, 30 + expired, 0 + deallocated, 0 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 30 + added, 10 + used, 20 + expired, 0 + deallocated, 0 + closing);
        }

        private void TestMultipleUseLimitedCreatedInBetweenExpiredAfter(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.Start.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 30 + added, 0 + used, 0 + expired, 0 + deallocated, 30 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 30 + added, 10 + used, 0 + expired, 0 + deallocated, 20 + closing);

            Use(allocation, 10, dateRange.End.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 30 + added, 20 + used, 0 + expired, 0 + deallocated, 10 + closing);
        }

        private void TestMultipleUseLimitedCreatedAfterExpiredAfter(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.End.Value.AddDays(1));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 0 + closing);

            Use(allocation, 10, dateRange.End.Value.AddDays(5));
            AssertReport(allocation.OwnerId, dateRange, 0 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 0 + closing);
        }

        private void TestMultipleUseLimitedCreatedBeforeExpiredAfter(int? quantity, DateRange dateRange, DateTime allocateDate, DateTime expireDate, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            var ownerId = Guid.NewGuid();
            var allocation2 = Allocate(ownerId, quantity, allocateDate);
            Expire(allocation2, expireDate);

            var allocation = Allocate(ownerId, 30, dateRange.Start.Value.AddDays(-7));
            Expire(allocation, dateRange.End.Value.AddDays(7));
            AssertReport(allocation.OwnerId, dateRange, 30 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 30 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(-5));
            AssertReport(allocation.OwnerId, dateRange, 20 + opening, 0 + added, 0 + used, 0 + expired, 0 + deallocated, 20 + closing);

            Use(allocation, 10, dateRange.Start.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20 + opening, 0 + added, 10 + used, 0 + expired, 0 + deallocated, 10 + closing);

            Use(allocation, 5, dateRange.End.Value.AddDays(3));
            AssertReport(allocation.OwnerId, dateRange, 20 + opening, 0 + added, 10 + used, 0 + expired, 0 + deallocated, 10 + closing);
        }

        private Allocation Allocate(Guid ownerId, int? quantity, DateTime date)
        {
            var creditId = _creditsQuery.GetCredit<ContactCredit>().Id;
            var allocation = new Allocation
                                 {
                                     Id = Guid.NewGuid(),
                                     OwnerId = ownerId,
                                     CreditId = creditId,
                                     CreatedTime = date,
                                     InitialQuantity = quantity,
                                     RemainingQuantity = quantity
                                 };

            allocation.ExpiryDate = allocation.CreatedTime.AddYears(1);
            _creditsRepository.CreateAllocation(allocation);
            return allocation;
        }

        private void Expire(Allocation allocation, DateTime date)
        {
            allocation.ExpiryDate = date;
            _creditsRepository.UpdateAllocation(allocation);
        }

        private void Deallocate(Allocation allocation, DateTime date)
        {
            allocation.DeallocatedTime = date;
            _creditsRepository.UpdateAllocation(allocation);
        }

        private void Use(Allocation allocation, int quantity, DateTime date)
        {
            for (var index = 0; index < quantity; ++index)
            {
                _creditsRepository.CreateExercisedCredit(new ExercisedCredit
                                                             {
                                                                 Id = Guid.NewGuid(),
                                                                 AdjustedAllocation = true,
                                                                 AllocationId = allocation.Id,
                                                                 CreditId = allocation.CreditId,
                                                                 ExercisedById = allocation.OwnerId,
                                                                 ExercisedOnId = Guid.NewGuid(),
                                                                 Time = date.AddMinutes(index),
                                                             });
                allocation.RemainingQuantity = allocation.RemainingQuantity.Value - 1;
            }

            _creditsRepository.UpdateAllocation(allocation);
        }

        private static DateRange GetDateRange()
        {
            return new DateRange(DateTime.Now.Date.AddDays(-10), DateTime.Now.Date.AddDays(10));
        }

        private void AssertReport(Guid ownerId, DateRange dateRange, int? opening, int? added, int? used, int? expired, int? deallocated, int? closing)
        {
            AssertReport(opening, added, used, expired, deallocated, closing, _creditReportsQuery.GetCreditReport<ContactCredit>(ownerId, dateRange));
        }

        private static void AssertReport(int? opening, int? added, int? used, int? expired, int? deallocated, int? closing, CreditReport report)
        {
            Assert.AreEqual(opening, report.OpeningCredits, "Opening credits wrong.");
            Assert.AreEqual(added, report.CreditsAdded, "Added credits wrong.");
            Assert.AreEqual(used, report.CreditsUsed, "Used credits wrong.");
            Assert.AreEqual(expired, report.CreditsExpired, "Expired credits wrong.");
            Assert.AreEqual(deallocated, report.CreditsDeallocated, "Deallocated credits wrong.");
            Assert.AreEqual(closing, report.ClosingCredits, "Closing credits wrong.");
        }
    }
}