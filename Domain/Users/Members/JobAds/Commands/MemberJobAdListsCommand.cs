using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.JobAds;

namespace LinkMe.Domain.Users.Members.JobAds.Commands
{
    public class MemberJobAdListsCommand
        : IMemberJobAdListsCommand
    {
        private static readonly int[] FlagListIncompatibleTypes = new[] { (int)BlockListType.Permanent };
        private static readonly int[] FolderIncompatibleTypes = new[] { (int)BlockListType.Permanent };
        private static readonly int[] BlockListIncompatibleTypes = new[] { (int)FlagListType.Flagged };

        private readonly IJobAdListsCommand _jobAdListsCommand;
        private readonly IJobAdListsQuery _jobAdListsQuery;

        public MemberJobAdListsCommand(IJobAdListsCommand jobAdListsCommand, IJobAdListsQuery jobAdListsQuery)
        {
            _jobAdListsCommand = jobAdListsCommand;
            _jobAdListsQuery = jobAdListsQuery;
        }

        int IMemberJobAdListsCommand.AddJobAdToFlagList(IMember member, JobAdFlagList list, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, list))
                throw new JobAdFlagListsPermissionsException(member, list.Id);

            AddJobAdsToFlagList(member.Id, list.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(list.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.AddJobAdsToFlagList(IMember member, JobAdFlagList list, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, list))
                throw new JobAdFlagListsPermissionsException(member, list.Id);

            AddJobAdsToFlagList(member.Id, list.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(list.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveJobAdFromFlagList(IMember member, JobAdFlagList flagList, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, flagList))
                throw new JobAdFlagListsPermissionsException(member, flagList.Id);

            RemoveJobAdsFromList(flagList.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveJobAdsFromFlagList(IMember member, JobAdFlagList flagList, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, flagList))
                throw new JobAdFlagListsPermissionsException(member, flagList.Id);

            RemoveJobAdsFromList(flagList.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.AddJobAdToBlockList(IMember member, JobAdBlockList list, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, list))
                throw new JobAdBlockListsPermissionsException(member, list.Id);

            AddJobAdsToBlockList(member.Id, list.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(list.Id, null);
        }

        int IMemberJobAdListsCommand.AddJobAdsToBlockList(IMember member, JobAdBlockList list, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, list))
                throw new JobAdBlockListsPermissionsException(member, list.Id);

            AddJobAdsToBlockList(member.Id, list.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(list.Id, null);
        }

        int IMemberJobAdListsCommand.RemoveJobAdFromBlockList(IMember member, JobAdBlockList blockList, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, blockList))
                throw new JobAdBlockListsPermissionsException(member, blockList.Id);

            RemoveJobAdsFromList(blockList.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(blockList.Id, null);
        }

