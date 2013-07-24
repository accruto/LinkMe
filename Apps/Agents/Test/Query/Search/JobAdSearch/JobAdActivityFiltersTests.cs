using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAdSearch
{
    [TestClass]
    public class JobAdActivityFiltersTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Resolve<IJobAdViewsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IExecuteJobAdSearchCommand _executeJobAdSearchCommand = Resolve<IExecuteJobAdSearchCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSearchHost.ClearIndex();
        }

/*        [TestMethod]
        public void TestFilterInFolders()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistCandidateFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            TestFilter(employer, CreateInFolderCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockInFolders()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var folder = _candidateFoldersQuery.GetShortlistCandidateFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateInFolderCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateInFolderCriteria(true));
        }

        [TestMethod]
        public void TestFilterIsFlagged()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var flagList = _candidateFlagListsQuery.GetCandidateFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member1.Id);

            // Filter.

            TestFilter(employer, CreateIsFlaggedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsFlagged()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var flagList = _candidateFlagListsQuery.GetCandidateFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateIsFlaggedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateIsFlaggedCriteria(true));
        }

        [TestMethod]
        public void TestFilterHasNotes()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has notes.

            var note = new CandidateNote { CandidateId = member1.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateCandidateNote(employer, note);

            // Filter.

            TestFilter(employer, CreateHasNotesCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasNotes()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has notes.

            var note = new CandidateNote { CandidateId = member.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateCandidateNote(employer, note);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasNotesCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasNotesCriteria(true));
        }
*/
        [TestMethod]
        public void TestFilterHasViewed()
        {
            var employer = CreateEmployer(1);

            var jobAd1 = CreateJobAd(employer);
            var jobAd2 = CreateJobAd(employer);

            var member = CreateMember(0);

            // JobAd1 has been viewed.

            _jobAdViewsCommand.ViewJobAd(member.Id, jobAd1.Id);

            // Filter.

            TestFilter(member, CreateHasViewedCriteria, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        /*        [TestMethod]
        public void TestFilterBlockHasViewed()
        {
            var member = CreateMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has been viewed.

            _employerMemberViewsCommand.ViewMember(employer, member);

            // Filter.

            TestFilter(new[] { member.Id }, member, CreateHasViewedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasViewedCriteria(true));
        }
*/
        [TestMethod]
        public void TestFilterHasApplied()
        {
            var employer = CreateEmployer(1);

            var jobAd1 = CreateJobAd(employer);
            var jobAd2 = CreateJobAd(employer);

            var member = CreateMember(0);
            var application = new InternalApplication { ApplicantId = member.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd1, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd1, application);

            // Filter.

            TestFilter(member, CreateHasAppliedCriteria, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        /*        [TestMethod]
                public void TestFilterBlockHasAccessed()
                {
                    var member = CreateMember(1);
                    var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
                    _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

                    // Member1 has been contacted.

                    _employerMemberViewsCommand.AccessMember(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

                    // Filter.

                    TestFilter(new[] { member.Id }, employer, CreateHasAccessedCriteria(true));

                    // Block.

                    _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
                    TestFilter(new Guid[0], employer, CreateHasAccessedCriteria(true));
                }

                [TestMethod]
                public void TestFilterAll()
                {
                    var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
                    _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

                    // Create members.

                    var memberIds = new Guid[6];
                    memberIds[0] = CreateMember(employer, 0, false, false, false, false, false).Id;
                    memberIds[1] = CreateMember(employer, 1, true, false, false, false, false).Id;
                    memberIds[2] = CreateMember(employer, 2, true, true, false, false, false).Id;
                    memberIds[3] = CreateMember(employer, 3, true, true, true, false, false).Id;
                    memberIds[4] = CreateMember(employer, 4, true, true, true, true, false).Id;
                    memberIds[5] = CreateMember(employer, 5, true, true, true, true, true).Id;

                    // None set.

                    var criteria = new AdvancedMemberSearchCriteria();
                    TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

                    // All true.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, HasAccessed = true };
                    TestFilter(new[] { memberIds[5] }, employer, criteria);

                    // All false.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[0] }, employer, criteria);

                    // In folder.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true };
                    TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false };
                    TestFilter(new[] { memberIds[0] }, employer, criteria);

                    // Is flagged.

                    criteria = new AdvancedMemberSearchCriteria { IsFlagged = true };
                    TestFilter(new[] { memberIds[2], memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { IsFlagged = false };
                    TestFilter(new[] { memberIds[0], memberIds[1] }, employer, criteria);

                    // Has notes.

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = true };
                    TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = false };
                    TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, criteria);

                    // Has viewed.

                    criteria = new AdvancedMemberSearchCriteria { HasViewed = true };
                    TestFilter(new[] { memberIds[4], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasViewed = false };
                    TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3] }, employer, criteria);

                    // Can contact.

                    criteria = new AdvancedMemberSearchCriteria { HasAccessed = true };
                    TestFilter(new[] { memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasAccessed = false };
                    TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, criteria);

                    // Some true.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, HasNotes = true };
                    TestFilter(new[] { memberIds[3], memberIds[4], memberIds[5] }, employer, criteria);

                    // Some false.

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[0], memberIds[1], memberIds[2] }, employer, criteria);

                    // Some true, some false.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, HasAccessed = false };
                    TestFilter(new[] { memberIds[1], memberIds[2], memberIds[3], memberIds[4] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = true, HasAccessed = false };
                    TestFilter(new[] { memberIds[3], memberIds[4] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, IsFlagged = true, HasViewed = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false, IsFlagged = false, HasViewed = true, HasAccessed = true };
                    TestFilter(new Guid[0], employer, criteria);

                    // Block some members.

                    _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), memberIds[1]);
                    _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetPermanentCandidateBlockList(employer), memberIds[4]);

                    // Do it again.

                    // None set.

                    criteria = new AdvancedMemberSearchCriteria();
                    TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

                    // All true.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, IsFlagged = true, HasNotes = true, HasViewed = true, HasAccessed = true };
                    TestFilter(new[] { memberIds[5] }, employer, criteria);

                    // All false.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false, IsFlagged = false, HasNotes = false, HasViewed = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[0] }, employer, criteria);

                    // In folder.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true };
                    TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false };
                    TestFilter(new[] { memberIds[0] }, employer, criteria);

                    // Is flagged.

                    criteria = new AdvancedMemberSearchCriteria { IsFlagged = true };
                    TestFilter(new[] { memberIds[2], memberIds[3], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { IsFlagged = false };
                    TestFilter(new[] { memberIds[0] }, employer, criteria);

                    // Has notes.

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = true };
                    TestFilter(new[] { memberIds[3], memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = false };
                    TestFilter(new[] { memberIds[0], memberIds[2] }, employer, criteria);

                    // Has viewed.

                    criteria = new AdvancedMemberSearchCriteria { HasViewed = true };
                    TestFilter(new[] { memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasViewed = false };
                    TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, criteria);

                    // Can contact.

                    criteria = new AdvancedMemberSearchCriteria { HasAccessed = true };
                    TestFilter(new[] { memberIds[5] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasAccessed = false };
                    TestFilter(new[] { memberIds[0], memberIds[2], memberIds[3] }, employer, criteria);

                    // Some true.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, HasNotes = true };
                    TestFilter(new[] { memberIds[3], memberIds[5] }, employer, criteria);

                    // Some false.

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[0], memberIds[2] }, employer, criteria);

                    // Some true, some false.

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, HasAccessed = false };
                    TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { HasNotes = true, HasAccessed = false };
                    TestFilter(new[] { memberIds[3] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = true, IsFlagged = true, HasViewed = false, HasAccessed = false };
                    TestFilter(new[] { memberIds[2], memberIds[3] }, employer, criteria);

                    criteria = new AdvancedMemberSearchCriteria { InFolder = false, IsFlagged = false, HasViewed = true, HasAccessed = true };
                    TestFilter(new Guid[0], employer, criteria);
                }
        */

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            return employer;
        }

        private Member CreateMember(int index)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            return member;
        }

        private void TestFilter(IMember member, Func<bool?, JobAdSearchCriteria> createCriteria, ICollection<Guid> allJobAdIds, ICollection<Guid> isSetJobAdIds, ICollection<Guid> isNotSetJobAdIds)
        {
            TestFilter(allJobAdIds, member, createCriteria(null));
            TestFilter(isSetJobAdIds, member, createCriteria(true));
            TestFilter(isNotSetJobAdIds, member, createCriteria(false));
        }

        private void TestFilter(ICollection<Guid> expectedJobAdIds, IMember member, JobAdSearchCriteria criteria)
        {
            AssertJobAdIds(expectedJobAdIds, Search(member, criteria).ToList());
        }

        private IEnumerable<Guid> Search(IMember member, JobAdSearchCriteria criteria)
        {
            return _executeJobAdSearchCommand.Search(member, criteria, null).Results.JobAdIds;
        }

        private static void AssertJobAdIds(ICollection<Guid> expectedJobAdIds, ICollection<Guid> jobAdIds)
        {
            Assert.AreEqual(expectedJobAdIds.Count, jobAdIds.Count);
            foreach (var expectedJobAdId in expectedJobAdIds)
                Assert.IsTrue(jobAdIds.Contains(expectedJobAdId));
        }

        private static JobAdSearchCriteria CreateHasViewedCriteria(bool? value)
        {
            return new JobAdSearchCriteria { HasViewed = value };
        }

        private static JobAdSearchCriteria CreateHasAppliedCriteria(bool? value)
        {
            return new JobAdSearchCriteria { HasApplied = value };
        }

        private JobAd CreateJobAd(IEmployer employer)
        {
            return _jobAdsCommand.PostTestJobAd(employer);
        }
    }
}