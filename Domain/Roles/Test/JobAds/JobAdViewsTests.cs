using System;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.JobAds
{
    [TestClass]
    public class JobAdViewsTests
        : TestClass
    {
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();
        private readonly IJobAdViewsQuery _jobAdViewsQuery = Resolve<IJobAdViewsQuery>();

        [TestMethod]
        public void TestNoViewings()
        {
            var jobAdId = Guid.NewGuid();

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedCount(jobAdId));
            Assert.AreEqual(0, _jobAdViewsQuery.GetDistinctViewedCount(jobAdId));

            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(Guid.NewGuid(), jobAdId));

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(Guid.NewGuid(), new[] { jobAdId }).Count);
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(Guid.NewGuid()).Count);
        }

        [TestMethod]
        public void TestViewings()
        {
            var viewerId1 = Guid.NewGuid();
            var viewerId2 = Guid.NewGuid();
            var jobAdId = Guid.NewGuid();

            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);

            Assert.AreEqual(1, _jobAdViewsQuery.GetViewedCount(jobAdId));
            Assert.AreEqual(1, _jobAdViewsQuery.GetDistinctViewedCount(jobAdId));

            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(viewerId1, jobAdId));
            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(viewerId2, jobAdId));

            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId1, new[] { jobAdId })));
            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId1)));

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId2, new[] { jobAdId }).Count);
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId2).Count);
        }

        [TestMethod]
        public void TestAnonymousViewings()
        {
            var viewerId1 = (Guid?)null;
            var viewerId2 = Guid.NewGuid();
            var jobAdId = Guid.NewGuid();

            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);

            Assert.AreEqual(1, _jobAdViewsQuery.GetViewedCount(jobAdId));
            Assert.AreEqual(1, _jobAdViewsQuery.GetDistinctViewedCount(jobAdId));

            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(viewerId2, jobAdId));

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId2, new[] { jobAdId }).Count);
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId2).Count);
        }

        [TestMethod]
        public void TestMultipleViewings()
        {
            var viewerId1 = Guid.NewGuid();
            var viewerId2 = Guid.NewGuid();
            var viewerId3 = Guid.NewGuid();
            var jobAdId = Guid.NewGuid();

            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAdId);

            Assert.AreEqual(5, _jobAdViewsQuery.GetViewedCount(jobAdId));
            Assert.AreEqual(2, _jobAdViewsQuery.GetDistinctViewedCount(jobAdId));

            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(viewerId1, jobAdId));
            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(viewerId2, jobAdId));
            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(viewerId3, jobAdId));

            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId1, new[] { jobAdId })));
            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId1)));

            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId2, new[] { jobAdId })));
            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId2)));

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId3, new[] { jobAdId }).Count);
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId3).Count);
        }

        [TestMethod]
        public void TestMultipleAnonymousViewings()
        {
            var viewerId1 = (Guid?)null;
            var viewerId2 = Guid.NewGuid();
            var viewerId3 = Guid.NewGuid();
            var jobAdId = Guid.NewGuid();

            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId1, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAdId);
            _jobAdViewsCommand.ViewJobAd(viewerId2, jobAdId);

            Assert.AreEqual(5, _jobAdViewsQuery.GetViewedCount(jobAdId));
            Assert.AreEqual(2, _jobAdViewsQuery.GetDistinctViewedCount(jobAdId));

            Assert.IsTrue(_jobAdViewsQuery.HasViewedJobAd(viewerId2, jobAdId));
            Assert.IsFalse(_jobAdViewsQuery.HasViewedJobAd(viewerId3, jobAdId));

            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId2, new[] { jobAdId })));
            Assert.IsTrue(new[] { jobAdId }.CollectionEqual(_jobAdViewsQuery.GetViewedJobAdIds(viewerId2)));

            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId3, new[] { jobAdId }).Count);
            Assert.AreEqual(0, _jobAdViewsQuery.GetViewedJobAdIds(viewerId3).Count);
        }
    }
}
