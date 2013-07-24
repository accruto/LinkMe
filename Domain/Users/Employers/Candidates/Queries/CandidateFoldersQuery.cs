using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Contenders.Commands;
using LinkMe.Domain.Roles.Contenders.Queries;

namespace LinkMe.Domain.Users.Employers.Candidates.Queries
{
    public class CandidateFoldersQuery
        : ICandidateFoldersQuery
    {
        private static readonly int[] ListTypes = new[] { (int)FolderType.Private, (int)FolderType.Shared, (int)FolderType.Shortlist, (int)FolderType.Mobile };
        private static readonly int[] NotIfInListTypes = new[] { (int)BlockListType.Permanent };
        private readonly IContenderListsCommand _contenderListsCommand;
        private readonly IContenderListsQuery _contenderListsQuery;

        public CandidateFoldersQuery(IContenderListsCommand contenderListsCommand, IContenderListsQuery contenderListsQuery)
        {
            _contenderListsCommand = contenderListsCommand;
            _contenderListsQuery = contenderListsQuery;
        }

        CandidateFolder ICandidateFoldersQuery.GetFolder(IEmployer employer, Guid id)
        {
            var folder = _contenderListsQuery.GetList<CandidateFolder>(id);
            return CanAccessFolder(employer, folder) ? folder : null;
        }

        IList<CandidateFolder> ICandidateFoldersQuery.GetFolders(IEmployer employer)
        {
            return employer == null
                ? new List<CandidateFolder>()
                : GetFolders(employer.Id, _contenderListsQuery.GetLists<CandidateFolder>(employer.Id, employer.Organisation.Id, ListTypes).ToList()).ToList();
        }

        CandidateFolder ICandidateFoldersQuery.GetShortlistFolder(IEmployer employer)
        {
            return employer == null
                ? null
                : GetSpecialFolder(employer.Id, FolderType.Shortlist);
        }

        CandidateFolder ICandidateFoldersQuery.GetMobileFolder(IEmployer employer)
        {
            return employer == null
                ? null
                : GetSpecialFolder(employer.Id, FolderType.Mobile);
        }

        IList<CandidateFolder> ICandidateFoldersQuery.GetPrivateFolders(IEmployer employer)
        {
            return employer == null
                ? new List<CandidateFolder>()
                : _contenderListsQuery.GetLists<CandidateFolder>(employer.Id, (int)FolderType.Private);
        }

        IList<CandidateFolder> ICandidateFoldersQuery.GetSharedFolders(IEmployer employer)
        {
            return employer == null
                ? new List<CandidateFolder>()
                : _contenderListsQuery.GetSharedLists<CandidateFolder>(employer.Organisation.Id, (int) FolderType.Shared);
        }

        bool ICandidateFoldersQuery.IsInFolder(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderListsQuery.IsListed(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes, candidateId);
        }

        bool ICandidateFoldersQuery.IsInFolder(IEmployer employer, Guid folderId, Guid candidateId)
        {
            var folder = _contenderListsQuery.GetList<CandidateFolder>(folderId);
            return CanAccessFolder(employer, folder)
                && _contenderListsQuery.IsListed(folderId, NotIfInListTypes, candidateId);
        }

        bool ICandidateFoldersQuery.IsInMobileFolder(IEmployer employer, Guid candidateId)
        {
            return employer != null
                && _contenderListsQuery.IsListed(employer.Id, null, new[] { (int)FolderType.Mobile }, NotIfInListTypes, candidateId);
        }

        IList<Guid> ICandidateFoldersQuery.GetInFolderCandidateIds(IEmployer employer, Guid folderId)
        {
            var folder = _contenderListsQuery.GetList<CandidateFolder>(folderId);
            return CanAccessFolder(employer, folder)
                ? _contenderListsQuery.GetListedContenderIds(folderId, NotIfInListTypes)
                : new List<Guid>();
        }

        int ICandidateFoldersQuery.GetInFolderCount(IEmployer employer, Guid folderId)
        {
            var folder = _contenderListsQuery.GetList<CandidateFolder>(folderId);
            return CanAccessFolder(employer, folder)
                ? _contenderListsQuery.GetListedCount(folderId, NotIfInListTypes)
                : 0;
        }

        IList<Guid> ICandidateFoldersQuery.GetInFolderCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes);
        }

        IList<Guid> ICandidateFoldersQuery.GetInMobileFolderCandidateIds(IEmployer employer)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, null, new[] { (int)FolderType.Mobile }, NotIfInListTypes);
        }

        IList<Guid> ICandidateFoldersQuery.GetInMobileFolderCandidateIds(IEmployer employer, IEnumerable<Guid> candidateIds)
        {
            return employer == null
                ? new List<Guid>()
                : _contenderListsQuery.GetListedContenderIds(employer.Id, null, new[] { (int)FolderType.Mobile }, NotIfInListTypes, candidateIds);
        }

        IDictionary<Guid, int> ICandidateFoldersQuery.GetInFolderCounts(IEmployer employer)
        {
            return employer == null
                ? new Dictionary<Guid, int>()
                : _contenderListsQuery.GetListedCounts(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes);
        }

        IDictionary<Guid, DateTime?> ICandidateFoldersQuery.GetLastUsedTimes(IEmployer employer)
        {
            return employer == null
                ? new Dictionary<Guid, DateTime?>()
                : _contenderListsQuery.GetLastUsedTimes(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes);
        }

        int ICandidateFoldersQuery.GetFolderCount(IEmployer employer, Guid candidateId)
        {
            return employer == null
                ? 0
                : _contenderListsQuery.GetListCount(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes, candidateId);
        }

        IDictionary<Guid, int> ICandidateFoldersQuery.GetFolderCounts(IEmployer employer, IEnumerable<Guid> candidateIds)
        {
            return employer == null
                ? new Dictionary<Guid, int>()
                : _contenderListsQuery.GetListCounts(employer.Id, employer.Organisation.Id, ListTypes, NotIfInListTypes, candidateIds);
        }

        private CandidateFolder GetSpecialFolder(Guid employerId, FolderType folderType)
        {
            // If it doesn't exist then create it.

            var folders = _contenderListsQuery.GetLists<CandidateFolder>(employerId, (int)folderType);
            return folders.Count == 0
                ? CreateSpecialFolder(employerId, folderType)
                : folders[0];
        }

        private CandidateFolder CreateSpecialFolder(Guid employerId, FolderType folderType)
        {
            var folder = new CandidateFolder { RecruiterId = employerId, FolderType = folderType };
            _contenderListsCommand.CreateList(folder);
            return folder;
        }

        private IEnumerable<CandidateFolder> GetFolders(Guid employerId, ICollection<CandidateFolder> folders)
        {
            // Look for a short list folder.

            if (!(from f in folders where f.FolderType == FolderType.Shortlist select f).Any())
                folders.Add(CreateSpecialFolder(employerId, FolderType.Shortlist));

            // Look for a mobile folder.

            if (!(from f in folders where f.FolderType == FolderType.Mobile select f).Any())
                folders.Add(CreateSpecialFolder(employerId, FolderType.Mobile));

            return folders;
        }

        private static bool CanAccessFolder(IEmployer employer, CandidateFolder folder)
        {
            if (employer == null)
                return false;
            if (folder == null)
                return false;
            if (folder.IsDeleted)
                return false;

            if (folder.FolderType == FolderType.Shared)
            {
                // The folder is shared, so the user must have access to this company's shared data to edit it. Even if they
                // are the owner of this folder they cannot edit it once they've left the company.

                return folder.OrganisationId == employer.Organisation.Id;
            }

            // The folder is private, so only the owner can edit or delete it.

            return folder.RecruiterId == employer.Id;
        }
    }
}