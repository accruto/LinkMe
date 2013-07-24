using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public class JobAdFlagListsQuery
        : IJobAdFlagListsQuery
    {
        private static readonly int[] ListTypes = new[] { (int) FlagListType.Flagged };
        private static readonly int[] NotIfInListTypes = new[] { (int) BlockListType.Permanent };

        private readonly IJobAdListsCommand _jobAdListsCommand;
        private readonly IJobAdListsQuery _jobAdListsQuery;

        public JobAdFlagListsQuery(IJobAdListsQuery jobAdListsQuery, IJobAdListsCommand jobAdListsCommand)
        {
            _jobAdListsQuery = jobAdListsQuery;
            _jobAdListsCommand = jobAdListsCommand;
        }

        JobAdFlagList IJobAdFlagListsQuery.GetFlagList(IMember member)
        {
            return member == null
                ? null
                : GetSpecialFlagList(member.Id, FlagListType.Flagged);
        }

        bool IJobAdFlagListsQuery.IsFlagged(IMember member, Guid jobAdId)
        {
            return member != null
                && _jobAdListsQuery.IsListed(member.Id, ListTypes, NotIfInListTypes, jobAdId);
        }

        IList<Guid> IJobAdFlagListsQuery.GetFlaggedJobAdIds(IMember member)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdListsQuery.GetListedJobAdIds(member.Id, ListTypes, NotIfInListTypes);
        }

        IList<Guid> IJobAdFlagListsQuery.GetFlaggedJobAdIds(IMember member, IEnumerable<Guid> jobAdIds)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdListsQuery.GetListedJobAdIds(member.Id, ListTypes, NotIfInListTypes, jobAdIds);
        }

        int IJobAdFlagListsQuery.GetFlaggedCount(IMember member)
        {
            if (member == null)
                return 0;
            var counts = _jobAdListsQuery.GetListedCounts(member.Id, ListTypes, NotIfInListTypes);
            return counts.Count != 1 ? 0 : counts.First().Value;
        }

        private JobAdFlagList GetSpecialFlagList(Guid memberId, FlagListType flagListType)
        {
            // If it doesn't exist then create it.

            var lists = _jobAdListsQuery.GetLists<JobAdFlagList>(memberId, (int)flagListType);
            return lists.Count == 0
                ? CreateSpecialFlagList(memberId, flagListType)
                : lists[0];
        }

        private JobAdFlagList CreateSpecialFlagList(Guid memberId, FlagListType flagListType)
        {
            var list = new JobAdFlagList { MemberId = memberId, FlagListType = flagListType };
            _jobAdListsCommand.CreateList(list);
            return list;
        }
    }
}
