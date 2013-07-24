using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public class CandidateBlockListsQuery
        : ICandidateBlockListsQuery
    {
        private static readonly int[] ListTypes = new[] {(int) BlockListType.Temporary, (int) BlockListType.Permanent};
        private readonly IContenderListsCommand _contenderListsCommand;
        private readonly IContenderListsQuery _contenderListsQuery;

        public CandidateBlockListsQuery(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        CandidateBlockList ICandidateBlockListsQuery.GetBlockList(IEmployer employer, Guid id)
        {
            var blockList = _contenderListsQuery.GetList<CandidateBlockList>(id);
            return CanAccessBlockList(employer, blockList) ? blockList : null;
        }

        IList<CandidateBlockList> ICandidateBlockListsQuery.GetBlockLists(IEmployer employer)
        {
            return employer == null
                ? new List<CandidateBlockList>()
                : GetBlockLists(employer.Id, _contenderListsQuery.GetLists<CandidateBlockList>(employer.Id, ListTypes)).ToList();
        }

        CandidateBlockList ICandidateBlockListsQuery.GetTemporaryBlockList(IEmployer employer)
        {
            return employer == null
                ? null
                : GetSpecialBlockList(employer.Id, BlockListType.Temporary);
        }

        CandidateBlockList ICandidateBlockListsQuery.GetPermanentBlockList(IEmployer employer)
        {
            return employer == null
                ? null
                : GetSpecialBlockList(employer.Id, BlockListType.Permanent);
        }

        bool ICandidateBlockListsQuery.IsBlocked(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderListsQuery.IsListed(employer.Id, employer.Organisation.Id, ListTypes, null, candidateId);
        }

        bool ICandidateBlockListsQuery.IsBlocked(IEmployer employer, Guid blockListId, Guid candidateId)
        {
            var blockList = _contenderListsQuery.GetList<CandidateBlockList>(blockListId);
            return CanAccessBlockList(employer, blockList)
                && _contenderListsQuery.IsListed(blockList.Id, null, candidateId);
        }

        bool ICandidateBlockListsQuery.IsPermanentlyBlocked(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderListsQuery.IsListed(employer.Id, employer.Organisation.Id, new[] { (int)BlockListType.Permanent }, null, candidateId);
        }

        IList<Guid> ICandidateBlockListsQuery.GetBlockedCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, employer.Organisation.Id, ListTypes, null);
        }

        IList<Guid> ICandidateBlockListsQuery.GetBlockedCandidateIds(IEmployer employer, Guid blockListId)
        {
            var blockList = _contenderListsQuery.GetList<CandidateBlockList>(blockListId);
            return CanAccessBlockList(employer, blockList)
                ? _contenderListsQuery.GetListedContenderIds(blockListId, null)
                : new List<Guid>();
        }

        IList<Guid> ICandidateBlockListsQuery.GetPermanentlyBlockedCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, employer.Organisation.Id, new[] { (int)BlockListType.Permanent }, null);
        }

        int ICandidateBlockListsQuery.GetBlockedCount(IEmployer employer, Guid blockListId)
        {
            var blockList = _contenderListsQuery.GetList<CandidateBlockList>(blockListId);
            return CanAccessBlockList(employer, blockList)
                ? _contenderListsQuery.GetListedCount(blockList.Id, null)
                : 0; 
        }

        IDictionary<Guid, int> ICandidateBlockListsQuery.GetBlockedCounts(IEmployer employer)
        {
            return employer == null
                ? new Dictionary<Guid, int>()
                : _contenderListsQuery.GetListedCounts(employer.Id, employer.Organisation.Id, ListTypes, null);
        }

        private CandidateBlockList GetSpecialBlockList(Guid employerId, BlockListType blockListType)
        {
            // If it doesn't exist then create it.

            var blockLists = _contenderListsQuery.GetLists<CandidateBlockList>(employerId, (int)blockListType);
            return blockLists.Count == 0
                ? CreateSpecialCandidateBlockList(employerId, blockListType)
                : blockLists[0];
        }

        private CandidateBlockList CreateSpecialCandidateBlockList(Guid employerId, BlockListType blockListType)
        {
            var blockList = new CandidateBlockList { RecruiterId = employerId, BlockListType = blockListType };
            _contenderListsCommand.CreateList(blockList);
            return blockList;
        }

        private IEnumerable<CandidateBlockList> GetBlockLists(Guid employerId, IList<CandidateBlockList> blockLists)
        {
            // Look for the blockLists.

            IList<CandidateBlockList> blockListList = null;

            if (!(from f in blockLists where f.BlockListType == BlockListType.Temporary select f).Any())
            {
                blockListList = blockLists.ToList();
                blockListList.Add(CreateSpecialCandidateBlockList(employerId, BlockListType.Temporary));
            }

            if (!(from f in blockLists where f.BlockListType == BlockListType.Permanent select f).Any())
            {
                if (blockListList == null)
                    blockListList = blockLists.ToList();
                blockListList.Add(CreateSpecialCandidateBlockList(employerId, BlockListType.Permanent));
            }

            return blockListList ?? blockLists;
        }

        private static bool CanAccessBlockList(IHasId<Guid> employer, CandidateBlockList blockList)
        {
            if (employer == null)
                return false;
            if (blockList == null)
                return false;
            if (blockList.IsDeleted)
                return false;

            // Only the owner can edit or delete it.

            return blockList.RecruiterId == employer.Id;
        }
    }
}