        int IMemberJobAdListsCommand.RemoveJobAdsFromBlockList(IMember member, JobAdBlockList blockList, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, blockList))
                throw new JobAdBlockListsPermissionsException(member, blockList.Id);

            RemoveJobAdsFromList(blockList.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(blockList.Id, null);
        }

        int IMemberJobAdListsCommand.AddJobAdToFolder(IMember member, JobAdFolder folder, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            AddJobAdsToFolder(member.Id, folder.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.AddJobAdsToFolder(IMember member, JobAdFolder folder, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            AddJobAdsToFolder(member.Id, folder.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveJobAdFromFolder(IMember member, JobAdFolder folder, Guid jobAdId)
        {
            if (!CanModifyJobAds(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            RemoveJobAdsFromList(folder.Id, new[] { jobAdId });
            return _jobAdListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveJobAdsFromFolder(IMember member, JobAdFolder folder, IEnumerable<Guid> jobAdIds)
        {
            if (!CanModifyJobAds(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            RemoveJobAdsFromList(folder.Id, jobAdIds);
            return _jobAdListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveAllJobAdsFromBlockList(IMember member, JobAdBlockList blockList)
        {
            if (!CanModifyJobAds(member, blockList))
                throw new JobAdBlockListsPermissionsException(member, blockList.Id);

            RemoveAllJobAdsFromList(blockList.Id);
            return 0;
        }

        int IMemberJobAdListsCommand.RemoveAllJobAdsFromFolder(IMember member, JobAdFolder folder)
        {
            if (!CanModifyJobAds(member, folder))
                throw new JobAdFoldersPermissionsException(member, folder.Id);

            RemoveAllJobAdsFromList(folder.Id);
            return _jobAdListsQuery.GetListedCount(folder.Id, new[] { (int)BlockListType.Permanent });
        }

        int IMemberJobAdListsCommand.RemoveAllJobAdsFromFlagList(IMember member, JobAdFlagList flagList)
        {
            if (!CanModifyJobAds(member, flagList))
                throw new JobAdFlagListsPermissionsException(member, flagList.Id);

            RemoveAllJobAdsFromList(flagList.Id);
            return _jobAdListsQuery.GetListedCount(flagList.Id, new[] { (int)BlockListType.Permanent });
        }

        private void AddJobAdsToFlagList(Guid employerId, Guid flagListId, IEnumerable<Guid> jobAdIds)
        {
            AddJobAdsToList(flagListId, jobAdIds);

            // Remove from incompatible lists.

            RemoveJobAdsFromList(employerId, FlagListIncompatibleTypes, jobAdIds);
        }

        private void AddJobAdsToBlockList(Guid employerId, Guid blockListId, IEnumerable<Guid> jobAdIds)
        {
            AddJobAdsToList(blockListId, jobAdIds);

            // Remove from incompatible lists.

            RemoveJobAdsFromList(employerId, BlockListIncompatibleTypes, jobAdIds);
        }

        private void AddJobAdsToFolder(Guid employerId, Guid folderId, IEnumerable<Guid> jobAdIds)
        {
            AddJobAdsToList(folderId, jobAdIds);

            // Remove from incompatible lists.

            RemoveJobAdsFromList(employerId, FolderIncompatibleTypes, jobAdIds);
        }

        private void AddJobAdsToList(Guid listId, IEnumerable<Guid> jobAdIds)
        {
            _jobAdListsCommand.CreateEntries(listId, jobAdIds);
        }

        private void RemoveJobAdsFromList(Guid listId, IEnumerable<Guid> jobAdIds)
        {
            _jobAdListsCommand.DeleteEntries(listId, jobAdIds);
        }

        private void RemoveAllJobAdsFromList(Guid listId)
        {
            _jobAdListsCommand.DeleteEntries(listId);
        }

        private void RemoveJobAdsFromList(Guid memberId, IEnumerable<int> listTypes, IEnumerable<Guid> jobAdIds)
        {
            _jobAdListsCommand.DeleteEntries(memberId, listTypes, jobAdIds);
        }

        private static bool CanModifyJobAds(IMember member, JobAdFlagList flagList)
        {
            if (!CanAccessFlagList(member, flagList, false))
                return false;

            // Must own the flagList.

            return flagList.MemberId == member.Id;
        }

        private static bool CanModifyJobAds(IMember member, JobAdBlockList blockList)
        {
            if (!CanAccessBlockList(member, blockList, false))
                return false;

            // Must own the blockList.

            return blockList.MemberId == member.Id;
        }

        private static bool CanModifyJobAds(IMember member, JobAdFolder folder)
        {
            if (!CanAccessFolder(member, folder, false))
                return false;

            // Must own the folder.

            return folder.MemberId == member.Id;
        }

        private static bool CanAccessFlagList(IMember member, JobAdList list, bool allowDeleted)
        {
            if (member == null)
                return false;
            if (list == null)
                return false;
            if (!allowDeleted && list.IsDeleted)
                return false;

            return true;
        }

        private static bool CanAccessBlockList(IMember member, JobAdList blockList, bool allowDeleted)
        {
            if (member == null)
                return false;
            if (blockList == null)
                return false;
            if (!allowDeleted && blockList.IsDeleted)
                return false;

            return true;
        }

        private static bool CanAccessFolder(IMember member, JobAdList folder, bool allowDeleted)
        {
            if (member == null)
                return false;
            if (folder == null)
                return false;
            if (!allowDeleted && folder.IsDeleted)
                return false;

            return true;
        }
    }
}