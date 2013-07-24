using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Activity
{
    [TestClass]
    public class JobAdSortFiltersTests
        : TestClass
    {
        private readonly IJobAdSortFiltersQuery _jobAdSortFiltersQuery = Resolve<IJobAdSortFiltersQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();
        private readonly IJobAdFoldersQuery _jobAdFoldersQuery = Resolve<IJobAdFoldersQuery>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        [TestMethod]
        public void TestFilterFlagListId()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // jobAd1 in folder.

            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd1.Id);

            // Filter.

            TestFlaggedFilter(new[] { jobAd1.Id }, member, new[] { jobAd1.Id, jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterBlockListId()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // jobAd1 in blockList.

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAd1.Id);

            // Filter.

            TestBlockedFilter(new[] { jobAd1.Id }, member, new[] { jobAd1.Id, jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterFolderId()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // jobAd1 in folder.

            var folders = _jobAdFoldersQuery.GetFolders(member);
            _memberJobAdListsCommand.AddJobAdToFolder(member, folders[0], jobAd1.Id);

            // Filter.

            TestFolderFilter(new[] { jobAd1.Id }, member, folders[0].Id, new[] { jobAd1.Id, jobAd2.Id });
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });

            return employer;
        }

        private void TestFolderFilter(ICollection<Guid> expectedJobAdIds, IMember member, Guid folderId, IEnumerable<Guid> allJobAdIds)
        {
            AssertJobAdIds(expectedJobAdIds, FolderFilter(member, folderId, allJobAdIds));
        }

        private void TestFlaggedFilter(ICollection<Guid> expectedJobAdIds, IMember member, IEnumerable<Guid> allJobAdIds)
        {
            AssertJobAdIds(expectedJobAdIds, FlaggedFilter(member, allJobAdIds));
        }

        private void TestBlockedFilter(ICollection<Guid> expectedJobAdIds, IMember member, IEnumerable<Guid> allJobAdIds)
        {
            AssertJobAdIds(expectedJobAdIds, BlockedFilter(member, allJobAdIds));
        }

        private ICollection<Guid> FolderFilter(IMember member, Guid folderId, IEnumerable<Guid> allJobAdIds)
        {
            var includeJobAdIds = _jobAdSortFiltersQuery.GetFolderIncludeJobAdIds(member, folderId);
            var jobAdIds = includeJobAdIds != null
                                ? allJobAdIds.Intersect(includeJobAdIds)
                                : allJobAdIds;

            var excludeJobAdIds = _jobAdSortFiltersQuery.GetFolderExcludeJobAdIds(member, folderId);
            jobAdIds = excludeJobAdIds != null
                            ? jobAdIds.Except(excludeJobAdIds)
                            : jobAdIds;

            return jobAdIds.ToArray();
        }

        private ICollection<Guid> FlaggedFilter(IMember member, IEnumerable<Guid> allJobAdIds)
        {
            var includeJobAdIds = _jobAdSortFiltersQuery.GetFlaggedIncludeJobAdIds(member);
            var jobAdIds = includeJobAdIds != null
                                ? allJobAdIds.Intersect(includeJobAdIds)
                                : allJobAdIds;

            var excludeJobAdIds = _jobAdSortFiltersQuery.GetFlaggedExcludeJobAdIds(member);
            jobAdIds = excludeJobAdIds != null
                            ? jobAdIds.Except(excludeJobAdIds)
                            : jobAdIds;

            return jobAdIds.ToArray();
        }

        private ICollection<Guid> BlockedFilter(IMember member, IEnumerable<Guid> allJobAdIds)
        {
            var includeJobAdIds = _jobAdSortFiltersQuery.GetBlockedIncludeJobAdIds(member);
            var jobAdIds = includeJobAdIds != null
                                ? allJobAdIds.Intersect(includeJobAdIds)
                                : allJobAdIds;

            var excludeJobAdIds = _jobAdSortFiltersQuery.GetBlockedExcludeJobAdIds(member);
            jobAdIds = excludeJobAdIds != null
                            ? jobAdIds.Except(excludeJobAdIds)
                            : jobAdIds;

            return jobAdIds.ToArray();
        }

        private static void AssertJobAdIds(ICollection<Guid> expectedJobAdIds, ICollection<Guid> jobAdIds)
        {
            Assert.AreEqual(expectedJobAdIds.Count, jobAdIds.Count);
            foreach (var expectedJobAdId in expectedJobAdIds)
                Assert.IsTrue(jobAdIds.Contains(expectedJobAdId));
        }
    }
}