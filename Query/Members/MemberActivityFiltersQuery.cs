using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.Applicants.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Views.Queries;

namespace LinkMe.Query.Members
{
    public class MemberActivityFiltersQuery
        : FiltersQueryCore, IMemberActivityFiltersQuery
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery;
        private readonly ICandidateFoldersQuery _candidateFoldersQuery;
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;
        private readonly ICandidateNotesQuery _candidateNotesQuery;
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery;
        private readonly IEmployerCreditsQuery _employerCreditsQuery;
        private readonly IJobAdApplicantsQuery _jobAdApplicantsQuery;

        public MemberActivityFiltersQuery(ICandidateBlockListsQuery candidateBlockListsQuery, ICandidateFoldersQuery candidateFoldersQuery, ICandidateFlagListsQuery candidateFlagListsQuery, ICandidateNotesQuery candidateNotesQuery, IEmployerMemberViewsQuery employerMemberViewsQuery, IEmployerCreditsQuery employerCreditsQuery, IJobAdApplicantsQuery jobAdApplicantsQuery)
        {
            _candidateBlockListsQuery = candidateBlockListsQuery;
            _candidateFoldersQuery = candidateFoldersQuery;
            _candidateFlagListsQuery = candidateFlagListsQuery;
            _candidateNotesQuery = candidateNotesQuery;
            _employerMemberViewsQuery = employerMemberViewsQuery;
            _employerCreditsQuery = employerCreditsQuery;
            _jobAdApplicantsQuery = jobAdApplicantsQuery;
        }

