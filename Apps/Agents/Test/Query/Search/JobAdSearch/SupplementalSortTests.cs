using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.JobAdsSupplemental;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using NUnit.Framework;

namespace LinkMe.Apps.Agents.Test.Query.Search.JobAds
{
    [TestFixture]
    public class SupplementalSortTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Container.Current.Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Container.Current.Resolve<ICandidatesCommand>();
        private readonly IEmployersCommand _employersCommand = Container.Current.Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Container.Current.Resolve<IOrganisationsCommand>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Container.Current.Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Container.Current.Resolve<ICandidateListsCommand>();
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Container.Current.Resolve<ICandidateFlagListsQuery>();
        private readonly ICandidateNotesCommand _candidateNotesCommand = Container.Current.Resolve<ICandidateNotesCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Container.Current.Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Container.Current.Resolve<IEmployerMemberViewsQuery>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Container.Current.Resolve<ICandidateBlockListsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Container.Current.Resolve<IAllocationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Container.Current.Resolve<ICreditsQuery>();
        private readonly IExecuteMemberSearchCommand _executeMemberSearchCommand = Container.Current.Resolve<IExecuteMemberSearchCommand>();
        private readonly IUpdateMemberSearchCommand _updateMemberSearchCommand = Container.Current.Resolve<IUpdateMemberSearchCommand>();

        private readonly IIndustriesQuery _industriesQuery = Container.Current.Resolve<IIndustriesQuery>();
        private readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Container.Current.Resolve<IJobAdsCommand>();
        private readonly IJobAdViewsCommand _jobAdViewsCommand = Container.Current.Resolve<IJobAdViewsCommand>();
        private Country _australia;

        static SupplementalSortTests()
        {
            JobAdSupplementalSearchHost.Start();
        }

        [SetUp]
        public void SetUp()
        {
            Container.Current.Resolve<IDbConnectionFactory>().DeleteAllTestData();
            JobAdSupplementalSearchHost.ClearIndex();
            _australia = _locationQuery.GetCountry("Australia");
        }

