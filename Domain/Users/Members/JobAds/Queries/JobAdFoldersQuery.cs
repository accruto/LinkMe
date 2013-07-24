using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Members.JobAds.Queries
{
    public class JobAdFoldersQuery
        : IJobAdFoldersQuery
    {
        private const int NumberOfPrivateFolders = 5;
        private const string FolderNameTemplate = "Folder {0}";

        private static readonly int[] ListTypes = new[] { (int)FolderType.Private, (int)FolderType.Mobile };
        private static readonly int[] NotIfInListTypes = new[] { (int)BlockListType.Permanent };

        private readonly IJobAdListsQuery _jobAdListsQuery;
        private readonly IJobAdListsCommand _jobAdListsCommand;

        public JobAdFoldersQuery(IJobAdListsQuery jobAdListsQuery, IJobAdListsCommand jobAdListsCommand)
        {
            _jobAdListsQuery = jobAdListsQuery;
            _jobAdListsCommand = jobAdListsCommand;
        }

        JobAdFolder IJobAdFoldersQuery.GetFolder(IMember member, Guid id)
        {
            var folder = _jobAdListsQuery.GetList<JobAdFolder>(id);
            return CanAccessFolder(member, folder) ? folder : null;
        }

        IList<JobAdFolder> IJobAdFoldersQuery.GetFolders(IMember member)
        {
            return member == null
                ? new List<JobAdFolder>()
                : GetFolders(member.Id, _jobAdListsQuery.GetLists<JobAdFolder>(member.Id, ListTypes).ToList()).ToList();
        }

        JobAdFolder IJobAdFoldersQuery.GetMobileFolder(IMember member)
        {
            return member == null
                ? null
                : GetSpecialFolder(member.Id, FolderType.Mobile);
        }

        bool IJobAdFoldersQuery.IsInFolder(IMember member, Guid folderId, Guid jobAdId)
        {
            var folder = _jobAdListsQuery.GetList<JobAdFolder>(folderId);
            return CanAccessFolder(member, folder)
                && _jobAdListsQuery.IsListed(folderId, NotIfInListTypes, jobAdId);
        }

        bool IJobAdFoldersQuery.IsInMobileFolder(IMember member, Guid jobAdId)
        {
            return member != null
                && _jobAdListsQuery.IsListed(member.Id, new[] { (int)FolderType.Mobile }, NotIfInListTypes, jobAdId);
        }

        IList<Guid> IJobAdFoldersQuery.GetInFolderJobAdIds(IMember member, Guid folderId)
        {
            var folder = _jobAdListsQuery.GetList<JobAdFolder>(folderId);
            return CanAccessFolder(member, folder)
                ? _jobAdListsQuery.GetListedJobAdIds(folderId, NotIfInListTypes)
                : new List<Guid>();
        }

        IList<Guid> IJobAdFoldersQuery.GetInMobileFolderJobAdIds(IMember member)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdListsQuery.GetListedJobAdIds(member.Id, new[] { (int)FolderType.Mobile }, NotIfInListTypes);
        }

        IList<Guid> IJobAdFoldersQuery.GetInMobileFolderJobAdIds(IMember member, IEnumerable<Guid> jobAdIds)
        {
            return member == null
                ? new List<Guid>()
                : _jobAdListsQuery.GetListedJobAdIds(member.Id, new[] { (int)FolderType.Mobile }, NotIfInListTypes, jobAdIds);
        }

        int IJobAdFoldersQuery.GetInFolderCount(IMember member, Guid folderId)
        {
            var folder = _jobAdListsQuery.GetList<JobAdFolder>(folderId);
            return CanAccessFolder(member, folder)
                ? _jobAdListsQuery.GetListedCount(folderId, NotIfInListTypes)
                : 0;
        }

        IDictionary<Guid, int> IJobAdFoldersQuery.GetInFolderCounts(IMember member)
        {
            return member == null
                ? new Dictionary<Guid, int>()
                : _jobAdListsQuery.GetListedCounts(member.Id, ListTypes, NotIfInListTypes);
        }

        IDictionary<Guid, DateTime?> IJobAdFoldersQuery.GetLastUsedTimes(IMember member)
        {
            return member == null
                ? new Dictionary<Guid, DateTime?>()
                : _jobAdListsQuery.GetLastUsedTimes(member.Id, ListTypes, NotIfInListTypes);
        }

        private IEnumerable<JobAdFolder> GetFolders(Guid memberId, ICollection<JobAdFolder> folders)
        {
            // Look for a mobile folder.

            if (!(from f in folders where f.FolderType == FolderType.Mobile select f).Any())
                folders.Add(CreateSpecialFolder(memberId, FolderType.Mobile));

            // Ensure there are enough private folders.

            var privateFolders = (from f in folders where f.FolderType == FolderType.Private select f).ToList();
            if (privateFolders.Count < NumberOfPrivateFolders)
            {
                // Create them in reverse order so Folder 1 appears before Folder 5 in a "last used" scenario.

                var folderNames = (from i in Enumerable.Range(1, NumberOfPrivateFolders)
                                   select string.Format(FolderNameTemplate, i))
                                   .Except(from f in privateFolders select f.Name).Take(NumberOfPrivateFolders - privateFolders.Count).Reverse();
                
                foreach (var folderName in folderNames)
                {
                    var folder = new JobAdFolder { MemberId = memberId, Name = folderName, FolderType = FolderType.Private };
                    _jobAdListsCommand.CreateList(folder);
                    folders.Add(folder);
                }
            }

            return folders;
        }

        private JobAdFolder GetSpecialFolder(Guid memberId, FolderType folderType)
        {
            // If it doesn't exist then create it.

            var folders = _jobAdListsQuery.GetLists<JobAdFolder>(memberId, (int)folderType);
            return folders.Count == 0
                ? CreateSpecialFolder(memberId, folderType)
                : folders[0];
        }

        private static bool CanAccessFolder(IHasId<Guid> member, JobAdFolder folder)
        {
            if (member == null)
                return false;
            if (folder == null)
                return false;
            if (folder.IsDeleted)
                return false;
            return folder.MemberId == member.Id;
        }

        private JobAdFolder CreateSpecialFolder(Guid memberId, FolderType folderType)
        {
            var folder = new JobAdFolder { MemberId = memberId, FolderType = folderType };
            _jobAdListsCommand.CreateList(folder);
            return folder;
        }
    }
}
