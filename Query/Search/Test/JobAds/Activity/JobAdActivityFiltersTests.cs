using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds;
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
    public class JobAdActivityFiltersTests
        : TestClass
    {
        private readonly IJobAdActivityFiltersQuery _jobAdActivityFiltersQuery = Resolve<IJobAdActivityFiltersQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        private readonly IMemberJobAdNotesCommand _memberJobAdNotesCommand = Resolve<IMemberJobAdNotesCommand>();
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();
        private readonly IJobAdBlockListsQuery _jobAdBlockListsQuery = Resolve<IJobAdBlockListsQuery>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();

        [TestMethod]
        public void TestFilterIsFlagged()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // jobAd1 in folder.

            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd1.Id);

            // Filter.

            TestFilter(member, CreateIsFlaggedQuery, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsFlagged()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member in folder.

            var flagList = _jobAdFlagListsQuery.GetFlagList(member);
            _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd.Id);

            // Filter.

            TestFilter(new[] { jobAd.Id }, member, CreateIsFlaggedQuery(true), new[] { jobAd.Id });

            // Block.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _jobAdBlockListsQuery.GetBlockList(member), jobAd.Id);
            TestFilter(new Guid[0], member, CreateIsFlaggedQuery(true), new[] { jobAd.Id });
        }

        [TestMethod]
        public void TestFilterHasNotes()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member1 has notes.

            var note = new MemberJobAdNote { JobAdId = jobAd1.Id, Text = "A note" };
            _memberJobAdNotesCommand.CreateNote(member, note);

            // Filter.

            TestFilter(member, CreateHasNotesQuery, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasNotes()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member has notes.

            var note = new MemberJobAdNote { JobAdId = jobAd.Id, Text = "A note" };
            _memberJobAdNotesCommand.CreateNote(member, note);

            // Filter.

            TestFilter(new[] { jobAd.Id }, member, CreateHasNotesQuery(true), new[] { jobAd.Id });

            // Block.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _jobAdBlockListsQuery.GetBlockList(member), jobAd.Id);
            TestFilter(new Guid[0], member, CreateHasNotesQuery(true), new[] { jobAd.Id });
        }

        [TestMethod]
        public void TestFilterHasViewed()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member1 has been viewed.

            _jobAdViewsCommand.ViewJobAd(member.Id, jobAd1.Id);

            // Filter.

            TestFilter(member, CreateHasViewedQuery, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasViewed()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Job has been viewed.

            _jobAdViewsCommand.ViewJobAd(member.Id, jobAd.Id);

            // Filter.

            TestFilter(new[] { jobAd.Id }, member, CreateHasViewedQuery(true), new[] { jobAd.Id });

            // Block.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _jobAdBlockListsQuery.GetBlockList(member), jobAd.Id);
            TestFilter(new Guid[0], member, CreateHasViewedQuery(true), new[] { jobAd.Id });
        }

        [TestMethod]
        public void TestFilterHasApplied()
        {
            var employer = CreateEmployer(0);
            var jobAd1 = _jobAdsCommand.PostTestJobAd(employer);
            var jobAd2 = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member has applied for jobAd1.

            var application = new InternalApplication { PositionId = jobAd1.Id, ApplicantId = member.Id, CoverLetterText = "Cover letter" };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd1, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd1, application);

            // Filter.

            TestFilter(member, CreateHasAppliedQuery, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasApplied()
        {
            var employer = CreateEmployer(0);
            var jobAd = _jobAdsCommand.PostTestJobAd(employer);
            var member = _membersCommand.CreateTestMember(1);

            // Member has applied for jobAd.

            var application = new InternalApplication { PositionId = jobAd.Id, ApplicantId = member.Id, CoverLetterText = "Cover letter" };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);

            // Filter.

            TestFilter(new[] { jobAd.Id }, member, CreateHasAppliedQuery(true), new[] { jobAd.Id });

            // Block.

            _memberJobAdListsCommand.AddJobAdToBlockList(member, _jobAdBlockListsQuery.GetBlockList(member), jobAd.Id);
            TestFilter(new Guid[0], member, CreateHasAppliedQuery(true), new[] { jobAd.Id });
        }
        
        [TestMethod]
        public void TestFilterAll()
        {
            var member = _membersCommand.CreateTestMember(1);

            // Create members.

            var jobAdIds = new Guid[6];
            jobAdIds[0] = CreateJobAd(member, 0, false, false, false, false).Id;
            jobAdIds[1] = CreateJobAd(member, 1, false, false, false, false).Id;
            jobAdIds[2] = CreateJobAd(member, 2, true, false, false, false).Id;
            jobAdIds[3] = CreateJobAd(member, 3, true, true, false, false).Id;
            jobAdIds[4] = CreateJobAd(member, 4, true, true, true, false).Id;
            jobAdIds[5] = CreateJobAd(member, 5, true, true, true, true).Id;

            // None set.

            var query = new JobAdSearchQuery();
            TestFilter(new[] { jobAdIds[0], jobAdIds[1], jobAdIds[2], jobAdIds[3], jobAdIds[4], jobAdIds[5] }, member, query, jobAdIds);

            // All true.

            query = new JobAdSearchQuery { IsFlagged = true, HasNotes = true, HasViewed = true, HasApplied = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            // All false.

            query = new JobAdSearchQuery { IsFlagged = false, HasNotes = false, HasViewed = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1] }, member, query, jobAdIds);

            // Is flagged.

            query = new JobAdSearchQuery { IsFlagged = true };
            TestFilter(new[] { jobAdIds[2], jobAdIds[3], jobAdIds[4], jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1] }, member, query, jobAdIds);

            // Has notes.

            query = new JobAdSearchQuery { HasNotes = true };
            TestFilter(new[] { jobAdIds[3], jobAdIds[4], jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasNotes = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1], jobAdIds[2] }, member, query, jobAdIds);

            // Has viewed.

            query = new JobAdSearchQuery { HasViewed = true };
            TestFilter(new[] { jobAdIds[4], jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasViewed = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1], jobAdIds[2], jobAdIds[3] }, member, query, jobAdIds);

            // Can contact.

            query = new JobAdSearchQuery { HasApplied = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasApplied = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1], jobAdIds[2], jobAdIds[3], jobAdIds[4] }, member, query, jobAdIds);

            // Some true.

            query = new JobAdSearchQuery { HasApplied = true, HasNotes = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            // Some false.

            query = new JobAdSearchQuery { HasNotes = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[1], jobAdIds[2] }, member, query, jobAdIds);

            // Some true, some false.

            query = new JobAdSearchQuery { HasNotes = true, HasApplied = false };
            TestFilter(new[] { jobAdIds[3], jobAdIds[4] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = true, HasViewed = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[2], jobAdIds[3] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = false, HasViewed = true, HasApplied = true };
            TestFilter(new Guid[0], member, query, jobAdIds);

            // Block some members.

            var blockList = _jobAdBlockListsQuery.GetBlockList(member);
            _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAdIds[1]);
            _memberJobAdListsCommand.AddJobAdToBlockList(member, blockList, jobAdIds[4]);

            // Do it again.

            // None set.

            query = new JobAdSearchQuery();
            TestFilter(new[] { jobAdIds[0], jobAdIds[2], jobAdIds[3], jobAdIds[5] }, member, query, jobAdIds);

            // All true.

            query = new JobAdSearchQuery { IsFlagged = true, HasNotes = true, HasViewed = true, HasApplied = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            // All false.

            query = new JobAdSearchQuery { IsFlagged = false, HasNotes = false, HasViewed = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[0] }, member, query, jobAdIds);

            // Is flagged.

            query = new JobAdSearchQuery { IsFlagged = true };
            TestFilter(new[] { jobAdIds[2], jobAdIds[3], jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = false };
            TestFilter(new[] { jobAdIds[0] }, member, query, jobAdIds);

            // Has notes.

            query = new JobAdSearchQuery { HasNotes = true };
            TestFilter(new[] { jobAdIds[3], jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasNotes = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[2] }, member, query, jobAdIds);

            // Has viewed.

            query = new JobAdSearchQuery { HasViewed = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasViewed = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[2], jobAdIds[3] }, member, query, jobAdIds);

            // Can contact.

            query = new JobAdSearchQuery { HasApplied = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { HasApplied = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[2], jobAdIds[3] }, member, query, jobAdIds);

            // Some true.

            query = new JobAdSearchQuery { HasApplied = true, HasNotes = true };
            TestFilter(new[] { jobAdIds[5] }, member, query, jobAdIds);

            // Some false.

            query = new JobAdSearchQuery { HasNotes = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[0], jobAdIds[2] }, member, query, jobAdIds);

            // Some true, some false.

            query = new JobAdSearchQuery { HasNotes = true, HasApplied = false };
            TestFilter(new[] { jobAdIds[3] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = true, HasViewed = false, HasApplied = false };
            TestFilter(new[] { jobAdIds[2], jobAdIds[3] }, member, query, jobAdIds);

            query = new JobAdSearchQuery { IsFlagged = false, HasViewed = true, HasApplied = true };
            TestFilter(new Guid[0], member, query, jobAdIds);
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });

            return employer;
        }

        private JobAd CreateJobAd(IMember member, int index, bool isFlagged, bool hasNotes, bool hasBeenViewed, bool hasApplied)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(1));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });

            var jobAd = _jobAdsCommand.PostTestJobAd(employer);

            // Is flagged.

            if (isFlagged)
            {
                var flagList = _jobAdFlagListsQuery.GetFlagList(member);
                _memberJobAdListsCommand.AddJobAdToFlagList(member, flagList, jobAd.Id);
            }

            // Has notes.

            if (hasNotes)
            {
                var note = new MemberJobAdNote { JobAdId = jobAd.Id, Text = "A note" };
                _memberJobAdNotesCommand.CreateNote(member, note);
            }

            // Has been viewed.

            if (hasBeenViewed)
                _jobAdViewsCommand.ViewJobAd(member.Id, jobAd.Id);

            // Has applied.

            if (hasApplied)
            {
                var application = new InternalApplication { PositionId = jobAd.Id, ApplicantId = member.Id, CoverLetterText = "Cover letter" };
                _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
                _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
            }

            return jobAd;
        }

        private void TestFilter(IMember member, Func<bool?, JobAdSearchQuery> createQuery, ICollection<Guid> allJobAdIds, ICollection<Guid> isSetJobAdIds, ICollection<Guid> isNotSetJobAdIds)
        {
            TestFilter(allJobAdIds, member, createQuery(null), allJobAdIds);
            TestFilter(isSetJobAdIds, member, createQuery(true), allJobAdIds);
            TestFilter(isNotSetJobAdIds, member, createQuery(false), allJobAdIds);
        }

        private void TestFilter(ICollection<Guid> expectedJobAdIds, IMember member, JobAdSearchQuery query, IEnumerable<Guid> allJobAdIds)
        {
            AssertJobAdIds(expectedJobAdIds, Filter(member, query, allJobAdIds));
        }

        private ICollection<Guid> Filter(IMember member, JobAdSearchQuery query, IEnumerable<Guid> allJobAdIds)
        {
            var includeJobAdIds = _jobAdActivityFiltersQuery.GetIncludeJobAdIds(member, query);
            var jobAdIds = includeJobAdIds != null
                                ? allJobAdIds.Intersect(includeJobAdIds)
                                : allJobAdIds;

            var excludeJobAdIds = _jobAdActivityFiltersQuery.GetExcludeJobAdIds(member, query);
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

        private static JobAdSearchQuery CreateIsFlaggedQuery(bool? value)
        {
            return new JobAdSearchQuery { IsFlagged = value };
        }

        private static JobAdSearchQuery CreateHasNotesQuery(bool? value)
        {
            return new JobAdSearchQuery { HasNotes = value };
        }

        private static JobAdSearchQuery CreateHasViewedQuery(bool? value)
        {
            return new JobAdSearchQuery { HasViewed = value };
        }

        private static JobAdSearchQuery CreateHasAppliedQuery(bool? value)
        {
            return new JobAdSearchQuery { HasApplied = value };
        }
    }
}