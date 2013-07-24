using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Location;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Commands;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Contenders;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class TestDataCandidatesTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();
        private readonly ICandidateListsCommand _candidateListsCommand = Resolve<ICandidateListsCommand>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly ICandidateNotesCommand _candidateNotesCommand = Resolve<ICandidateNotesCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IUserSessionsCommand _userSessionsCommand = Resolve<IUserSessionsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestDeleteData()
        {
            //just run the setup which deletes all data

        }

        [TestMethod]
        public void TestEmployers()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();

            var unlimited = CreateEmployer(0);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, OwnerId = unlimited.Id, InitialQuantity = null });

            var limited = CreateEmployer(1);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, OwnerId = limited.Id, InitialQuantity = 10 });

            var none = CreateEmployer(2);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, OwnerId = none.Id, InitialQuantity = 1 });

            var hasNotAccessed = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(hasNotAccessed.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var hasAccessed = _memberAccountsCommand.CreateTestMember(1);
            candidate = _candidatesQuery.GetCandidate(hasAccessed.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            _employerMemberViewsCommand.AccessMember(_app, unlimited, _employerMemberViewsQuery.GetProfessionalView(unlimited, hasAccessed), MemberAccessReason.Unlock);
            _employerMemberViewsCommand.AccessMember(_app, limited, _employerMemberViewsQuery.GetProfessionalView(limited, hasAccessed), MemberAccessReason.Unlock);
            _employerMemberViewsCommand.AccessMember(_app, none, _employerMemberViewsQuery.GetProfessionalView(none, hasAccessed), MemberAccessReason.Unlock);
        }

        [TestMethod]
        public void TestLotsToContact()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            for (var index = 0; index < 150; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                candidate.DesiredSalary = new Salary {LowerBound = null, UpperBound = index * 1000, Currency = Currency.AUD, Rate = SalaryRate.Year};
                _candidatesCommand.UpdateCandidate(candidate);
                _candidateResumesCommand.AddTestResume(candidate);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }
        }

        [TestMethod]
        public void TestLimitedCredits()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 10, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            for (var index = 0; index < 50; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                candidate.DesiredSalary = new Salary { LowerBound = null, UpperBound = index * 1000, Currency = Currency.AUD, Rate = SalaryRate.Year };
                _candidatesCommand.UpdateCandidate(candidate);
                _candidateResumesCommand.AddTestResume(candidate);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdsCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdsCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            for (var index = 0; index < 50; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                candidate.DesiredSalary = new Salary { LowerBound = null, UpperBound = index * 1000, Currency = Currency.AUD, Rate = SalaryRate.Year };
                _candidatesCommand.UpdateCandidate(candidate);
                _candidateResumesCommand.AddTestResume(candidate);
                _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
            }
        }

        [TestMethod]
        public void TestJobTypes()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.FullTime;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);

            member = _memberAccountsCommand.CreateTestMember(1);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.FullTime | JobTypes.PartTime;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);

            member = _memberAccountsCommand.CreateTestMember(2);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.FullTime | JobTypes.Contract;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);

            member = _memberAccountsCommand.CreateTestMember(3);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.Temp;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);

            member = _memberAccountsCommand.CreateTestMember(4);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = JobTypes.JobShare;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
        }

        [TestMethod]
        public void TestCanContactByPhoneActions()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id});

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Can see because already contacted.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see another with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Cannot see although unlocked because phone numbers not visible.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Could see with credit but phone numbers not visible.

            member = _memberAccountsCommand.CreateTestMember(++index);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.PhoneNumbers);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Cannot see although unlocked because no phone numbers.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            member.PhoneNumbers = null;
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Cannot see with credit because no phone numbers.

            member = _memberAccountsCommand.CreateTestMember(++index);
            member.PhoneNumbers = null;
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestCanContactActions()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Can see because already contacted.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestCanAccessResumeActions()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Can see because already contacted and has resume.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see another with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see another with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Can see another with credit.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Cannot see although unlocked because no resume.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Could see with credit but no resume.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestHasNotes()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer0 = CreateEmployer(0, organisation);
            var employer1 = CreateEmployer(1, organisation);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer0.Id });
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer1.Id });

            // Put them all in the short list folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer0);

            // No notes.

            var index = 0;
            var months = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);

            // One private note.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);
            _candidateNotesCommand.CreatePrivateNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "Private note" });

            // Multiple private notes.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);
            _candidateNotesCommand.CreatePrivateNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "Multiple private notes" });
            _candidateNotesCommand.CreatePrivateNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "Multiple private notes" });

            // Shared note.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);
            _candidateNotesCommand.CreateSharedNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "Shared note" });

            // Other shared note.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);
            _candidateNotesCommand.CreateSharedNote(employer1, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "Other shared note" });

            // All.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _candidateListsCommand.AddCandidateToFolder(employer0, folder, member.Id);
            _candidateNotesCommand.CreatePrivateNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "All private note" });
            _candidateNotesCommand.CreateSharedNote(employer0, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "All shared note" });
            _candidateNotesCommand.CreateSharedNote(employer1, new CandidateNote { CandidateId = member.Id, CreatedTime = DateTime.Now.AddMonths(--months), UpdatedTime = DateTime.Now.AddMonths(--months), Text = "All other shared note" });
        }

        [TestMethod]
        public void TestPhotos()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Member with a photo.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _memberAccountsCommand.AddTestProfilePhoto(member);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.ProfilePhoto);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Member without a photo.

            member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestName()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Member with name not hidden.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Member with name hidden.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Resume);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Member with resume hidden.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Resume);
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.SetFlag(ProfessionalVisibility.Name);
            _memberAccountsCommand.UpdateMember(member);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestLastLogin()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            // Member with a photo.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);
            _userSessionsCommand.CreateUserLogin(new UserLogin {UserId = member.Id, AuthenticationStatus = AuthenticationStatus.Authenticated});
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestFoldersBlockLists()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

            // Folder.

            var index = 0;
            var member = _memberAccountsCommand.CreateTestMember(++index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // BlockList.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member.Id);

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member.Id);
        }

        [TestMethod]
        public void TestAffiliate()
        {
            var employer = CreateEmployer(0);
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = 100, OwnerId = employer.Id });

            var aat = TestCommunity.Aat.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var ahri = TestCommunity.Ahri.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var gta = TestCommunity.Gta.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            // Set up members.

            var index = 0;

            // 0 is member of community with an image.

            var member = _memberAccountsCommand.CreateTestMember(++index, aat.Id);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // 1 is a member of another community with an image.

            member = _memberAccountsCommand.CreateTestMember(++index, ahri.Id);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // 2 is a member of a community with no image.

            member = _memberAccountsCommand.CreateTestMember(++index, gta.Id);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // 3 is not a member of any community.

            member = _memberAccountsCommand.CreateTestMember(++index);
            candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
        }

        [TestMethod]
        public void TestJobAds()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            // Set up members which the employer can and cannot see and add them to the shortlist folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);

            // Folder.
            var member = _memberAccountsCommand.CreateTestMember(0);
            for (var index = 1; index < 50; ++index)
            {
                member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.Unlock);
                if (index <= 45)
                    _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);
                else _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member.Id);
            }

            // Add some jobs.

            var jobAd = _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);
            for (var i = 0; i < 6; i++)
            {
                _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);
                _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Closed);
            }

            var application = new InternalApplication { ApplicantId = member.Id, PositionId = jobAd.Id };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
        }

        [TestMethod]
        public void TestManageCandidates()
        {
            var employer = CreateEmployer(0);

            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, InitialQuantity = null, OwnerId = employer.Id });
            var jobAdCredit = _creditsQuery.GetCredit<JobAdCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = jobAdCredit.Id, InitialQuantity = null, OwnerId = employer.Id });

            // Members.

            for (var index = 0; index < 50; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                var candidate = _candidatesQuery.GetCandidate(member.Id);
                _candidateResumesCommand.AddTestResume(candidate);
            }

            // Add some jobs.

            for (var i = 0; i < 2; i++)
                _jobAdsCommand.PostTestJobAd(employer, JobAdStatus.Open);
        }

        [TestMethod]
        public void TestAllPostCodeMembers()
        {
            var postCodes = new List<PostalCode>();

            foreach (var locality in _locationQuery.GetLocalities(_locationQuery.GetCountry("Australia")))
            {
                postCodes = postCodes.Concat(_locationQuery.GetPostalCodes(locality)).ToList();
            } 

            foreach (var postCode in postCodes)
            {
                var hasNotAccessed = _memberAccountsCommand.CreateTestMember(int.Parse(postCode.Postcode), new LocationReference(postCode));
                var candidate = _candidatesQuery.GetCandidate(hasNotAccessed.Id);
                _candidateResumesCommand.AddTestResume(candidate);
            }
        }

        private Employer CreateEmployer(int index)
        {
            return CreateEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private Employer CreateEmployer(int index, IOrganisation organisation)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, organisation);
        }
    }
}