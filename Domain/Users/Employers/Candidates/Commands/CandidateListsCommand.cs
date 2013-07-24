using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;

namespace LinkMe.Domain.Users.Employers.Candidates.Commands
{
    public class CandidateListsCommand
        : ICandidateListsCommand
    {
        private readonly IContenderListsCommand _contenderListsCommand;
        private readonly IContenderListsQuery _contenderListsQuery;
        private static readonly int[] PermanentBlockListIncompatibleTypes = new[] { (int)BlockListType.Temporary, (int)FlagListType.Flagged };
        private static readonly int[] FlagListIncompatibleTypes = new[] { (int)BlockListType.Permanent };
        private static readonly int[] FolderIncompatibleTypes = new[] { (int)BlockListType.Permanent };

        public CandidateListsCommand(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        int ICandidateListsCommand.AddCandidateToBlockList(IEmployer employer, CandidateBlockList blockList, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, blockList))
                throw new CandidateBlockListsPermissionsException(employer, blockList.Id);

            AddCandidatesToBlockList(employer.Id, blockList, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(blockList.Id, null);
        }

        int ICandidateListsCommand.AddCandidatesToBlockList(IEmployer employer, CandidateBlockList blockList, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, blockList))
                throw new CandidateBlockListsPermissionsException(employer, blockList.Id);

