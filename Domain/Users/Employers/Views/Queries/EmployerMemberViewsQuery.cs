using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Representatives.Queries;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Views.Queries
{
    public class EmployerMemberViewsQuery
        : IEmployerMemberViewsQuery
    {
        private readonly IEmployerViewsRepository _repository;
        private readonly IMembersQuery _membersQuery;
        private readonly IRepresentativesQuery _representativesQuery;
        private readonly IRecruitersQuery _recruitersQuery;
        private readonly IContendersQuery _contendersQuery;
        private readonly IExercisedCreditsQuery _exercisedCreditsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidateFoldersQuery _candidateFoldersQuery;
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;
        private readonly ICandidateNotesQuery _candidateNotesQuery;

        public EmployerMemberViewsQuery(IEmployerViewsRepository repository, IMembersQuery membersQuery, IRepresentativesQuery representativesQuery, IRecruitersQuery recruitersQuery, IContendersQuery contendersQuery, IExercisedCreditsQuery exercisedCreditsQuery, IEmployerCreditsQuery employerCreditsQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumeQuery, ICandidateFoldersQuery candidateFoldersQuery, ICandidateFlagListsQuery candidateFlagListsQuery, ICandidateNotesQuery candidateNotesQuery)
        {
            _repository = repository;
            _membersQuery = membersQuery;
            _representativesQuery = representativesQuery;
            _recruitersQuery = recruitersQuery;
            _contendersQuery = contendersQuery;
            _exercisedCreditsQuery = exercisedCreditsQuery;
            _employerCreditsQuery = employerCreditsQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumeQuery;
            _candidateFoldersQuery = candidateFoldersQuery;
            _candidateFlagListsQuery = candidateFlagListsQuery;
            _candidateNotesQuery = candidateNotesQuery;
        }

        ProfessionalView IEmployerMemberViewsQuery.GetProfessionalView(IEmployer employer, Guid memberId)
        {
            return GetView<ProfessionalView>(employer, memberId);
        }

        ProfessionalView IEmployerMemberViewsQuery.GetProfessionalView(IEmployer employer, Member member)
        {
            return GetView<ProfessionalView>(employer, member);
        }

        ProfessionalViews IEmployerMemberViewsQuery.GetProfessionalViews(IEmployer employer, IEnumerable<Guid> memberIds)
        {
            return GetViews<ProfessionalViews, ProfessionalView>(employer, memberIds.ToArray());
        }

        ProfessionalViews IEmployerMemberViewsQuery.GetProfessionalViews(IEmployer employer, IEnumerable<Member> members)
        {
            return GetViews<ProfessionalViews, ProfessionalView>(employer, members);
        }

        EmployerMemberView IEmployerMemberViewsQuery.GetEmployerMemberView(IEmployer employer, Guid memberId)
        {
            var view = GetView<EmployerMemberView>(employer, memberId);
            if (view != null)
                SetView(employer, memberId, view);
            return view;
        }

        EmployerMemberView IEmployerMemberViewsQuery.GetEmployerMemberView(IEmployer employer, Member member)
        {
            var view = GetView<EmployerMemberView>(employer, member);
            SetView(employer, member.Id, view);
            return view;
        }

        EmployerMemberViews IEmployerMemberViewsQuery.GetEmployerMemberViews(IEmployer employer, IEnumerable<Guid> memberIds)
        {
            memberIds = memberIds.ToArray();
            var views = GetViews<EmployerMemberViews, EmployerMemberView>(employer, memberIds);
            SetViews(employer, memberIds, views);
            return views;
        }

        EmployerMemberViews IEmployerMemberViewsQuery.GetEmployerMemberViews(IEmployer employer, IEnumerable<Member> members)
        {
            var views = GetViews<EmployerMemberViews, EmployerMemberView>(employer, members);
            SetViews(employer, from m in members select m.Id, views);
            return views;
        }

        IList<Guid> IEmployerMemberViewsQuery.GetViewedMemberIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _repository.GetViewedMemberIds(employer.Id);
        }

        bool IEmployerMemberViewsQuery.HasViewedMember(IEmployer employer, Guid memberId)
        {
            return HasViewedMember(employer, memberId);
        }

        CanContactStatus IEmployerMemberViewsQuery.CanContact(IEmployer employer, Member member)
        {
            var view = GetView<ProfessionalView>(employer, member);
            return view.CanContact();
        }

        bool IEmployerMemberViewsQuery.HasAccessedMember(IEmployer employer, Guid memberId)
        {
            return HasAccessedMember(employer, memberId);
        }

        IList<Guid> IEmployerMemberViewsQuery.GetAccessedMemberIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _repository.GetAccessedMemberIds(employer.Id);
        }

        IList<MemberAccess> IEmployerMemberViewsQuery.GetMemberAccesses(IEmployer employer, Guid memberId)
        {
            return employer == null
                ? new List<MemberAccess>()
                : _repository.GetMemberAccesses(employer.Id, memberId);
        }

        private void SetView(IEmployer employer, Guid memberId, EmployerMemberView view)
        {
            var candidate = _candidatesQuery.GetCandidate(memberId);
            var resume = candidate.ResumeId == null
                ? null
                : _resumesQuery.GetResume(candidate.ResumeId.Value);

            view.Set(
                candidate,
                resume,
                HasViewedMember(employer, memberId),
                _candidateFlagListsQuery.IsFlagged(employer, memberId),
                _candidateFoldersQuery.IsInMobileFolder(employer, memberId),
                _candidateFoldersQuery.GetFolderCount(employer, memberId),
                _candidateNotesQuery.GetNoteCount(employer, memberId));
        }

        private void SetViews(IEmployer employer, IEnumerable<Guid> memberIds, IEnumerable<EmployerMemberView> views)
        {
            // Get everything.

            var candidates = _candidatesQuery.GetCandidates(memberIds);
            var resumeIds = (from c in candidates where c.ResumeId != null select c.ResumeId.Value).ToList();
            var resumes = _resumesQuery.GetResumes(resumeIds);
            var viewedMembers = GetViewedMemberIds(employer, memberIds);
            var flaggedCandidates = _candidateFlagListsQuery.GetFlaggedCandidateIds(employer, memberIds);
            var mobileCandidates = _candidateFoldersQuery.GetInMobileFolderCandidateIds(employer, memberIds);
            var folderedCandidates = _candidateFoldersQuery.GetFolderCounts(employer, memberIds);
            var noteCounts = _candidateNotesQuery.GetNoteCounts(employer, memberIds);

            foreach (var view in views)
            {
                var viewId = view.Id;
                var candidate = (from c in candidates where c.Id == viewId select c).Single();
                var resume = candidate.ResumeId == null
                    ? null
                    : (from r in resumes where r.Id == candidate.ResumeId.Value select r).Single();
                view.Set(
                    candidate,
                    resume,
                    viewedMembers.Contains(viewId),
                    flaggedCandidates.Contains(viewId),
                    mobileCandidates.Contains(viewId),
                    folderedCandidates.ContainsKey(viewId) ? folderedCandidates[viewId] : 0,
                    noteCounts.ContainsKey(viewId) ? noteCounts[viewId] : 0);
            }
        }

        private bool HasViewedMember(IHasId<Guid> employer, Guid memberId)
        {
            return employer != null
                && _repository.HasViewedMember(employer.Id, memberId);
        }

        private IList<Guid> GetViewedMemberIds(IHasId<Guid> employer, IEnumerable<Guid> memberIds)
        {
            return employer == null
                ? new List<Guid>()
                : _repository.GetViewedMemberIds(employer.Id, memberIds);
        }

        private bool HasAccessedMember(IHasId<Guid> employer, Guid memberId)
        {
            return employer != null
                && _repository.HasAccessedMember(employer.Id, memberId);
        }

        private IList<Guid> GetAccessedMemberIds(IHasId<Guid> employer, IEnumerable<Guid> memberIds)
        {
            return employer == null
                ? new List<Guid>()
                : _repository.GetAccessedMemberIds(employer.Id, memberIds);
        }

        private TView GetView<TView>(IEmployer employer, Guid memberId)
            where TView : ProfessionalView, new()
        {
            var member = _membersQuery.GetMember(memberId);
            return member == null ? null : GetView<TView>(employer, member);
        }

        private TView GetView<TView>(IEmployer employer, Member member)
            where TView : ProfessionalView, new()
        {
            var contactCreditsAllocation = GetContactCreditsAllocation(employer);
            var hasBeenAccessed = HasAccessedMember(employer, member.Id);
            var effectiveContactDegree = GetEffectiveContactDegree(employer, member, contactCreditsAllocation, hasBeenAccessed);
            var isRepresented = GetIsRepresented(member.Id);

            return CreateView<TView>(member, contactCreditsAllocation, effectiveContactDegree, hasBeenAccessed, isRepresented);
        }

        private static TView CreateView<TView>(Member member, Allocation contactCreditsAllocation, ProfessionalContactDegree degree, bool hasBeenAccessed, bool isRepresented)
            where TView : ProfessionalView
        {
            return (TView)(typeof(TView) == typeof(EmployerMemberView)
                ? new EmployerMemberView(member, contactCreditsAllocation.RemainingQuantity, degree, hasBeenAccessed, isRepresented)
                : new ProfessionalView(member, contactCreditsAllocation.RemainingQuantity, degree, hasBeenAccessed, isRepresented));
        }

        private TViews GetViews<TViews, TView>(IEmployer employer, IEnumerable<Guid> memberIds)
            where TViews : ProfessionalViewCollection<TView>, new()
            where TView : ProfessionalView, new()
        {
            var members = _membersQuery.GetMembers(memberIds);
            return GetViews<TViews, TView>(employer, members);
        }

        private TViews GetViews<TViews, TView>(IEmployer employer, IEnumerable<Member> members)
            where TViews : ProfessionalViewCollection<TView>, new()
            where TView : ProfessionalView, new()
        {
            var contactCreditsAllocation = GetContactCreditsAllocation(employer);
            var memberIds = (from c in members select c.Id).ToList();

            // Determine who has been accessed.

            var hasAccessedMembers = GetAccessedMemberIds(employer, memberIds);

            // Determine who is represented.

            var representedIds = _representativesQuery.GetRepresentativeIds(memberIds);

            // Look for applicants.

            var organisationalCreditHierarchy = GetOrganisationHierarchyPath(employer);

            var applicantIds = employer == null
                ? new Guid[0].AsEnumerable()
                : _contendersQuery.GetApplicants(organisationalCreditHierarchy, memberIds);
            memberIds = memberIds.Except(applicantIds).ToList();

            // Get the affiliation.

            var affiliateId = employer == null
                ? null
                : employer.Organisation.AffiliateId;

            // Look for paid contacts.

            var contactedIds = employer == null
                ? new Guid[0]
                : affiliateId == null
                    ? GetContactedContacts(contactCreditsAllocation, organisationalCreditHierarchy, memberIds)
                    : GetContactedContacts(affiliateId.Value, organisationalCreditHierarchy, memberIds, members);

            var views = new TViews { ContactCredits = contactCreditsAllocation.RemainingQuantity };

            foreach (var member in members)
            {
                // Check the contact degree.

                var hasBeenAccessed = hasAccessedMembers.Contains(member.Id);
                var contactDegree = applicantIds.Contains(member.Id)
                    ? ProfessionalContactDegree.Applicant
                    : contactedIds.Contains(member.Id)
                        ? ProfessionalContactDegree.Contacted
                        : ProfessionalContactDegree.NotContacted;

                var effectiveContactDegree = GetEffectiveContactDegree(employer, member, contactDegree, contactCreditsAllocation, hasBeenAccessed);

                var isRepresented = representedIds.Keys.Contains(member.Id);

                views.Add(CreateView<TView>(member, contactCreditsAllocation, effectiveContactDegree, hasBeenAccessed, isRepresented));
            }

            return views;
        }

        private Allocation GetContactCreditsAllocation(IEmployer employer)
        {
            // Anonymous employer means no contact credits expired in the past.

            if (employer == null)
                return new Allocation { RemainingQuantity = 0, ExpiryDate = DateTime.MinValue };

            // Check the employer's and their organisation's active allocations.

            return _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
        }

        private ProfessionalContactDegree GetEffectiveContactDegree(IEmployer employer, IRegisteredUser member, Allocation contactCreditsAllocation, bool hasBeenAccessed)
        {
            if (employer == null)
                return ProfessionalContactDegree.NotContacted;

            // Determine whether the contact degree of the contact.

            var organisationalCreditHierarchy = GetOrganisationHierarchyPath(employer);
            var contactDegree = _contendersQuery.GetContactDegree(organisationalCreditHierarchy, member.Id);

            // If they are paid or an applicant then return that.

            return GetEffectiveContactDegree(employer, member, contactDegree, contactCreditsAllocation, hasBeenAccessed);
        }

        private static ProfessionalContactDegree GetEffectiveContactDegree(IEmployer employer, IRegisteredUser member, ProfessionalContactDegree contactDegree, Allocation contactCreditsAllocation, bool hasBeenAccessed)
        {
            if (employer == null || member == null)
                return contactDegree;

            if (contactDegree == ProfessionalContactDegree.Applicant)
                return contactDegree;

            var affiliateId = employer.Organisation.AffiliateId;
            return affiliateId == null
                ? GetEffectiveContactDegree(contactDegree, contactCreditsAllocation, hasBeenAccessed)
                : GetEffectiveContactDegree(affiliateId.Value, member);
        }

        private static ProfessionalContactDegree GetEffectiveContactDegree(ProfessionalContactDegree contactDegree, Allocation contactCreditsAllocation, bool hasBeenAccessed)
        {
            // If contacts are unlimited then access to everyone as long as the allocation has not expired.

            if (contactCreditsAllocation.RemainingQuantity == null)
            {
                if (!contactCreditsAllocation.HasExpired)
                    return ProfessionalContactDegree.Contacted;
            }
            else
            {
                // contactDegree reflects whether a contact credit has been used. For some old accesses though (before 22nd March 2010)
                // a credit usage may not have been recorded, e.g. for unlimited allocations.  Therefore it is possible that an
                // employer has accessed a member but the contactDegree is still NotContacted.  Adjust the effectiveContactDegree
                // by assuming that if a member has been accessed then there must have been a credit used somewhere, even if it
                // wasn't properly recorded.

                var effectiveContactDegree = contactDegree == ProfessionalContactDegree.Contacted || hasBeenAccessed
                    ? ProfessionalContactDegree.Contacted
                    : contactDegree;

                // Even if the member has been contacted they must still have credits.

                if (effectiveContactDegree == ProfessionalContactDegree.Contacted && !contactCreditsAllocation.HasExpired)
                    return effectiveContactDegree;
            }

            return ProfessionalContactDegree.NotContacted;
        }

        private static ProfessionalContactDegree GetEffectiveContactDegree(Guid affiliateId, IRegisteredUser member)
        {
            // Restricted to affiliate so contacts are those in the same affiliate.

            return member.AffiliateId == affiliateId ? ProfessionalContactDegree.Contacted : ProfessionalContactDegree.NotContacted;
        }

        private bool GetIsRepresented(Guid memberId)
        {
            return _representativesQuery.GetRepresentativeId(memberId) != null;
        }

        private IEnumerable<Guid> GetContactedContacts(Allocation contactCreditsAllocation, HierarchyPath hierarchyPath, IEnumerable<Guid> memberIds)
        {
            // If contacts are unlimited then paid access to everyone.

            if (contactCreditsAllocation.RemainingQuantity == null)
                return memberIds;

            // Need to explicitly check whether any credits have been used.

            return _exercisedCreditsQuery.HasExercisedCredits<ContactCredit, ApplicantCredit>(hierarchyPath, from c in memberIds select c);
        }

        private IEnumerable<Guid> GetContactedContacts(Guid affiliateId, HierarchyPath hierarchyPath, IEnumerable<Guid> memberIds, IEnumerable<Member> members)
        {
            // Restricted to affiliate so paid contacts are those that have been explicitly paid for...

            var exercisedIds = _exercisedCreditsQuery.HasExercisedCredits<ContactCredit, ApplicantCredit>(hierarchyPath, from c in memberIds select c);
            memberIds = memberIds.Except(exercisedIds);

            // .. and those in the same affiliate.

            var affiliateIds = from c in members
                               where memberIds.Contains(c.Id)
                               && c.AffiliateId == affiliateId
                               select c.Id;
            return exercisedIds.Concat(affiliateIds);
        }

        private OrganisationHierarchyPath GetOrganisationHierarchyPath(IHasId<Guid> employer)
        {
            return employer == null
                ? new OrganisationHierarchyPath()
                : _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
        }
    }
}
