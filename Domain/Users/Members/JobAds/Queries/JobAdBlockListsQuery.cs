using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public class JobAdBlockListsQuery
        : IJobAdBlockListsQuery
    {
        private static readonly int[] ListTypes = new[] { (int) BlockListType.Permanent };
        private readonly IJobAdListsQuery _jobAdListsQuery;
        private readonly IJobAdListsCommand _jobAdListsCommand;

        public JobAdBlockListsQuery(IJobAdListsQuery jobAdListsQuery, IJobAdListsCommand jobAdListsCommand)
        {
            _jobAdListsQuery = jobAdListsQuery;
            _jobAdListsCommand = jobAdListsCommand;
        }

        JobAdBlockList IJobAdBlockListsQuery.GetBlockList(IMember member)
        {
            return member == null
                ? null
                : GetSpecialBlockList(member.Id, BlockListType.Permanent);
        }

        bool IJobAdBlockListsQuery.IsBlocked(IMember member, Guid jobAdId)
        {
            return member != null
                && _jobAdListsQuery.IsListed(member.Id, ListTypes, null, jobAdId);
        }

        IList<Guid> IJobAdBlockListsQuery.GetBlockedJobAdIds(IMember member)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdListsQuery.GetListedJobAdIds(member.Id, ListTypes, null);
        }

        int IJobAdBlockListsQuery.GetBlockedCount(IMember member)
        {
            if (member == null)
                return 0;
            var counts = _jobAdListsQuery.GetListedCounts(member.Id, ListTypes, null);
            return counts.Count != 1 ? 0 : counts.First().Value;
        }

        private JobAdBlockList GetSpecialBlockList(Guid memberId, BlockListType blockListType)
        {
            // If it doesn't exist then create it.

            var blockLists = _jobAdListsQuery.GetLists<JobAdBlockList>(memberId, (int)blockListType);
            return blockLists.Count == 0
                ? CreateSpecialBlockList(memberId, blockListType)
                : blockLists[0];
        }

        private JobAdBlockList CreateSpecialBlockList(Guid memberId, BlockListType blockListType)
        {
            var blockList = new JobAdBlockList { MemberId = memberId, BlockListType = blockListType };
            _jobAdListsCommand.CreateList(blockList);
            return blockList;
        }
    }
}