            AddCandidatesToBlockList(employer.Id, blockList, candidateIds);
            return _contenderListsQuery.GetListedCount(blockList.Id, null);
        }

        int ICandidateListsCommand.RemoveCandidateFromBlockList(IEmployer employer, CandidateBlockList blockList, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, blockList))
                throw new CandidateBlockListsPermissionsException(employer, blockList.Id);

            RemoveCandidatesFromList(blockList.Id, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(blockList.Id, null);
        }

        int ICandidateListsCommand.RemoveCandidatesFromBlockList(IEmployer employer, CandidateBlockList blockList, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, blockList))
                throw new CandidateBlockListsPermissionsException(employer, blockList.Id);

            RemoveCandidatesFromList(blockList.Id, candidateIds);
            return _contenderListsQuery.GetListedCount(blockList.Id, null);
        }

        int ICandidateListsCommand.RemoveAllCandidatesFromBlockList(IEmployer employer, CandidateBlockList blockList)
        {
            if (!CanModifyCandidates(employer, blockList))
                throw new CandidateBlockListsPermissionsException(employer, blockList.Id);

            RemoveAllCandidatesFromList(blockList.Id);
            return 0;
        }

        int ICandidateListsCommand.RemoveAllCandidatesFromBlockList(IEmployer employer, BlockListType blockListType)
        {
            if (employer != null)
                _contenderListsCommand.DeleteAllEntries(employer.Id, new[] { (int)blockListType });
            return 0;
        }

        int ICandidateListsCommand.AddCandidateToFlagList(IEmployer employer, CandidateFlagList flagList, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, flagList))
                throw new CandidateFlagListsPermissionsException(employer, flagList.Id);

            AddCandidatesToFlagList(employer.Id, flagList.Id, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.AddCandidatesToFlagList(IEmployer employer, CandidateFlagList flagList, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, flagList))
                throw new CandidateFlagListsPermissionsException(employer, flagList.Id);

            AddCandidatesToFlagList(employer.Id, flagList.Id, candidateIds);
            return _contenderListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveCandidateFromFlagList(IEmployer employer, CandidateFlagList flagList, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, flagList))
                throw new CandidateFlagListsPermissionsException(employer, flagList.Id);

            RemoveCandidatesFromList(flagList.Id, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveCandidatesFromFlagList(IEmployer employer, CandidateFlagList flagList, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, flagList))
                throw new CandidateFlagListsPermissionsException(employer, flagList.Id);

            RemoveCandidatesFromList(flagList.Id, candidateIds);
            return _contenderListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveAllCandidatesFromFlagList(IEmployer employer, CandidateFlagList flagList)
        {
            if (!CanModifyCandidates(employer, flagList))
                throw new CandidateFlagListsPermissionsException(employer, flagList.Id);

            RemoveAllCandidatesFromList(flagList.Id);
            return _contenderListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.AddCandidateToFolder(IEmployer employer, CandidateFolder folder, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            AddCandidatesToFolder(employer.Id, folder.Id, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.AddCandidatesToFolder(IEmployer employer, CandidateFolder folder, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            AddCandidatesToFolder(employer.Id, folder.Id, candidateIds);
            return _contenderListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveCandidateFromFolder(IEmployer employer, CandidateFolder folder, Guid candidateId)
        {
            if (!CanModifyCandidates(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            RemoveCandidatesFromList(folder.Id, new[] { candidateId });
            return _contenderListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveCandidatesFromFolder(IEmployer employer, CandidateFolder folder, IEnumerable<Guid> candidateIds)
        {
            if (!CanModifyCandidates(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            RemoveCandidatesFromList(folder.Id, candidateIds);
            return _contenderListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int ICandidateListsCommand.RemoveAllCandidatesFromFolder(IEmployer employer, CandidateFolder folder)
        {
            if (!CanModifyCandidates(employer, folder))
                throw new CandidateFoldersPermissionsException(employer, folder.Id);

            RemoveAllCandidatesFromList(folder.Id);
            return _contenderListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        private void AddCandidatesToBlockList(Guid employerId, CandidateBlockList blockList, IEnumerable<Guid> candidateIds)
        {
            AddCandidatesToList(blockList.Id, candidateIds);

            // Remove from incompatible lists.

            if (blockList.BlockListType == BlockListType.Permanent)
                RemoveCandidatesFromList(employerId, PermanentBlockListIncompatibleTypes, candidateIds);
        }

        private void AddCandidatesToFlagList(Guid employerId, Guid flagListId, IEnumerable<Guid> candidateIds)
        {
            AddCandidatesToList(flagListId, candidateIds);

            // Remove from incompatible lists.

            RemoveCandidatesFromList(employerId, FlagListIncompatibleTypes, candidateIds);
        }

        private void AddCandidatesToFolder(Guid employerId, Guid folderId, IEnumerable<Guid> candidateIds)
        {
            AddCandidatesToList(folderId, candidateIds);

            // Remove from incompatible lists.

            RemoveCandidatesFromList(employerId, FolderIncompatibleTypes, candidateIds);
        }

        private void AddCandidatesToList(Guid listId, IEnumerable<Guid> candidateIds)
        {
            _contenderListsCommand.CreateEntries(listId, candidateIds);
        }

        private void RemoveCandidatesFromList(Guid listId, IEnumerable<Guid> candidateIds)
        {
            _contenderListsCommand.DeleteEntries(listId, candidateIds);
        }

        private void RemoveCandidatesFromList(Guid employerId, IEnumerable<int> listTypes, IEnumerable<Guid> candidateIds)
        {
            _contenderListsCommand.DeleteEntries(employerId, listTypes, candidateIds);
        }

        private void RemoveAllCandidatesFromList(Guid listId)
        {
            _contenderListsCommand.DeleteAllEntries(listId);
        }

        private static bool CanModifyCandidates(IEmployer employer, CandidateBlockList blockList)
        {
            if (!CanAccessBlockList(employer, blockList, false))
                return false;

            // Must own the blockList.

            return blockList.RecruiterId == employer.Id;
        }

        private static bool CanAccessBlockList(IEmployer employer, ContenderList blockList, bool allowDeleted)
        {
            if (employer == null)
                return false;
            if (blockList == null)
                return false;
            if (!allowDeleted && blockList.IsDeleted)
                return false;

            return true;
        }

        private static bool CanModifyCandidates(IEmployer employer, CandidateFlagList flagList)
        {
            if (!CanAccessFlagList(employer, flagList, false))
                return false;

            // Must own the list.

            return flagList.RecruiterId == employer.Id;
        }

        private static bool CanAccessFlagList(IEmployer employer, ContenderList list, bool allowDeleted)
        {
            if (employer == null)
                return false;
            if (list == null)
                return false;
            if (!allowDeleted && list.IsDeleted)
                return false;

            return true;
        }

        private static bool CanModifyCandidates(IEmployer employer, CandidateFolder folder)
        {
            if (!CanAccessFolder(employer, folder, false))
                return false;

            // If in same organisation can modify candidates in shared folders.

            if (folder.FolderType == FolderType.Shared)
                return folder.OrganisationId == employer.Organisation.Id;

            // Else, must own the folder.

            return folder.RecruiterId == employer.Id;
        }

        private static bool CanAccessFolder(IEmployer employer, ContenderList folder, bool allowDeleted)
        {
            if (employer == null)
                return false;
            if (folder == null)
                return false;
            if (!allowDeleted && folder.IsDeleted)
                return false;

            return true;
        }
    }
}
