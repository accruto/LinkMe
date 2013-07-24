﻿using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.FlagLists.Web
{
    [TestClass]
    public class WebFlagListsTests
        : FlagListsTests
    {
        [TestMethod]
        public void TestNoJobs()
        {
            var member = CreateMember();
            LogIn(member);

            Get(GetFlagListUrl());
            AssertEmptyJobAds(true);
        }

        [TestMethod]
        public void TestJob()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            var member = CreateMember();
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd.Id);

            LogIn(member);
            Get(GetFlagListUrl());
            AssertEmptyJobAds(false);
            AssertJobAds(true, jobAd);

            Get(GetPartialFlagListUrl());
            AssertJobAds(true, jobAd);
        }

        [TestMethod]
        public void TestSortCreatedTime()
        {
            TestSort(JobAdSortOrder.CreatedTime, (j, i) => j.CreatedTime = DateTime.Now.AddDays(-1 * i));
        }

        [TestMethod]
        public void TestSortSalary()
        {
            TestSort(JobAdSortOrder.Salary, (j, i) => j.Description.Salary = new Salary { LowerBound = 200000 - 10 * i, Currency = Currency.AUD });
        }

        [TestMethod]
        public void TestSortJobType()
        {
            var employer = CreateEmployer();

            // Create 25 of each.

            const int count = 25;
            var jobAds = Enumerable.Range(1, count).Select(index => CreateJobAd(employer, j => j.Description.JobTypes = JobTypes.FullTime)).ToList();
            jobAds.AddRange(Enumerable.Range(1, count).Select(index => CreateJobAd(employer, j => j.Description.JobTypes = JobTypes.PartTime)));
            jobAds.AddRange(Enumerable.Range(1, count).Select(index => CreateJobAd(employer, j => j.Description.JobTypes = JobTypes.Contract)));

            var member = CreateMember();
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id);

            // Default, more recently created appear at top.

            var criteria = new JobAdSearchSortCriteria { SortOrder = JobAdSortOrder.JobType, ReverseSortOrder = false };

            LogIn(member);

            // First 25 should be full time.

            int? page = 1;
            int? items = 25;
            var url = GetFlagListUrl(criteria, page, items);
            var partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.FullTime));

            // Next 25 should be part time.

            page = 2;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.PartTime));

            // Next 25 should be contract.

            page = 3;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.Contract));

            // Change direction.

            criteria.ReverseSortOrder = true;

            // First 25 should be contract.

            page = 1;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.Contract));

            // Next 25 should be part time.

            page = 2;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.PartTime));

            // Next 25 should be full time.

            page = 3;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(false, url, partialUrl, criteria, page, items, 3 * count, jobAds.Where(j => j.Description.JobTypes == JobTypes.FullTime));
        }

        [TestMethod]
        public void TestJobAdStatus()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            var member = CreateMember();
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd.Id);
            LogIn(member);

            // Open.

            Assert.AreEqual(JobAdStatus.Open, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);
            Get(GetFlagListUrl());
            AssertJobAds(true, jobAd);

            Get(GetPartialFlagListUrl());
            AssertJobAds(true, jobAd);

            // Closed.

            _jobAdsCommand.CloseJobAd(jobAd);
            Assert.AreEqual(JobAdStatus.Closed, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);
            Get(GetFlagListUrl());
            AssertJobAds(true, jobAd);

            Get(GetPartialFlagListUrl());
            AssertJobAds(true, jobAd);

            // Deleted.

            _jobAdsCommand.DeleteJobAd(jobAd);
            Assert.AreEqual(JobAdStatus.Deleted, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).Status);
            Get(GetFlagListUrl());
            AssertJobAds(true, jobAd);

            Get(GetPartialFlagListUrl());
            AssertJobAds(true, jobAd);
        }

        private void TestSort(JobAdSortOrder sortOrder, Action<JobAd, int> prepareCreate)
        {
            var employer = CreateEmployer();

            // More recently created job ads appear at the top.

            const int count = 205;
            var jobAds = Enumerable.Range(1, count).Select(index => CreateJobAd(employer, j => prepareCreate(j, index))).ToList();

            var member = CreateMember();
            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdsToFlagList(member, flagList, from j in jobAds select j.Id);

            // Default, more recently created appear at top.

            var criteria = new JobAdSearchSortCriteria { SortOrder = sortOrder, ReverseSortOrder = false };

            LogIn(member);
            int? page = null;
            int? items = null;
            ReadOnlyUrl url;
            ReadOnlyUrl partialUrl;

            if (sortOrder == JobAdSortOrder.CreatedTime)
            {
                url = GetFlagListUrl();
                partialUrl = GetPartialFlagListUrl();
                AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Take(10));
            }

            // Explicit default.

            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Take(10));

            // Explicit default pagination.

            page = 1;
            items = 10;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Take(10));

            // Change page and items.

            page = 3;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Skip((page.Value - 1) * items.Value).Take(items.Value));

            // Change direction.

            criteria.ReverseSortOrder = true;
            jobAds.Reverse();

            page = null;
            items = null;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Take(10));

            // Explicit default pagination.

            page = 1;
            items = 10;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Take(10));

            // Change page and items.

            page = 3;
            items = 25;
            url = GetFlagListUrl(criteria, page, items);
            partialUrl = GetPartialFlagListUrl(criteria, page, items);
            AssertJobAds(true, url, partialUrl, criteria, page, items, count, jobAds.Skip((page.Value - 1) * items.Value).Take(items.Value));
        }

        private void AssertJobAds(bool assertSequential, ReadOnlyUrl url, ReadOnlyUrl partialUrl, JobAdSearchSortCriteria criteria, int? page, int? items, int count, IEnumerable<JobAd> expectedJobAds)
        {
            Get(url);

            // Check elements.

            AssertEmptyJobAds(false);
            AssertSortOrders(criteria.SortOrder, new[] { JobAdSortOrder.CreatedTime, JobAdSortOrder.JobType, JobAdSortOrder.Salary });
            AssertSortOrder(criteria);
            AssertItemsPerPage(items);
            AssertPagination(page, items, count);

            // Check job ads.

            var jobAds = expectedJobAds.ToArray();
            AssertJobAds(assertSequential, jobAds);

            Get(partialUrl);
            AssertJobAds(assertSequential, jobAds);
        }

        private static ReadOnlyUrl GetFlagListUrl()
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/flaglist");
        }

        private static ReadOnlyUrl GetFlagListUrl(JobAdSearchSortCriteria criteria, int? page, int? items)
        {
            return GetUrl(GetFlagListUrl().AsNonReadOnly(), criteria, page, items);
        }

        private static ReadOnlyUrl GetPartialFlagListUrl()
        {
            return new ReadOnlyApplicationUrl(true, "~/members/jobs/flaglist/partial");
        }

        private static ReadOnlyUrl GetPartialFlagListUrl(JobAdSearchSortCriteria criteria, int? page, int? items)
        {
            return GetUrl(GetPartialFlagListUrl().AsNonReadOnly(), criteria, page, items);
        }
    }
}