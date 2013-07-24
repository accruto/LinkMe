using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Applicants.Queries
{
    public class JobAdApplicantsQuery
        : JobAdApplicantsComponent, IJobAdApplicantsQuery
    {
        private static readonly int[] NotIfInListTypes = new[] { (int)BlockListType.Permanent };
        private readonly IApplicationsQuery _applicationsQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly ICandidateBlockListsQuery _blockListsQuery;

        public JobAdApplicantsQuery(IApplicationsQuery applicationsQuery, IContenderListsCommand contendersListCommand, IContenderListsQuery contenderListsQuery, IMembersQuery membersQuery, ICandidateBlockListsQuery blockListsQuery)
            : base(contendersListCommand, contenderListsQuery)
        {
            _applicationsQuery = applicationsQuery;
            _membersQuery = membersQuery;
            _blockListsQuery = blockListsQuery;
        }

        InternalApplication IJobAdApplicantsQuery.GetApplication(Guid applicationId)
        {
            return _applicationsQuery.GetApplication<InternalApplication>(applicationId, false);
        }

        InternalApplication IJobAdApplicantsQuery.GetApplication(Guid applicantId, Guid positionId)
        {
            return _applicationsQuery.GetApplication<InternalApplication>(applicantId, positionId, false);
        }

        IList<InternalApplication> IJobAdApplicantsQuery.GetApplications(IEmployer employer, Guid applicantId)
        {
            // Get all lists that belong to this employer.

            var positionIds = from l in GetLists(employer.Id) select l.Id;
            return _applicationsQuery.GetApplications<InternalApplication>(applicantId, positionIds, false);
        }

        IList<InternalApplication> IJobAdApplicantsQuery.GetApplicationsByPositionId(Guid positionId)
        {
            return _applicationsQuery.GetApplicationsByPositionId<InternalApplication>(positionId, false);
        }

        ApplicantList IJobAdApplicantsQuery.GetApplicantList(IEmployer employer, IJobAd jobAd)
        {
            return CanAccessList(employer, jobAd)
                ? EnsureList(jobAd)
                : null;
        }

        ApplicantStatus IJobAdApplicantsQuery.GetApplicantStatus(Guid applicationId)
        {
            return _contenderListsQuery.GetApplicantStatus(applicationId);
        }

        IList<Guid> IJobAdApplicantsQuery.GetApplicantIds(Guid jobAdId)
        {
            return _contenderListsQuery.GetListedContenderIds(jobAdId, NotIfInListTypes);
        }

        IList<Guid> IJobAdApplicantsQuery.GetApplicantIds(Guid jobAdId, ApplicantStatus status)
        {
            return _contenderListsQuery.GetListedContenderIds(jobAdId, NotIfInListTypes, status);
        }

        int IJobAdApplicantsQuery.GetApplicantCount(IEmployer employer, ApplicantList applicantList)
        {
            // Check access.

            if (!CanAccessList(employer, applicantList))
                return 0;

            // Only count entries which are not in a blockList.

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(applicantList.Id, null);
            if (entries.Count == 0)
                return 0;

            // Update the entries.

            var blockedCandidateIds = _blockListsQuery.GetPermanentlyBlockedCandidateIds(employer);
            if (!blockedCandidateIds.IsNullOrEmpty())
            {
                // Remove any blocked candidates before counting.

                entries = (from e in entries where !blockedCandidateIds.Contains(e.ApplicantId) select e).ToList();
            }

            // Get all members.

            var members = _membersQuery.GetMembers(from e in entries select e.ApplicantId).ToDictionary(m => m.Id, m => m);

            // Need to get counts for all member-generated applications where the member is enabled
            // plus all employer-generated applications (i.e. manually added) where the member is enabled AND activated

            return (from s in new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }
                    select new
                    {
                        Status = s,
                        Count = (from e in entries
                                 where e.ApplicantStatus == s
                                 && members.ContainsKey(e.ApplicantId)
                                 let m = members[e.ApplicantId]
                                 where m.IsEnabled
                                 && (m.IsActivated || e.ApplicantStatus != ApplicantStatus.NotSubmitted)
                                 select e).Count()
                    }).Sum(s => s.Count);
        }

        IDictionary<ApplicantStatus, int> IJobAdApplicantsQuery.GetApplicantCounts(IEmployer employer, ApplicantList applicantList)
        {
            // Check access.

            if (!CanAccessList(employer, applicantList))
                return new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }.ToDictionary(s => s, s => 0);

            // Only count entries which are not in a blockList.

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(applicantList.Id, NotIfInListTypes);
            if (entries.Count == 0)
                return new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }.ToDictionary(s => s, s => 0);

            // Get all members.

            var members = _membersQuery.GetMembers(from e in entries select e.ApplicantId).ToDictionary(m => m.Id, m => m);

            // Need to get counts for all member-generated applications where the member is enabled
            // plus all employer-generated applications (i.e. manually added) where the member is enabled AND activated
            
            return (from s in new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }
                    select new
                    {
                        Status = s,
                        Count = (from e in entries
                                 where e.ApplicantStatus == s
                                 && members.ContainsKey(e.ApplicantId)
                                 let m = members[e.ApplicantId]
                                 where m.IsEnabled
                                 && (m.IsActivated || e.ApplicantStatus != ApplicantStatus.NotSubmitted)
                                 select e).Count()
                    }).ToDictionary(s => s.Status, s => s.Count);
        }

        IDictionary<Guid, IDictionary<ApplicantStatus, int>> IJobAdApplicantsQuery.GetApplicantCounts(IEmployer employer, IEnumerable<ApplicantList> applicantLists)
        {
            var defaultCounts = (from s in new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted } select s).ToDictionary(s => s, s => 0);

            // Create defaults for all lists.

            var counts = applicantLists.ToDictionary(l => l.Id, l => defaultCounts);

            // Get all the lists the employer has access to.

            applicantLists = from l in applicantLists
                             where CanAccessList(employer, l)
                             select l;
            if (!applicantLists.Any())
                return counts.ToDictionary(x => x.Key, x => (IDictionary<ApplicantStatus, int>)x.Value);

            // Only count entries which are not in a blockList.

            var entries = _contenderListsQuery.GetEntries<ApplicantListEntry>(from l in applicantLists select l.Id, NotIfInListTypes);

            // Get all candidate ids.

            var applicantIds = (from e in entries.SelectMany(e => e.Value) select e.ApplicantId).Distinct();
            if (!applicantIds.Any())
                return counts.ToDictionary(x => x.Key, x => (IDictionary<ApplicantStatus, int>)x.Value);
            var members = _membersQuery.GetMembers(applicantIds).ToDictionary(m => m.Id, m => m);

            // Update the counts.

            foreach (var applicantList in applicantLists)
            {
                IList<ApplicantListEntry> listEntries;
                entries.TryGetValue(applicantList.Id, out listEntries);
                if (listEntries == null)
                    continue;
                
                // Need to get counts for all member-generated applications where the member is enabled
                // plus all employer-generated applications (i.e. manually added) where the member is enabled AND activated.

                var listCounts = (from s in new[] { ApplicantStatus.New, ApplicantStatus.Rejected, ApplicantStatus.Shortlisted }
                                  select new
                                  {
                                      Status = s,
                                      Count = (from e in listEntries
                                               where e.ApplicantStatus == s
                                               && members.ContainsKey(e.ApplicantId)
                                               let m = members[e.ApplicantId]
                                               where m.IsEnabled
                                               && (m.IsActivated || e.ApplicantStatus != ApplicantStatus.NotSubmitted)
                                               select e).Count()
                                  }).ToDictionary(s => s.Status, s => s.Count);

                counts[applicantList.Id] = listCounts.ToDictionary(x => x.Key, x => x.Value);
            }

            return counts.ToDictionary(x => x.Key, x => (IDictionary<ApplicantStatus, int>)x.Value);
        }

        private static bool CanAccessList(IHasId<Guid> employer, ApplicantList applicantList)
        {
            if (employer == null || applicantList == null)
                return false;
            return employer.Id == applicantList.PosterId;
        }

        private static bool CanAccessList(IHasId<Guid> employer, IJobAd jobAd)
        {
            if (employer == null || jobAd == null)
                return false;
            return employer.Id == jobAd.PosterId;
        }
    }
}