        IList<Guid> IMemberActivityFiltersQuery.GetIncludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            return GetIncludeMemberIds(employer, query, null);
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFolderIncludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query)
        {
            // Include those in the folder.

            var memberIds = _candidateFoldersQuery.GetInFolderCandidateIds(employer, folderId);

            // Include filters.

            return GetIncludeMemberIds(employer, query, memberIds);
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFlaggedIncludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            // Include those who are flagged.

            var memberIds = _candidateFlagListsQuery.GetFlaggedCandidateIds(employer);

            // Include filters.

            return GetIncludeMemberIds(employer, query, memberIds);
        }

        public IList<Guid> GetBlockListIncludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query)
        {
            // Include those who are blocked.

            var memberIds = _candidateBlockListsQuery.GetBlockedCandidateIds(employer, blockListId);

            // Include filters.

            return GetIncludeMemberIds(employer, query, memberIds);
        }

        IList<Guid> IMemberActivityFiltersQuery.GetSuggestedIncludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query)
        {
            return GetIncludeMemberIds(employer, query, null);
        }

        IList<Guid> IMemberActivityFiltersQuery.GetManagedIncludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query)
        {
            // Include those that are applicants for the job.

            var memberIds = _jobAdApplicantsQuery.GetApplicantIds(jobAdId, status);

            // Include filters.

            return GetIncludeMemberIds(employer, query, memberIds);
        }

        IList<Guid> IMemberActivityFiltersQuery.GetExcludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);

            // Exclude all blocked candidates.

            var candidateIds = _candidateBlockListsQuery.GetBlockedCandidateIds(employer);
            if (candidateIds.Count > 0)
                memberIds = GetExcludeList(memberIds, candidateIds);

            return memberIds == null ? null : memberIds.ToList();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFolderExcludeMemberIds(IEmployer employer, Guid folderId, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);

            // Exclude only the permanently blocked candidates.

            var candidateIds = _candidateBlockListsQuery.GetPermanentlyBlockedCandidateIds(employer);
            if (candidateIds.Count > 0)
                memberIds = GetExcludeList(memberIds, candidateIds);

            return memberIds == null ? null : memberIds.ToList();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetFlaggedExcludeMemberIds(IEmployer employer, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);

            // Exclude only the permanently blocked candidates.

            var candidateIds = _candidateBlockListsQuery.GetPermanentlyBlockedCandidateIds(employer);
            if (candidateIds.Count > 0)
                memberIds = GetExcludeList(memberIds, candidateIds);

            return memberIds == null ? null : memberIds.ToList();
        }

        public IList<Guid> GetBlockListExcludeMemberIds(IEmployer employer, Guid blockListId, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);
            return memberIds == null ? null : memberIds.ToList();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetSuggestedExcludeMemberIds(IEmployer employer, Guid jobAdId, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);

            // Exclude job ad specific members.

            if (employer != null)
                memberIds = GetExcludeList(memberIds, _jobAdApplicantsQuery.GetApplicantIds(jobAdId));

            // Exclude all blocked candidates.

            var candidateIds = _candidateBlockListsQuery.GetBlockedCandidateIds(employer);
            if (candidateIds.Count > 0)
                memberIds = GetExcludeList(memberIds, candidateIds);

            return memberIds == null ? null : memberIds.ToList();
        }

        IList<Guid> IMemberActivityFiltersQuery.GetManagedExcludeMemberIds(IEmployer employer, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query)
        {
            // Exclude filters.

            var memberIds = GetExcludeMemberIds(employer, query, null);

            // Exclude all blocked candidates.

            var candidateIds = _candidateBlockListsQuery.GetBlockedCandidateIds(employer);
            if (candidateIds.Count > 0)
                memberIds = GetExcludeList(memberIds, candidateIds);

            return memberIds == null ? null : memberIds.ToList();
        }

        private IList<Guid> GetIncludeMemberIds(IEmployer employer, MemberSearchQuery query, IEnumerable<Guid> memberIds)
        {
            // Include filters.

            if (query.InFolder != null && query.InFolder.Value)
                memberIds = GetIncludeList(memberIds, _candidateFoldersQuery.GetInFolderCandidateIds(employer));

            if (query.IsFlagged != null && query.IsFlagged.Value)
                memberIds = GetIncludeList(memberIds, _candidateFlagListsQuery.GetFlaggedCandidateIds(employer));

            if (query.HasNotes != null && query.HasNotes.Value)
                memberIds = GetIncludeList(memberIds, _candidateNotesQuery.GetHasNotesCandidateIds(employer));

            if (query.HasViewed != null && query.HasViewed.Value)
                memberIds = GetIncludeList(memberIds, _employerMemberViewsQuery.GetViewedMemberIds(employer));

            if (query.IsUnlocked != null && query.IsUnlocked.Value)
            {
                // Must have active allocations or else no-one is unlocked.

                var allocation = GetContactCreditsAllocation(employer);
                memberIds = allocation.HasExpired
                    ? GetIncludeList(memberIds, new Guid[0])
                    : GetIncludeList(memberIds, _employerMemberViewsQuery.GetAccessedMemberIds(employer));
            }

            return memberIds == null ? null : memberIds.ToList();
        }

        private IEnumerable<Guid> GetExcludeMemberIds(IEmployer employer, MemberSearchQuery query, IEnumerable<Guid> memberIds)
        {
            if (query.InFolder != null && !query.InFolder.Value)
                memberIds = GetExcludeList(memberIds, _candidateFoldersQuery.GetInFolderCandidateIds(employer));

            if (query.IsFlagged != null && !query.IsFlagged.Value)
                memberIds = GetExcludeList(memberIds, _candidateFlagListsQuery.GetFlaggedCandidateIds(employer));

            if (query.HasNotes != null && !query.HasNotes.Value)
                memberIds = GetExcludeList(memberIds, _candidateNotesQuery.GetHasNotesCandidateIds(employer));

            if (query.HasViewed != null && !query.HasViewed.Value)
                memberIds = GetExcludeList(memberIds, _employerMemberViewsQuery.GetViewedMemberIds(employer));

            if (query.IsUnlocked != null && !query.IsUnlocked.Value)
            {
                // Must have active allocations or else no-one is unlocked.

                var allocation = GetContactCreditsAllocation(employer);
                if (!allocation.HasExpired)
                    memberIds = GetExcludeList(memberIds, _employerMemberViewsQuery.GetAccessedMemberIds(employer));
            }

            return memberIds;
        }

        private Allocation GetContactCreditsAllocation(IEmployer employer)
        {
            // Anonymous employer means no contact credits expired in the past.

            if (employer == null)
                return new Allocation { RemainingQuantity = 0, ExpiryDate = DateTime.MinValue };

            // Check the employer's and their organisation's active allocations.

            return _employerCreditsQuery.GetEffectiveActiveAllocation<ContactCredit>(employer);
        }
    }
}