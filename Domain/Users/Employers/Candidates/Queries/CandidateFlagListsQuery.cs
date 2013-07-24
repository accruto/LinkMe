using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public class CandidateFlagListsQuery
        : ICandidateFlagListsQuery
    {
        private static readonly int[] ListTypes = new[] { (int)FlagListType.Flagged };
        private static readonly int[] NotIfInListTypes = new[] { (int)BlockListType.Permanent };

        private readonly IContenderListsCommand _contenderListsCommand;
        private readonly IContenderListsQuery _contenderListsQuery;

        public CandidateFlagListsQuery(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        CandidateFlagList ICandidateFlagListsQuery.GetFlagList(IEmployer employer)
        {
            return employer == null
                ? null
                : GetFlagList(employer.Id);
        }

        bool ICandidateFlagListsQuery.IsFlagged(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderListsQuery.IsListed(employer.Id, null, ListTypes, NotIfInListTypes, candidateId);
        }

        IList<Guid> ICandidateFlagListsQuery.GetFlaggedCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, null, ListTypes, NotIfInListTypes);
        }

        IList<Guid> ICandidateFlagListsQuery.GetFlaggedCandidateIds(IEmployer employer, IEnumerable<Guid> candidateIds)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, null, ListTypes, NotIfInListTypes, candidateIds);
        }

        int ICandidateFlagListsQuery.GetFlaggedCount(IEmployer employer)
        {
            if (employer == null)
                return 0;
            var counts = _contenderListsQuery.GetListedCounts(employer.Id, null, ListTypes, NotIfInListTypes);
            return counts.Count != 1 ? 0 : counts.First().Value;
        }

        private CandidateFlagList GetFlagList(Guid employerId)
        {
            // If it doesn't exist then create it.

            var lists = _contenderListsQuery.GetLists<CandidateFlagList>(employerId, (int) FlagListType.Flagged);
            return lists.Count == 0
                ? CreateFlagList(employerId)
                : lists[0];
        }

        private CandidateFlagList CreateFlagList(Guid employerId)
        {
            var list = new CandidateFlagList { RecruiterId = employerId, FlagListType = FlagListType.Flagged };
            _contenderListsCommand.CreateList(list);
            return list;
        }
    }
}