        [Test]
        public void TestFilterFolderId()
        {
            var member1 = CreateJobAd(1);
            var member2 = CreateJobAd(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistCandidateFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            var allMemberIds = new[] { member1.Id, member2.Id };
            TestFilter(allMemberIds, employer, new AdvancedMemberSearchCriteria());
            TestFilter(new[] { member1.Id }, employer, new AdvancedMemberSearchCriteria { FolderId = folder.Id });
        }

        [Test]
        public void TestFilterBlockListId()
        {
            var member1 = CreateJobAd(1);
            var member2 = CreateJobAd(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in blockList.

            var blockList = _candidateBlockListsQuery.GetPermanentCandidateBlockList(employer);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member1.Id);

            // Filter.

            TestFilter(new[] { member2.Id }, employer, new AdvancedMemberSearchCriteria());
            TestFilter(new[] { member1.Id }, employer, new AdvancedMemberSearchCriteria { BlockListId = blockList.Id });
        }

        [Test]
        public void TestFilterBlockInFolders()
        {
            var member = CreateJobAd(1);
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

        [Test]
        public void TestFilterIsFlagged()
        {
            var member1 = CreateJobAd(1);
            var member2 = CreateJobAd(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var flagList = _candidateFlagListsQuery.GetCandidateFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member1.Id);

            // Filter.

            TestFilter(employer, CreateIsFlaggedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [Test]
        public void TestFilterBlockIsFlagged()
        {
            var member = CreateJobAd(1);
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

        [Test]
        public void TestFilterHasNotes()
        {
            var member1 = CreateJobAd(1);
            var member2 = CreateJobAd(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has notes.

            var note = new CandidateNote { CandidateId = member1.Id, Text = "A note" };
            _candidateNotesCommand.CreatePrivateCandidateNote(employer, note);

            // Filter.

            TestFilter(employer, CreateHasNotesCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [Test]
        public void TestFilterBlockHasNotes()
        {
            var member = CreateJobAd(1);
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

        [Test]
        public void TestFilterHasViewed()
        {
            var jobAd1 = CreateJobAd(1);
            var jobAd2 = CreateJobAd(2);
            var memberId = Guid.NewGuid();

            // JobAd1 has been viewed.

            _jobAdViewsCommand.ViewJobAd(memberId, jobAd1.Id);

            // Filter.

            TestFilter(employer, CreateHasViewedCriteria, new[] { jobAd1.Id, jobAd2.Id }, new[] { jobAd1.Id }, new[] { jobAd2.Id });
        }

        [Test]
        public void TestFilterBlockHasViewed()
        {
            var member = CreateJobAd(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has been viewed.

            _employerMemberViewsCommand.ViewMember(employer, member);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasViewedCriteria(true));

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryCandidateBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasViewedCriteria(true));
        }

        [Test]
        public void TestFilterHasAccessed()
        {
            var member1 = CreateJobAd(1);
            var member2 = CreateJobAd(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

            // Member1 has been viewed.

            _employerMemberViewsCommand.AccessMember(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), MemberAccessReason.PhoneNumberViewed);

            // Filter.

            TestFilter(employer, CreateHasAccessedCriteria, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [Test]
        public void TestFilterBlockHasAccessed()
        {
            var member = CreateJobAd(1);
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

        [Test]
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

        private JobAd CreateJobAd(int index)
        {
            var jobAd = new JobAd
            {
                Id = Guid.NewGuid(),
                Status = JobAdStatus.Open,
                Title = string.Format("Job Ad {0}", index),
                CreatedTime = DateTime.Now.AddDays(-100),
                JobDescription = new JobDescription
                {
                    BulletPoints = new[] { "good verbal communication", "self management and independency", "bullet point 3" },
                    Content =
                        "Mutley, you snickering, floppy eared hound. When courage is needed, you're never around.",
                    JobTypes = JobTypes.FullTime,
                    Industries = new List<Industry> { _industriesQuery.GetIndustry("Engineering") },
                    Location = new LocationReference(_locationQuery.GetCountrySubdivision(_australia, "QLD")),
                }
            };

            _jobAdsCommand.CreateJobAd(jobAd);

            return jobAd;
        }

        private Member CreateMember(IEmployer employer, int index, bool inFolder, bool isFlagged, bool hasNotes, bool hasBeenViewed, bool canContact)
        {
            var member = _memberAccountsCommand.CreateTestMember(index);
            _candidatesCommand.AddTestResume(member);

            // In folder.

            if (inFolder)
            {
                var folder = _candidateFoldersQuery.GetShortlistCandidateFolder(employer);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }

            // Is flagged.

            if (isFlagged)
            {
                var flagList = _candidateFlagListsQuery.GetCandidateFlagList(employer);
                _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member.Id);
            }

            // Has notes.

            if (hasNotes)
            {
                var note = new CandidateNote { CandidateId = member.Id, Text = "A note" };
                _candidateNotesCommand.CreatePrivateCandidateNote(employer, note);
            }

            // Has been viewed.

            if (hasBeenViewed)
                _employerMemberViewsCommand.ViewMember(employer, member);

            // Has been contacted.

            if (canContact)
                _employerMemberViewsCommand.AccessMember(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

            _updateMemberSearchCommand.AddMember(member.Id);
            return member;
        }

        private void TestFilter(IEmployer employer, Func<bool?, MemberSearchCriteria> createCriteria, ICollection<Guid> allMemberIds, ICollection<Guid> isSetMemberIds, ICollection<Guid> isNotSetMemberIds)
        {
            TestFilter(allMemberIds, employer, createCriteria(null));
            TestFilter(isSetMemberIds, employer, createCriteria(true));
            TestFilter(isNotSetMemberIds, employer, createCriteria(false));
        }

        private void TestFilter(ICollection<Guid> expectedMemberIds, IEmployer employer, MemberSearchCriteria criteria)
        {
            AssertMemberIds(expectedMemberIds, Search(employer, criteria));
        }

        private ICollection<Guid> Search(IEmployer employer, MemberSearchCriteria criteria)
        {
            return _executeMemberSearchCommand.Search(employer, criteria, null).MemberIds;
        }

        private static void AssertMemberIds(ICollection<Guid> expectedMemberIds, ICollection<Guid> memberIds)
        {
            Assert.AreEqual(expectedMemberIds.Count, memberIds.Count);
            foreach (var expectedMemberId in expectedMemberIds)
                Assert.IsTrue(memberIds.Contains(expectedMemberId));
        }

        private static JobAdSortCriteria CreateInFolderCriteria(bool? value)
        {
            return new JobAdSortCriteria { InFolder = value };
        }

        private static JobAdSortCriteria CreateIsFlaggedCriteria(bool? value)
        {
            return new JobAdSortCriteria { IsFlagged = value };
        }

        private static JobAdSortCriteria CreateHasNotesCriteria(bool? value)
        {
            return new JobAdSortCriteria { HasNotes = value };
        }

        private static JobAdSortCriteria CreateHasViewedCriteria(bool? value)
        {
            return new JobAdSortCriteria { HasViewed = value };
        }

        private static JobAdSortCriteria CreateHasAccessedCriteria(bool? value)
        {
            return new JobAdSortCriteria { HasAccessed = value };
        }
    }
}